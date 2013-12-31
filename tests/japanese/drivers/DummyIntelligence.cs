using System;

namespace NMahjong.Japanese.Drivers
{
    public class DummyIntelligence : IIntelligenceFactory
    {
        public Intelligence Create(IntelligenceArgs args,
                                   IEventHandlerRegisterer registerer)
        {
            throw new NotSupportedException();
        }
    }
}
