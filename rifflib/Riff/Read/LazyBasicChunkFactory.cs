using System.IO;
using Riff.Read.Chunk;

namespace Riff.Read
{
    /// <summary>
    /// A <see cref="IChunkFactory"/> that works with the simplest chunk types.
    /// Will create leaf chunks that lazily load their data. Useful for memory efficiency
    /// </summary>
    public class LazyBasicChunkFactory : IChunkFactory
    {
        private readonly BinaryReader _reader;
        private readonly IStreamProvider _streamProvider;

        /// <summary>
        /// Construct the factory
        /// </summary>
        /// <param name="reader">The source to read metadata from</param>
        /// <param name="streamProvider">The source to lazily read data from</param>
        public LazyBasicChunkFactory(BinaryReader reader, IStreamProvider streamProvider)
        {
            _reader = reader;
            _streamProvider = streamProvider;
        }

        ///<inheritdoc/>
        public ChunkDescriptorBase Create(string identifier)
        {
            return (identifier.ToLowerInvariant()) switch
            {
                "riff" => new RiffChunkDescriptor(_reader, this),
                "list" => new ListChunkDescriptor(identifier, _reader, this),
                _ => new LazyByteArrayChunkDescriptor(identifier, _reader, _streamProvider),
            };
        }
    }
}