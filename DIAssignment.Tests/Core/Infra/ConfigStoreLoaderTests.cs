using DIAssignment.Core.Infra;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using Xunit;

namespace DIAssignment.Tests.Core.Infra
{
    public class ConfigStoreLoaderTests
    {
        [Fact]
        public void LoadConfig_TestNoFile()
        {
            var subject = ConfigStoreLoader.LoadConfig(Folder);

            Assert.Empty(subject);
        }

        [Fact]
        public void LoadConfig_TestLoad()
        {
            File.WriteAllText(Config, "{ \"KEY1\": \"VALUE1\" }");
            var subject = ConfigStoreLoader.LoadConfig(Folder);

            Assert.NotEmpty(subject);
            Assert.Equal("VALUE1", subject["KEY1"]);

            Clean();
        }

        [Fact]
        public void LoadConfig_TestOverwrite()
        {
            var configValues = new { KEY1 = "VALUE1", KEY2 = "VALUE2" };
            var secretValues = new { KEY1 = "SECRET", KEY3 = "VALUE3" };

            File.WriteAllText(Config, JsonConvert.SerializeObject(configValues));
            File.WriteAllText(Secret, JsonConvert.SerializeObject(secretValues));

            var subject = ConfigStoreLoader.LoadConfig(Folder);

            Assert.Equal(3, subject.Count);
            Assert.Equal(secretValues.KEY1, subject["KEY1"]);
            Assert.Equal(configValues.KEY2, subject["KEY2"]);
            Assert.Equal(secretValues.KEY3, subject["KEY3"]);

            Clean();
        }

        private static string Folder => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private static string Config => Path.Combine(Folder, ConfigStoreLoader.CONFIG_FILE_NAME);
        private static string Secret => Path.Combine(Folder, ConfigStoreLoader.SECRETS_FILE_NAME);

        private static void Clean()
        {
            var f = new FileInfo(Config);
            if (f.Exists) f.Delete();

            f = new FileInfo(Secret);
            if (f.Exists) f.Delete();
        }
    }
}
