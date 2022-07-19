/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class PointField : IDeserializableRos1<PointField>, IDeserializableRos2<PointField>, IMessageRos1, IMessageRos2
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
        /// <summary> Name of field </summary>
        [DataMember (Name = "name")] public string Name;
        /// <summary> Offset from start of point struct </summary>
        [DataMember (Name = "offset")] public uint Offset;
        /// <summary> Datatype enumeration, see above </summary>
        [DataMember (Name = "datatype")] public byte Datatype;
        /// <summary> How many elements in the field </summary>
        [DataMember (Name = "count")] public uint Count;
    
        /// Constructor for empty message.
        public PointField()
        {
            Name = "";
        }
        
        /// Explicit constructor.
        public PointField(string Name, uint Offset, byte Datatype, uint Count)
        {
            this.Name = Name;
            this.Offset = Offset;
            this.Datatype = Datatype;
            this.Count = Count;
        }
        
        /// Constructor with buffer.
        public PointField(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.Deserialize(out Offset);
            b.Deserialize(out Datatype);
            b.Deserialize(out Count);
        }
        
        /// Constructor with buffer.
        public PointField(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Name);
            b.Deserialize(out Offset);
            b.Deserialize(out Datatype);
            b.Deserialize(out Count);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new PointField(ref b);
        
        public PointField RosDeserialize(ref ReadBuffer b) => new PointField(ref b);
        
        public PointField RosDeserialize(ref ReadBuffer2 b) => new PointField(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Offset);
            b.Serialize(Datatype);
            b.Serialize(Count);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Name);
            b.Serialize(Offset);
            b.Serialize(Datatype);
            b.Serialize(Count);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 13 + WriteBuffer.GetStringSize(Name);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Name);
            WriteBuffer2.AddLength(ref c, Offset);
            WriteBuffer2.AddLength(ref c, Datatype);
            WriteBuffer2.AddLength(ref c, Count);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/PointField";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "268eacb2962780ceac86cbd17e328150";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE02Qz06EMBCH7zzFL+FqTJZdkQsHozGamF0P6wNUGJYmtEPaQcPb20LXZU6Tr9/86eQ4" +
                "99rDkPfqQuh5aD2kJ7TkG6dH0WzBHdgSRtZWQFbcDG2jleX4jPB54Kkt/rt07IyS+2wKTxXej+cKIWrs" +
                "EvlKqEZxc3blQvYbJ6Iah5uzLxbnYeNEVKNM5PXj9BRRjcctKQ+BVFnmxWl7gVWGsESOY8zDBztNQ7vU" +
                "hHLuOk+yCqc17xwbeFFOor2eIrSbGkmD0CpRMo8Ui16uOdnJkFPxjHfwRFDf/EPXOQ1PVtIib/wLo+wM" +
                "GsiEI/t047RZ9gctJPa/qgEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
