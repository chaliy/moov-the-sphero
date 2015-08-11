using System;
using System.Collections.Generic;

namespace Eleks.MoovTheSphero.Utils
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<Tuple<T, T>> Segments<T>(this IEnumerable<T> @this)
        {
            T prev = default(T);
            foreach(var item in @this)
            {
                if (prev != null)
                {
                    yield return Tuple.Create(prev, item);
                }
                prev = item;
            }
        }

        public static void Iterate<T>(this IEnumerable<T> @this, Action<T> action)
        {
            foreach (var item in @this)
            {
                action(item);
            }
        }
    }
}
