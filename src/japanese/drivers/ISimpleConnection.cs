using System;

namespace NMahjong.Japanese.Drivers
{
    public interface ISimpleConnection : IDisposable
    {
        string Receive();
        void Send(string message);
    }
}
