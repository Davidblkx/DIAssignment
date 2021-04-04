using DIAssignment.Core.DBFile;
using System;
using System.Threading.Tasks;

namespace DIAssignment.FileHandler.Services
{
    public sealed class DbFileReaderService : IDbFileReaderService
    {
        public async Task ReadDbFileAsync(string path, Action<DbFileRow> action)
        {
            using var reader = new DbFileReader(path);

            DbFileRow? row;
            while ((row = await reader.ReadRowAsync()) is not null)
                action(row);
        }

        public void ReadDbFile(string path, Action<DbFileRow> action)
        {
            using var reader = new DbFileReader(path);

            DbFileRow? row;
            while ((row = reader.ReadRow()) is not null)
                action(row);
        }
    }
}
