using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Riff
{
    /// <summary>
    /// Handy extensions for
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Depth first search of a tree of nodes
        /// </summary>
        /// <typeparam name="T">The type of elements in the tree</typeparam>
        /// <param name="head">Search start point</param>
        /// <returns>The tree elements in depth first order</returns>
        public static IEnumerable<T> AsDepthFirstEnumerable<T>(this T head)
            where T : IEnumerable<T>
        {
            return AsDepthFirstEnumerable(head, n => n);
        }

        /// <summary>
        /// Depth first search of a tree of nodes
        /// </summary>
        /// <typeparam name="T">The type of elements in the tree</typeparam>
        /// <param name="head">Search start point</param>
        /// <param name="childSelector">Returns the child nodes for an element</param>
        /// <returns>The tree elements in depth first order</returns>
        public static IEnumerable<T> AsDepthFirstEnumerable<T>(this T head, Func<T, IEnumerable<T>> childSelector)
            where T : IEnumerable<T>
        {
            yield return head;

            foreach (var node in childSelector(head))
            {
                foreach (var child in AsDepthFirstEnumerable(node))
                {
                    yield return child;
                }
            }
        }
    }
}