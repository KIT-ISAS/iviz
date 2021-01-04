using System;
using Iviz.Msgs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.MsgsGen.Dynamic
{
    [JsonConverter(typeof(UserConverter))]
    public interface IField
    {
        int RosMessageLength { get; }
        void RosValidate();
        void RosSerialize(ref Buffer b);
        void RosDeserializeInPlace(ref Buffer b);
        IField Generate();
        object Value { get; }
    }

    [Preserve]
    public class UserConverter : JsonConverter
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
                    var fields = ((string, IField)[]) ((IField) field).Value; 
                    foreach (var (fieldName, fieldValue) in fields)
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