using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Iviz.Msgs
{
    /// <summary>
    /// JSON serializer that shortens arrays and strings
    /// </summary>
    public class ClampJsonConverter : JsonConverter
    {
        readonly int maxStringLength;
        readonly int maxArrayLength;

        public ClampJsonConverter(int maxStringLength = 200, int maxArrayLength = 5)
        {
            this.maxStringLength = maxStringLength;
            this.maxArrayLength = maxArrayLength;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string) || objectType.IsArray;
        }

        public override bool CanRead => false;

        public override bool CanWrite => true;

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            switch (value)
            {
                case null:
                    JValue.CreateNull().WriteTo(writer);
                    return;
                case string str when str.Length < maxStringLength:
                    JValue.CreateString(str).WriteTo(writer);
                    break;
                case string str:
                    string shortString =
                        $"{str.Substring(0, maxStringLength)} <i>... +{str.Length - maxStringLength} chars</i>";
                    JValue.CreateString(shortString).WriteTo(writer);
                    break;
                case Array array when array.Length < maxArrayLength:
                    JArray shortArray = new JArray();
                    foreach (object? element in array)
                    {
                        shortArray.Add(element == null ? JValue.CreateNull() : JToken.FromObject(element, serializer));
                    }

                    JToken.FromObject(shortArray, serializer).WriteTo(writer);
                    break;
                case Array array:
                    JValue.CreateComment($"<i>(Showing first {maxArrayLength} of {array.Length})</i>").WriteTo(writer);
                    JArray longArray = new JArray();
                    for (int i = 0; i < maxArrayLength; i++)
                    {
                        var element = array.GetValue(i);
                        longArray.Add(element == null ? JValue.CreateNull() : JToken.FromObject(element, serializer));
                    }

                    JToken.FromObject(longArray, serializer).WriteTo(writer);
                    break;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}