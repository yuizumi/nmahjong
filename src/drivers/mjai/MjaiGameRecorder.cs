using System.IO;
using NMahjong.Japanese;
using NMahjong.Japanese.Drivers;

namespace NMahjong.Drivers.Mjai
{
    internal class MjaiGameRecorder : MjaiSpectator
    {
        private readonly TextWriter mWriter;

        internal MjaiGameRecorder(TextWriter writer)
            : base()
        {
            mWriter = writer;
        }

        protected override void RegisterHandlers(IEventHandlerRegisterer registerer)
        {
            var recorder = new GameRecorder(GameState, mWriter);
            recorder.RegisterHandlers(registerer);
        }
    }
}
