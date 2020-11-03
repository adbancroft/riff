using System.IO;

namespace Riff.Write.Chunk
{
    /// <summary>
    /// Encapsulates a chunk with a null terminated string data payload
    /// </summary>
    public class StringChunk : ChunkBase
    {
        /// <summary>
        /// Chunk data
        /// </summary>
        public string Data { get; set; }

        /// <inheritdoc/>
        public override int DataSize => Data?.Length+1 ?? 0;

        /// <inheritdoc/>
        public override int TotalSize => RiffUtils.CalculateTotalChunkSIze(DataSize);

        /// <inheritdoc/>
        public override void WriteData(BinaryWriter writer)
        {
            writer.WriteChunkData(System.Text.Encoding.ASCII.GetBytes(Data+"\0"));
        }        
    }
}