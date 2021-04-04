using DIAssignment.Core.DBFile;
using MongoDB.Bson.Serialization.Attributes;

namespace DIAssignment.Core.Models.Entity
{
    [BsonDiscriminator("CollectionMatch")]
    public class CollectionMatch
    {
        [DbFileColumn("collection_id")]
        public long CollectionId { get; set; }

        [DbFileColumn("upc")]
        public string UPC { get; set; } = "";
    }
}
