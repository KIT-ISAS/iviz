/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract (Name = "sensor_msgs/PointField")]
    public sealed class PointField : IDeserializable<PointField>, IMessage
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
        [DataMember (Name = "name")] public string Name { get; set; } // Name of field
        [DataMember (Name = "offset")] public uint Offset { get; set; } // Offset from start of point struct
        [DataMember (Name = "datatype")] public byte Datatype { get; set; } // Datatype enumeration, see above
        [DataMember (Name = "count")] public uint Count { get; set; } // How many elements in the field
    
        /// <summary> Constructor for empty message. </summary>
        public PointField()
        {
            Name = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public PointField(string Name, uint Offset, byte Datatype, uint Count)
        {
            this.Name = Name;
            this.Offset = Offset;
            this.Datatype = Datatype;
            this.Count = Count;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PointField(ref Buffer b)
        {
            Name = b.DeserializeString();
            Offset = b.Deserialize<uint>();
            Datatype = b.Deserialize<byte>();
            Count = b.Deserialize<uint>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PointField(ref b);
        }
        
        PointField IDeserializable<PointField>.RosDeserialize(ref Buffer b)
        {
            return new PointField(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(Offset);
            b.Serialize(Datatype);
            b.Serialize(Count);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 13;
                size += BuiltIns.UTF8.GetByteCount(Name);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/PointField";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "268eacb2962780ceac86cbd17e328150";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE02Qz06EMBCH7zzFL+FqTJZdkQsHozGamF0P6wNUGJYmtEPaQcPb20LXZU6Tr9/86eQ4" +
                "99rDkPfqQuh5aD2kJ7TkG6dH0WzBHdgSRtZWQFbcDG2jleX4jPB54Kkt/rt07IyS+2wKTxXej+cKIWrs" +
                "EvlKqEZxc3blQvYbJ6Iah5uzLxbnYeNEVKNM5PXj9BRRjcctKQ+BVFnmxWl7gVWGsESOY8zDBztNQ7vU" +
                "hHLuOk+yCqc17xwbeFFOor2eIrSbGkmD0CpRMo8Ui16uOdnJkFPxjHfwRFDf/EPXOQ1PVtIib/wLo+wM" +
                "GsiEI/t047RZ9gctJPa/qgEAAA==";
                
    }
}
