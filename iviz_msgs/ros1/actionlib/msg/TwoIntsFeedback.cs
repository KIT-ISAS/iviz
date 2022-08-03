/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TwoIntsFeedback : IDeserializable<TwoIntsFeedback>, IMessage, IFeedback<TwoIntsActionFeedback>
    {
    
        public TwoIntsFeedback()
        {
        }
        
        public TwoIntsFeedback(ref ReadBuffer b)
        {
        }
        
        public TwoIntsFeedback(ref ReadBuffer2 b)
        {
        }
        
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
    
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => 0;
        
        public void AddRos2MessageLength(ref int _) { }
    
        public const string MessageType = "actionlib/TwoIntsFeedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = BuiltIns.EmptyMd5Sum;
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 => BuiltIns.EmptyDependenciesBase64;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
