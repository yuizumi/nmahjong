using System.Collections.Generic;

namespace NMahjong.Aux
{
    public class ImmutableDictionary<TKey, TValue> : ReadOnlyDictionaryView<TKey, TValue>
    {
        internal static readonly ImmutableDictionary<TKey, TValue> Empty =
            new ImmutableDictionary<TKey, TValue>(new Dictionary<TKey, TValue>());

        private ImmutableDictionary(Dictionary<TKey, TValue> items)
            : base(items)
        {
        }

        internal static ImmutableDictionary<TKey, TValue> Create(
            IDictionary<TKey, TValue> items)
        {
            var dict = items as ImmutableDictionary<TKey, TValue>;
            if (dict != null) {
                return dict;
            }
            if (items.Count == 0) {
                return Empty;
            }
            return new ImmutableDictionary<TKey, TValue>(new Dictionary<TKey, TValue>(items));
        }
    }

    public static class ImmutableDictionary
    {
        public static ImmutableDictionary<TKey, TValue> Of<TKey, TValue>()
        {
            return ImmutableDictionary<TKey, TValue>.Empty;
        }

        public static ImmutableDictionary<TKey, TValue> Of<TKey, TValue>(
            IDictionary<TKey, TValue> items)
        {
            return ImmutableDictionary<TKey, TValue>.Create(items);
        }
    }
}
