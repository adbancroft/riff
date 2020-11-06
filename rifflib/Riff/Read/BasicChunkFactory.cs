using System.IO;
using Riff.Read.Chunk;

namespace Riff.Read
{
    /// <summary>
    /// A <see cref="IChunkFactory"/> that works with the simplest chunk types
    /// and initializes chunks during construction by reading from a binary source.
    /// </summary>
    public class BasicChunkFactory : IChunkFactory
    {
        private readonly BinaryReader _reader;

        /// <summary>
        /// Construct the factory
        /// </summary>
        /// <param name="reader">Binary data source to initialize chunks from</param>
        public BasicChunkFactory(BinaryReader reader)
        {
            _reader = reader;
        }

        ///<inheritdoc/>
        public ChunkDescriptorBase Create(string identifier)
        {
            return (identifier.ToLowerInvariant()) switch
            {
                "riff" => new RiffChunkDescriptor(_reader, this),
                "list" => new ListChunkDescriptor(identifier, _reader, this),
                _ => new ByteArrayChunkDescriptor(identifier, _reader),
            };
        }
    }
}