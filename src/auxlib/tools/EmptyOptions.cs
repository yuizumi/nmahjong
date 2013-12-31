using Mono.Options;

namespace NMahjong.Aux.Tools
{
    public class EmptyOptions : OptionSetProvider
    {
        public override OptionSet GetOptionSet()
        {
            return new OptionSet();
        }
    }
}
