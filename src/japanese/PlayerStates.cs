using System.Linq;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Japanese
{
    public static class PlayerStates
    {
        public static bool HasRiichiDeclared(this IPlayerState player)
        {
            return player.Discards.Any(a => a.Has(TA.Riichi));
        }
    }
}
