/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TwoIntsFeedback : IDeserializable<TwoIntsFeedback>, IFeedback<TwoIntsActionFeedback>
    {
    
        /// Constructor for empty message.
        public TwoIntsFeedback()
        {
        }
        
        /// Constructor with buffer.
        internal TwoIntsFeedback(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => Singleton;
        
        TwoIntsFeedback IDeserializable<TwoIntsFeedback>.RosDeserialize(ref Buffer b) => Singleton;
        
        public static readonly TwoIntsFeedback Singleton = new TwoIntsFeedback();
    
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
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = BuiltIns.EmptyMd5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACuMCAJMG1zIBAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
