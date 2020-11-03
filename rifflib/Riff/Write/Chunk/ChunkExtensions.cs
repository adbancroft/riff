using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Riff.Write.Chunk
{
    /// <summary>
    /// Chunk extension methods
    /// </summary>
    public static class ChunkExtensions
    {
        /// <summary>
        /// Handy extension method to find a list chunk with a specific list type 
        /// </summary>
        /// <param name="source">ENumerable to search</param>
        /// <param name="listType">List type identifier</param>
        /// <returns></returns>
        public static IEnumerable<ListChunk> WhereListType(this IEnumerable<ChunkBase> source, string listType)
        {
            return source.OfType<ListChunk>().Where(cb => string.Equals(cb.ListType, listType, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Search the chunk tree for a specific child chunk.
        /// </summary>
        /// <param name="parent">Root to start searching from</param>
        /// <param name="path">Path to the child chunk.
        ///     path := [path-element]{"\"path-element}
        ///     path-element := identifier["-"list-type]["-"index]
        ///     identifier := four character chunk code (FourCC)
        ///     list-type := four character list identifer (FourCC)
        ///     index := integer index within chunks with the same identifier. Zero based
        /// E.g. "LIST-hdrl-1\\\\strd"
        ///     Path to the second LIST chunk with list type "hdrl"
        ///     Path to the first chunk with identifier "strd"
        /// 
        /// </param>
        /// <returns></returns>
        public static ChunkBase FindChunk(this ChunkBase parent, string path)
        {
            foreach (Match part in Regex.Matches(path, pathRegEx, RegexOptions.IgnoreCase))
            {
                parent = FindDirectChild(parent, part);
            }

            return parent;
        }

        private static ChunkBase FindDirectChild(ChunkBase parent, Match match)
        {
            // We always have an id
            var enumerable = parent.Where(c => string.Equals(c.Identifier, match.Groups["id"].Value, StringComparison.InvariantCultureIgnoreCase));
            // List type is optional
            enumerable = match.Groups["list"].Success
                        ? enumerable.OfType<ListChunk>().Where(c => string.Equals(c.ListType, match.Groups["list"].Value, StringComparison.InvariantCultureIgnoreCase))
                        : enumerable;
            // Index is optional
            int index = match.Groups["index"].Success
                        ? int.Parse(match.Groups["index"].Value)
                        : 0;
            return enumerable.ElementAtOrDefault(index) ?? throw new ArgumentException($"Invalid chunk identifier: {match}", nameof(match));;
        }

        private const string ChunkDelimiter = "^|[\\\\|]";
        private const string PartDelimiter = "-";
        private const string IdExpression = "(?<id>.{4})";
        private const string ListExpression = "(?<list>.{4})";
        private const string IndexExpression = @"(?<index>\d+)";
        private static string pathElementRegEx = $"{IdExpression}(?:{PartDelimiter}{ListExpression})?(?:{PartDelimiter}{IndexExpression})?";
        private static string pathRegEx = $"(?:{ChunkDelimiter}){pathElementRegEx}";
    }
}