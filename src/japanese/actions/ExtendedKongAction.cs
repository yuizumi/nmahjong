using System;

namespace NMahjong.Japanese
{
    public class ExtendedKongAction : MeldAction, IPlayerAction
    {
        internal ExtendedKongAction(ExtendedKong kong)
            : base(kong)
        {
        }

        public override string ToString()
        {
            return String.Format("Actions.ExtendedKong({0})", Meld);
        }
    }
}
