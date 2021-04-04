using DIAssignment.FileHandler.Services;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace DIAssignment.Tests.FileHandler.Services
{
    public class ZipServiceTests
    {
        [Fact]
        public async Task ExtractFileTest()
        {
            var path = "Assets/file.zip";
            var expected = "text";
            var service = new ZipService();

            var target = service.ExtractZip(path);
            Assert.NotNull(target);
            Assert.True(File.Exists(target));
            var content = await File.ReadAllTextAsync(target);
            Assert.Equal(expected, content);
        }
    }
}
