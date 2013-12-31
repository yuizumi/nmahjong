namespace NMahjong.Drivers.Mjai
{
    internal class MjaiMissingFieldException : MjaiInvalidFieldException
    {
        private const string DefaultMessage = "Message does not have the specified field.";

        internal MjaiMissingFieldException(string fieldName, MjaiJson mjaiJson)
            : base(fieldName, mjaiJson, DefaultMessage)
        {
        }

        internal MjaiMissingFieldException(string fieldName, MjaiJson mjaiJson, string message)
            : base(fieldName, mjaiJson, message)
        {
        }
    }
}
