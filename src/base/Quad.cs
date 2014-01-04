using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;

using IEnumerable = System.Collections.IEnumerable;
using IEnumerator = System.Collections.IEnumerator;

namespace NMahjong.Base
{
    /**
      <summary>
        Contains player-wise information.
      </summary>
      <typeparam name="T">
        The type of the items containing information.
      </typeparam>
      <remarks>
        Use the <see cref="Quad"/> class (with no type parameter) to create an instance of
        <see cref="Quad{T}"/>.
      </remarks>
      <seealso cref="Quad"/>
    */
    public sealed class Quad<T> : IList<T>
    {
        private readonly T[] mItems;

        private Quad(T[] items)
        {
            mItems = items;
        }

        internal static Quad<T> Create(T x0, T x1, T x2, T x3)
        {
            return new Quad<T>(new [] {x0, x1, x2, x3});
        }

        internal static Quad<T> Create(IEnumerable<T> items)
        {
            return new Quad<T>(items.ToArray());
        }

        /**
          <summary>
            Gets the item for the specified player number.
          </summary>
          <param name="index">
            The player number of the item to get.
          </param>
          <value>
            The item at <paramref name="index"/>.
          </value>
          <exception cref="ArgumentOutOfRangeException">
            <paramref name="index"/> is not in the range between 0 and 3.
          </exception>
        */
        public T this[int index]
        {
            get {
                CheckArg.Range(index, "index", 0, 3);
                return mItems[index];
            }
        }

        /**
          <summary>
            Gets the item for the specified player.
          </summary>
          <param name="player">
            The player of the item to get.
          </param>
          <value>
            The item associated to <paramref name="player"/>.
          </value>
        */
        public T this[PlayerId player]
        {
            get {
                return mItems[player.Id];
            }
        }

        /**
          <summary>
            Returns an enumerator that iterates over this <see cref="Quad{T}"/>.
          </summary>
          <returns>
            An enumerator to iterate over this <see cref="Quad{T}"/>.
          </returns>
        */
        public IEnumerator<T> GetEnumerator()
        {
            return (mItems as IEnumerable<T>).GetEnumerator();
        }

        #region Explicit Interface Implementaions
        /**
          <summary>
            Gets or sets the item at the specified index.
          </summary>
          <param name="index">
            The zero-based index of the item to get or set.
          </param>
          <value>
            The item at <paramref name="index"/>.
          </value>
          <exception cref="ArgumentOutOfRangeException">
            <paramref name="index"/> is negative.
            <para>-or-</para>
            <paramref name="index"/> is equal to or greater than
            <see cref="ICollection{T}.Count"/>.
          </exception>
          <exception cref="NotSupportedException">
            The property is set.
          </exception>
        */
        T IList<T>.this[int index]
        {
            get {
                return this[index];
            }
            set {
                throw new NotSupportedException("Collection is read-only.");
            }
        }

        /**
          <summary>
            Gets the number of items contained in this collection.
          </summary>
          <value>
            The number of items in this collection (4).
          </value>
        */
        int ICollection<T>.Count
        {
            get { return 4; }
        }

        /**
          <summary>
            Indicates whether this collection is read-only.
          </summary>
          <value>
            <see langword="true"/>.
          </value>
        */
        bool ICollection<T>.IsReadOnly
        {
            get { return true; }
        }

        /**
          <summary>
            Adds an item to this collection.
            This operation is not supported.
          </summary>
          <param name="item">
            The item to add to this collection (ignored).
          </param>
          <exception cref="NotSupportedException">
            Always thrown.
          </exception>
        */
        void ICollection<T>.Add(T item)
        {
            throw new NotSupportedException("Collection is read-only.");
        }

        /**
          <summary>
            Removes all items from this collection.
            This operation is not supported.
          </summary>
          <exception cref="NotSupportedException">
            Always thrown.
          </exception>
        */
        void ICollection<T>.Clear()
        {
            throw new NotSupportedException("Collection is read-only.");
        }

        /**
          <summary>
            Determines whether this collection contains a specific value.
          </summary>
          <param name="item">
            The object to locate in this collection.
          </param>
          <returns>
            <see langword="true"/> if <paramref name="item"/> is found in this collection;
            otherwise, <see langword="false"/>.
          </returns>
        */
        bool ICollection<T>.Contains(T item)
        {
            return Array.IndexOf(mItems, item) >= 0;
        }

        /**
          <summary>
            Copies the items of this collection to an array.
          </summary>
          <param name="array">
            The destination array.
          </param>
          <param name="index">
            The zero-based index in <paramref name="array"/> from which the items are copied.
          </param>
          <exception cref="ArgumentNullException">
            <paramref name="array"/> is <see langword="null"/>.
          </exception>
          <exception cref="ArgumentOutOfRangeException">
            <paramref name="index"/> is negative.
          </exception>
          <exception cref="ArgumentException">
            <paramref name="array"/> is not one-dimensional.
            <para>-or-</para>
            <paramref name="array"/> does not have space enough to store the copied items from
            <paramref name="index"/>.
          </exception>
        */
        void ICollection<T>.CopyTo(T[] array, int index)
        {
            mItems.CopyTo(array, index);
        }

        /**
          <summary>
            Returns an enumerator that iterates over this <see cref="Quad{T}"/>.
          </summary>
          <returns>
            An enumerator to iterate over this <see cref="Quad{T}"/>.
          </returns>
        */
        IEnumerator IEnumerable.GetEnumerator()
        {
            return mItems.GetEnumerator();
        }

