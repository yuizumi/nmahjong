using System.Collections.Generic;

namespace NMahjong.Aux
{
    public static class Collections
    {
        public static void AddRange<T>(this ICollection<T> collection,
                                       IEnumerable<T> items)
        {
            items.ForEach(collection.Add);
        }

        public static ReadOnlyCollectionView<T> AsReadOnlyView<T>(
            this ICollection<T> collection)
        {
            return new ReadOnlyCollectionView<T>(collection);
        }

        public static ReadOnlyDictionaryView<TKey, TValue> AsReadOnlyView<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary)
        {
            return new ReadOnlyDictionaryView<TKey, TValue>(dictionary);
        }

        public static ReadOnlyListView<T> AsReadOnlyView<T>(this IList<T> list)
        {
            return new ReadOnlyListView<T>(list);
        }

        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
                                               TKey key)
        {
            return Get(dictionary, key, default(TValue));
        }

        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
                                               TKey key, TValue defaultValue)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }
    }
}
