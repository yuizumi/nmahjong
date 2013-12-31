using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Japanese
{
    public static class AnnotatedTiles
    {
        public static bool Has(this AnnotatedTile tile, TileAnnotations annotations)
        {
            return (tile.Annotations & annotations) != 0;
        }

        public static CanonicalTile StripAnnotations(this AnnotatedTile tile)
        {
            return ((tile.Annotations & TA.Red) != 0)
                ? CanonicalTile.Red(tile.BaseTile) : CanonicalTile.Plain(tile.BaseTile);
        }

        public static AnnotatedTile With(this AnnotatedTile tile,
                                         TileAnnotations annotations)
        {
            if (annotations == TA.None) {
                return tile;
            }
            return AnnotatedTile.Of(tile.BaseTile, tile.Annotations |  annotations);
        }

        public static AnnotatedTile Without(this AnnotatedTile tile,
                                            TileAnnotations annotations)
        {
            if (annotations == TA.None) {
                return tile;
            }
            return AnnotatedTile.Of(tile.BaseTile, tile.Annotations & ~annotations);
        }
    }
}
