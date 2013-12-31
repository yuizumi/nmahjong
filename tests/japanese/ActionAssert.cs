using NUnit.Framework;
using NMahjong.Japanese;

namespace NMahjong.Testing
{
    public static class ActionAssert
    {
        public static void AreIdentical(IPlayerAction expected, IPlayerAction actual)
        {
            Assert.That(actual, Is.EqualTo(expected).Using(ActionIdentity.Comparer));
        }

        public static void AreIdentical(IPlayerAction expected, IPlayerAction actual,
                                        string message)
        {
            Assert.That(actual, Is.EqualTo(expected).Using(ActionIdentity.Comparer), message);
        }
    }
}
