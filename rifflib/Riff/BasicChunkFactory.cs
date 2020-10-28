using Riff.Chunk;

namespace Riff
{
    public class BasicChunkFactory : IChunkFactory
    {
        public ChunkBase Create(string identifier)
        {
            switch (identifier)
            {
                case "LIST": return new ListChunk(identifier);
                default: return new RawChunk(identifier);
            }
        }
    }
}