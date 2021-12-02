/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ObjectRecognitionFeedback : IDeserializable<ObjectRecognitionFeedback>, IFeedback<ObjectRecognitionActionFeedback>
    {
        //no feedback
    
        /// Constructor for empty message.
        public ObjectRecognitionFeedback()
        {
        }
        
        /// Constructor with buffer.
        internal ObjectRecognitionFeedback(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => Singleton;
        
        ObjectRecognitionFeedback IDeserializable<ObjectRecognitionFeedback>.RosDeserialize(ref Buffer b) => Singleton;
        
        public static readonly ObjectRecognitionFeedback Singleton = new ObjectRecognitionFeedback();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = BuiltIns.EmptyMd5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACuMCAJMG1zIBAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
