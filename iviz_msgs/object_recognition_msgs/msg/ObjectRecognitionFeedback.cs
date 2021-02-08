/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/ObjectRecognitionFeedback")]
    public sealed class ObjectRecognitionFeedback : IDeserializable<ObjectRecognitionFeedback>, IFeedback<ObjectRecognitionActionFeedback>
    {
        //no feedback
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectRecognitionFeedback()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ObjectRecognitionFeedback(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        ObjectRecognitionFeedback IDeserializable<ObjectRecognitionFeedback>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly ObjectRecognitionFeedback Singleton = new ObjectRecognitionFeedback();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d41d8cd98f00b204e9800998ecf8427e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAA+MCAJMG1zIBAAAA";
                
    }
}
