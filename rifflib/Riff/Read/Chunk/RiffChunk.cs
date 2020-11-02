using System.IO;
using Validation;

namespace Riff.Read.Chunk
{
    /// <summary>
    /// Top level chunk that captures the entire RIFF file
    /// </summary>
    /// <remarks>
    /// A RIFF file consists of a series of chunks. The 1st chunk has the following form:
    ///     'RIFF' fileSize fileType (data)
    /// 
    /// A chunk has the following form:
    ///     ckID ckSize ckData
    /// 
    /// A list has the following form:
    ///     'LIST' listSize listType listData
    ///
    /// See https://docs.microsoft.com/en-us/windows/win32/directshow/avi-riff-file-reference for details.
    /// </remarks>
    public class RiffChunkDescriptor : ListChunkDescriptor
    {
        /// <summary>
        /// Construct by reading from a BinaryReader
        /// </summary>
        /// <param name="reader">The source to read from</param>
        /// <param name="chunkFactory">Used to create child chunks</param>
        public RiffChunkDescriptor(BinaryReader reader, IChunkFactory chunkFactory) : base("RIFF", reader, chunkFactory)
        {
        }
    }
}