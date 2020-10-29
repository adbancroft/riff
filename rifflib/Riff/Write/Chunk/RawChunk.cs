using System.IO;

namespace Riff.Write.Chunk
{
    public class RawChunk : ChunkBase
    {
        public virtual byte[] Data { get; set; }

        public override int DataSize { get { return Data?.Length ?? 0; } }

        protected override void WriteData(BinaryWriter writer)
        {
            writer.WriteChunkData(Data);
        }        
    }
}