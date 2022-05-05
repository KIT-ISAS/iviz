/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [DataContract]
    public sealed class ObjectRecognitionFeedback : IDeserializable<ObjectRecognitionFeedback>, IFeedback<ObjectRecognitionActionFeedback>
    {
        //no feedback
    
        /// Constructor for empty message.
        public ObjectRecognitionFeedback()
        {
        }
        
        /// Constructor with buffer.
        public ObjectRecognitionFeedback(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public ObjectRecognitionFeedback RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static ObjectRecognitionFeedback? singleton;
        public static ObjectRecognitionFeedback Singleton => singleton ??= new ObjectRecognitionFeedback();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "object_recognition_msgs/ObjectRecognitionFeedback";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => BuiltIns.EmptyMd5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 => BuiltIns.EmptyDependenciesBase64;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
