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

        public static IEnumerable<TResult> Map<T, TResult> (this IEnumerable<T> source, Func<T, TResult> selector)
        {
            List<TResult> result = new List<TResult>();

            foreach(T item in source)
            {
                result.Add(selector(item));
            }

            return result;
        }

        public static T[] ToArray<T> (this IEnumerable<T> source)
        {
            return new List<T>(source).ToArray();
        }

        public static List<T> ToList<T> (this IEnumerable<T> source)
        {
            return new List<T>(source);
        }

        public static double Sum<T> (this IEnumerable<T> source, Func<T, double> transform)
        {
            var result = 0.0;

            foreach(T item in source)
            {
                result += transform(item);
            }

            return result;
        }
    }
}
