using System.IO;

namespace Riff.Chunk
{
    public static class ChunkUtils
    {
        public static string ReadIdentifier(BinaryReader source)
        {
            return source.ReadFixedString(ChunkBase.IdentifierSize);
        }
    }
}