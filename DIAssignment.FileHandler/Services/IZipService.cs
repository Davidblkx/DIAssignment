namespace DIAssignment.FileHandler.Services
{
    public interface IZipService
    {
        /// <summary>
        /// Extracts a zip file to a folder
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        string ExtractZip(string filePath);
    }
}
