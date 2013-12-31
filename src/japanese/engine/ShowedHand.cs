using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NMahjong.Aux;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Japanese.Engine
{
    internal class ShowedHand : ReadOnlyListView<AnnotatedTile>, IPlayerHandInternal
    {
        [VisibleForTesting]
        internal ShowedHand(List<AnnotatedTile> tiles)
            : base(tiles)
        {
        }

        internal static ShowedHand Create(IEnumerable<CanonicalTile> tiles)
        {
            CheckArg.NotContainsNull(tiles, "tiles");
            return new ShowedHand(tiles.Cast<AnnotatedTile>().ToList());
        }

        public void Discard(AnnotatedTile tile)
        {
            CheckArg.NotNull(tile, "tile");
            tile = tile.Without(TA.Riichi);
            CheckArg.Expect(Items.Remove(tile), "tile", "Hand does not have the specified tile.");
            ClearAnnotations();
        }

        public void Draw(CanonicalTile tile)
        {
            CheckArg.NotNull(tile, "tile");
            Items.Add(tile.With(TA.Drawn));
        }

        public void Exclude(IEnumerable<CanonicalTile> tiles)
        {
            CheckArg.NotContainsNull(tiles, "tiles");

            IEnumerable<Int32> range = Enumerable.Range(0, Items.Count);
            var indices = new HashSet<Int32>();
            foreach (CanonicalTile tile in tiles) {
                bool success = range.Any(i => Items[i].Without(~TA.Red) == tile && indices.Add(i));
                CheckArg.Expect(success, "tiles", "Hand does not have all specified tiles.");
            }
            ClearAnnotations();
            tiles.ForEach(tile => Items.Remove(tile));
        }

        private void ClearAnnotations()
        {
            for (int i = 0; i < Items.Count; i++) Items[i] = Items[i].Without(~TA.Red);
        }
    }
}
