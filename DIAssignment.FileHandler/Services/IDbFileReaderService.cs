using DIAssignment.Core.DBFile;
using System;
using System.Threading.Tasks;

namespace DIAssignment.FileHandler.Services
{
    public interface IDbFileReaderService
    {
        /// <summary>
        /// Reads the file row and invoke an action for each row
        /// </summary>
        /// <param name="path"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        Task ReadDbFile(string path, Action<DbFileRow> action);
    }
}
