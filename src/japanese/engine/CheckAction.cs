using System;

namespace NMahjong.Japanese.Engine
{
    public static class CheckAction
    {
        public static void Expect(bool condition, string message)
        {
            if (!condition) {
                throw new InvalidActionException(message);
            }
        }

        public static void Expect(bool condition, string messageFormat,
                                  params object[] messageArgs)
        {
            if (!condition) {
                throw new InvalidActionException(String.Format(messageFormat, messageArgs));
            }
        }

        public static void NotNull(IPlayerAction action)
        {
            if (action == null) {
                throw new InvalidActionException("Intelligence returned a null action.");
            }
        }
    }
}
