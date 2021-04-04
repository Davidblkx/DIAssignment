using DIAssignment.Core.Models;
using DIAssignment.Core.Models.Entity;
using DIAssignment.Core.Models.Messages;
using DIAssignment.Core.Services;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DIAssignment.Projection.Services
{
    public class MongoService : IMongoService
    {
        private readonly IMongoDatabase _dbEvents;
        private readonly IMongoDatabase _dbProjections;

        private readonly string ArtistKey = ImportFileNames
            .GetKey(ImportEntityType.Artist);
        private readonly string ArtistCollectionKey = ImportFileNames
            .GetKey(ImportEntityType.ArtistCollection);

        public MongoService(IConfigService config)
        {
            var user = config.Get("MONGO_USER") ?? "mongo";
            var pass = config.Get("MONGO_PASS") ?? "mongo";
            var events = config.Get("MONGO_DB_EVENTS") ?? "events";
            var proj = config.Get("MONGO_DB_PROJECTION") ?? "projections";

            var client = GetClient(user, pass);

            _dbEvents = client.GetDatabase(events);
            _dbProjections = client.GetDatabase(proj);
        }

        public Album? GetAlbum(long id)
            => GetProjectionCollection()
                .Find(e => e.Id == id)
                .FirstOrDefault();

        public Artist? GetArtist(long artistId)
            => _dbEvents.GetCollection<UpsertArtist>(ArtistKey)
                .Find(e => e.ArtistId == artistId)
                .FirstOrDefault()?.Entity as Artist;

        public IEnumerable<ArtistCollection> GetArtistCollections(long artistId)
        {
            var result = _dbEvents
                .GetCollection<UpsertArtistCollection>(ArtistCollectionKey)
                .Find(e => e.ArtistId == artistId);

            foreach (var r in result.ToList())
                if (r.Entity is ArtistCollection a)
                    yield return a;
        }

        public Album UpsertAlbum(Album item)
        {
            var options = new FindOneAndReplaceOptions<Album, Album>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After,
            };

            var col = GetProjectionCollection();

            if (GetAlbum(item.Id) is null)
                col.InsertOne(item);
            else 
                col.FindOneAndReplace<Album>(
                    e => e.Id == item.Id, item, options);

            return item;
        }

        private IMongoCollection<Album> GetProjectionCollection()
            => _dbProjections.GetCollection<Album>("albums");

        private MongoClient GetClient(string user, string pass)
        {
            var url = $"mongodb://{user}:{pass}@mongo:27017";
            return new MongoClient(url);
        }
    }
}
