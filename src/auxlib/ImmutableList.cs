using System.Collections.Generic;
using System.Linq;

namespace NMahjong.Aux
{
    public class ImmutableList<T> : ReadOnlyListView<T>
    {
        internal static readonly ImmutableList<T> Empty =
            new ImmutableList<T>(new T[0]);

        private ImmutableList(T[] items)
            : base(items)
        {
        }

        internal static ImmutableList<T> Create(IEnumerable<T> items)
        {
            var list = items as ImmutableList<T>;
            if (list != null) {
                return list;
            }
            return items.IsEmpty() ? Empty : new ImmutableList<T>(items.ToArray());
        }
    }

    public static class ImmutableList
    {
        public static ImmutableList<T> Of<T>()
        {
            return ImmutableList<T>.Empty;
        }

        public static ImmutableList<T> Of<T>(IEnumerable<T> items)
        {
            return ImmutableList<T>.Create(items);
        }

        public static ImmutableList<T> Of<T>(params T[] items)
        {
            return ImmutableList<T>.Create(items);
        }
    }
}
