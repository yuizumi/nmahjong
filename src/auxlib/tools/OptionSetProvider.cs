using Mono.Options;

namespace NMahjong.Aux.Tools
{
    public abstract class OptionSetProvider
    {
        public abstract OptionSet GetOptionSet();

        public virtual void OnParseComplete()
        {
        }
    }
}
