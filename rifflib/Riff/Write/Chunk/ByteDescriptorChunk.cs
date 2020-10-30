using System.IO;
using Riff.Read.Chunk;

namespace Riff.Write.Chunk
{
    /// <summary>
    /// A writeable chunk that mirrors a byte chunk descriptor
    /// </summary>
    public class LazyByteDescriptorChunk : ChunkBase
    {
        private readonly LazyByteArrayChunkDescriptor _source;

        /// <summary>
        /// Construct from a byte chunk descriptor 
        /// </summary>
        /// <param name="source">The byte chunk descriptor</param>
        public LazyByteDescriptorChunk(Riff.Read.Chunk.LazyByteArrayChunkDescriptor source)
        {
            _source = source;
        }

        /// <inheritdoc>
        public override int DataSize => _source.Size;

        /// <inheritdoc>
        public override string Identifier => _source.Identifier;

        /// <inheritdoc>
        public override int TotalSize  => RiffUtils.CalculateTotalChunkSIze(DataSize);

        // Just copy the data from the source chunk to the writer        
        protected override void WriteData(BinaryWriter writer)
        {
            writer.WriteChunkData(_source.ReadData());
        }
    }
}