using System.IO;

namespace Riff.Read
{
    /// <summary>
    /// BinaryReader extension methods
    /// </summary>
    public static class BinaryReaderExtensions
    {
        /// <summary>
        /// Read a chunk identifier from the reader.
        /// </summary>
        public static string ReadIdentifier(this BinaryReader source)
        {
            return source.ReadFixedString(RiffUtils.IdentifierSize);
        }

        /// <summary>
        /// Read a fixed length ASCII string from the binary reader.
        /// </summary>
        /// <param name="reader">The reader to read from</param>
        /// <param name="size">The size of the string in bytes</param>
        public static string ReadFixedString(this BinaryReader reader, int size)
        {
            return System.Text.Encoding.ASCII.GetString(reader.ReadBytes(size));
        }
    }
}