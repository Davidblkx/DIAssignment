using System.IO.Compression;
using System.IO;
using System.Linq;

namespace DIAssignment.FileHandler.Services
{
    public class ZipService : IZipService
    {
        public string ExtractZip(string filePath)
        {
            var target = $"{filePath}_output";

            using var stream = File.OpenRead(filePath);
            var zipFile = new ZipArchive(stream, ZipArchiveMode.Read);
            zipFile.ExtractToDirectory(target, true);
            stream.Close();

            return new DirectoryInfo(target)
                .EnumerateFiles()
                .First().FullName;
        }
    }
}
