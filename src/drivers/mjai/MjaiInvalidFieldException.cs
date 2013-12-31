using System;

namespace NMahjong.Drivers.Mjai
{
    internal class MjaiInvalidFieldException : MjaiJsonException
    {
        private const string DefaultMessage = "Message has a field with an invalid value.";

        private readonly string mFieldName;
        private readonly MjaiJson mMjaiJson;

        internal MjaiInvalidFieldException(string fieldName, MjaiJson mjaiJson)
            : this(fieldName, mjaiJson, DefaultMessage)
        {
        }

        internal MjaiInvalidFieldException(string fieldName, MjaiJson mjaiJson, string message)
            : base(message)
        {
            mFieldName = fieldName;
            mMjaiJson = mjaiJson;
        }

        internal string FieldName
        {
            get { return mFieldName; }
        }

        internal MjaiJson MjaiJson
        {
            get { return mMjaiJson; }
        }

        public override string Message
        {
            get {
                string message = base.Message;
                if (mFieldName != null) {
                    message += Environment.NewLine + "Field name: " + mFieldName;
                }
                if (mMjaiJson != null) {
                    message += Environment.NewLine + "Mjai message: " + mMjaiJson;
                }
                return message;
            }
        }
    }
}
