using System;
using System.Collections;
using System.Collections.Generic;

namespace MyQuery.Logic
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Counts the elements of a collection IEnumerable<T>
        /// </summary>
        /// <typeparam name="T">Type of elements in collection 'source'</typeparam>
        /// <param name="source">Collection of type 'T'</param>
        /// <returns>Count of elements in collection</returns>
        public static int Count<T> (this IEnumerable<T> source)
        {
            var result = 0;

            foreach(var item in source)
            {
                result++;
            }

            return result;
        }

        /// <summary>
        /// Filters a collection by a specified condition
        /// </summary>
        /// <typeparam name="T">Type of collection 'source'</typeparam>
        /// <param name="source">Collection of type 'T'</param>
        /// <param name="predicate">Condition on which is filtered</param>
        /// <returns>Filtered collection</returns>
        public static IEnumerable<T> Filter<T> (this IEnumerable<T> source, Predicate<T> predicate)
        {
            source.CheckArgument(nameof(source));
            predicate.CheckArgument(nameof(predicate));

            var result = new List<T>();

            foreach(var item in source)
            {
                if (predicate(item))
                    result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// Maps all elements of a collection to another collection of another type
        /// </summary>
        /// <typeparam name="T">Type of elements in collection 'source'</typeparam>
        /// <typeparam name="TResult">Type of mapped element</typeparam>
        /// <param name="source">Collection of type 'T'</param>
        /// <param name="selector">Selector which returns the new type/value</param>
        /// <returns>Mapped collection of new type</returns>
        public static IEnumerable<TResult> Map<T, TResult> (this IEnumerable<T> source, Func<T, TResult> selector)
        {
            source.CheckArgument(nameof(source));
            selector.CheckArgument(nameof(selector));

            var result = new List<TResult>();

            foreach(var item in source)
            {
                result.Add(selector(item));
            }

            return result;
        }

        /// <summary>
        /// Returns an array of any collection
        /// </summary>
        /// <typeparam name="T">Type of elements in 'source'</typeparam>
        /// <param name="source">Collection of type 'T'</param>
        /// <returns>Array of type 'T[]'</returns>
        public static T[] ToArray<T> (this IEnumerable<T> source)
        {
            source.CheckArgument(nameof(source));

            return new List<T>(source).ToArray();
        }

        public static List<T> ToList<T> (this IEnumerable<T> source)
        {
            source.CheckArgument(nameof(source));

            return new List<T>(source);
        }
        /// <summary>
        /// Creates the sum of a collection based on an transform method
        /// </summary>
        /// <typeparam name="T">collection type</typeparam>
        /// <param name="source">source collection</param>
        /// <param name="transform">transform method that represents an item</param>
        /// <returns></returns>
        public static double Sum<T> (this IEnumerable<T> source, Func<T, double> transform)
        {
            source.CheckArgument(nameof(source));
            transform.CheckArgument(nameof(transform));

            var result = 0.0;

            foreach(var item in source)
            {
                result += transform(item);
            }

            return result;
        }

        /// <summary>
        /// Returns the smallest element in the collection based on a transform method
        /// </summary>
        /// <typeparam name="T">collection type</typeparam>
        /// <param name="source">source collection</param>
        /// <param name="transform">transform method that represents an item</param>
        /// <returns>smallest element</returns>
        public static double? Min<T>(this IEnumerable<T> source, Func<T, double> transform)
        {
            source.CheckArgument(nameof(source));
            transform.CheckArgument(nameof(transform));

            var result = double.MaxValue;

            foreach(var item in source)
            {
                if(transform(item) < result)
                {
                    result = transform(item);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the biggest element in the collection based on a transform method
        /// </summary>
        /// <typeparam name="T">collection type</typeparam>
        /// <param name="source">source collection</param>
        /// <param name="transform">transform method that represents an item</param>
        /// <returns>biggest element</returns>
        public static double? Max<T>(this IEnumerable<T> source, Func<T, double> transform)
        {
            source.CheckArgument(nameof(source));
            transform.CheckArgument(nameof(transform));

            var result = double.MinValue;

            foreach (var item in source)
            {
                if (transform(item) > result)
                {
                    result = transform(item);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the average of all elements in the collection based on a transform method
        /// </summary>
        /// <typeparam name="T">collection type</typeparam>
        /// <param name="source">source collection</param>
        /// <param name="transform">transform method that represents an item</param>
        /// <returns>average</returns>
        public static double? Average<T>(this IEnumerable<T> source, Func<T, double> transform)
        {
            source.CheckArgument(nameof(source));
            transform.CheckArgument(nameof(transform));

            return source.Sum(transform) / source.Count();
        }
        
        /// <summary>
        /// Executes an action on every item in collection
        /// </summary>
        /// <typeparam name="T">type of collection</typeparam>
        /// <param name="source">source collection</param>
        /// <param name="action">action to be performed</param>
        /// <returns>source collection</returns>
        public static IEnumerable<T> ForEach<T> (this IEnumerable<T> source, Action<T> action)
        {
            source.CheckArgument(nameof(source));
            action.CheckArgument(nameof(action));

            foreach (var item in source)
            {
                action(item);
            }

            return source;
        }

        /// <summary>
        /// Executes an action on every item in collection with index
        /// </summary>
        /// <typeparam name="T">type of collection</typeparam>
        /// <param name="source">source collection</param>
        /// <param name="action">action to be performed</param>
        /// <returns>source collection</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<int, T> action)
        {
            source.CheckArgument(nameof(source));
            action.CheckArgument(nameof(action));

            int index = 0;

            foreach (var item in source)
            {
                action(index++, item);
            }

            return source;
        }

        public static IEnumerable<T> SortBy<T> (this IEnumerable<T> source, Func<T, T, int> compare)
        {
            source.CheckArgument(nameof(source));
            compare.CheckArgument(nameof(compare));


            var srcList = new List<T>(source);
            bool changed = false;

            do
            {
                changed = false;

                for(int i = 0; i < srcList.Count - 1; i++)
                {
                    if(compare(srcList[i+1], srcList[i]) > 0)
                    {
                        var temp = srcList[i];
                        srcList[i] = srcList[i + 1];
                        srcList[i + 1] = temp;

                        changed = true;
                    }
                }

            } while (changed);

            return srcList;
        }

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> source)
        {
            source.CheckArgument(nameof(source));

            List<T> result = new List<T>();

            foreach (var item in source)
            {
                if (!result.Contains(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        class SortByComparer<T, TKey> : IComparer<T>
            where TKey : IComparable
        {
            public Func<T, TKey> OrderBy { get; }

            public SortByComparer(Func<T, TKey> orderby)
            {
                orderby.CheckArgument(nameof(orderby));

                OrderBy = orderby;
            }

            public int Compare(T? x, T? y)
            {
                return OrderBy(x).CompareTo(OrderBy(y));
            }
        }

        public static IEnumerable<T> SortBy<T, TKey> (this IEnumerable<T> source, Func<T, TKey> orderBy)
            where TKey : IComparable
        {
            source.CheckArgument(nameof(source));
            orderBy.CheckArgument(nameof(orderBy));


            var result = source.ToArray();

            Array.Sort(result, new SortByComparer<T, TKey>(orderBy));

            return result;
            /*
            bool changed = false;

            do
            {
                changed = false;

                for (int i = 0; i < result.Count - 1; i++)
                {
                    if (orderBy(result[i]).CompareTo(orderBy(result[i + 1])) > 0)
                    {
                        var temp = result[i];
                        result[i] = result[i + 1];
                        result[i + 1] = temp;

                        changed = true;
                    }
                }
            } while (changed);
            
            return result;
            */
        }

        public delegate void ActionRef<T1, T2, T3>(T1 arg1, ref T2 arg2, T3 arg3);
        public static IEnumerable<T> ForEach<T> (this IEnumerable<T> source, ActionRef<int, bool, T> action)
        {
            source.CheckArgument(nameof(source));
            action.CheckArgument(nameof(action));

            var srcList = new List<T>(source);

            bool quit = false;
            for(int i = 0; i < srcList.Count && !quit; i++)
            {
                action(i, ref quit, srcList[i]);
            }

            return source;
        }
    }
}
