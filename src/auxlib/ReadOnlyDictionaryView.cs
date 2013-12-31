using System;
using System.Collections.Generic;

namespace NMahjong.Aux
{
    public class ReadOnlyDictionaryView<TKey, TValue>
        : ReadOnlyCollectionView<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> mItems;

        // Lazily initialized.
        private ReadOnlyCollectionView<TKey> mKeys;
        private ReadOnlyCollectionView<TValue> mValues;

        public ReadOnlyDictionaryView(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {
            mItems = dictionary;
        }

        protected new IDictionary<TKey, TValue> Items
        {
            get { return mItems; }
        }

        public TValue this[TKey key]
        {
            get { return mItems[key]; }
        }

        public ReadOnlyCollectionView<TKey> Keys
        {
            get {
                return mKeys ?? (mKeys = new ReadOnlyCollectionView<TKey>(mItems.Keys));
            }
        }

        public ReadOnlyCollectionView<TValue> Values
        {
            get {
                return mValues ?? (mValues = new ReadOnlyCollectionView<TValue>(mItems.Values));
            }
        }

        public bool ContainsKey(TKey key)
        {
            return mItems.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return mItems.TryGetValue(key, out value);
        }

        #region Explicit Interface Implementations
        TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get {
                return mItems[key];
            }
            set {
                throw new NotSupportedException("Collection is read-only.");
            }
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get { return Keys; }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get { return Values; }
        }

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            throw new NotSupportedException("Collection is read-only.");
        }

        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            throw new NotSupportedException("Collection is read-only.");
        }
        #endregion  // Explicit Interface Implementations
    }
}
