/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TwoIntsFeedback : IDeserializableRos1<TwoIntsFeedback>, IDeserializableRos2<TwoIntsFeedback>, IMessageRos1, IMessageRos2, IFeedback<TwoIntsActionFeedback>
    {
    
        /// Constructor for empty message.
        public TwoIntsFeedback()
        {
        }
        
        /// Constructor with buffer.
        public TwoIntsFeedback(ref ReadBuffer b)
        {
        }
        
        /// Constructor with buffer.
        public TwoIntsFeedback(ref ReadBuffer2 b)
        {
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public TwoIntsFeedback RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public TwoIntsFeedback RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static TwoIntsFeedback? singleton;
        public static TwoIntsFeedback Singleton => singleton ??= new TwoIntsFeedback();
    
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
        public const string MessageType = "actionlib/TwoIntsFeedback";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = BuiltIns.EmptyMd5Sum;
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 => BuiltIns.EmptyDependenciesBase64;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
