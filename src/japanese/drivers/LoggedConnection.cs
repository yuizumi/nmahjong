using System;
using System.IO;
using NMahjong.Aux;

namespace NMahjong.Japanese.Drivers
{
    public class LoggedConnection : ISimpleConnection
    {
        private ISimpleConnection mConnection;
        private TextWriter mTextWriter;

        public LoggedConnection(ISimpleConnection connection,
                                TextWriter textWriter)
        {
            CheckArg.NotNull(connection, "connection");
            mConnection = connection;
            CheckArg.NotNull(textWriter, "textWriter");
            mTextWriter = textWriter;
        }

        ~LoggedConnection()
        {
            Dispose(false);
        }

        private bool IsDisposed
        {
            get {
                return mConnection == null && mTextWriter == null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing) {
                mConnection.Dispose();
                mTextWriter.Dispose();
            }
            mConnection = null;
            mTextWriter = null;
        }

        public string Receive()
        {
            string message = mConnection.Receive();
            mTextWriter.WriteLine(">> Recv: {0}", message);
            mTextWriter.Flush();
            return message;
        }

        public void Send(string message)
        {
            mTextWriter.WriteLine("<< Send: {0}", message);
            mTextWriter.Flush();
            mConnection.Send(message);
        }
    }
}
