namespace NMahjong.Japanese
{
    internal abstract class WallImpl : IWall
    {
        public abstract int Count { get; }

        internal virtual void OnDoraAdded(Dora dora)
        {
        }

        internal virtual void OnTileDrawn(CanonicalTile tile)
        {
        }
    }
}
