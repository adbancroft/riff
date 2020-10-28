using System.IO;
using Riff.Chunk;

namespace Riff
{
    /// <summary>
    /// 
    /// </summary>
    public static class RiffReader
    {   
        /// <summary>
        /// Read a RIFF file into a series of chunks in a tree like structure.
        /// </summary>
        /// <param name="reader">The source to read from</param>
        /// <param name="chunkFactory">Create chunk instances from identifers</param>
        /// <remarks>
        /// See https://docs.microsoft.com/en-us/windows/win32/directshow/avi-riff-file-reference for details.
        /// </remarks>
        public static RiffChunk Read(BinaryReader reader, IChunkFactory chunkFactory)
        {
            var topChunk = new RiffChunk(ChunkUtils.ReadIdentifier(reader));
            topChunk.Read(reader, chunkFactory);
            return topChunk;
        }
    }
}