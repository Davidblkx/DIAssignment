using System.Collections.Generic;
using System.IO;
using DIAssignment.Core.Extensions;

using static Newtonsoft.Json.JsonConvert;

namespace DIAssignment.Core.Infra
{
    /// <summary>
    /// Load settings from config.json and config.secrets.json
    /// </summary>
    public class ConfigStoreLoader
    {
        public const string CONFIG_FILE_NAME = "config.json";
        public const string SECRETS_FILE_NAME = "config.secrets.json";

        /// <summary>
        /// Load values from config files
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static Dictionary<string, string> LoadConfig(string folder)
        {
            var result = new Dictionary<string, string>();

            var config = new FileInfo(Path.Combine(folder, CONFIG_FILE_NAME));
            var secret = new FileInfo(Path.Combine(folder, SECRETS_FILE_NAME));

            result.Merge(ImportJsonFile(config));
            result.Merge(ImportJsonFile(secret));

            return result;
        }

        private static Dictionary<string, string> ImportJsonFile(FileInfo file)
        {
            if (!file.Exists) return new();

            var jsonText = File.ReadAllText(file.FullName);
            if (DeserializeObject<Dictionary<string, string>>(jsonText) is Dictionary<string, string> res)
                return res;

            return new();
        }
    }
}
