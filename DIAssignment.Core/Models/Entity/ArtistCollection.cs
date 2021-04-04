using DIAssignment.Core.DBFile;
using MongoDB.Bson.Serialization.Attributes;

namespace DIAssignment.Core.Models.Entity
{
    [BsonDiscriminator("ArtistCollection")]
    public class ArtistCollection
    {
        [DbFileColumn("artist_id")]
        public long ArtistId { get; set; }
        [DbFileColumn("collection_id")]
        public long CollectionId { get; set; }
        [DbFileColumn("is_primary_artist")]
        public bool IsPrimaryArtist { get; set; }
        [DbFileColumn("role_id")]
        public int RoleId { get; set; }
    }
}
