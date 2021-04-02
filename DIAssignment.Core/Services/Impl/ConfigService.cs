using DIAssignment.Core.Infra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DIAssignment.Core.Services
{
    public class ConfigService : IConfigService
    {
        private readonly Dictionary<string, string> _config;

        public ConfigService()
        {
            _config = ConfigStoreLoader.LoadConfig(GetFolder());
        }

        public string? Get(string key)
        {
            if (Environment.GetEnvironmentVariable(key) is string value)
                return value;

            if (_config.ContainsKey(key))
                return _config[key];

            return null;
        }

        private static string GetFolder()
        {
            if (Path.GetDirectoryName(GetAssembly().Location) is string x)
                return x;

            throw new Exception("Can't find executing assembly path");
        }

        private static Assembly GetAssembly()
            => Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
    }
}
