using NUnit.Framework;
using NMahjong.Japanese;

namespace NMahjong.Testing
{
    public static class MeldAssert
    {
        public static void AreIdentical(RevealedMeld expected, RevealedMeld actual)
        {
            Assert.That(actual, Is.EqualTo(expected).Using(MeldIdentity.Comparer));
        }

        public static void AreIdentical(RevealedMeld expected, RevealedMeld actual,
                                        string message)
        {
            Assert.That(actual, Is.EqualTo(expected).Using(MeldIdentity.Comparer), message);
        }
    }
}
