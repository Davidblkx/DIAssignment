using Akka.Routing;
using DIAssignment.Core.Models.Entity;

namespace DIAssignment.Core.Models.Messages
{
    public class AlbumArtistMessage : Message, IConsistentHashable
    {
        public long CollectionId { get; set; }
        public Artist Artist { get; set; }

        public object ConsistentHashKey => CollectionId;

        public AlbumArtistMessage(Artist artist, long collectionId)
        {
            Artist = artist;
            CollectionId = collectionId;
        }
    }
}
