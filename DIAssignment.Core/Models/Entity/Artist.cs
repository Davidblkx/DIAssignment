using DIAssignment.Core.DBFile;
using MongoDB.Bson.Serialization.Attributes;

namespace DIAssignment.Core.Models.Entity
{
    [BsonDiscriminator("Artist")]
    public class Artist
    {
        [DbFileColumn("artist_id")]
        public long Id { get; set; }

        [DbFileColumn("name")]
        public string Name { get; set; } = "";

        public long ArtistID
        {
            set => Id = value;
            get => Id;
        }
    }
}
