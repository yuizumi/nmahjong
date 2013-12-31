using System.Collections.Generic;
using NMahjong.Aux;
using NMahjong.Japanese;

namespace NMahjong.Testing
{
    public class MeldIdentity : IEqualityComparer<RevealedMeld>
    {
        public static MeldIdentity Comparer = new MeldIdentity();

        private MeldIdentity()
        {
        }

        public bool Equals(RevealedMeld a, RevealedMeld b)
        {
            return RevealedMelds.AreIdentical(a, b);
        }

        public int GetHashCode(RevealedMeld meld)
        {
            CheckArg.NotNull(meld, "meld");
            return meld.BaseMeld.GetHashCode();  // Exists only to compile.
        }
    }
}
