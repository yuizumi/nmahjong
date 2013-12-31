using System;

namespace NMahjong.Aux
{
    public static class CheckState
    {
        public static void Expect(bool condition, string message)
        {
            if (!condition) {
                throw new InvalidOperationException(message);
            }
        }

        public static void Expect(bool condition, string messageFormat,
                                  params object[] messageArgs)
        {
            if (!condition) {
                throw new InvalidOperationException(String.Format(messageFormat, messageArgs));
            }
        }

        public static void IsSet<T>(T value, string name)
            where T : class
        {
            if (value == null) {
                throw new InvalidOperationException(String.Format("{0} must be set.", name));
            }
        }

        public static void IsSet<T>(Nullable<T> value, string name)
            where T : struct
        {
            if (!value.HasValue) {
                throw new InvalidOperationException(String.Format("{0} must be set.", name));
            }
        }
    }
}
