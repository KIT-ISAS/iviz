/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class AveragingFeedback : IDeserializable<AveragingFeedback>, IMessage, IFeedback<AveragingActionFeedback>
    {
        //feedback
    
        public AveragingFeedback()
        {
        }
        
        public AveragingFeedback(ref ReadBuffer b)
        {
        }
        
        public AveragingFeedback(ref ReadBuffer2 b)
        {
        }
        
        public AveragingFeedback RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public AveragingFeedback RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static AveragingFeedback? singleton;
        public static AveragingFeedback Singleton => singleton ??= new AveragingFeedback();
    
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
    
        public const string MessageType = "actionlib_tutorials/AveragingFeedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = BuiltIns.EmptyMd5Sum;
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+PiAgBrE+NbAgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
