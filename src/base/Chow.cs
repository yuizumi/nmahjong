using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;

namespace NMahjong.Base
{
    using ChowsMap = ImmutableDictionary<MeldState, ImmutableDictionary<Tile, Chow>>;

    public class Chow : Meld
    {
        private static readonly ChowsMap ChowsMap = Enums.Values<MeldState>()
            .ToImmutableDictionary(state => state, BuildChows);

        private readonly ImmutableList<Tile> mTiles;
        private readonly MeldState mState;

        private Chow(Tile tile, MeldState state)
        {
            Suit suit = tile.Suit;
            int  rank = tile.Rank;
            mTiles = ImmutableList.Of(Tile.Of(suit, rank), Tile.Of(suit, rank + 1),
                                      Tile.Of(suit, rank + 2));
            mState = state;
        }

        private static bool IsChowBase(Tile tile)
        {
            return tile.IsNumberTile() && tile.Rank <= 7;
        }

        private static ImmutableDictionary<Tile, Chow> BuildChows(MeldState state)
        {
            return Tile.AllTiles.Where(IsChowBase)
                .ToImmutableDictionary(t => t, t => new Chow(t, state));
        }

        public override bool IsChow
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

        public static Chow Concealed(Tile tile)
        {
            return Of(tile, MeldState.Concealed);
        }

        public static Chow Open(Tile tile)
        {
            return Of(tile, MeldState.Open);
        }

        public static Chow Of(Tile tile, MeldState state)
        {
            CheckArg.NotNull(tile, "tile");
            CheckArg.Enum(state, "state");
            CheckArg.Expect(IsChowBase(tile), "tile",
                            "Argument must be a number tile from 1 to 7.");
            return ChowsMap[state][tile];
        }

        public static Chow Find(IEnumerable<Tile> tiles, MeldState state)
        {
            CheckArg.NotContainsNull(tiles, "tiles");
            CheckArg.Enum(state, "state");
            if (!tiles.All(t => t.IsNumberTile())) {
                return null;
            }
            return MeldsHelper.Find(ChowsMap[state], tiles.OrderBy(t => t.Rank));
        }

        public static ICollection<Chow> GetAllChows(MeldState state)
        {
            return ChowsMap[state].Values;
        }

        public override string ToString()
        {
            return String.Format("Chow({0}, {1})", mTiles.BracedString(), mState);
        }
    }
}
