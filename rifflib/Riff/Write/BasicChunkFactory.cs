using Riff.Write.Chunk;

namespace Riff.Write
{
    /// <summary>
    /// A <see cref="IChunkFactory"/> that works with the simplest chunk types.
    /// </summary>
    public class BasicChunkFactory : IChunkFactory
    {
        ///<inheritdoc/>
        public ChunkBase Create(string identifier)
        {
            switch (identifier)
            {
                case "LIST": return new ListChunk { Identifier = identifier };
                default: return new RawChunk { Identifier = identifier };
            }
        }
    }
}