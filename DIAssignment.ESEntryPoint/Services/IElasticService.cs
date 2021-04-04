using DIAssignment.Core.Models.Entity;
using System.Threading.Tasks;

namespace DIAssignment.ESEntryPoint.Services
{
    /// <summary>
    /// Allow to index albums to elastic
    /// </summary>
    public interface IElasticService
    {
        /// <summary>
        /// Index a document of <typeparamref name="T"/>
        /// </summary>
        bool Index<T>(T document) where T : class;

        /// <summary>
        /// Ensure that an index exists for <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        bool EnsureIndex<T>();

        /// <summary>
        /// Wait for elastic to be ready
        /// </summary>
        /// <returns></returns>
        Task AwaitForConnection();

        /// <summary>
        /// Check if connection is available
        /// </summary>
        /// <returns></returns>
        bool Ping();
    }
}
