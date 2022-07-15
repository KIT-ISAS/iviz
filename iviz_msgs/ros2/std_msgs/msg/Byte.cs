/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.StdMsgs
{
    [DataContract]
    public sealed class Byte : IDeserializable<Byte>, IMessageRos2
    {
        // This was originally provided as an example message.
        // It is deprecated as of Foxy
        // It is recommended to create your own semantically meaningful message.
        // However if you would like to continue using this please use the equivalent in example_msgs.
        [DataMember (Name = "data")] public byte Data;
    
        /// Constructor for empty message.
        public Byte()
        {
        }
        
        /// Explicit constructor.
        public Byte(byte Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public Byte(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        public Byte RosDeserialize(ref ReadBuffer2 b) => new Byte(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 1;
        
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, Data);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Byte";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
