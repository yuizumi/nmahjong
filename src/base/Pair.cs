using System;
using System.Collections.Generic;
using NMahjong.Aux;

namespace NMahjong.Base
{
    using PairsMap = ImmutableDictionary<Tile, Pair>;

    /**
      <summary>
        Represents a pair of two identical tiles, also known as "eyes."
      </summary>
      <remarks>
        <para>
          This class creates only one instance for given tile set. Therefore, the equality of
          two <see cref="Pair"/> instances can be determined just by testing reference equality.
        </para>
      </remarks>
      <example>
        <code language="cs">
          public void Main()
          {
              Pair pair0 = Pair.Of(Tile.T5);
              Pair pair1 = Pair.Of(Tile.T5);
              Console.WriteLine(pair0 == pair1);  // True
              Pair pair2 = Pair.Find(new [] {Tile.T5, Tile.T5});
              Console.WriteLine(pair0 == pair2);  // True
          }
        </code>
      </example>
    */
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

        /// <inheritdoc/>
        public override bool IsPair
        {
            get { return true; }
        }

        /// <inheritdoc/>
        public override MeldState State
        {
            get { return MeldState.Concealed; }
        }

        /// <inheritdoc/>
        public override ImmutableList<Tile> Tiles
        {
            get { return mTiles; }
        }

        /**
          <summary>
            Gets a pair of the specified tile.
          </summary>
          <param name="tile">
            The tile of the pair.
          </param>
          <returns>
            An object representing a pair of <paramref name="tile"/>.
          </returns>
        */
        public static Pair Of(Tile tile)
        {
            CheckArg.NotNull(tile, "tile");
            return PairsMap[tile];
        }

        /**
          <summary>
            Returns <see cref="Pair"/> containing the specified tiles.
          </summary>
          <param name="tiles">
            The tiles the pair consists of.
          </param>
          <returns>
            <see cref="Pair"/> containing <paramref name="tiles"/>, if they form a pair;
            otherwise, <see langword="null"/>.
          </returns>
        */
        public static Pair Find(IEnumerable<Tile> tiles)
        {
            CheckArg.NotContainsNull(tiles, "tiles");
            return MeldsHelper.Find(PairsMap, tiles);
        }

        /**
          <summary>
            Gets all <see cref="Pair"/> objects with the specified <see cref="MeldState"/>.
          </summary>
          <returns>
            A collection containing all <see cref="Pair"/> objects.
          </returns>
        */
        public static ICollection<Pair> GetAllPairs()
        {
            return PairsMap.Values;
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
            return String.Format("Pair({0})", mTiles[0]);
        }
    }
}
