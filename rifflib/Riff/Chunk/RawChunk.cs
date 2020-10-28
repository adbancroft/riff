using System.IO;

namespace Riff.Chunk
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
            reader.BaseStream.Seek(Size+Padding, SeekOrigin.Current);
        }
    }
}