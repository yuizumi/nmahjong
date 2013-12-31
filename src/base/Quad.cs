using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;

using IEnumerable = System.Collections.IEnumerable;
using IEnumerator = System.Collections.IEnumerator;

namespace NMahjong.Base
{
    public sealed class Quad<T> : IList<T>
    {
        private readonly T[] mItems;

        private Quad(T[] items)
        {
            mItems = items;
        }

        public T this[int index]
        {
            get {
                CheckArg.Range(index, "index", 0, 3);
                return mItems[index];
            }
        }

        public T this[PlayerId player]
        {
            get {
                return mItems[player.Id];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (mItems as IEnumerable<T>).GetEnumerator();
        }

        public static Quad<T> Create(T x1, T x2, T x3, T x4)
        {
            return new Quad<T>(new [] { x1, x2, x3, x4 });
        }

        public static Quad<T> Create(IEnumerable<T> items)
        {
            CheckArg.NotNull(items, "items");
            CheckArg.Expect(items.Count() == 4,
                            "items", "Sequence must contain exactly four items.");
            return new Quad<T>(items.ToArray());
        }

        #region Explicit Interface Implementaions
        T IList<T>.this[int index]
        {
            get {
                return this[index];
            }
            set {
                throw new NotSupportedException("Collection is read-only.");
            }
        }

        int ICollection<T>.Count
        {
            get { return 4; }
        }

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

        bool ICollection<T>.Contains(T item)
        {
            return Array.IndexOf(mItems, item) >= 0;
        }

        void ICollection<T>.CopyTo(T[] array, int index)
        {
            mItems.CopyTo(array, index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return mItems.GetEnumerator();
        }

        int IList<T>.IndexOf(T item)
        {
            return Array.IndexOf(mItems, item);
        }

        void IList<T>.Insert(int index, T item)
        {
            throw new NotSupportedException("Collection is read-only.");
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException("Collection is read-only.");
        }

        void IList<T>.RemoveAt(int index)
        {
            throw new NotSupportedException("Collection is read-only.");
        }
        #endregion  // Explicit Interface Implementaions
    }

    public static class Quad
    {
        public static Quad<T> Of<T>(T x1, T x2, T x3, T x4)
        {
            return Quad<T>.Create(x1, x2, x3, x4);
        }

        public static Quad<T> Of<T>(IEnumerable<T> items)
        {
            return Quad<T>.Create(items);
        }

        public static Quad<T> Of<T>(IEnumerable<T> items, int offset)
        {
            CheckArg.NotNull(items, "items");
            CheckArg.Expect(items.Count() == 4,
                            "items", "Sequence must contain exactly four items.");
            CheckArg.Range(offset, "offset", 0, 3);
            return Quad<T>.Create(items.Concat(items).Skip(offset).Take(4));
        }

        public static Quad<T> Of<T>(Func<Int32, T> generator)
        {
            CheckArg.NotNull(generator, "generator");
            return Quad<T>.Create(Enumerable.Range(0, 4).Select(generator));
        }
    }
}
