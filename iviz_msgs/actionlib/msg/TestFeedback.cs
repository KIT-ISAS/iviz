/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TestFeedback : IDeserializable<TestFeedback>, IFeedback<TestActionFeedback>
    {
        [DataMember (Name = "feedback")] public int Feedback;
    
        /// Constructor for empty message.
        public TestFeedback()
        {
        }
        
        /// Explicit constructor.
        public TestFeedback(int Feedback)
        {
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        internal TestFeedback(ref ReadBuffer b)
        {
            Feedback = b.Deserialize<int>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestFeedback(ref b);
        
        public TestFeedback RosDeserialize(ref ReadBuffer b) => new TestFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Feedback);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib/TestFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "49ceb5b32ea3af22073ede4a0328249e";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzE2UkhLTU1JSkzO5uICAIj0iCsQAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
