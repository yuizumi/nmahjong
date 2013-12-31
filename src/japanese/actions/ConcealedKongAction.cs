using System;

namespace NMahjong.Japanese
{
    public class ConcealedKongAction : MeldAction, IPlayerAction
    {
        internal ConcealedKongAction(ConcealedKong kong)
            : base(kong)
        {
        }

        public override string ToString()
        {
            return String.Format("Actions.ConcealedKong({0})", Meld);
        }
    }
}
