/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class LookupTransformFeedback : IDeserializable<LookupTransformFeedback>, IFeedback<LookupTransformActionFeedback>
    {
        /// Constructor for empty message.
        public LookupTransformFeedback()
        {
        }
        
        /// Constructor with buffer.
        internal LookupTransformFeedback(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => Singleton;
        
        LookupTransformFeedback IDeserializable<LookupTransformFeedback>.RosDeserialize(ref Buffer b) => Singleton;
        
        public static readonly LookupTransformFeedback Singleton = new LookupTransformFeedback();
    
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
        [Preserve] public const string RosMessageType = "tf2_msgs/LookupTransformFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = BuiltIns.EmptyMd5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACuMCAJMG1zIBAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
