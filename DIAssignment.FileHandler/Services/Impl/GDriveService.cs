using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DIAssignment.FileHandler.Services
{
    public sealed class GDriveService : IGDriveService
    {
        public async Task<string> DownloadFile(string fileId)
        {
            var url = BuildUrl(fileId);
            string fileName = $"local_{fileId}_{Guid.NewGuid()}";

            await DownloadFile(url, fileName);

            return fileName;
        }

        private async Task DownloadFile(string url, string destination)
        {
            using var client = new HttpClient();
            using var stream = await client.GetStreamAsync(url);
            using var fileStream = File.OpenWrite(destination);
            await stream.CopyToAsync(fileStream, 1024);
        }

        private static string BuildUrl(string id)
            => $"https://www.googleapis.com/drive/v3/files/{id}/?key={GetKey()}&alt=media";

        private static string GetKey()
            => string.Join("", "AIzaS", "yBsfNWa9mN", "SXJMOx3Z-j", "bWugox9qsC8wwo");
    }
}
