namespace NMahjong.Japanese
{
    public static class Wall
    {
        private const int DefaultInitialCount = 70;

        public static IWall Simple(int count)
        {
            return new SimpleWall(count);
        }

        public static IWall Simple()
        {
            return new SimpleWall(DefaultInitialCount);
        }
    }
}
