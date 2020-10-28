using System.IO;

namespace Riff.Read.Chunk
{
    public class RawChunk : ChunkBase 
    {
        public RawChunk(string identifier)
            : base(identifier)
        {
        }

        public override void Read(BinaryReader reader, IChunkFactory chunkFactory)
        {
            base.Read(reader, chunkFactory);
            // Callers expect the reader to point to the 1st byte
            reader.BaseStream.Seek(Size+Padding, SeekOrigin.Current);
        }
    }
}