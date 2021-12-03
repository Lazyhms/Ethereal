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
        public static string ToString(
            this Stream stream,
            Encoding encoding)
        {
            Check.NotNull(stream, "stream");

            using var reader = new StreamReader(stream, encoding);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// Reads all characters from the current position to the end of the stream asynchronously
        ///     and returns them as one string.
        /// </summary>
        public static async Task<string> ToStringAsync(
            this Stream stream,
            Encoding encoding)
        {
            Check.NotNull(stream, "stream");

            using var reader = new StreamReader(stream, encoding);
            return await reader.ReadToEndAsync();
        }

        /// <summary>
        /// Reads characters from the current position to the end of the stream.
        /// </summary>
        public static byte[] ToByteArray(
            this Stream stream,
            bool close = true)
        {
            Check.NotNull(stream, "stream");

            var bytes = new byte[stream.Length];
            stream.Read(bytes);
            stream.Seek(0L, SeekOrigin.Begin);

            if (close)
            {
                stream.Close();
            }
            return bytes;
        }

        /// <summary>
        /// Reads all characters from the current position to the end of the stream asynchronously
        ///     and returns them as one byte.
        /// </summary>
        public static async Task<byte[]> ToByteArrayAsync(
            this Stream stream,
            bool close = true)
        {
            Check.NotNull(stream, "stream");

            var bytes = new byte[stream.Length];
            await stream.ReadAsync(bytes);
            stream.Seek(0L, SeekOrigin.Begin);

            if (close)
            {
                stream.Close();
            }
            return bytes;
        }

        /// <summary>
        /// Reads all characters from the current position to the end of the stream.
        /// </summary>
        public static string ToBase64String(
            this Stream stream)
        {
            var inArray = stream.ToByteArray();
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        /// Reads all characters from the current position to the end of the stream asynchronously
        ///     and returns them as one base-64 string.
        /// </summary>
        public static async Task<string> ToBase64StringAsync(
            this Stream stream)
        {
            var inArray = await stream.ToByteArrayAsync();
            return Convert.ToBase64String(inArray);
        }
    }
}
