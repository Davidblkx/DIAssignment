using System;
using System.IO;
using System.Threading.Tasks;

namespace DIAssignment.Core.DBFile
{
    /// <summary>
    /// Reads an exported file
    /// </summary>
    public sealed class DbFileReader : IDisposable
    {
        private const char SPLITTER = '\u0001';
        private const string TERMINATION = "\u0002";

        private readonly FileStream _fileStream;
        private readonly BufferedStream _bufferStream;
        private readonly StreamReader _reader;
        private readonly string[] _headers;

        public DbFileReader(string path)
        {
            _fileStream = File.OpenRead(path);
            _bufferStream = new BufferedStream(_fileStream);
            _reader = new StreamReader(_bufferStream);
            _headers = ReadHeader();
        }

        /// <summary>
        /// Read the next row
        /// </summary>
        /// <returns>null if eof</returns>
        public async Task<DbFileRow?> ReadRowAsync()
        {
            var row = await _reader.ReadLineAsync();
            if (row is null) return null;
            // Skip empty rows and ones that start by #
            if (string.IsNullOrEmpty(row) || row[0] == '#') return await ReadRowAsync();
            return new DbFileRow(_headers, SplitRow(row));
        }

        public DbFileRow? ReadRow()
        {
            var row = _reader.ReadLine();
            if (row is null) return null;
            if (string.IsNullOrEmpty(row) || row[0] == '#') return ReadRow();
            return new DbFileRow(_headers, SplitRow(row));
        }

        /// <summary>
        /// Reads the first row
        /// </summary>
        /// <returns></returns>
        private string[] ReadHeader()
            => SplitRow(_reader.ReadLine());

        /// <summary>
        /// Split row by columns and remove line termination
        /// </summary>
        private string[] SplitRow(string? row)
            => row?.Replace(TERMINATION, "")
                .Split(SPLITTER)
            ?? throw new NullReferenceException("row is empty");

        public void Dispose()
        {
            _reader.Close();
            _bufferStream.Close();
            _fileStream.Close();
        }
    }
}
