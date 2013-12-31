using System;
using System.Collections.Generic;

namespace NMahjong.Aux
{
    public class ReadOnlyListView<T> : ReadOnlyCollectionView<T>, IList<T>
    {
        private readonly IList<T> mItems;

        public ReadOnlyListView(IList<T> list)
            : base(list)
        {
            mItems = list;
        }

        protected new IList<T> Items
        {
            get { return mItems; }
        }

        public T this[int index]
        {
            get { return mItems[index]; }
        }

        public int IndexOf(T item)
        {
            return mItems.IndexOf(item);
        }

        #region Explicit Interface Implementations
        T IList<T>.this[int index]
        {
            get {
                return mItems[index];
            }
            set {
                throw new NotSupportedException("Collection is read-only.");
            }
        }

        void IList<T>.Insert(int index, T item)
        {
            throw new NotSupportedException("Collection is read-only.");
        }

        void IList<T>.RemoveAt(int index)
        {
            throw new NotSupportedException("Collection is read-only.");
        }
        #endregion  // Explicit Interface Implementations
    }
}
