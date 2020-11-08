using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

        /// <summary>
        /// Find a chunks parent
        /// </summary>
        /// <param name="root">Root chunk to search from</param>
        /// <param name="path">Path to the child chunk.</param>
        /// <returns></returns>
        public static T FindParent<T>(this T root, string path)
            where T : IChunk, IEnumerable<T>
        {
            return FindChunk(root, Regex.Matches(path, pathRegEx, RegexOptions.IgnoreCase).Where(f => f != null).SkipLast(1));
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
        public static T FindChunk<T>(this T parent, string path)
            where T : IChunk, IEnumerable<T>
        {
            return FindChunk(parent, Regex.Matches(path, pathRegEx, RegexOptions.IgnoreCase));
        }

        private static T FindChunk<T>(T parent, IEnumerable<Match> path)
            where T : IChunk, IEnumerable<T>
        {
            foreach (Match part in path)
            {
                parent = FindDirectChild(parent, part);
            }
            return parent;
        }

        private static T FindDirectChild<T>(T parent, Match match)
            where T : IChunk, IEnumerable<T>
        {
            // We always have an id
            var enumerable = parent.Where(c => string.Equals(c.Identifier, match.Groups["id"].Value, StringComparison.InvariantCultureIgnoreCase));
            // List type is optional
            enumerable = match.Groups["list"].Success
                        ? enumerable.OfType<IListChunk>()
                                    .Where(c => string.Equals(c.ListType, match.Groups["list"].Value, StringComparison.InvariantCultureIgnoreCase))
                                    .OfType<T>()
                        : enumerable;
            // Index is optional
            int index = match.Groups["index"].Success
                        ? int.Parse(match.Groups["index"].Value)
                        : 0;
            return enumerable.ElementAtOrDefault(index) ?? throw new ArgumentException($"Invalid chunk identifier: {match}", nameof(match));
        }

        private const string ChunkDelimiter = "^|[\\\\|]";
        private const string PartDelimiter = "-";
        private const string IdExpression = "(?<id>.{4})";
        private const string ListExpression = "(?<list>.{4})";
        private const string IndexExpression = @"(?<index>\d+)";
        private static readonly string pathElementRegEx = $"{IdExpression}(?:{PartDelimiter}{ListExpression})?(?:{PartDelimiter}{IndexExpression})?";
        private static readonly string pathRegEx = $"(?:{ChunkDelimiter}){pathElementRegEx}";
    }
}