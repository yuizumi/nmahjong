using System;
using System.IO;
using System.Reflection;
using NMahjong.Aux;
using NMahjong.Aux.Tools;

namespace NMahjong.Japanese.Drivers
{
    public static class IntelligenceOptionsExtension
    {
        public static IIntelligenceFactory GetFactory(this IntelligenceOptions options)
        {
            CheckArg.NotNull(options, "options");
            CheckArg.Expect(options.AssemblyFile != null,
                            "options", "Options have not been parsed yet.");
            return GetFactory(options.AssemblyFile, options.TypeName);
        }

        [VisibleForTesting]
        internal static IIntelligenceFactory GetFactory(string assemblyFile,
                                                        string typeName)
        {
            Assembly assembly;
            try {
                assembly = Assembly.LoadFrom(assemblyFile);
            } catch (IOException e) {
                throw new CommandLineException(
                    String.Format("Failed to load {0}: {1}", assemblyFile, e.Message), e);
            } catch (BadImageFormatException e) {
                throw new CommandLineException(
                    String.Format("Failed to load {0}: {1}", assemblyFile, e.Message), e);
            }

            IIntelligenceFactory factory = IntelligenceFinder.GetFactory(
                assembly, typeName);
            if (factory == null) {
                throw new CommandLineException("Failed to find an AI implementation.");
            }

            return factory;
        }
    }
}
