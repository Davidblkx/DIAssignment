using DIAssignment.Core.Models.Entity;
using System.Collections.Generic;

namespace DIAssignment.Projection.Services
{
    /// <summary>
    /// Connect to mongo
    /// </summary>
    public interface IMongoService
    {
        /// <summary>
        /// Get artist collections for a artist id
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns></returns>
        IEnumerable<ArtistCollection> GetArtistCollections(long artistId);

        /// <summary>
        /// Get an artist with specified id
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns></returns>
        Artist? GetArtist(long artistId);

        /// <summary>
        /// Insert or update an album
        /// </summary>
        /// <param name="item"></param>
        Album UpsertAlbum(Album item);

        /// <summary>
        /// Get an album projection by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Album? GetAlbum(long id);
    }
}
