/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.StdMsgs
{
    [DataContract]
    public sealed class Int8 : IDeserializableRos2<Int8>, IMessageRos2
    {
        // This was originally provided as an example message.
        // It is deprecated as of Foxy
        // It is recommended to create your own semantically meaningful message.
        // However if you would like to continue using this please use the equivalent in example_msgs.
        [DataMember (Name = "data")] public sbyte Data;
    
        /// Constructor for empty message.
        public Int8()
        {
        }
        
        /// Explicit constructor.
        public Int8(sbyte Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public Int8(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        public Int8 RosDeserialize(ref ReadBuffer2 b) => new Int8(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public void AddRosMessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Data);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Int8";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
