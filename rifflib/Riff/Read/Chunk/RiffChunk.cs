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
        public RiffChunkDescriptor(IChunkFactory chunkFactory) : base("RIFF", chunkFactory)
        {
        }
    }
}