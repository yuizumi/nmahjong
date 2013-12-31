namespace NMahjong.Japanese
{
    public class RiichiAction : IPlayerAction
    {
        internal static readonly RiichiAction Instance = new RiichiAction();

        private RiichiAction()
        {
        }

        public override string ToString()
        {
            return "Actions.Riichi()";
        }
    }
}
