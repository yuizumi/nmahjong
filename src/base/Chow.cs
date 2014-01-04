using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;

namespace NMahjong.Base
{
    using ChowsMap = ImmutableDictionary<MeldState, ImmutableDictionary<Tile, Chow>>;

    /**
      <summary>
        Represents a meld with three number tiles in sequence (e.g. 5-6-7) of the same suit.
      </summary>
      <remarks>
        <para>
          This class creates only one instance for given tile set and open state. Therefore,
          the equality of two <see cref="Chow"/> instances can be determined just by testing
          reference equality.
        </para>
      </remarks>
      <example>
        <code language="cs">
          public void Main()
          {
              Chow chow0 = Chow.Of(Tile.T5, MeldState.Open);
              Chow chow1 = Chow.Of(Tile.T5, MeldState.Open);
              Console.WriteLine(chow0 == chow1);  // True
              Chow chow2 = Chow.Find(new [] {Tile.T5, Tile.T6, Tile.T7}, MeldState.Open);
              Console.WriteLine(chow0 == chow2);  // True
          }
        </code>
      </example>
    */
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

        private static ImmutableDictionary<Tile, Chow> BuildChows(MeldState state)
        {
            return Tile.AllTiles
                .Where(IsChowBase).ToImmutableDictionary(t => t, t => new Chow(t, state));
        }

        /// <inheritdoc/>
        public override bool IsChow
        {
            get { return true; }
        }

        /// <inheritdoc/>
        public override MeldState State
        {
            get { return mState; }
        }

        /// <inheritdoc/>
        public override ImmutableList<Tile> Tiles
        {
            get { return mTiles; }
        }

        /**
          <summary>
            Gets a chow starting with the specified tile.
          </summary>
          <param name="tile">
            The tile of the lowest rank in the chow.
          </param>
          <param name="state">
            <see cref="MeldState"/> representing whether the chow is concealed or open.
          </param>
          <returns>
            <see cref="Chow"/> representing a chow starting with <paramref name="tile"/>.
          </returns>
          <remarks>
            <para>
              For example, <c>Chow.Of(Tile.T5, MeldState.Concealed)</c> returns an object for
              a concealed chow consisting of <see cref="Tile.T5"/>, <see cref="Tile.T6"/>, and
              <see cref="Tile.T7"/>.
            </para>
          </remarks>
          <exception cref="ArgumentException">
            <paramref name="tile"/> is not a number tile ranked from 1 to 7.
          </exception>
        */
        public static Chow Of(Tile tile, MeldState state)
        {
            CheckArg.NotNull(tile, "tile");
            CheckArg.Enum(state, "state");
            CheckArg.Expect(IsChowBase(tile), "tile",
                            "Argument must be a number tile from 1 to 7.");
            return ChowsMap[state][tile];
        }

        /**
          <summary>
            Returns <see cref="Chow"/> containing the specified tiles.
          </summary>
          <param name="tiles">
            The tiles the chow consists of.
          </param>
          <param name="state">
            <see cref="MeldState"/> representing whether the chow is concealed or open.
          </param>
          <returns>
            <see cref="Chow"/> containing <paramref name="tiles"/>, if they form a chow;
            otherwise, <see langword="null"/>.
          </returns>
        */
        public static Chow Find(IEnumerable<Tile> tiles, MeldState state)
        {
            CheckArg.NotContainsNull(tiles, "tiles");
            CheckArg.Enum(state, "state");
            if (!tiles.All(t => t.IsNumberTile())) {
                return null;
            }
            return MeldsHelper.Find(ChowsMap[state], tiles.OrderBy(t => t.Rank));
        }

        /**
          <summary>
            Gets all <see cref="Chow"/> objects with the specified <see cref="MeldState"/>.
          </summary>
          <param name="state">
            <see cref="MeldState"/> representing whether to retrieve concealed or open chows.
          </param>
          <returns>
            A collection containing all <see cref="Chow"/> objects with <paramref name="state"/>.
          </returns>
        */
        public static ICollection<Chow> GetAllChows(MeldState state)
        {
            return ChowsMap[state].Values;
        }

        /**
          <summary>
            Returns a string that represents the current object.
          </summary>
          <returns>
            A string that represents the current object.
          </returns>
        */
        public override string ToString()
        {
            return String.Format("Chow({0}, {1})", mTiles.BracedString(), mState);
        }

        private static bool IsChowBase(Tile tile)
        {
            return tile.IsNumberTile() && tile.Rank <= 7;
        }
    }
}
