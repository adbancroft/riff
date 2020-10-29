using System.IO;
using Riff.Write.Chunk.Lazy;

namespace Riff.Read.Chunk
{
    public class RawChunkDescriptor : ChunkDescriptorBase 
    {
        public RawChunkDescriptor(string identifier)
            : base(identifier)
        {
        }

        protected override void ReadData(BinaryReader reader, IChunkFactory chunkFactory)
        {
            // Callers expect the reader to point to the 1st byte after this chunk
            // Note that the data is always padded to the nearest word boundary
            reader.BaseStream.Seek(Size+RiffUtils.CalculatePadding(Size), SeekOrigin.Current);
        }

        // <inheritdoc>
        public override Riff.Write.Chunk.ChunkBase CreateWriteChunk(ISourceStreamProvider provider)
        {
            return new LazyReadRawChunk(this, provider);
        }
    }
}