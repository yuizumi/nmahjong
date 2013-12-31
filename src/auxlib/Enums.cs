using System;

namespace NMahjong.Aux
{
    public static class Enums
    {
        public static bool IsValid(Enum value)
        {
            return Array.IndexOf(Enum.GetValues(value.GetType()), value) >= 0;
        }

        public static TEnum[] Values<TEnum>()
            where TEnum : struct  // FIXME: enum
        {
            return Enum.GetValues(typeof(TEnum)) as TEnum[];
        }
    }
}
