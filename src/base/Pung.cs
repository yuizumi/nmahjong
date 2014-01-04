using System;
using System.Collections.Generic;
using NMahjong.Aux;

namespace NMahjong.Base
{
    using PungsMap = ImmutableDictionary<MeldState, ImmutableDictionary<Tile, Pung>>;

    /**
      <summary>
        Represents a meld with three identital tiles.
      </summary>
      <remarks>
        <para>
          This class creates only one instance for given tile set and open state. Therefore,
          the equality of two <see cref="Pung"/> instances can be determined just by testing
          reference equality.
        </para>
      </remarks>
      <example>
        <code language="cs">
          public void Main()
          {
              Pung pung0 = Pung.Of(Tile.T5, MeldState.Open);
              Pung pung1 = Pung.Of(Tile.T5, MeldState.Open);
              Console.WriteLine(pung0 == pung1);  // True
              Pung pung2 = Pung.Find(new [] {Tile.T5, Tile.T5, Tile.T5}, MeldState.Open);
              Console.WriteLine(pung0 == pung2);  // True
          }
        </code>
      </example>
    */
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

        /// <inheritdoc/>
        public override bool IsPung
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
            Gets a pung of the specified tile.
          </summary>
          <param name="tile">
            The tile of the pung.
          </param>
          <param name="state">
            <see cref="MeldState"/> representing whether the pung is concealed or open.
          </param>
          <returns>
            An object representing a pung of <paramref name="tile"/>.
          </returns>
        */
        public static Pung Of(Tile tile, MeldState state)
        {
            CheckArg.NotNull(tile, "tile");
            CheckArg.Enum(state, "state");
            return PungsMap[state][tile];
        }

        /**
          <summary>
            Returns <see cref="Pung"/> containing the specified tiles.
          </summary>
          <param name="tiles">
            The tiles the pung consists of.
          </param>
          <param name="state">
            <see cref="MeldState"/> representing whether the pung is concealed or open.
          </param>
          <returns>
            <see cref="Pung"/> containing <paramref name="tiles"/>, if they form a pung;
            otherwise, <see langword="null"/>.
          </returns>
        */
        public static Pung Find(IEnumerable<Tile> tiles, MeldState state)
        {
            CheckArg.NotContainsNull(tiles, "tiles");
            CheckArg.Enum(state, "state");
            return MeldsHelper.Find(PungsMap[state], tiles);
        }

        /**
          <summary>
            Gets all <see cref="Pung"/> objects with the specified <see cref="MeldState"/>.
          </summary>
          <param name="state">
            <see cref="MeldState"/> representing whether to retrieve concealed or open pungs.
          </param>
          <returns>
            A collection containing all <see cref="Pung"/> objects with <paramref name="state"/>.
          </returns>
        */
        public static ICollection<Pung> GetAllPungs(MeldState state)
        {
            return PungsMap[state].Values;
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
            return String.Format("Pung({0}, {1})", mTiles[0], mState);
        }
    }
}
