/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TestRequestFeedback : IDeserializable<TestRequestFeedback>, IFeedback<TestRequestActionFeedback>
    {
    
        /// Constructor for empty message.
        public TestRequestFeedback()
        {
        }
        
        /// Constructor with buffer.
        internal TestRequestFeedback(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => Singleton;
        
        TestRequestFeedback IDeserializable<TestRequestFeedback>.RosDeserialize(ref Buffer b) => Singleton;
        
        public static readonly TestRequestFeedback Singleton = new TestRequestFeedback();
    
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
        [Preserve] public const string RosMessageType = "actionlib/TestRequestFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = BuiltIns.EmptyMd5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACuMCAJMG1zIBAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
