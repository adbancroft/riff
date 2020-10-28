using System.IO;

namespace Riff.Chunk
{
    /// <summary>
    /// BinaryReader extension methodss
    /// </summary>
    public static class BinaryReaderExtensions
    {
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