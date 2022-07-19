/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.StdMsgs
{
    [DataContract]
    public sealed class Bool : IDeserializableRos2<Bool>, IMessageRos2
    {
        // This was originally provided as an example message.
        // It is deprecated as of Foxy
        // It is recommended to create your own semantically meaningful message.
        // However if you would like to continue using this please use the equivalent in example_msgs.
        [DataMember (Name = "data")] public bool Data;
    
        /// Constructor for empty message.
        public Bool()
        {
        }
        
        /// Explicit constructor.
        public Bool(bool Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public Bool(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        public Bool RosDeserialize(ref ReadBuffer2 b) => new Bool(ref b);
    
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
        public const string MessageType = "std_msgs/Bool";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
