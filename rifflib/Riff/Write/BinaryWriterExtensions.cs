using System.IO;

namespace Riff.Write.Chunk
{
    /// <summary>
    /// A set of extension methods for <cref see="BinaryWriter"/>
    /// </summary>
    internal static class BinaryWriterExtensions
    {
        /// <summary>
        /// Write a fixed length ASCII string to the binary writer.
        /// </summary>
        /// <param name="writer">The reader to read from</param>
        /// <param name="toWrite">The string to write</param>
        /// <param name="size">The size of the string in bytes</param>
        public static void WriteFixedString(this BinaryWriter writer, string toWrite, int size)
        {
            toWrite = toWrite.PadRight(size).Substring(0, size);
            writer.Write(System.Text.Encoding.ASCII.GetBytes(toWrite));
        }

        public static void WriteChunkData(this BinaryWriter writer, byte[] data)
        {
            writer.Write(data ?? new byte[0]);

            // The data is always padded to the nearest word boundary
            int padding = RiffUtils.CalculatePadding(data?.Length ?? 0);
            while (padding!=0)
            {
                writer.Write(new byte());
                --padding;
            }
        }
    }
}