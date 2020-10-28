using System.IO;

namespace Riff.Chunk
{
    public static class BinaryReaderExtensions
    {
        public static string ReadFixedString(this BinaryReader reader, int size)
        {
            return System.Text.Encoding.UTF8.GetString(reader.ReadBytes(size));
        }
    }
}