using System;
using Newtonsoft.Json;

namespace NMahjong.Japanese.Drivers
{
    public abstract class JsonWriteOnlyConverter<T> : JsonConverter
    {
        protected JsonWriteOnlyConverter()
        {
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        protected abstract object Convert(T value);

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                        object existingValue,
                                        JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override void WriteJson(JsonWriter writer,
                                       object value,
                                       JsonSerializer serializer)
        {
            serializer.Serialize(writer, Convert((T) value));
        }
    }
}
