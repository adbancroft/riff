using System.IO;
using Riff.Read.Chunk;

namespace Riff.Read
{
    /// <summary>
    /// A <see cref="IChunkFactory"/> that works with the simplest chunk types.
    /// </summary>
    public class LazyBasicChunkFactory : IChunkFactory
    {
        private readonly BinaryReader _reader;
        private readonly IStreamProvider _streamProvider;

        public LazyBasicChunkFactory(BinaryReader reader, IStreamProvider streamProvider)
        {
            _reader = reader;
            _streamProvider = streamProvider;
        }

        ///<inheritdoc/>
        public ChunkDescriptorBase Create(string identifier)
        {
            switch (identifier.ToLowerInvariant())
            { 
                case "riff": return new RiffChunkDescriptor(_reader, this);
                case "list": return new ListChunkDescriptor(identifier, _reader, this);
                default: return new LazyByteArrayChunkDescriptor(identifier, _reader, _streamProvider);
            }
        }
    }
}