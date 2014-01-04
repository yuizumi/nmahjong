// -*- coding: utf-8 -*-

namespace NMahjong.Base
{
    /**
      <summary>
        Identifies the type of a tile.
      </summary>
    */
    public enum TileType
    {
        /// <summary>A number tile from 2 to 8 (中張牌).</summary>
        Simple,
        /// <summary>A number tile of either 1 or 9 (老頭牌).</summary>
        Terminal,
        /// <summary>Either a wind or dragon tile (字牌).</summary>
        Honor,
    }
}
