/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.StdMsgs
{
    [DataContract]
    public sealed class UInt64 : IDeserializable<UInt64>, IMessageRos2
    {
        // This was originally provided as an example message.
        // It is deprecated as of Foxy
        // It is recommended to create your own semantically meaningful message.
        // However if you would like to continue using this please use the equivalent in example_msgs.
        [DataMember (Name = "data")] public ulong Data;
    
        /// Constructor for empty message.
        public UInt64()
        {
        }
        
        /// Explicit constructor.
        public UInt64(ulong Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public UInt64(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        public UInt64 RosDeserialize(ref ReadBuffer2 b) => new UInt64(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 8;
        
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, Data);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/UInt64";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
