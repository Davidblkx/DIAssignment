using DIAssignment.Core.Models.Messages;
using DIAssignment.Core.Services;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static DIAssignment.Core.Models.ImportFileNames;

namespace DIAssignment.EventStore.Services
{
    public class MongoService : IMongoService
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;

        public MongoService(IConfigService config)
        {
            var user = config.Get("MONGO_USER") ?? "mongo";
            var pass = config.Get("MONGO_PASS") ?? "mongo";
            var dbName = config.Get("MONGO_DB") ?? "events";

            _client = GetClient(user, pass);
            _db = _client.GetDatabase(dbName);
        }

        public void Insert<T>(T item) where T : UpsertMessage
            => _db.GetCollection<T>(GetKey(item.Type)).InsertOne(item);

        public List<T> Get<T>(Expression<Func<T, bool>> exp) where T : UpsertMessage, new()
            => _db.GetCollection<T>(GetKey(new T().Type)).Find(exp).ToList();

        private MongoClient GetClient(string user, string pass)
        {
            var url = $"mongodb://{user}:{pass}@mongo:27017";
            return new MongoClient(url);
        }
    }
}
