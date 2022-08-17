using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Iviz.Tools;

/// <summary>
/// Contains an arbitrary object/value. Used by XML-RPC functions and the ROS parameter system.
/// </summary>
public readonly struct RosValue
{
    public enum Type
    {
        Empty,
        Integer,
        Double,
        Boolean,
        DateTime,
        String,
        Array,
        ByteArray,
        Struct,
        Long,
        BooleanArray,
        LongArray,
        DoubleArray,
        StringArray
    }

    readonly long l;
    readonly object? o;
    readonly Type type;

    public Type ValueType => type;
    public bool IsEmpty => type == Type.Empty;

    public RosValue(double d)
    {
        o = null;
        l = ToLong(d);
        type = Type.Double;
    }

    public RosValue(bool b)
    {
        o = null;
        l = b ? 1 : 0;
        type = Type.Boolean;
    }

    public RosValue(int i)
    {
        o = null;
        l = i;
        type = Type.Integer;
    }

    public RosValue(string s)
    {
        l = 0;
        o = s;
        type = Type.String;
    }

    public RosValue(DateTime dt)
    {
        l = dt.Ticks;
        o = null;
        type = Type.DateTime;
    }

    public RosValue(byte[] bs)
    {
        l = 0;
        o = bs;
        type = Type.ByteArray;
    }

    public RosValue(RosValue[] os)
    {
        l = 0;
        o = os;
        type = Type.Array;
    }

    public RosValue((string Key, RosValue Value)[] vs)
    {
        l = 0;
        o = vs;
        type = Type.Struct;
    }
    
    public RosValue(long i)
    {
        o = null;
        l = i;
        type = Type.Long;
    }

    public RosValue(bool[] i)
    {
        o = i;
        l = 0;
        type = Type.BooleanArray;
    }

    public RosValue(long[] i)
    {
        o = i;
        l = 0;
        type = Type.LongArray;
    }

    public RosValue(double[] i)
    {
        o = i;
        l = 0;
        type = Type.DoubleArray;
    }

    public RosValue(string[] i)
    {
        o = i;
        l = 0;
        type = Type.StringArray;
    }

    public bool TryGet(out bool value)
    {
        if (type == Type.Boolean)
        {
            value = (l != 0);
            return true;
        }

        value = default;
        return false;
    }

    public bool TryGet(out double value)
    {
        if (type == Type.Double)
        {
            value = ToDouble(l);
            return true;
        }

        value = default;
        return false;
    }

    public bool TryGet(out int value)
    {
        if (type == Type.Integer)
        {
            value = (int)l;
            return true;
        }

        value = default;
        return false;
    }
    
    public bool TryGet(out long value)
    {
        if (type == Type.Long)
        {
            value = l;
            return true;
        }

        value = default;
        return false;
    }

    public bool TryGet(out string value)
    {
        if (type == Type.String)
        {
            value = (string)o!;
            return true;
        }

        value = "";
        return false;
    }

    public bool TryGet(out DateTime value)
    {
        if (type == Type.DateTime)
        {
            value = new DateTime(l);
            return true;
        }

        value = default;
        return false;
    }

    public bool TryGet(out byte[] value)
    {
        if (type == Type.ByteArray)
        {
            value = (byte[])o!;
            return true;
        }

        value = Array.Empty<byte>();
        return false;
    }

    public bool TryGetArray(out RosValue[] value)
    {
        if (type == Type.Array)
        {
            value = (RosValue[])o!;
            return true;
        }

        value = Array.Empty<RosValue>();
        return false;
    }

    public bool TryGetStruct(out (string Key, RosValue Value)[] value)
    {
        if (type == Type.Struct)
        {
            value = ((string, RosValue)[])o!;
            return true;
        }

        value = Array.Empty<(string, RosValue)>();
        return false;
    }
    
    public bool TryGet(out bool[] value)
    {
        if (type == Type.BooleanArray)
        {
            value = (bool[])o!;
            return true;
        }

        value = Array.Empty<bool>();
        return false;
    }
    
    public bool TryGet(out long[] value)
    {
        if (type == Type.LongArray)
        {
            value = (long[])o!;
            return true;
        }

        value = Array.Empty<long>();
        return false;
    }
    
    public bool TryGet(out double[] value)
    {
        if (type == Type.DoubleArray)
        {
            value = (double[])o!;
            return true;
        }

        value = Array.Empty<double>();
        return false;
    }
    
    public bool TryGet(out string[] value)
    {
        if (type == Type.StringArray)
        {
            value = (string[])o!;
            return true;
        }

        value = Array.Empty<string>();
        return false;
    }

    public override string ToString()
    {
        return type switch
        {
            Type.Integer => $"[int:{l.ToString()}]",
            Type.Long => $"[long:{l.ToString()}]",
            Type.Empty => "[empty]",
            Type.Double => $"[double:{ToDouble(l).ToString(Defaults.Culture)}]",
            Type.Boolean => l != 0 ? "[bool:true]" : "[bool:false]",
            Type.DateTime => $"[dateTime:{new DateTime(l).ToString(Defaults.Culture)}]",
            Type.String => $"[string:{(string)o!}]",
            Type.Array => $"[array:{ArrayLengthAsString} elems]",
            Type.ByteArray => $"[byteArray:{ArrayLengthAsString} bytes]",
            Type.Struct => $"[struct:{ArrayLengthAsString} fields]",
            Type.BooleanArray => $"[booleanArray:{ArrayLengthAsString} elems]",
            Type.LongArray => $"[longArray:{ArrayLengthAsString} elems]",
            Type.DoubleArray => $"[doubleArray:{ArrayLengthAsString} elems]",
            Type.StringArray => $"[stringArray:{ArrayLengthAsString} elems]",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    string ArrayLengthAsString => ((Array)o!).Length.ToString();

    public void ThrowIfEmpty()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("Value is empty");
        }
    }

    static double ToDouble(long l) => Unsafe.As<long, double>(ref l);
    
    static long ToLong(double d) => Unsafe.As<double, long>(ref d);

    public class JsonConverter : JsonConverter<RosValue>
    {
        public int MaxStringLength { get; set; } = 10000;

        public override void WriteJson(JsonWriter writer, RosValue value, JsonSerializer serializer)
        {
            switch (value.ValueType)
            {
                case Type.String:
                    value.TryGet(out string str);
                    if (str.Length <= MaxStringLength)
                    {
                        writer.WriteValue(str);
                    }
                    else
                    {
                        writer.WriteValue(str[..MaxStringLength]);
                        writer.WriteComment("... + " + (str.Length - MaxStringLength) + " chars");
                    }

                    break;
                case Type.Integer:
                    value.TryGet(out int i);
                    writer.WriteValue(i);
                    break;
                case Type.Long:
                    value.TryGet(out long l);
                    writer.WriteValue(l);
                    break;
                case Type.Double:
                    value.TryGet(out double d);
                    writer.WriteValue(d);
                    break;
                case Type.Boolean:
                    value.TryGet(out bool b);
                    writer.WriteValue(b);
                    break;
                case Type.DateTime:
                    value.TryGet(out DateTime dt);
                    writer.WriteValue(dt.ToString("yyyy-MM-dd HH-mm-ss"));
                    break;
                case Type.Array:
                    value.TryGetArray(out var array);
                    var a = new JArray();
                    foreach (var innerValue in array)
                    {
                        a.Add(JToken.FromObject(innerValue, serializer));
                    }

                    a.WriteTo(writer);
                    break;
                case Type.Struct:
                    value.TryGetStruct(out (string Key, RosValue Value)[] dict);
                    var o = new JObject();
                    foreach ((string key, RosValue innerValue) in dict)
                    {
                        o.Add(new JProperty(key, JToken.FromObject(innerValue, serializer)));
                    }

                    o.WriteTo(writer);
                    break;
                case Type.ByteArray:
                case Type.BooleanArray:
                case Type.LongArray:
                case Type.DoubleArray:
                case Type.StringArray:
                    writer.WriteValue($"[{value.ArrayLengthAsString} elems]");
                    break;
                case Type.Empty:
                    writer.WriteValue("[]");
                    break;
            }
        }

        public override RosValue ReadJson(JsonReader reader, System.Type objectType,
            RosValue existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead => false;
    }
}