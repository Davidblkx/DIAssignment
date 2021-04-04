using Akka.Actor;
using DIAssignment.FileHandler.Services;
using DIAssignment.Core.Services;
using DIAssignment.Core.Models.Messages;
using Akka.Event;
using DIAssignment.Core.Models;
using DIAssignment.Core.DBFile;
using System.IO;
using DIAssignment.Core.Infra;
using System.Threading.Tasks;
using Akka.Cluster;
using System.Linq;
using System;

namespace DIAssignment.FileHandler.Actors
{
    /// <summary>
    /// Actor to download, extract and parse DB files from google drive
    /// </summary>
    public class FileHandlerActor : ReceiveActor
    {
        private readonly IDbFileReaderService _fileReader;
        private readonly IGDriveService _googleService;
        private readonly IZipService _zipService;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly Cluster _cluster;

        private IActorRef? _eventStoreActor;

        public FileHandlerActor(IInjector injector)
        {
            _fileReader = injector.Get<IDbFileReaderService>();
            _googleService = injector.Get<IGDriveService>();
            _zipService = injector.Get<IZipService>();
            _cluster = Cluster.Get(Context.System);

            ReceiveAsync<ImportFile>(ImportDriveFile);
            Receive<IActorRef>(e => _eventStoreActor = e);
        }

        public async Task ImportDriveFile(ImportFile msg)
        {
            if (string.IsNullOrEmpty(msg.FileId))
            {
                _log.Warning("Attempt to download an empty file id");
                return;
            }

            _log.Info($"Starting importing file [{msg.Type}] from {msg.FileId}");

            var zipFile = await _googleService.DownloadFile(msg.FileId);
            if (zipFile is null)
            {
                _log.Error("Can't download file from google drive");
                return;
            }

            var fileName = _zipService.ExtractZip(zipFile);

            var reader = new DbFileReader(fileName);
            DbFileRow? row;
            while ((row = reader.ReadRow()) != null)
                SendToSerializer(row, msg.Type);

            File.Delete(zipFile);
            var dir = Path.GetDirectoryName(fileName);
            if (dir is not null)
                Directory.Delete(dir, true);

            _log.Info($"Finish importing file [{msg.Type}] from {msg.FileId}");
        }

        public void SendToSerializer(DbFileRow row, ImportEntityType type)
        {
            var message = new ImportEntityMessage(type, row);
            if (_eventStoreActor is not null)
            {
                _eventStoreActor.Tell(message);
                return;
            }

            var target = _cluster.State.Members.First(e => e.HasRole("eventstore"));
            var targetUrl = $"{target.Address}/user/{ActorNames.Serializer}";

            Context.ActorSelection(targetUrl).Tell(message);
        }
    }
}
