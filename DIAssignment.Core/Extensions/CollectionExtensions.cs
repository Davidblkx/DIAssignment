using System.Collections.Generic;

namespace DIAssignment.Core.Extensions
{
    /// <summary>
    /// Extension methods for collections
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Add values from <paramref name="toMerge"/> into <paramref name="self"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="toMerge"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> Merge<TKey, TValue> (
            this Dictionary<TKey, TValue> self,
            Dictionary<TKey, TValue> toMerge
        ) where TKey : notnull {
            foreach (var pair in toMerge)
                self[pair.Key] = pair.Value;

            return self;
        }
    }
}
