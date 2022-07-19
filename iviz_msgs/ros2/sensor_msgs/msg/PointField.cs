/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class PointField : IDeserializableRos2<PointField>, IMessageRos2
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
        // Common PointField names are x, y, z, intensity, rgb, rgba
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
        public PointField(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Name);
            b.Deserialize(out Offset);
            b.Deserialize(out Datatype);
            b.Deserialize(out Count);
        }
        
        public PointField RosDeserialize(ref ReadBuffer2 b) => new PointField(ref b);
    
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
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Name);
            WriteBuffer2.AddLength(ref c, Offset);
            WriteBuffer2.AddLength(ref c, Datatype);
            WriteBuffer2.AddLength(ref c, Count);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/PointField";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
