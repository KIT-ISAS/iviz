using System.Runtime.Serialization;

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
        
        public string name { get; set; } // Name of field
        public uint offset { get; set; } // Offset from start of point struct
        public byte datatype { get; set; } // Datatype enumeration, see above
        public uint count { get; set; } // How many elements in the field
    
        /// <summary> Constructor for empty message. </summary>
        public PointField()
        {
            name = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public PointField(string name, uint offset, byte datatype, uint count)
        {
            this.name = name ?? throw new System.ArgumentNullException(nameof(name));
            this.offset = offset;
            this.datatype = datatype;
            this.count = count;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PointField(Buffer b)
        {
            this.name = BuiltIns.DeserializeString(b);
            this.offset = BuiltIns.DeserializeStruct<uint>(b);
            this.datatype = BuiltIns.DeserializeStruct<byte>(b);
            this.count = BuiltIns.DeserializeStruct<uint>(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new PointField(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.name, b);
            BuiltIns.Serialize(this.offset, b);
            BuiltIns.Serialize(this.datatype, b);
            BuiltIns.Serialize(this.count, b);
        }
        
        public void Validate()
        {
            if (name is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 13;
                size += BuiltIns.UTF8.GetByteCount(name);
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "sensor_msgs/PointField";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "268eacb2962780ceac86cbd17e328150";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE02Qz06EMBCH7zzFL+FqTJZdkQsHozGamF0P6wNUGJYmtEPaQcPb20LXZU6Tr9/86eQ4" +
                "99rDkPfqQuh5aD2kJ7TkG6dH0WzBHdgSRtZWQFbcDG2jleX4jPB54Kkt/rt07IyS+2wKTxXej+cKIWrs" +
                "EvlKqEZxc3blQvYbJ6Iah5uzLxbnYeNEVKNM5PXj9BRRjcctKQ+BVFnmxWl7gVWGsESOY8zDBztNQ7vU" +
                "hHLuOk+yCqc17xwbeFFOor2eIrSbGkmD0CpRMo8Ui16uOdnJkFPxjHfwRFDf/EPXOQ1PVtIib/wLo+wM" +
                "GsiEI/t047RZ9gctJPa/qgEAAA==";
                
    }
}
