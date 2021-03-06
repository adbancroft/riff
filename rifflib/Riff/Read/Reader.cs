using System.IO;
using Riff.Read.Chunk;

namespace Riff.Read
{
    /// <summary>
    /// Root call to read a RIFF file
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
            return chunkFactory.Create(reader.ReadIdentifier());
        }
    }
}