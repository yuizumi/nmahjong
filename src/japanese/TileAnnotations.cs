using System;

namespace NMahjong.Japanese
{
    [Flags]
    public enum TileAnnotations
    {
        None = 0,
        Red = 1,
        Drawn = 2,
        Claimed = 4,
        Extending = 8,
        Riichi = 16,
    }
}
