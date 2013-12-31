using NUnit.Framework;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NMahjong.Testing;

namespace NMahjong.Japanese.Drivers
{
    public abstract class JsonSerializeTestBase : TestHelperWithAnnotatedTiles
    {
        private JsonSerializer mSerializer;

        [SetUp]
        public void Setup()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters = GetConverters();
            mSerializer = JsonSerializer.Create(settings);
        }

        protected string Serialize(object value)
        {
            return JToken.FromObject(value, mSerializer).ToString(Formatting.None);
        }

        protected abstract IList<JsonConverter> GetConverters();
    }
}
