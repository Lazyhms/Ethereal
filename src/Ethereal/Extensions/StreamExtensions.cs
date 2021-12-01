// Copyright (c) Ethereal. All rights reserved.

using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
    /// <summary>
    /// Stream
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Reads all characters from the current position to the end of the stream.
        /// </summary>
        public static string ToString(this Stream stream, Encoding encoding)
        {
            using var reader = new StreamReader(stream, encoding);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// Reads all characters from the current position to the end of the stream asynchronously
        ///     and returns them as one string.
        /// </summary>
        public static async Task<string> ToStringAsync(this Stream stream, Encoding encoding)
        {
            using var reader = new StreamReader(stream, encoding);
            return await reader.ReadToEndAsync();
        }
    }
}
