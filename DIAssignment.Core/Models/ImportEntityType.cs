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

        public static string GetKey(string name)
            => $"FILE_{name}".ToUpper();

        public static string GetKey(ImportEntityType type)
            => type switch
            {
                ImportEntityType.Artist => ARTIST,
                ImportEntityType.ArtistCollection => ARTIST_COLLECTION,
                ImportEntityType.CollectionMatch => COLLECTION_MATCH,
                ImportEntityType.Collection => COLLECTION,
                _ => throw new System.NotImplementedException()
            };
    }
}
