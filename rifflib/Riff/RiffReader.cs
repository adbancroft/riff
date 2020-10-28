using System.IO;
using Riff.Chunk;
using System.Collections.Generic;

namespace Riff
{
    public static class RiffReader
    {   
        public static RiffChunk Read(BinaryReader reader, IChunkFactory chunkFactory)
        {
            var topChunk = new RiffChunk(ChunkUtils.ReadIdentifier(reader));
            topChunk.Read(reader, chunkFactory);
            return topChunk;
        }
    }
}