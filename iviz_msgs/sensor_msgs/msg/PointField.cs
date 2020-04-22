
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
                "H4sIAAAAAAAACk2QQU7DMBBF95G4w5eyRUhNS8gmiwqEQEIti3IA04wbS7Ensieg3B47SWm8Gj0/z4x/" +
                "jlNrAiyFoC6ElrsmQFpCQ+HsTS+GHViDHaFn4wTkxI8wLllZjs8EnzsemuK/i2ZvlTxkQ7yq8H44VYin" +
                "xmYhXwuqUdycTTmR7cpJqMbu5myLyXlcOQnVKBfy+nHcJ1TjaU3KXSRVlgXxxl3glKW0EZDjkOr4QW2o" +
                "a6Y38TlrHUhm4TjX2rNFEOUl2XMUsd1wlmUQGiVKxj52zvFyrckNlrxKMd4jEEF98w9d55x5iG3mOW/8" +
                "C6vcCOrIxpDDkvGy2V32BwU69jirAQAA";
                
    }
}
