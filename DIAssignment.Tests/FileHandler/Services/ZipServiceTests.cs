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

            var file = Path.Combine(target, "file.txt");
            Assert.True(File.Exists(file));

            var content = await File.ReadAllTextAsync(file);
            Assert.Equal(expected, content);
        }
    }
}
