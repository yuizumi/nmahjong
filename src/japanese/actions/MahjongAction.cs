namespace NMahjong.Japanese
{
    public class MahjongAction : IPlayerAction
    {
        internal static readonly MahjongAction Instance = new MahjongAction();

        private MahjongAction()
        {
        }

        public override string ToString()
        {
            return "Actions.Mahjong()";
        }
    }
}
