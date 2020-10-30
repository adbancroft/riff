using Riff.Read.Chunk;

namespace Riff.Read
{
    /// <summary>
    /// A <see cref="IChunkFactory"/> that works with the simplest chunk types.
    /// </summary>
    public class BasicChunkFactory : IChunkFactory
    {
        private readonly ISourceStreamProvider _streamProvider;

        public BasicChunkFactory(ISourceStreamProvider streamProvider)
        {
            _streamProvider = streamProvider;
        }

        ///<inheritdoc/>
        public ChunkDescriptorBase Create(string identifier)
        {
            switch (identifier.ToLowerInvariant())
            { 
                case "riff": return new RiffChunkDescriptor(this);
                case "list": return new ListChunkDescriptor(identifier, this);
                default: return new RawChunkDescriptor(identifier, _streamProvider);
            }
        }
    }
}