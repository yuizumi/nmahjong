namespace NMahjong.Japanese
{
    public interface IIntelligenceFactory
    {
        Intelligence Create(IntelligenceArgs args, IEventHandlerRegisterer registerer);
    }
}
