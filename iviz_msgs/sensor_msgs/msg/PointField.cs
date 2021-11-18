/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/PointField")]
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
        [DataMember (Name = "name")] public string Name; // Name of field
        [DataMember (Name = "offset")] public uint Offset; // Offset from start of point struct
        [DataMember (Name = "datatype")] public byte Datatype; // Datatype enumeration, see above
        [DataMember (Name = "count")] public uint Count; // How many elements in the field
    
        /// <summary> Constructor for empty message. </summary>
        public PointField()
        {
            Name = string.Empty;
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
        internal PointField(ref Buffer b)
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
    
        public int RosMessageLength => 13 + BuiltIns.GetStringSize(Name);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/PointField";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "268eacb2962780ceac86cbd17e328150";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACk2QQU7DMBBF95G4w5eyRUhNS8gmiwqEQEIti3IA04wbS7Ensieg3B47SWm8Gj0/z4x/" +
                "jlNrAiyFoC6ElrsmQFpCQ+HsTS+GHViDHaFn4wTkxI8wLllZjs8EnzsemuK/i2ZvlTxkQ7yq8H44VYin" +
                "xmYhXwuqUdycTTmR7cpJqMbu5myLyXlcOQnVKBfy+nHcJ1TjaU3KXSRVlgXxxl3glKW0EZDjkOr4QW2o" +
                "a6Y38TlrHUhm4TjX2rNFEOUl2XMUsd1wlmUQGiVKxj52zvFyrckNlrxKMd4jEEF98w9d55x5iG3mOW/8" +
                "C6vcCOrIxpDDkvGy2V32BwU69jirAQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
