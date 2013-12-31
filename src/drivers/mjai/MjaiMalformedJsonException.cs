using System;

namespace NMahjong.Drivers.Mjai
{
    internal class MjaiMalformedJsonException : MjaiJsonException
    {
        private const string BaseMessage = "Message is not a valid JSON object.";

        private readonly string mMjaiMessage;

        internal MjaiMalformedJsonException(string mjaiMessage)
            : base(BaseMessage)
        {
            mMjaiMessage = mjaiMessage;
        }

        internal MjaiMalformedJsonException(string mjaiMessage, Exception innerException)
            : base(BaseMessage, innerException)
        {
            mMjaiMessage = mjaiMessage;
        }

        internal string MjaiMessage
        {
            get { return mMjaiMessage; }
        }

        public override string Message
        {
            get {
                string message = base.Message;
                if (mMjaiMessage != null) {
                    message += Environment.NewLine + "Mjai message: " + mMjaiMessage;
                }
                return message;
            }
        }
    }
}
