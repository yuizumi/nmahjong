using NMahjong.Aux;

namespace NMahjong.Japanese
{
    internal class SimpleWall : WallImpl
    {
        private int mCount;

        internal SimpleWall(int count)
        {
            CheckArg.Minimum(count, "count", 0);
            mCount = count;
        }

        public override int Count
        {
            get { return mCount; }
        }

        internal override void OnTileDrawn(CanonicalTile tile)
        {
            CheckState.Expect(mCount > 0, "All tiles have been taken.");
            --mCount;
        }
    }
}
