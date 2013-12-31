using System;

namespace NMahjong.Japanese
{
    public class ChowAction : MeldAction, IPlayerAction
    {
        internal ChowAction(OpenChow chow)
            : base(chow)
        {
        }

        public override string ToString()
        {
            return String.Format("Actions.Chow({0})", Meld);
        }
    }
}
