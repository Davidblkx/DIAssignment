using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DIAssignment.FileHandler.Services
{
    public sealed class GDriveService : IGDriveService
    {
        public async Task<string> DownloadFile(string fileId)
        {
            using var client = new HttpClient();
            var stream = await client.GetStreamAsync(BuildUrl(fileId));

            string fileName = $"local_{fileId}_{Guid.NewGuid()}";
            var fileStream = File.Create(fileName);

            await stream.CopyToAsync(fileStream, 1024);

            fileStream.Close();
            stream.Close();

            return fileName;
        }

        private static string BuildUrl(string id)
            => $"https://drive.google.com/uc?id={id}&authuser=0&export=download";
    }
}
