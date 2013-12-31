using System;
using NMahjong.Aux;

using TA = NMahjong.Japanese.TileAnnotations;

namespace NMahjong.Japanese
{
    public class DiscardAction : IPlayerAction
    {
        private readonly AnnotatedTile mTile;

        internal DiscardAction(AnnotatedTile tile)
        {
            CheckTile.HasOnly(tile, "tile", TA.Red | TA.Drawn);
            mTile = tile;
        }

        public AnnotatedTile Tile
        {
            get { return mTile; }
        }

        public override string ToString()
        {
            return String.Format("Actions.Discard({0})", Tile);
        }
    }
}
