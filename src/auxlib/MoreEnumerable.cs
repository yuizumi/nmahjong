using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NMahjong.Aux
{
    public static class MoreEnumerable
    {
        public static IEnumerable<TSource> Append<TSource>(
            this IEnumerable<TSource> source, TSource element)
        {
            // TODO(yuizumi): Consider a more optimized object.
            return source.Concat(ImmutableList.Of(element));
        }

        public static string BracedString<TSource>(
            this IEnumerable<TSource> source)
        {
            StringBuilder sb = new StringBuilder("{");
            string separator = "";
            foreach (TSource element in source) {
                sb.Append(separator).Append(element.ToString());
                separator = ", ";
            }
            return sb.Append("}").ToString();
        }

        public static void ForEach<TSource>(this IEnumerable<TSource> source,
                                            Action<TSource> action)
        {
            foreach (TSource element in source) action(element);
        }

        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return !source.Any();
        }

        public static ImmutableDictionary<TKey, TSource>
        ToImmutableDictionary<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return ImmutableDictionary.Of(source.ToDictionary(keySelector));
        }

        public static ImmutableDictionary<TKey, TElement>
        ToImmutableDictionary<TSource, TKey, TElement>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector)
        {
            return ImmutableDictionary.Of(source.ToDictionary(keySelector, elementSelector));
        }

        public static ImmutableList<TSource> ToImmutableList<TSource>(
            this IEnumerable<TSource> source)
        {
            return ImmutableList.Of(source);
        }
    }
}
