using System;
using System.Collections.Generic;
using NMahjong.Aux;

namespace NMahjong.Base
{
    using KongsMap = ImmutableDictionary<MeldState, ImmutableDictionary<Tile, Kong>>;

    public class Kong : Meld
    {
        private static readonly KongsMap KongsMap = Enums.Values<MeldState>()
            .ToImmutableDictionary(state => state, BuildKongs);

        private readonly ImmutableList<Tile> mTiles;
        private readonly MeldState mState;

        private Kong(Tile tile, MeldState state)
        {
            mTiles = ImmutableList.Of(tile, tile, tile, tile);
            mState = state;
        }

        private static ImmutableDictionary<Tile, Kong> BuildKongs(MeldState state)
        {
            return Tile.AllTiles.ToImmutableDictionary(t => t, t => new Kong(t, state));
        }

        public override bool IsPung
        {
            get { return true; }
        }

        public override bool IsKong
        {
            get { return true; }
        }

        public override MeldState State
        {
            get { return mState; }
        }

        public override ImmutableList<Tile> Tiles
        {
            get { return mTiles; }
        }

        public static Kong Of(Tile tile, MeldState state)
        {
            CheckArg.NotNull(tile, "tile");
            CheckArg.Enum(state, "state");
            return KongsMap[state][tile];
        }

        public static Kong Find(IEnumerable<Tile> tiles, MeldState state)
        {
            CheckArg.NotContainsNull(tiles, "tiles");
            CheckArg.Enum(state, "state");
            return MeldsHelper.Find(KongsMap[state], tiles);
        }

        public static ICollection<Kong> GetAllKongs(MeldState state)
        {
            return KongsMap[state].Values;
        }

        public override string ToString()
        {
            return String.Format("Kong({0}, {1})", mTiles[0], mState);
        }
    }
}
