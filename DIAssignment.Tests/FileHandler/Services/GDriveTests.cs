using DIAssignment.FileHandler.Services;
using System.IO;
using Xunit;

    
namespace DIAssignment.Tests.FileHandler.Services
{
    public class GDriveTests
    {
        [Fact]
        public async void TestDownload()
        {
            var service = new GDriveService();
            var id = "1e7NjXmSbmVk1bZYNTFXF8CoYWtg3dBcg";
            var expected = "hello world";

            var filePath = await service.DownloadFile(id);

            Assert.NotNull(filePath);

            var content = await File.ReadAllTextAsync(filePath);
            Assert.Equal(expected, content);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
