using System;
using System.Collections.Generic;
using System.Linq;
using NMahjong.Aux;
using NMahjong.Base;

namespace NMahjong.Japanese
{
    public class WinningDeclaredEventArgs : EventArgs
    {
        private readonly WinningInfo mWinningInfo;
        private readonly ImmutableList<Dora> mUradora;

        public WinningDeclaredEventArgs(WinningInfo winningInfo, IEnumerable<Dora> uradora)
        {
            CheckArg.NotNull(winningInfo, "winningInfo");
            mWinningInfo = winningInfo;
            CheckArg.NotContainsNull(uradora, "uradora");
            mUradora = uradora.ToImmutableList();
        }

        public PlayerId Player
        {
            get { return mWinningInfo.Winner; }
        }

        public WinningInfo WinningInfo
        {
            get { return mWinningInfo; }
        }

        public ImmutableList<Dora> Uradora
        {
            get { return mUradora; }
        }
    }
}
