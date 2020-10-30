using System.IO;

namespace Riff.Write.Chunk
{
    /// <summary>
    /// Encapsulates a chunk with a byte[] data payload
    /// </summary>
    public class ByteArrayChunk : ChunkBase
    {
        /// <summary>
        /// The chunk payload
        /// </summary>
        public byte[] Data { get; set; }

        /// <inheritdoc>
        public override int DataSize => Data?.Length ?? 0;

        /// <inheritdoc>
        public override int TotalSize => RiffUtils.CalculateTotalChunkSIze(DataSize);

        protected override void WriteData(BinaryWriter writer)
        {
            writer.WriteChunkData(Data);
        }        
    }
}