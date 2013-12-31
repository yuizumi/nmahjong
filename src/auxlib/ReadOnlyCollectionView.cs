using System;
using System.Collections.Generic;

using IEnumerable = System.Collections.IEnumerable;
using IEnumerator = System.Collections.IEnumerator;

namespace NMahjong.Aux
{
    public class ReadOnlyCollectionView<T> : ICollection<T>
    {
        private readonly ICollection<T> mItems;

        public ReadOnlyCollectionView(ICollection<T> collection)
        {
            mItems = collection;
        }

        protected ICollection<T> Items
        {
            get { return mItems; }
        }

        public int Count
        {
            get { return mItems.Count; }
        }

        public bool Contains(T item)
        {
            return mItems.Contains(item);
        }

        public void CopyTo(T[] array, int index)
        {
            mItems.CopyTo(array, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return mItems.GetEnumerator();
        }

        #region Explicit Interface Implementations
        bool ICollection<T>.IsReadOnly
        {
            get { return true; }
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotSupportedException("Collection is read-only.");
        }

        void ICollection<T>.Clear()
        {
            throw new NotSupportedException("Collection is read-only.");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return mItems.GetEnumerator();
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException("Collection is read-only.");
        }
        #endregion  // Explicit Interface Implementations
    }
}
