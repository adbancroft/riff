using Riff.Chunk;

namespace Riff
{
    public interface IChunkFactory
    {
        ChunkBase Create(string identifier);
    }
}