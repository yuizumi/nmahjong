using System;

namespace NMahjong.Japanese
{
    public class PungAction : MeldAction, IPlayerAction
    {
        internal PungAction(OpenPung pung)
            : base(pung)
        {
        }

        public override string ToString()
        {
            return String.Format("Actions.Pung({0})", Meld);
        }
    }
}
