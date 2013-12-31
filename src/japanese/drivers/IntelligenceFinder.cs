using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NMahjong.Aux;

namespace NMahjong.Japanese.Drivers
{
    public static class IntelligenceFinder
    {
        public static IIntelligenceFactory GetFactory(Assembly assembly, string typeName)
        {
            CheckArg.NotNull(assembly, "assembly");
            return GetFactoryInternal(assembly.GetTypes(), typeName);
        }

        [VisibleForTesting]
        internal static IIntelligenceFactory GetFactoryInternal(
            IEnumerable<Type> types, string typeName)
        {
            if (typeName != null) {
                types = types.Where(type => type.Name == typeName);
            }

            foreach (Type type in types.Where(IsIntelligenceFactoryType)) {
                ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
                if (ctor != null) {
                    return ctor.Invoke(new object[0]) as IIntelligenceFactory;
                }
            }

            return null;  // Not found.
        }

        private static bool IsIntelligenceFactoryType(Type type)
        {
            return typeof(IIntelligenceFactory).IsAssignableFrom(type) && !type.IsAbstract;
        }
    }
}
