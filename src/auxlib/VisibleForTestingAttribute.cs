using System;

namespace NMahjong.Aux
{
    using A = AttributeTargets;

    [AttributeUsage(A.Class|A.Constructor|A.Delegate|A.Enum|A.Event|
                    A.Field|A.Interface|A.Method|A.Property|A.Struct)]
    public class VisibleForTestingAttribute : Attribute
    {
    }
}
