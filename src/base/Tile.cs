// -*- coding: utf-8 -*-

using System;
using System.Collections.Generic;
using NMahjong.Aux;

namespace NMahjong.Base
{
    public sealed class Tile
    {
        private readonly string mName;
        private readonly Suit mSuit;
        private readonly int mRank;

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

            var numbers = new Dictionary<Suit, ImmutableList<Tile>>() {
                { Suit.Dots, ImmutableList.Of(T1, T2, T3, T4, T5, T6, T7, T8, T9) },
                { Suit.Bams, ImmutableList.Of(S1, S2, S3, S4, S5, S6, S7, S8, S9) },
                { Suit.Craks, ImmutableList.Of(W1, W2, W3, W4, W5, W6, W7, W8, W9) },
            };
            NumberTiles = ImmutableDictionary.Of(numbers);
        }

        public string Name
        {
            get { return mName; }
        }

        public Suit Suit
        {
            get { return mSuit; }
        }

        public int Rank
        {
            get {
                CheckState.Expect(
                    mRank >= 0, "The object is not a number tile.");
                return mRank;
            }
        }

        public TileType TileType
        {
            get {
                if (mRank >= 2 && mRank <= 8) return TileType.Simple;
                if (mRank == 1 || mRank == 9) return TileType.Terminal;
                return TileType.Honor;
            }
        }

        public static Tile Of(Suit suit, int rank)
        {
            CheckArg.Expect(NumberTiles.ContainsKey(suit),
                            "suit", "Argument is not a numbered suit.");
            CheckArg.Range(rank, "rank", 1, 9);
            return NumberTiles[suit][rank - 1];
        }

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
