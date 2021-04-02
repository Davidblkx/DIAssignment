using DIAssignment.Core.DBFile;
using System;
using System.Threading.Tasks;

namespace DIAssignment.FileHandler.Services
{
    public sealed class DbFileReaderService : IDbFileReaderService
    {
        public async Task ReadDbFile(string path, Action<DbFileRow> action)
        {
            using var reader = new DbFileReader(path);

            DbFileRow? row;
            while ((row = await reader.ReadRow()) is not null)
                action(row);
        }
    }
}
