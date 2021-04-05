using System.Collections.Generic;

namespace DIAssignment.Core.Models.Entity
{
    /// <summary>
    /// Projection of a collection and its artists
    /// Entity to be indexed in elastic
    /// </summary>
    public class Album : Collection
    {
        public string UPC { get; set; } = "";
        public List<Artist> Artists { get; set; } = new();

        public Album() { }

        public Album(Collection c)
        {
            MergeCollection(c);
        }

        public void MergeCollection(Collection c)
        {
            Id = c.Id;
            Name = c.Name;
            Url = c.Url;
            ReleaseDate = c.ReleaseDate;
            IsCompilation = c.IsCompilation;
            Label = c.Label;
            ImageUrl = c.ImageUrl;
        }
    }
}
