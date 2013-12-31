using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Japanese
{
    public static class TileAnnotationsExtension
    {
        private const TileAnnotations ConflictWithExtending =
            TA.Claimed | TA.Riichi;

        private const TileAnnotations AllAnnotations =
            TA.Red | TA.Drawn | TA.Claimed | TA.Extending | TA.Riichi;

        public static bool IsValid(this TileAnnotations annotations)
        {
            if ((annotations & ~AllAnnotations) != 0) {
                return false;
            }

            if ((annotations & TA.Extending) == 0) {
                return true;
            } else {
                return (annotations & ConflictWithExtending) == 0;
            }
        }
    }
}
