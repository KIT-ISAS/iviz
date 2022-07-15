/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.StdMsgs
{
    [DataContract]
    public sealed class Empty : IDeserializable<Empty>, IMessageRos2
    {
        /// Constructor for empty message.
        public Empty()
        {
        }
        
        /// Constructor with buffer.
        public Empty(ref ReadBuffer2 b)
        {
        }
        
        public Empty RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static Empty? singleton;
        public static Empty Singleton => singleton ??= new Empty();
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public void GetRosMessageLength(ref int c) { }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Empty";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
