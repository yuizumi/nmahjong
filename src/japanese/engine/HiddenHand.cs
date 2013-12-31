using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;

using IEnumerable = System.Collections.IEnumerable;
using IEnumerator = System.Collections.IEnumerator;

namespace NMahjong.Japanese.Engine
{
    internal class HiddenHand : IPlayerHandInternal
    {
        private int mCount;

        internal HiddenHand(int count)
        {
            CheckArg.Minimum(count, "count", 0);
            Count = count;
        }

        public AnnotatedTile this[int index]
        {
            get {
                CheckArg.Range(index, "index", 0, Count - 1);
                return null;
            }
        }

        public int Count
        {
            get {
                return mCount;
            }
            private set {
                CheckState.Expect(value >= 0, "Tile count cannot be negative.");
                mCount = value;
            }
        }

        public void Discard(AnnotatedTile tile)
        {
            --Count;
        }

        public void Draw(CanonicalTile tile)
        {
            CheckArg.Expect(tile == null, "tile", "Tile must be null.");
            ++Count;
        }

        public void Exclude(CanonicalTile tile)
        {
            --Count;
        }

        public void Exclude(IEnumerable<CanonicalTile> tiles)
        {
            Count -= tiles.Count();
        }

        public IEnumerator<AnnotatedTile> GetEnumerator()
        {
            return Enumerable.Repeat<AnnotatedTile>(null, Count).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
