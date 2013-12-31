using System;
using NMahjong.Aux;
using NMahjong.Base;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Japanese
{
    public class TileDiscardedEventArgs : EventArgs
    {
        private readonly PlayerId mPlayer;
        private readonly AnnotatedTile mDiscard;

        public TileDiscardedEventArgs(PlayerId player, AnnotatedTile discard)
        {
            CheckTile.HasOnly(discard, "discard", TA.Red | TA.Drawn | TA.Riichi);
            mPlayer = player;
            mDiscard = discard;
        }

        public PlayerId Player
        {
            get { return mPlayer; }
        }

        public AnnotatedTile Discard
        {
            get { return mDiscard; }
        }
    }
}
