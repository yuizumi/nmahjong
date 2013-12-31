using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NMahjong.Drivers.Mjai
{
    internal class MjaiJsonConverter : JsonConverter
    {
        internal static readonly MjaiJsonConverter Converter =
            new MjaiJsonConverter();

        private MjaiJsonConverter()
        {
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(MjaiJson);
        }

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                        object existingValue,
                                        JsonSerializer serializer)
        {
            JObject jobject = JToken.ReadFrom(reader) as JObject;
            if (jobject == null) {
                throw new JsonSerializationException("Value is not an object.");
            }
            return new MjaiJson(jobject);
        }

        public override void WriteJson(JsonWriter writer,
                                       object value,
                                       JsonSerializer serializer)
        {
            writer.WriteRaw(value.ToString());
        }
    }
}
