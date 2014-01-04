// -*- coding: utf-8 -*-

using System;
using System.Collections.Generic;
using NMahjong.Aux;

namespace NMahjong.Base
{
    /**
      <summary>
        Provides the most basic representation of a number or honor tile.
      </summary>
      <remarks>
        <para>
          Instances of the <see cref="Tile"/> class can be obtained only through its public
          fields. The class does not provide any public constructors or methods for the
          client code to instantiate a new object. This ensures each tile to be represented
          by a single unique object and allows the equality of <see cref="Tile"/> instances
          to be determined just by testing reference equality.
        </para>
        <para>
          Each tile has a name of two letters given in the following manner:
        </para>
        <list type="table">
          <listheader>
            <term>Suit</term>
            <description>Naming rule</description>
          </listheader>
          <item>
            <term>Circles</term>
            <description>
              <b>T</b> (<i>tǒng-zi</i>, 筒子) followed by the rank. (e.g. <see cref="T1"/>)
            </description>
          </item>
          <item>
            <term>Bamboos</term>
            <description>
              <b>S</b> (<i>suǒ-zi</i>, 索子) followed by the rank. (e.g. <see cref="S1"/>)
            </description>
          </item>
          <item>
            <term>Characters</term>
            <description>
              <b>W</b> (<i>wàn-zi</i>, 萬子) followed by the rank. (e.g. <see cref="W1"/>)
            </description>
          </item>
          <item>
            <term>Winds</term>
            <description>
              <b>F</b> (<i>fēng-pái</i>, 風牌) followed by the direction
              (<b>E</b>, <b>S</b>, <b>W</b>, <b>N</b>). (e.g. <see cref="FE"/>)
            </description>
          </item>
          <item>
            <term>Dragons</term>
            <description>
              <b>J</b> (<i>jiàn-pái</i>, 箭牌) followed by the Western tile index
              (<b>P</b>, <b>F</b>, <b>C</b>). (e.g. <see cref="JP"/>)
            </description>
          </item>
         </list>
         <note>
           <b>E</b>, <b>S</b>, <b>W</b>, <b>N</b>, <b>P</b>, <b>F</b>, and <b>C</b> can be found
           on Western mahjong tiles as indices (typically printed at the top-left corner).
           Those for dragons match to the Chinese tile names in Wade-Giles romanization:
           <b>P</b> (<i>pai2</i>, 白, white dragon), <b>F</b> (<i>fa1</i>, 發, green dragon), and
           <b>C</b> (<i>chung1</i>, 中, red dragon).
         </note>
         <para>
           <see cref="Tile"/> represents only normal tiles. Special tiles, such as flowers and
           jokers, are currently not supported by NMahjong.
         </para>
       </remarks>
    */
    public sealed class Tile
    {
        private readonly string mName;
        private readonly Suit mSuit;
        private readonly int mRank;

        /// <summary>Contains all instances of <see cref="Tile"/>.</summary>
        public static readonly ImmutableList<Tile> AllTiles;

        private static readonly ImmutableDictionary<Suit, ImmutableList<Tile>>
            NumberTiles;

        private Tile(string name, Suit suit, int rank)
        {
            mName = name;
            mSuit = suit;
            mRank = rank;
        }

        static Tile()
        {
            AllTiles = ImmutableList.Of(T1, T2, T3, T4, T5, T6, T7, T8, T9,
                                        S1, S2, S3, S4, S5, S6, S7, S8, S9,
                                        W1, W2, W3, W4, W5, W6, W7, W8, W9,
                                        FE, FS, FW, FN, JP, JF, JC);
            var numberTiles = new Dictionary<Suit, ImmutableList<Tile>>() {
                { Suit.Dots, ImmutableList.Of(T1, T2, T3, T4, T5, T6, T7, T8, T9) },
                { Suit.Bams, ImmutableList.Of(S1, S2, S3, S4, S5, S6, S7, S8, S9) },
                { Suit.Craks, ImmutableList.Of(W1, W2, W3, W4, W5, W6, W7, W8, W9) },
            };
            NumberTiles = ImmutableDictionary.Of(numberTiles);
        }

        /**
          <summary>
            Gets the name of this tile.
          </summary>
          <value>
            The name of this tile.
          </value>
        */
        public string Name
        {
            get { return mName; }
        }

        /**
          <summary>
            Gets the suit of this tile.
          </summary>
          <value>
            The suit of this tile.
          </value>
        */
        public Suit Suit
        {
            get { return mSuit; }
        }

        /**
          <summary>
            Gets the rank of this tile, if it is numbered.
          </summary>
          <value>
            An integer from 1 to 9 that represents the rank of this tile.
          </value>
          <exception cref="InvalidOperationException">
            The object is not a number tile.
          </exception>
        */
        public int Rank
        {
            get {
                CheckState.Expect(
                    mRank >= 0, "The object is not a number tile.");
                return mRank;
            }
        }

        /**
          <summary>
            Gets whether this tile is simple, terminal, or honor.
          </summary>
          <value>
            <see cref="TileType"/> that indicates the type of this tile.
          </value>
        */
        public TileType TileType
        {
            get {
                if (mRank >= 2 && mRank <= 8) return TileType.Simple;
                if (mRank == 1 || mRank == 9) return TileType.Terminal;
                return TileType.Honor;
            }
        }

