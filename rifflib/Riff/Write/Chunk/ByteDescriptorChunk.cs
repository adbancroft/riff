using System.IO;
using Riff.Read.Chunk;

namespace Riff.Write.Chunk
{
    /// <summary>
    /// A writeable chunk that mirrors a byte chunk descriptor
    /// </summary>
    public class ByteDescriptorChunk : ChunkBase
    {
        private readonly ChunkDescriptorBase _source;

        /// <summary>
        /// Construct from a byte chunk descriptor
        /// </summary>
        /// <param name="source">The byte chunk descriptor</param>
        public ByteDescriptorChunk(ChunkDescriptorBase source)
        {
            _source = source;
        }

        /// <summary>
        /// The chunk payload
        /// </summary>
        public byte[] Data => _source.Data;

        /// <inheritdoc/>
        public override int DataSize => _source.Size;

        /// <inheritdoc/>
        public override string Identifier => _source.Identifier;

        /// <inheritdoc/>
        public override int TotalSize  => RiffUtils.CalculateTotalChunkSIze(DataSize);

        /// <inheritdoc/>
        // Just copy the data from the source chunk to the writer        
        public override void WriteData(BinaryWriter writer)
        {
            writer.WriteChunkData(Data);
        }
    }
}