using System.Threading.Tasks;

namespace DIAssignment.FileHandler.Services
{
    /// <summary>
    /// Service to download files from a Google Drive folder
    /// </summary>
    public interface IGDriveService
    {
        /// <summary>
        /// Get the local path for a Google Drive file
        /// </summary>
        /// <returns></returns>
        Task<string> DownloadFile(string fileId);
    }
}