        /**
          <summary>
            Gets a number tile of the specified suit and rank.
          </summary>
          <param name="suit">
            The suit of the tile.
          </param>
          <param name="rank">
            The rank of the tile.
          </param>
          <returns>
            <see cref="Tile"/> that has <paramref name="suit"/> and <paramref name="rank"/>.
          </returns>
          <exception cref="ArgumentException">
            <paramref name="suit"/> is not a suit of number tiles.
          </exception>
          <exception cref="ArgumentOutOfRangeException">
            <paramref name="rank"/> is not in the range from 1 to 9.
          </exception>
        */
        public static Tile Of(Suit suit, int rank)
        {
            CheckArg.Expect(NumberTiles.ContainsKey(suit),
                            "suit", "Argument is not a numbered suit.");
            CheckArg.Range(rank, "rank", 1, 9);
            return NumberTiles[suit][rank - 1];
        }

        /**
          <summary>
            Returns a string that represents this tile.
          </summary>
          <returns>
            The name of this tile.
          </returns>
        */
        public override string ToString()
        {
            return mName;
        }

        #region Tile Definitions
        /// <summary>1 of Circles (一筒).</summary>
        public static readonly Tile T1 = new Tile("T1", Suit.Dots, 1);
        /// <summary>2 of Circles (二筒).</summary>
        public static readonly Tile T2 = new Tile("T2", Suit.Dots, 2);
        /// <summary>3 of Circles (三筒).</summary>
        public static readonly Tile T3 = new Tile("T3", Suit.Dots, 3);
        /// <summary>4 of Circles (四筒).</summary>
        public static readonly Tile T4 = new Tile("T4", Suit.Dots, 4);
        /// <summary>5 of Circles (五筒).</summary>
        public static readonly Tile T5 = new Tile("T5", Suit.Dots, 5);
        /// <summary>6 of Circles (六筒).</summary>
        public static readonly Tile T6 = new Tile("T6", Suit.Dots, 6);
        /// <summary>7 of Circles (七筒).</summary>
        public static readonly Tile T7 = new Tile("T7", Suit.Dots, 7);
        /// <summary>8 of Circles (八筒).</summary>
        public static readonly Tile T8 = new Tile("T8", Suit.Dots, 8);
        /// <summary>9 of Circles (九筒).</summary>
        public static readonly Tile T9 = new Tile("T9", Suit.Dots, 9);
        /// <summary>1 of Bamboo (一索).</summary>
        public static readonly Tile S1 = new Tile("S1", Suit.Bams, 1);
        /// <summary>2 of Bamboo (二索).</summary>
        public static readonly Tile S2 = new Tile("S2", Suit.Bams, 2);
        /// <summary>3 of Bamboo (三索).</summary>
        public static readonly Tile S3 = new Tile("S3", Suit.Bams, 3);
        /// <summary>4 of Bamboo (四索).</summary>
        public static readonly Tile S4 = new Tile("S4", Suit.Bams, 4);
        /// <summary>5 of Bamboo (五索).</summary>
        public static readonly Tile S5 = new Tile("S5", Suit.Bams, 5);
        /// <summary>6 of Bamboo (六索).</summary>
        public static readonly Tile S6 = new Tile("S6", Suit.Bams, 6);
        /// <summary>7 of Bamboo (七索).</summary>
        public static readonly Tile S7 = new Tile("S7", Suit.Bams, 7);
        /// <summary>8 of Bamboo (八索).</summary>
        public static readonly Tile S8 = new Tile("S8", Suit.Bams, 8);
        /// <summary>9 of Bamboo (九索).</summary>
        public static readonly Tile S9 = new Tile("S9", Suit.Bams, 9);
        /// <summary>1 of Characters (一萬).</summary>
        public static readonly Tile W1 = new Tile("W1", Suit.Craks, 1);
        /// <summary>2 of Characters (二萬).</summary>
        public static readonly Tile W2 = new Tile("W2", Suit.Craks, 2);
        /// <summary>3 of Characters (三萬).</summary>
        public static readonly Tile W3 = new Tile("W3", Suit.Craks, 3);
        /// <summary>4 of Characters (四萬).</summary>
        public static readonly Tile W4 = new Tile("W4", Suit.Craks, 4);
        /// <summary>5 of Characters (五萬).</summary>
        public static readonly Tile W5 = new Tile("W5", Suit.Craks, 5);
        /// <summary>6 of Characters (六萬).</summary>
        public static readonly Tile W6 = new Tile("W6", Suit.Craks, 6);
        /// <summary>7 of Characters (七萬).</summary>
        public static readonly Tile W7 = new Tile("W7", Suit.Craks, 7);
        /// <summary>8 of Characters (八萬).</summary>
        public static readonly Tile W8 = new Tile("W8", Suit.Craks, 8);
        /// <summary>9 of Characters (九萬).</summary>
        public static readonly Tile W9 = new Tile("W9", Suit.Craks, 9);
        /// <summary>East (東).</summary>
        public static readonly Tile FE = new Tile("FE", Suit.Winds, -1);
        /// <summary>South (南).</summary>
        public static readonly Tile FS = new Tile("FS", Suit.Winds, -2);
        /// <summary>West (西).</summary>
        public static readonly Tile FW = new Tile("FW", Suit.Winds, -3);
        /// <summary>North (北).</summary>
        public static readonly Tile FN = new Tile("FN", Suit.Winds, -4);
        /// <summary>White Dragon (白).</summary>
        public static readonly Tile JP = new Tile("JP", Suit.Dragons, -1);
        /// <summary>Green Dragon (發).</summary>
        public static readonly Tile JF = new Tile("JF", Suit.Dragons, -2);
        /// <summary>Red Dragon (中).</summary>
        public static readonly Tile JC = new Tile("JC", Suit.Dragons, -3);
        #endregion
    }
}
