using NUnit.Framework;
using System;

namespace NMahjong.Japanese.Drivers
{
    [TestFixture]
    public class IntelligenceFinderTest
    {
        private static readonly Type[] FactoryTypes = {
            typeof(BadFactoryNoDefaultCtor),
            typeof(BadFactoryPrivateCtor),
            typeof(BadFactoryAbstractClass),
            typeof(BadFactoryMissingImplements),
            typeof(GoodFactoryAlpha),
            typeof(GoodFactoryBravo),
        };

        [Test]
        public void TestGetFactoryInternal()
        {
            var factory = IntelligenceFinder.GetFactoryInternal(FactoryTypes, null);
            Assert.AreEqual(typeof(GoodFactoryAlpha), factory.GetType());
        }


        [Test]
        public void TestGetFactoryInternalNotFound()
        {
            var factory = IntelligenceFinder.GetFactoryInternal(Type.EmptyTypes, null);
            Assert.IsNull(factory);
        }

        [Test]
        public void TestGetFactoryInternalWithName()
        {
            var factory = IntelligenceFinder.GetFactoryInternal(FactoryTypes, "GoodFactoryBravo");
            Assert.AreEqual(typeof(GoodFactoryBravo), factory.GetType());
        }

        private class BadFactoryNoDefaultCtor : IIntelligenceFactory
        {
            public BadFactoryNoDefaultCtor(object dummy)
            {
            }

            public Intelligence Create(IntelligenceArgs args, IEventHandlerRegisterer registerer)
            {
                return null;
            }
        }

        private class BadFactoryPrivateCtor : IIntelligenceFactory
        {
            private BadFactoryPrivateCtor()
            {
            }

            public Intelligence Create(IntelligenceArgs args, IEventHandlerRegisterer registerer)
            {
                return null;
            }
        }

        private abstract class BadFactoryAbstractClass : IIntelligenceFactory
        {
            public BadFactoryAbstractClass()
            {
            }

            public Intelligence Create(IntelligenceArgs args, IEventHandlerRegisterer registerer)
            {
                return null;
            }
        }

        private class BadFactoryMissingImplements
        {
            public BadFactoryMissingImplements()
            {
            }

            public Intelligence Create(IntelligenceArgs args, IEventHandlerRegisterer registerer)
            {
                return null;
            }
        }

        private class GoodFactoryAlpha : IIntelligenceFactory
        {
            public GoodFactoryAlpha()
            {
            }

            public Intelligence Create(IntelligenceArgs args, IEventHandlerRegisterer registerer)
            {
                return null;
            }
        }

        private class GoodFactoryBravo : IIntelligenceFactory
        {
            public GoodFactoryBravo()
            {
            }

            public Intelligence Create(IntelligenceArgs args, IEventHandlerRegisterer registerer)
            {
                return null;
            }
        }
    }
}
