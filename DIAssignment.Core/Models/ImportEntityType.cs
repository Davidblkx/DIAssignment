using DIAssignment.Core.Models.Entity;

namespace DIAssignment.Core.Models
{
    public enum ImportEntityType
    {
        Artist = 1,
        Collection = 2,
        ArtistCollection = 3,
        CollectionMatch = 4,
    }

    public static class ImportFileNames
    {
        public const string ARTIST_COLLECTION = "artist_collection";
        public const string ARTIST = "artist";
        public const string COLLECTION_MATCH = "collection_match";
        public const string COLLECTION = "collection";

        public static string GetKey(ImportEntityType type)
            => type switch
            {
                ImportEntityType.Artist => ARTIST,
                ImportEntityType.ArtistCollection => ARTIST_COLLECTION,
                ImportEntityType.CollectionMatch => COLLECTION_MATCH,
                ImportEntityType.Collection => COLLECTION,
                _ => throw new System.NotImplementedException()
            };

        public static string GetKey<T>() where T : new()
            => new T() switch
            {
                Artist => ARTIST,
                ArtistCollection => ARTIST_COLLECTION,
                Collection => COLLECTION,
                CollectionMatch => COLLECTION_MATCH,
                _ => throw new System.NotImplementedException()
            };
    }
}
