using Riff.Read.Chunk;

namespace Riff.Read
{
    /// <summary>
    /// A <see cref="IChunkFactory"/> that works with the simplest chunk types.
    /// </summary>
    public class BasicChunkFactory : IChunkFactory
    {
        ///<inheritdoc/>
        public ChunkDescriptorBase Create(string identifier)
        {
            switch (identifier)
            {
                case "LIST": return new ListChunkDescriptor(identifier);
                default: return new RawChunkDescriptor(identifier);
            }
        }
    }
}