        /**
          <summary>
            Searches for the specified object and returns the zero-based index of the first
            occurrence in this collection.
          </summary>
          <param name="item">
            The object to locate in this collection.
          </param>
          <returns>
            The zero-based index of the first occurrence of <paramref name="item"/> in this
            collection, if it is found; otherwise, -1.
          </returns>
        */
        int IList<T>.IndexOf(T item)
        {
            return Array.IndexOf(mItems, item);
        }

        /**
          <summary>
            Inserts an item to this collection at the specified index.
            This operation is not supported.
          </summary>
          <param name="index">
            The zero-based index at which <paramref name="item"/> to be inserted (ignored).
          </param>
          <param name="item">
            The item to insert (ignored).
          </param>
          <exception cref="NotSupportedException">
            Always thrown.
          </exception>
        */
        void IList<T>.Insert(int index, T item)
        {
            throw new NotSupportedException("Collection is read-only.");
        }

        /**
          <summary>
            Removes the first occurrence of the specified object from this collection.
            This operation is not supported.
          </summary>
          <param name="item">
            The object to be removed from this collection (ignored).
          </param>
          <returns>
            This method never returns a value.
          </returns>
          <exception cref="NotSupportedException">
            Always thrown.
          </exception>
        */
        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException("Collection is read-only.");
        }

        /**
          <summary>
            Removes the item at the specified index.
            This operation is not supported.
          </summary>
          <param name="index">
            The zero-based index of the item to remove (ignored).
          </param>
          <exception cref="NotSupportedException">
            Always thrown.
          </exception>
        */
        void IList<T>.RemoveAt(int index)
        {
            throw new NotSupportedException("Collection is read-only.");
        }
        #endregion  // Explicit Interface Implementaions
    }

    /**
      <summary>
        Provides methods to construct <see cref="Quad{T}"/> instances.
      </summary>
    */
    public static class Quad
    {
        /**
          <summary>
            Creates a new instance of <see cref="Quad{T}"/> with the specified items.
          </summary>
          <param name="item0">
            The item to associate to Player #0.
          </param>
          <param name="item1">
            The item to associate to Player #1.
          </param>
          <param name="item2">
            The item to associate to Player #2.
          </param>
          <param name="item3">
            The item to associate to Player #3.
          </param>
          <returns>
            A <see cref="Quad{T}"/> containing the specified items.
          </returns>
       */
        public static Quad<T> Of<T>(T item0, T item1, T item2, T item3)
        {
            return Quad<T>.Create(item0, item1, item2, item3);
        }

        /**
          <summary>
            Creates a new instance of <see cref="Quad{T}"/> with the specified items.
          </summary>
          <param name="items">
            An <see cref="IEnumerable{T}"/> containing exactly four items.
          </param>
          <returns>
            A <see cref="Quad{T}"/> containing the specified items.
          </returns>
          <exception cref="ArgumentNullException">
            <paramref name="items"/> is <see langword="null"/>.
          </exception>
          <exception cref="ArgumentException">
            <paramref name="items"/> contains more than or less than four items.
          </exception>
        */
        public static Quad<T> Of<T>(IEnumerable<T> items)
        {
            CheckArg.NotNull(items, "items");
            CheckArg.Expect(items.Count() == 4,
                            "items", "Sequence must contain exactly four items.");
            return Quad<T>.Create(items);
        }

        /**
          <summary>
            Creates a new instance of <see cref="Quad{T}"/> with the specified items
            and offset.
          </summary>
          <param name="items">
            A <see cref="IList{T}"/> containing exactly four items.
          </param>
          <param name="offset">
            A zero-based index of the item in <paramref name="items"/> to associate to
            Player #0.
          </param>
          <returns>
            A <see cref="Quad{T}"/> containing the specified items, in which
            <paramref name="items"/>[<paramref name="offset"/>] is associated to Player #0,
            the next one (with loop-around) is associated to Player #1, and so forth.
          </returns>
          <remarks>
            <para>
              This method is equivalent to the following code:
              <code language="cs">
                Quad.Of(items[offset], items[(offset + 1) % 4],
                        items[(offset + 2) % 4], items[(offset + 3) % 4])
              </code>
            </para>
          </remarks>
          <exception cref="ArgumentNullException">
            <paramref name="items"/> is <see langword="null"/>.
          </exception>
          <exception cref="ArgumentOutOfRangeException">
            <paramref name="offset"/> is not in the range between 0 and 3.
          </exception>
          <exception cref="ArgumentException">
            <paramref name="items"/> contains more than or less than four items.
          </exception>
        */
        public static Quad<T> Of<T>(IList<T> items, int offset)
        {
            CheckArg.NotNull(items, "items");
            CheckArg.Expect(items.Count == 4,
                            "items", "Sequence must contain exactly four items.");
            CheckArg.Range(offset, "offset", 0, 3);
            return Quad.Of(index => items[(index + offset) % 4]);
        }

        /**
          <summary>
            Creates a new instance of <see cref="Quad{T}"/> with the specified item
            generator function.
          </summary>
          <param name="generator">
            A function that takes a player number and returns the item for that player.
          </param>
          <returns>
            A <see cref="Quad{T}"/> containing the items generated by <paramref name="generator"/>.
          </returns>
          <exception cref="ArgumentNullException">
            <paramref name="generator"/> is <see langword="null"/>.
          </exception>
        */
        public static Quad<T> Of<T>(Func<Int32, T> generator)
        {
            CheckArg.NotNull(generator, "generator");
            return Quad<T>.Create(Enumerable.Range(0, 4).Select(generator));
        }
    }
}
