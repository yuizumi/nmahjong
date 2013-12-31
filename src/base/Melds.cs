namespace NMahjong.Base
{
    public static class Melds
    {
        public static bool IsOpen(this Meld meld)
        {
            return meld.State == MeldState.Open;
        }
    }
}
