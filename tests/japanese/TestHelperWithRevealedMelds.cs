using NMahjong.Base;
using NMahjong.Japanese;

namespace NMahjong.Testing
{
    public class TestHelperWithRevealedMelds : TestHelperWithAnnotatedTiles
    {
        protected static OpenPung OPung(CanonicalTile tile1,
                                        CanonicalTile tile2,
                                        CanonicalTile tile3,
                                        PlayerId feeder)
        {
            return OpenPung.Create(List(tile1, tile2), tile3, feeder);
        }

        protected static OpenChow OChow(CanonicalTile tile1,
                                        CanonicalTile tile2,
                                        CanonicalTile tile3,
                                        PlayerId feeder)
        {
            return OpenChow.Create(List(tile1, tile2), tile3, feeder);
        }

        protected static OpenKong OKong(CanonicalTile tile1,
                                        CanonicalTile tile2,
                                        CanonicalTile tile3,
                                        CanonicalTile tile4,
                                        PlayerId feeder)
        {
            return OpenKong.Create(List(tile1, tile2, tile3), tile4, feeder);
        }

        protected static ExtendedKong  EKong(CanonicalTile tile1,
                                             CanonicalTile tile2,
                                             CanonicalTile tile3,
                                             PlayerId feeder,
                                             CanonicalTile tile4)
        {
            return ExtendedKong.Create(OpenPung.Create(List(tile1, tile2), tile3, feeder), tile4);
        }

        protected static ConcealedKong CKong(CanonicalTile tile1,
                                             CanonicalTile tile2,
                                             CanonicalTile tile3,
                                             CanonicalTile tile4)
        {
            return ConcealedKong.Create(List(tile1, tile2, tile3, tile4));
        }
    }
}
