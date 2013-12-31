using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NMahjong.Aux;

namespace NMahjong.Drivers.Mjai
{
    internal class DictionaryBasedConverter<T> : JsonConverter
    {
        private readonly ImmutableDictionary<String, T> mReaderMap;
        private readonly ImmutableDictionary<T, String> mWriterMap;

        internal DictionaryBasedConverter(ImmutableDictionary<String, T> mapping)
        {
            CheckArg.NotEmpty(mapping, "mapping");
            mReaderMap = mapping;
            // Serializers take an Object as their argument, rather than a typed value,
            // and thus will not call this converter with null.
            mWriterMap = mapping
                .Where(e => e.Value != null).ToImmutableDictionary(e => e.Value, e => e.Key);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(T);
        }

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                        object existingValue,
                                        JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String) {
                throw new JsonSerializationException("Value is not string.");
            }

            string source = reader.Value.ToString();
            T output;
            if (!mReaderMap.TryGetValue(source, out output)) {
                throw new JsonSerializationException("Value is not valid.");
            }
            return output;
        }

        public override void WriteJson(JsonWriter writer,
                                       object value,
                                       JsonSerializer serializer)
        {
            T source = (T) value;
            string output;
            if (!mWriterMap.TryGetValue(source, out output)) {
                throw new JsonSerializationException("Value is not valid.");
            }
            writer.WriteValue(output);
        }
    }

    internal static class DictionaryBasedConverter
    {
        internal static DictionaryBasedConverter<T> Create<T>(
            ImmutableDictionary<String, T> mapping)
        {
            return new DictionaryBasedConverter<T>(mapping);
        }
    }
}
