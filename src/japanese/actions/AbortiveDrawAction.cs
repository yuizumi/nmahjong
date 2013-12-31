namespace NMahjong.Japanese
{
    public class AbortiveDrawAction : IPlayerAction
    {
        internal static readonly AbortiveDrawAction Instance = new AbortiveDrawAction();

        private AbortiveDrawAction()
        {
        }

        public override string ToString()
        {
            return "Actions.AbortiveDraw()";
        }
    }
}
