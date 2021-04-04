using DIAssignment.Core.Models.Entity;
using DIAssignment.Core.Services;
using Nest;
using System;
using System.Threading.Tasks;

namespace DIAssignment.ESEntryPoint.Services
{
    public class ElasticService : IElasticService
    {
        private readonly ElasticClient _client;

        public ElasticService(IConfigService config)
        {
            var url = config.Get("ELASTICSEARCH_URL") ?? "http://elastic:9200";
            var settings = new ConnectionSettings(new Uri(url));
            _client = new ElasticClient(settings);
        }

        public bool EnsureIndex<T>()
        {
            var name = GetIndexName<T>();

            var response = _client.Indices.Exists(name);
            if (response.Exists) return true;

            var res = _client.Indices.Create(name);

            return res.IsValid;
        }

        public bool Index<T>(T document) where T : class
            => _client.Index(
                document,
                e => e.Index(GetIndexName<T>())
              ).IsValid;

        /// <summary>
        /// Checks every 5 seconds if elastic is ready
        /// </summary>
        /// <returns></returns>
        public async Task AwaitForConnection()
        {
            while (true)
            {
                var ping = await _client.PingAsync();
                if (ping.IsValid) break;
                Console.WriteLine("Waiting for connection");
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }

        public bool Ping()
            => _client.Ping().IsValid;

        private string GetIndexName<T>()
            => (typeof(T).FullName ?? typeof(T).Name)
                .ToLower()
                .Replace("+", "")
                .Replace(".", "_");
    }
}
