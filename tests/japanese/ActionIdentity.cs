using System.Collections.Generic;
using NMahjong.Aux;
using NMahjong.Japanese;

namespace NMahjong.Testing
{
    public class ActionIdentity : IEqualityComparer<IPlayerAction>
    {
        public static ActionIdentity Comparer = new ActionIdentity();

        private ActionIdentity()
        {
        }

        public bool Equals(IPlayerAction a, IPlayerAction b)
        {
            return Actions.AreIdentical(a, b);
        }

        public int GetHashCode(IPlayerAction action)
        {
            CheckArg.NotNull(action, "action");
            return action.GetType().GetHashCode();  // Exists only to compile.
        }
    }
}
