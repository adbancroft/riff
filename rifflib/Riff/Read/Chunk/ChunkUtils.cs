using System.IO;

namespace Riff.Read.Chunk
{
    /// <summary>
    /// Shared chunk related methods.`
    /// </summary>
    public static class ChunkUtils
    {
        /// <summary>
        /// Read a chunk identifier from the reader.
        /// </summary>
        public static string ReadIdentifier(BinaryReader source)
        {
            return source.ReadFixedString(RiffUtils.IdentifierSize);
        }
    }
}