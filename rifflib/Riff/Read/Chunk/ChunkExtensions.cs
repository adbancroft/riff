using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Riff.Read.Chunk
{
    /// <summary>
    /// Chunk extension methods
    /// </summary>
    public static class ChunkExtensions
    {
        /// <summary>
        /// Chunk data could be very large, so we lazyily read it.
        /// </summary>
        /// <param name="source">The source stream. MUST BE THE SAME DATA SOURCE (E.G. FILE) AS WAS ORIGINALLY USED WITH Read()/param>
        /// <remarks>
        /// We do not store the BinaryReader used to populate the chunk descriptor: it's disposable, which in turn means
        /// this class and sub-classes would also need to be disposable. That is too heavy a burden on all users
        /// of this library.
        /// </remarks>
        static public byte[] ReadData(this ChunkDescriptorBase chunk, Stream source)
        {
            source.Seek(chunk.ChunkOffset + RiffUtils.IdentifierSize + ChunkDescriptorBase.LengthSize, SeekOrigin.Begin);
            using (var reader = new BinaryReader(source, System.Text.Encoding.ASCII, true))
            {
                return reader.ReadBytes(chunk.Size);
            }
        }

        /// <summary>
        /// Read the chunk data as a string.
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ReadStringData(this ChunkDescriptorBase chunk, Stream source)
        {
           return System.Text.Encoding.ASCII.GetString(chunk.ReadData(source)); 
        }       

        /// <summary>
        /// Handy extension method to find a list chunk with a specific list type 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="fourCc"></param>
        /// <returns></returns>
        public static IEnumerable<ChunkDescriptorBase> WhereListType(this IEnumerable<ChunkDescriptorBase> source, string listType)
        {
            return source.OfType<ListChunkDescriptor>().Where(cb => cb.ListType==listType);
        }
    }
}