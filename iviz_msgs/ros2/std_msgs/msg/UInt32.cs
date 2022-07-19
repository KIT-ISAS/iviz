/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.StdMsgs
{
    [DataContract]
    public sealed class UInt32 : IDeserializableRos2<UInt32>, IMessageRos2
    {
        // This was originally provided as an example message.
        // It is deprecated as of Foxy
        // It is recommended to create your own semantically meaningful message.
        // However if you would like to continue using this please use the equivalent in example_msgs.
        [DataMember (Name = "data")] public uint Data;
    
        /// Constructor for empty message.
        public UInt32()
        {
        }
        
        /// Explicit constructor.
        public UInt32(uint Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public UInt32(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        public UInt32 RosDeserialize(ref ReadBuffer2 b) => new UInt32(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public void AddRosMessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Data);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/UInt32";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
