using DIAssignment.Core.DBFile;
using MongoDB.Bson.Serialization.Attributes;

namespace DIAssignment.Core.Models.Entity
{
    [BsonDiscriminator("Collection")]
    public class Collection
    {
        [DbFileColumn("collection_id")]
        public long Id { get; set; }

        [DbFileColumn("name")]
        public string Name { get; set; } = "";

        [DbFileColumn("view_url")]
        public string Url { get; set; } = "";

        [DbFileColumn("original_release_date")]
        public string ReleaseDate { get; set; } = "";

        [DbFileColumn("is_compilation")]
        public bool IsCompilation { get; set; }

        [DbFileColumn("label_studio")]
        public string Label { get; set; } = "";

        [DbFileColumn("artwork_url")]
        public string ImageUrl { get; set; } = "";

        public long CollectionID
        {
            set => Id = value;
            get => Id;
        }
    }
}
