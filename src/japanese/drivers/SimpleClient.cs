using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace NMahjong.Japanese.Drivers
{
    public class SimpleClient : ISimpleConnection
    {
        private TcpClient mTcpClient;
        private TextReader mReader;
        private TextWriter mWriter;

        public SimpleClient(string host, int port)
        {
            mTcpClient = new TcpClient(host, port);
            Stream stream = mTcpClient.GetStream();
            mReader = new StreamReader(stream, Encoding.ASCII);
            mWriter = new StreamWriter(stream, Encoding.ASCII);
        }

        ~SimpleClient()
        {
            Dispose(false);
        }

        private bool IsDisposed
        {
            get { return mTcpClient == null; }
        }

        public void Close()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing) {
                mReader.Dispose();
                mWriter.Dispose();
                (mTcpClient as IDisposable).Dispose();
            }
            mReader = null;
            mWriter = null;
            mTcpClient = null;
        }

        public string Receive()
        {
            if (IsDisposed) throw new ObjectDisposedException(null);
            return mReader.ReadLine();
        }

        public void Send(string message)
        {
            if (IsDisposed) throw new ObjectDisposedException(null);
            mWriter.WriteLine(message);
            mWriter.Flush();
        }
    }
}
