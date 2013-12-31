namespace NMahjong.Japanese.Drivers
{
    public class AnnotatedTileConverter : JsonWriteOnlyConverter<AnnotatedTile>
    {
        public static readonly AnnotatedTileConverter Converter =
            new AnnotatedTileConverter();

        private AnnotatedTileConverter()
        {
        }

        protected override object Convert(AnnotatedTile value)
        {
            return new { BaseTile = value.BaseTile, Annotations = value.Annotations };
        }
    }
}
