/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
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
        public TestFeedback(ref ReadBuffer b)
        {
            b.Deserialize(out Feedback);
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
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib/TestFeedback";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "49ceb5b32ea3af22073ede4a0328249e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMKzE2UkhLTU1JSkzO5uICAIj0iCsQAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
