using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Riff.Chunk
{
    /// <summary>
    /// Chunk extension methods
    /// </summary>
    public static class ChunkExtensions
    {
        /// <summary>
        /// Read the chunk data as a string.
        /// </summary>
        /// <param name="chunk"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ReadStringData(this ChunkBase chunk, Stream source)
        {
           return System.Text.Encoding.ASCII.GetString(chunk.ReadData(source)); 
        }       

        /// <summary>
        ///  Find the 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="fourCc"></param>
        /// <returns></returns>
        public static IEnumerable<ChunkBase> WhereListType(this IEnumerable<ChunkBase> source, string listType)
        {
            return source.OfType<ListChunk>().Where(cb => cb.ListType==listType);
        }
    }
}