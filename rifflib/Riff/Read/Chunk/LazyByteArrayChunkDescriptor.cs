using System.IO;
using Riff.Write.Chunk;
using Validation;

namespace Riff.Read.Chunk
{
    /// <summary>
    /// A chunk that has a byte[] payload that is lazily loaded on demand
    /// </summary>
    public class LazyByteArrayChunkDescriptor : ChunkDescriptorBase 
    {
        private readonly IStreamProvider _streamProvider;

        /// <summary>
        /// Construct by reading from a BinaryReader
        /// </summary>
        /// <param name="identifier">The chunk identifer</param>
        /// <param name="reader">The source to read from</param>
        /// <param name="streamProvider">The stream provider used to lazy load the data payload</param>
        public LazyByteArrayChunkDescriptor(string identifier, BinaryReader reader, IStreamProvider streamProvider)
            : base(identifier, reader)
        {
            Requires.NotNull(streamProvider, nameof(streamProvider));
            _streamProvider = streamProvider;

            // Callers expect the reader to point to the 1st byte after this chunk
            // Note that the data is always padded to the nearest word boundary
            reader.BaseStream.Seek(Size+RiffUtils.CalculatePadding(Size), SeekOrigin.Current);
        }

        /// <summary>
        /// Read the chunk data payload. RIFF chunk data can be very large (up to 2Gb), so it is lazy 
        /// loaded rather than loaded in the constructor.
        /// </summary>
        public override byte[] Data
        {
            get 
            {
                var source = _streamProvider.Provide();
                source.Seek(ChunkOffset + RiffUtils.HeaderSize, SeekOrigin.Begin);
                using (var reader = new BinaryReader(source))
                {
                    return reader.ReadBytes(Size);
                }
            }
        }

        // <inheritdoc>
        public override Riff.Write.Chunk.ChunkBase CreateWriteChunk()
        {
            return new ByteDescriptorChunk(this);
        }
    }
}