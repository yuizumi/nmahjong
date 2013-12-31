using System;
using System.Collections.Generic;
using System.Linq;

namespace NMahjong.Aux
{
    public static class CheckArg
    {
        public static void Expect(bool condition, string message)
        {
            if (!condition) {
                throw new ArgumentException(message);
            }
        }

        public static void Expect(bool condition, string name, string message)
        {
            if (!condition) {
                throw new ArgumentException(message, name);
            }
        }

        public static void Expect(bool condition, string name, string messageFormat,
                                  params object[] messageArgs)
        {
            if (!condition) {
                throw new ArgumentException(String.Format(messageFormat, messageArgs), name);
            }
        }

        public static void NotNull<T>(T arg, string name)
            where T : class
        {
            if (arg == null) {
                throw new ArgumentNullException(name);
            }
        }

        public static void Enum(Enum arg, string name)
        {
            if (!Enums.IsValid(arg)) {
                throw new ArgumentException("Argument is an invalid enum value.", name);
            }
        }

        public static void Minimum<T>(T arg, string name, T min)
            where T : IComparable<T>
        {
            if (arg.CompareTo(min) < 0) {
                throw new ArgumentOutOfRangeException(name);
            }
        }

        public static void Maximum<T>(T arg, string name, T max)
            where T : IComparable<T>
        {
            if (arg.CompareTo(max) > 0) {
                throw new ArgumentOutOfRangeException(name);
            }
        }

        public static void Range<T>(T arg, string name, T min, T max)
            where T : IComparable<T>
        {
            Minimum(arg, name, min);
            Maximum(arg, name, max);
        }

        public static void NotContainsNull<T>(IEnumerable<T> arg, string name)
            where T : class
        {
            NotNull(arg, name);
            Expect(!arg.Contains(null), name, "Sequence cannot contain null.");
        }

        public static void NotEmpty<T>(IEnumerable<T> arg, string name)
        {
            NotNull(arg, name);
            Expect(!arg.IsEmpty(), name, "Sequence cannot be empty.");
        }
    }
}
