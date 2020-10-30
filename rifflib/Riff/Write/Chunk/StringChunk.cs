using System.IO;

namespace Riff.Write.Chunk
{
    /// <summary>
    /// Encapsulates a chunk with a null terminated string data payload
    /// </summary>
    public class StringChunk : ChunkBase
    {
        public string Data { get; set; }

        public override int DataSize => Data?.Length+1 ?? 0;

        public override int TotalSize => RiffUtils.CalculateChunkDiskSize(DataSize);

        protected override void WriteData(BinaryWriter writer)
        {
            writer.WriteChunkData(System.Text.Encoding.ASCII.GetBytes(Data+"\0"));
        }        
    }
}