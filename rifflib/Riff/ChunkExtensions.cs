using System.Collections.Generic;
using System.Linq;

namespace Riff
{
    /// <summary>
    /// Chunk extension methods
    /// </summary>
    public static class ChunkExtensions
    {
        /// <summary>
        /// Handy extension method to find a list chunk with a specific list type
        /// </summary>
        /// <param name="source">The sequence to search</param>
        /// <param name="listType">The list type identifier</param>
        /// <returns>An <see cref="IEnumerable{IListChunk}"/> that contains elements from the input sequence that satisfy the condition.</returns>
        public static IEnumerable<IListChunk> WhereListType(this IEnumerable<IChunk> source, string listType)
        {
            return source.OfType<IListChunk>().Where(cb => cb.ListType==listType);
        }
    }
}