using System;
using System.Collections.Generic;
using NMahjong.Aux;

namespace NMahjong.Base
{
    using PairsMap = ImmutableDictionary<Tile, Pair>;

    public class Pair : Meld
    {
        private static readonly PairsMap PairsMap = BuildPairs();

        private readonly ImmutableList<Tile> mTiles;

        private Pair(Tile tile)
        {
            mTiles = ImmutableList.Of(tile, tile);
        }

        private static ImmutableDictionary<Tile, Pair> BuildPairs()
        {
            return Tile.AllTiles.ToImmutableDictionary(t => t, t => new Pair(t));
        }

        public override bool IsPair
        {
            get { return true; }
        }

        public override MeldState State
        {
            get { return MeldState.Concealed; }
        }

        public override ImmutableList<Tile> Tiles
        {
            get { return mTiles; }
        }

        public static Pair Of(Tile tile)
        {
            CheckArg.NotNull(tile, "tile");
            return PairsMap[tile];
        }

        public static Pair Find(IEnumerable<Tile> tiles)
        {
            CheckArg.NotContainsNull(tiles, "tiles");
            return MeldsHelper.Find(PairsMap, tiles);
        }

        public static ICollection<Pair> GetAllPairs()
        {
            return PairsMap.Values;
        }

        public override string ToString()
        {
            return String.Format("Pair({0})", mTiles[0]);
        }
    }
}
