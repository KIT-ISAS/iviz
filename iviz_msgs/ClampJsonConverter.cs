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
        readonly int maxArrayLengthForUnmanaged;

        public ClampJsonConverter(int maxStringLength = 200, int maxArrayLength = 5,
            int maxArrayLengthForUnmanaged = 16)
        {
            this.maxStringLength = maxStringLength;
            this.maxArrayLength = maxArrayLength;
            this.maxArrayLengthForUnmanaged = maxArrayLengthForUnmanaged;
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
                    writer.WriteValue(str);
                    break;
                case string str:
                    string shortString =
                        $"{str.Substring(0, maxStringLength)} <i>... +{(str.Length - maxStringLength).ToString()} chars</i>";
                    writer.WriteValue(shortString);
                    break;
                case Array array:
                    _ = array switch
                    {
                        int[] i => WriteArray(i, writer.WriteValue),
                        uint[] i => WriteArray(i, writer.WriteValue),
                        short[] i => WriteArray(i, writer.WriteValue),
                        ushort[] i => WriteArray(i, writer.WriteValue),
                        byte[] i => WriteArray(i, writer.WriteValue),
                        sbyte[] i => WriteArray(i, writer.WriteValue),
                        long[] i => WriteArray(i, writer.WriteValue),
                        ulong[] i => WriteArray(i, writer.WriteValue),
                        float[] i => WriteArray(i, writer.WriteValue),
                        double[] i => WriteArray(i, writer.WriteValue),
                        _ => WriteObjectArray(array)
                    };
                    break;
            }

            bool WriteArray<T>(T[] array, Action<T> writerDel)
            {
                if (array.Length > maxArrayLengthForUnmanaged)
                {
                    JValue.CreateComment(
                            $"<i>(Showing {maxArrayLengthForUnmanaged.ToString()}/{array.Length.ToString()})</i>")
                        .WriteTo(writer);
                }

                writer.WriteStartArray();
                for (int i = 0; i < Math.Min(array.Length, maxArrayLengthForUnmanaged); i++)
                {
                    writerDel(array[i]);
                }

                writer.WriteEndArray();

                return true;
            }

            bool WriteObjectArray(Array array)
            {
                if (array.Length > maxArrayLength)
                {
                    JValue.CreateComment($"<i>(Showing {maxArrayLength.ToString()}/{array.Length.ToString()})</i>")
                        .WriteTo(writer);
                }

                var shortArray = new JArray();
                for (int i = 0; i < Math.Min(array.Length, maxArrayLength); i++)
                {
                    object? element = array.GetValue(i);
                    shortArray.Add(element == null ? JValue.CreateNull() : JToken.FromObject(element, serializer));
                }

                JToken.FromObject(shortArray, serializer).WriteTo(writer);
                return true;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}