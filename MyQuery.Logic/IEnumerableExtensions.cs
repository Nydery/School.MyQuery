using System;
using System.Collections.Generic;

namespace MyQuery.Logic
{
    public static class IEnumerableExtensions
    {
        public static int Count<T> (this IEnumerable<T> source)
        {
            var result = 0;

            foreach(var item in source)
            {
                result++;
            }

            return result;
        }

        public static IEnumerable<T> Filter<T> (this IEnumerable<T> source, Func<T, bool> predicate)
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

        public static double? Average<T>(this IEnumerable<T> source, Func<T, double> transform)
        {
            source.CheckArgument(nameof(source));
            transform.CheckArgument(nameof(transform));

            return source.Sum(transform) / source.Count();
        }

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

                for(int i = 0; i < srcList.Count - 2; i++)
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
