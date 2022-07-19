/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [DataContract]
    public sealed class LookupTransformFeedback : IDeserializableRos1<LookupTransformFeedback>, IDeserializableRos2<LookupTransformFeedback>, IMessageRos1, IMessageRos2, IFeedback<LookupTransformActionFeedback>
    {
        /// Constructor for empty message.
        public LookupTransformFeedback()
        {
        }
        
        /// Constructor with buffer.
        public LookupTransformFeedback(ref ReadBuffer b)
        {
        }
        
        /// Constructor with buffer.
        public LookupTransformFeedback(ref ReadBuffer2 b)
        {
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public LookupTransformFeedback RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public LookupTransformFeedback RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static LookupTransformFeedback? singleton;
        public static LookupTransformFeedback Singleton => singleton ??= new LookupTransformFeedback();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => 0;
        
        public void AddRos2MessageLength(ref int c) { }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "tf2_msgs/LookupTransformFeedback";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = BuiltIns.EmptyMd5Sum;
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 => BuiltIns.EmptyDependenciesBase64;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
