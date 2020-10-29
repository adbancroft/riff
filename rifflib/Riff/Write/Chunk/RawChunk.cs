using System.IO;

namespace Riff.Write.Chunk
{
    public class RawChunk : ChunkBase
    {
        public virtual byte[] Data { get; set; }

        public override int DataSize => Data?.Length ?? 0;

        public override int TotalSize => RiffUtils.CalculateChunkDiskSize(DataSize);

        protected override void WriteData(BinaryWriter writer)
        {
            writer.WriteChunkData(Data);
        }        
    }
}