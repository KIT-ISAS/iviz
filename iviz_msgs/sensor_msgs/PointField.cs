
namespace Iviz.Msgs.sensor_msgs
{
    public sealed class PointField : IMessage 
    {
        // This message holds the description of one point entry in the
        // PointCloud2 message format.
        public const byte INT8 = 1;
        public const byte UINT8 = 2;
        public const byte INT16 = 3;
        public const byte UINT16 = 4;
        public const byte INT32 = 5;
        public const byte UINT32 = 6;
        public const byte FLOAT32 = 7;
        public const byte FLOAT64 = 8;
        
        public string name; // Name of field
        public uint offset; // Offset from start of point struct
        public byte datatype; // Datatype enumeration, see above
        public uint count; // How many elements in the field

        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/PointField";

        public IMessage Create() => new PointField();

        public int GetLength()
        {
            int size = 13;
            size += name.Length;
            return size;
        }

        /// <summary> Constructor for empty message. </summary>
        public PointField()
        {
            name = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out name, ref ptr, end);
            BuiltIns.Deserialize(out offset, ref ptr, end);
            BuiltIns.Deserialize(out datatype, ref ptr, end);
            BuiltIns.Deserialize(out count, ref ptr, end);
        }

        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(name, ref ptr, end);
            BuiltIns.Serialize(offset, ref ptr, end);
            BuiltIns.Serialize(datatype, ref ptr, end);
            BuiltIns.Serialize(count, ref ptr, end);
        }

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "268eacb2962780ceac86cbd17e328150";

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAAE02Qz06EMBCH7zzFL+FqTJZdkQsHozGamF0P6wNUGJYmtEPaQcPb20LXZU6Tr9/86eQ4" +
            "99rDkPfqQuh5aD2kJ7TkG6dH0WzBHdgSRtZWQFbcDG2jleX4jPB54Kkt/rt07IyS+2wKTxXej+cKIWrs" +
            "EvlKqEZxc3blQvYbJ6Iah5uzLxbnYeNEVKNM5PXj9BRRjcctKQ+BVFnmxWl7gVWGsESOY8zDBztNQ7vU" +
            "hHLuOk+yCqc17xwbeFFOor2eIrSbGkmD0CpRMo8Ui16uOdnJkFPxjHfwRFDf/EPXOQ1PVtIib/wLo+wM" +
            "GsiEI/t047RZ9gctJPa/qgEAAA==";

    }
}
