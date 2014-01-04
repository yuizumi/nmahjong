using System;
using System.Collections.Generic;
using NMahjong.Aux;

namespace NMahjong.Base
{
    using KongsMap = ImmutableDictionary<MeldState, ImmutableDictionary<Tile, Kong>>;

    /**
      <summary>
        Represents a meld with four identital tiles.
      </summary>
      <remarks>
        <para>
          This class creates only one instance for given tile set and open state. Therefore,
          the equality of two <see cref="Kong"/> instances can be determined just by testing
          reference equality.
        </para>
      </remarks>
      <example>
        <code language="cs">
          public void Main()
          {
              Kong kong0 = Kong.Of(Tile.T5, MeldState.Open);
              Kong kong1 = Kong.Of(Tile.T5, MeldState.Open);
              Console.WriteLine(kong0 == kong1);  // True
              Kong kong2 = Kong.Find(new [] {Tile.T5, Tile.T5, Tile.T5, Tile.T5}, MeldState.Open);
              Console.WriteLine(kong0 == kong2);  // True
          }
        </code>
      </example>
    */
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

        /// <inheritdoc/>
        public override bool IsPung
        {
            get { return true; }
        }

        /// <inheritdoc/>
        public override bool IsKong
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
            Gets a kong of the specified tile.
          </summary>
          <param name="tile">
            The tile of the kong.
          </param>
          <param name="state">
            <see cref="MeldState"/> representing whether the kong is concealed or open.
          </param>
          <returns>
            An object representing a kong of <paramref name="tile"/>.
          </returns>
        */
        public static Kong Of(Tile tile, MeldState state)
        {
            CheckArg.NotNull(tile, "tile");
            CheckArg.Enum(state, "state");
            return KongsMap[state][tile];
        }

        /**
          <summary>
            Returns <see cref="Kong"/> containing the specified tiles.
          </summary>
          <param name="tiles">
            The tiles the kong consists of.
          </param>
          <param name="state">
            <see cref="MeldState"/> representing whether the kong is concealed or open.
          </param>
          <returns>
            <see cref="Kong"/> containing <paramref name="tiles"/>, if they form a kong;
            otherwise, <see langword="null"/>.
          </returns>
        */
        public static Kong Find(IEnumerable<Tile> tiles, MeldState state)
        {
            CheckArg.NotContainsNull(tiles, "tiles");
            CheckArg.Enum(state, "state");
            return MeldsHelper.Find(KongsMap[state], tiles);
        }

        /**
          <summary>
            Gets all <see cref="Kong"/> objects with the specified <see cref="MeldState"/>.
          </summary>
          <param name="state">
            <see cref="MeldState"/> representing whether to retrieve concealed or open kongs.
          </param>
          <returns>
            A collection containing all <see cref="Kong"/> objects with <paramref name="state"/>.
          </returns>
        */
        public static ICollection<Kong> GetAllKongs(MeldState state)
        {
            return KongsMap[state].Values;
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
            return String.Format("Kong({0}, {1})", mTiles[0], mState);
        }
    }
}
