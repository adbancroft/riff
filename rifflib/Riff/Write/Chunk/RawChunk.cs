using System.IO;

namespace Riff.Write.Chunk
{
    /// <summary>
    /// Encapsulates a chunk with a byte[] data payload
    /// </summary>
    public class RawChunk : ChunkBase
    {
        public byte[] Data { get; set; }

        public override int DataSize => Data?.Length ?? 0;

        public override int TotalSize => RiffUtils.CalculateChunkDiskSize(DataSize);

        protected override void WriteData(BinaryWriter writer)
        {
            writer.WriteChunkData(Data);
        }        
    }
}