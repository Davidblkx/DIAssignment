using Akka.Actor;
using DIAssignment.FileHandler.Services;
using DIAssignment.Core.Services;
using DIAssignment.Core.Models.Messages;
using Akka.Event;
using DIAssignment.Core.Models;
using DIAssignment.Core.DBFile;

namespace DIAssignment.FileHandler.Actors
{
    public class FileHandlerActor : ReceiveActor
    {
        private readonly IDbFileReaderService _fileReader;
        private readonly IGDriveService _googleService;
        private readonly IZipService _zipService;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        public FileHandlerActor(IInjector injector)
        {
            _fileReader = injector.Get<IDbFileReaderService>();
            _googleService = injector.Get<IGDriveService>();
            _zipService = injector.Get<IZipService>();

            Receive<ImportFile>(ImportDriveFile);
        }

        public async void ImportDriveFile(ImportFile msg)
        {
            if (string.IsNullOrEmpty(msg.FileId))
            {
                _log.Warning("Attempt to download an empty file id");
                return;
            }

            Sender.Tell(new ImportFile.ImportFileStarted());

            var zipFile = await _googleService.DownloadFile(msg.FileId);
            if (zipFile is null)
            {
                _log.Error("Can't download file from google drive");
                return;
            }

            var fileName = _zipService.ExtractZip(zipFile);
            await _fileReader.ReadDbFile(fileName, r => SendToSerializer(r, msg.Type));
        }

        public void SendToSerializer(DbFileRow row, ImportEntityType type)
        {

        }
    }
}
