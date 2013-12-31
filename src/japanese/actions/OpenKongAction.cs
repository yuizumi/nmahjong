using System;

namespace NMahjong.Japanese
{
    public class OpenKongAction : MeldAction, IPlayerAction
    {
        internal OpenKongAction(OpenKong kong)
            : base(kong)
        {
        }

        public override string ToString()
        {
            return String.Format("Actions.OpenKong({0})", Meld);
        }
    }
}
