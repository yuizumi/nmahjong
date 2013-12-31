using NMahjong.Base;

namespace NMahjong.Japanese.Drivers
{
    public class RevealedMeldConverter : JsonWriteOnlyConverter<RevealedMeld>
    {
        public static readonly RevealedMeldConverter Converter =
            new RevealedMeldConverter();

        private RevealedMeldConverter()
        {
        }

        protected override object Convert(RevealedMeld value)
        {
            if (value.IsOpen()) {
                return new { Type = value.GetType().Name, Feeder = value.Feeder,
                             AnnotatedTiles = value.AnnotatedTiles };
            } else {
                return new { Type = value.GetType().Name, AnnotatedTiles = value.AnnotatedTiles };
            }
        }
    }
}
