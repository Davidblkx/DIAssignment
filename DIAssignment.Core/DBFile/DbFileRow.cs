using System.Linq;

namespace DIAssignment.Core.DBFile
{
    /// <summary>
    /// Allow to grab specific columns from a 
    /// </summary>
    public sealed class DbFileRow
    {
        public string[] Headers { get; }
        public string[] Values { get; }

        public DbFileRow(string[] headers, string[] values)
        {
            Headers = headers;
            Values = values;
        }

        /// <summary>
        /// Get a value by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetValue(int index)
            => index < Values.Length ? Values[index] : "";

        /// <summary>
        /// Get value by column
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public string GetValue(string column)
        {
            for (var i = 0; i < Headers.Length; i++)
                if (string.Equals(Headers[i], column, System.StringComparison.OrdinalIgnoreCase))
                    return GetValue(i);
            return "";
        }
    }
}
