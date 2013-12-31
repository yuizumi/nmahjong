using System;
using System.Collections.Generic;
using NMahjong.Aux;

namespace NMahjong.Base
{
    using PungsMap = ImmutableDictionary<MeldState, ImmutableDictionary<Tile, Pung>>;

    public class Pung : Meld
    {
        private static readonly PungsMap PungsMap = Enums.Values<MeldState>()
            .ToImmutableDictionary(state => state, BuildPungs);

        private readonly ImmutableList<Tile> mTiles;
        private readonly MeldState mState;

        private Pung(Tile tile, MeldState state)
        {
            mTiles = ImmutableList.Of(tile, tile, tile);
            mState = state;
        }

        private static ImmutableDictionary<Tile, Pung> BuildPungs(MeldState state)
        {
            return Tile.AllTiles.ToImmutableDictionary(t => t, t => new Pung(t, state));
        }

        public override bool IsPung
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

        public static Pung Concealed(Tile tile)
        {
            return Of(tile, MeldState.Concealed);
        }

        public static Pung Open(Tile tile)
        {
            return Of(tile, MeldState.Open);
        }

        public static Pung Of(Tile tile, MeldState state)
        {
            CheckArg.NotNull(tile, "tile");
            CheckArg.Enum(state, "state");
            return PungsMap[state][tile];
        }

        public static Pung Find(IEnumerable<Tile> tiles, MeldState state)
        {
            CheckArg.NotContainsNull(tiles, "tiles");
            CheckArg.Enum(state, "state");
            return MeldsHelper.Find(PungsMap[state], tiles);
        }

        public static ICollection<Pung> GetAllPungs(MeldState state)
        {
            return PungsMap[state].Values;
        }

        public override string ToString()
        {
            return String.Format("Pung({0}, {1})", mTiles[0], mState);
        }
    }
}
