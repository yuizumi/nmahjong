using NMahjong.Base;

namespace NMahjong.Japanese.Drivers
{
    public class TileConverter : JsonWriteOnlyConverter<Tile>
    {
        public static readonly TileConverter Converter =
            new TileConverter();

        private TileConverter()
        {
        }

        protected override object Convert(Tile value)
        {
            return value.Name;
        }
    }
}
