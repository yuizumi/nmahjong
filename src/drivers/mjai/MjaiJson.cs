using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NMahjong.Drivers.Mjai
{
    internal class MjaiJson
    {
        private static readonly JsonSerializer Serializer = CreateJsonSerializer();

        internal readonly JObject mObject;

        internal MjaiJson(JObject jobject)
        {
            this.mObject = jobject;

            if (Get<String>("type") == "error") {
                throw new MjaiErrorResponseException(Get<String>("message"));
            }
        }

        private static JsonSerializer CreateJsonSerializer()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(DictionaryBasedConverter.Create(MjaiConsts.MjaiToTile));
            settings.Converters.Add(DictionaryBasedConverter.Create(MjaiConsts.MjaiToWind));
            settings.Converters.Add(DictionaryBasedConverter.Create(MjaiConsts.MjaiToDrawHand));
            settings.Converters.Add(MjaiJsonConverter.Converter);
            return JsonSerializer.Create(settings);
        }

        internal string Type
        {
            get {
                return mObject.Value<String>("type");
            }
        }

        internal static MjaiJson Parse(string message)
        {
            JObject jobject;
            try {
                jobject = JObject.Parse(message);
            } catch (JsonException e) {
                throw new MjaiMalformedJsonException(message, e);
            }
            return new MjaiJson(jobject);
        }

        internal static string Serialize(object value)
        {
            return JToken.FromObject(value, Serializer).ToString(Formatting.None);
        }

        internal T Get<T>(string name)
        {
            JToken value = mObject.GetValue(name);
            if (value == null) {
                throw new MjaiMissingFieldException(name, this);
            }
            try {
                return value.ToObject<T>(Serializer);
            } catch (JsonException) {
                throw new MjaiInvalidFieldException(name, this);
            }
        }

        internal bool Has(string name)
        {
            return mObject.GetValue(name) != null;
        }

        public override string ToString()
        {
            return mObject.ToString(Formatting.None);
        }
    }
}
