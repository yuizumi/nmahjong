using System;

namespace NMahjong.Japanese
{
    public static class CheckTile
    {
        public static void HasOnly(AnnotatedTile tile, string name,
                                   TileAnnotations annotations)
        {
            if (tile.Has(~annotations)) {
                string message = String.Format(
                    "Annotations must be a subset of <{0}>.", annotations);
                throw new ArgumentException(message, name);
            }
        }
    }
}
