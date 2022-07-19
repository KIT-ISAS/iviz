/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class AveragingFeedback : IDeserializableRos1<AveragingFeedback>, IDeserializableRos2<AveragingFeedback>, IMessageRos1, IMessageRos2, IFeedback<AveragingActionFeedback>
    {
        //feedback
    
        /// Constructor for empty message.
        public AveragingFeedback()
        {
        }
        
        /// Constructor with buffer.
        public AveragingFeedback(ref ReadBuffer b)
        {
        }
        
        /// Constructor with buffer.
        public AveragingFeedback(ref ReadBuffer2 b)
        {
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
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
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => 0;
        
        public void AddRos2MessageLength(ref int c) { }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib_tutorials/AveragingFeedback";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = BuiltIns.EmptyMd5Sum;
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+PiAgBrE+NbAgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
