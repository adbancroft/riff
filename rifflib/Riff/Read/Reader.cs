using System.IO;
using Riff.Read.Chunk;

namespace Riff.Read
{
    /// <summary>
    /// 
    /// </summary>
    public static class Reader
    {   
        /// <summary>
        /// Read a RIFF file into a series of chunks in a tree like structure.
        /// </summary>
        /// <param name="reader">The source to read from</param>
        /// <param name="chunkFactory">Create chunk instances from identifers</param>
        /// <remarks>
        /// See https://docs.microsoft.com/en-us/windows/win32/directshow/avi-riff-file-reference for details.
        /// </remarks>
        public static ChunkDescriptorBase Read(BinaryReader reader, IChunkFactory chunkFactory)
        {
            var topChunk = chunkFactory.Create(ChunkUtils.ReadIdentifier(reader));
            topChunk.Read(reader, chunkFactory);
            return topChunk;
        }
    }
}