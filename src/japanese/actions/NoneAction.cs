namespace NMahjong.Japanese
{
    public class NoneAction : IPlayerAction
    {
        internal static readonly NoneAction Instance = new NoneAction();

        private NoneAction()
        {
        }

        public override string ToString()
        {
            return "Actions.None()";
        }
    }
}
