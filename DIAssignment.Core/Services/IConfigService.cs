namespace DIAssignment.Core.Services
{
    /// <summary>
    /// Read variables from environment or config file
    /// </summary>
    public interface IConfigService
    {
        /// <summary>
        /// Get the variable for <paramref name="key"/>, Environment -> Secret -> Config -> null
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string? Get(string key);
    }
}
