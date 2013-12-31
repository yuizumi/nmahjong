using System;

namespace NMahjong.Drivers.Mjai
{
    internal class MjaiErrorResponseException : MjaiJsonException
    {
        private const string BaseMessage = "Server returned an error response.";

        private readonly string mErrorMessage;

        internal MjaiErrorResponseException(string errorMessage)
            : base(BaseMessage)
        {
            mErrorMessage = errorMessage;
        }

        internal string ErrorMessage
        {
            get { return mErrorMessage; }
        }

        public override string Message
        {
            get {
                string message = base.Message;
                if (mErrorMessage != null) {
                    message += Environment.NewLine + "Error message: " + mErrorMessage;
                }
                return message;
            }
        }
    }
}
