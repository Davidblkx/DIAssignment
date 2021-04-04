using Akka.Routing;
using DIAssignment.Core.DBFile;
using DIAssignment.Core.Models.Entity;
using MongoDB.Bson.Serialization.Attributes;

namespace DIAssignment.Core.Models.Messages
{
    /// <summary>
    /// Insert or update an entity values
    /// </summary>
    public abstract class UpsertMessage : Message, IConsistentHashable
    {
        private object? _entity;

        [BsonIgnore]
        public object? Entity
        {
            get => _entity;
            set => SetEntity(value);
        }
        public abstract ImportEntityType Type { get; }

        public long ArtistId { get; set; }
        public long CollectionId { get; set; }

        protected abstract long GetArtistId();
        protected abstract long GetCollectionId();

        public void SetEntity(object? entity)
        {
            _entity = entity;
            ArtistId = GetArtistId();
            CollectionId = GetCollectionId();
        }

        public object ConsistentHashKey => CollectionId;
    }

    [BsonIgnoreExtraElements]
    public class UpsertArtist : UpsertMessage
    {
        public override ImportEntityType Type => ImportEntityType.Artist;
        protected override long GetArtistId() => (Entity as Artist)?.Id ?? -1;
        protected override long GetCollectionId() => -1;

        public Artist? ArtistValue
        {
            set => Entity = value;
            get => Entity as Artist;
        }

        public static UpsertArtist FromRow(DbFileRow row)
            => new() { Entity = DbFileRowSerializer.Serialize<Artist>(row) };
    }

    [BsonIgnoreExtraElements]
    public class UpsertArtistCollection : UpsertMessage
    {
        public override ImportEntityType Type => ImportEntityType.ArtistCollection;
        protected override long GetArtistId() => (Entity as ArtistCollection)?.ArtistId ?? -1;
        protected override long GetCollectionId() => (Entity as ArtistCollection)?.CollectionId ?? -1;

        public ArtistCollection? ArtistValue
        {
            set => Entity = value;
            get => Entity as ArtistCollection;
        }

        public static UpsertArtistCollection FromRow(DbFileRow row)
            => new() { Entity = DbFileRowSerializer.Serialize<ArtistCollection>(row) };
    }

    [BsonIgnoreExtraElements]
    public class UpsertCollection : UpsertMessage
    {
        public override ImportEntityType Type => ImportEntityType.Collection;
        protected override long GetArtistId() => -1;
        protected override long GetCollectionId() => (Entity as Collection)?.Id ?? -1;

        public Collection? ArtistValue
        {
            set => Entity = value;
            get => Entity as Collection;
        }

        public static UpsertCollection FromRow(DbFileRow row)
            => new() { Entity = DbFileRowSerializer.Serialize<Collection>(row) };
    }

    [BsonIgnoreExtraElements]
    public class UpsertCollectionMatch : UpsertMessage
    {
        public override ImportEntityType Type => ImportEntityType.CollectionMatch;
        protected override long GetArtistId() => -1;
        protected override long GetCollectionId() => (Entity as CollectionMatch)?.CollectionId ?? -1;

        public CollectionMatch? ArtistValue
        {
            set => Entity = value;
            get => Entity as CollectionMatch;
        }

        public static UpsertCollectionMatch FromRow(DbFileRow row)
            => new() { Entity = DbFileRowSerializer.Serialize<CollectionMatch>(row) };
    }
}
