using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Riff.Write.Chunk
{
    /// <summary>
    /// Chunk extension methods
    /// </summary>
    public static class ChunkExtensions
    {
         // <summary>
        /// Handy extension method to find a list chunk with a specific list type 
        /// </summary>
        /// <param name="source">ENumerable to search</param>
        /// <param name="listType">List type identifier</param>
        /// <returns></returns>
        public static IEnumerable<ListChunk> WhereListType(this IEnumerable<ChunkBase> source, string listType)
        {
            return source.OfType<ListChunk>().Where(cb => cb.ListType==listType);
        }
    }
}