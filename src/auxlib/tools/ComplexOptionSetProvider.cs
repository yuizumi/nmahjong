using System.Collections.Generic;
using System.Linq;
using Mono.Options;

namespace NMahjong.Aux.Tools
{
    public class ComplexOptionSetProvider : OptionSetProvider
    {
        private List<OptionSetProvider> mInnerProviders;

        [VisibleForTesting]
        protected internal ComplexOptionSetProvider()
        {
            mInnerProviders = new List<OptionSetProvider>();
        }

        public void Add(OptionSetProvider provider)
        {
            mInnerProviders.Add(provider);
        }

        public TOptionSetProvider Get<TOptionSetProvider>()
            where TOptionSetProvider : OptionSetProvider
        {
            return mInnerProviders.OfType<TOptionSetProvider>().FirstOrDefault();
        }

        public override OptionSet GetOptionSet()
        {
            var options = new OptionSet();
            mInnerProviders.ForEach(provider => options.AddRange(provider.GetOptionSet()));
            return options;
        }

        public override void OnParseComplete()
        {
            mInnerProviders.ForEach(provider => provider.OnParseComplete());
        }
    }
}
