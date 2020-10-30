using System.IO;
using Riff.Write.Chunk.Lazy;
using Validation;

namespace Riff.Read.Chunk
{
    public class RawChunkDescriptor : ChunkDescriptorBase 
    {
        private readonly ISourceStreamProvider _streamProvider;

        public RawChunkDescriptor(string identifier, ISourceStreamProvider streamProvider)
            : base(identifier)
        {
            Requires.NotNull(streamProvider, nameof(streamProvider));
            _streamProvider = streamProvider;
        }

        protected override void ReadData(BinaryReader reader)
        {
            // Callers expect the reader to point to the 1st byte after this chunk
            // Note that the data is always padded to the nearest word boundary
            reader.BaseStream.Seek(Size+RiffUtils.CalculatePadding(Size), SeekOrigin.Current);
        }

        public byte[] ReadData2()
        {
            var source = _streamProvider.Provide();
            source.Seek(ChunkOffset + RiffUtils.HeaderSize, SeekOrigin.Begin);
            using (var reader = new BinaryReader(source))
            {
                return reader.ReadBytes(Size);
            }
        }

        // <inheritdoc>
        public override Riff.Write.Chunk.ChunkBase CreateWriteChunk()
        {
            return new LazyReadRawChunk(this);
        }
    }
}