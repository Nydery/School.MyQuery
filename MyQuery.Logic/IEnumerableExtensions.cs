using System;
using System.Collections.Generic;

namespace MyQuery.Logic
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Filter<T> (this IEnumerable<T> source, Func<T, bool> predicate)
        {
            List<T> result = new List<T>();

            foreach(T item in source)
            {
                if (predicate(item))
                    result.Add(item);
            }

            return result;
        }

        public static 
    }
}
