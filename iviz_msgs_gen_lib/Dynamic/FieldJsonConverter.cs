using System;
using Iviz.Msgs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Iviz.MsgsGen.Dynamic
{
    [Preserve]
    public class FieldJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IField).IsAssignableFrom(objectType);
        }

        public override bool CanRead => false;

        public override bool CanWrite => true;

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            switch (value)
            {
                case DynamicMessage field:
                    JObject jObject = new JObject();
                    foreach (var (fieldName, fieldValue) in field.Fields)
                    {
                        jObject.Add(new JProperty(fieldName, JToken.FromObject(fieldValue, serializer)));
                    }

                    jObject.WriteTo(writer);
                    return;
                case IField field:
                    JToken.FromObject(field.Value, serializer).WriteTo(writer);
                    return;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}