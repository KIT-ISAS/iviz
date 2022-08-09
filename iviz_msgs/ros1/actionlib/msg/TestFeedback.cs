/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestFeedback : IDeserializable<TestFeedback>, IMessage, IFeedback<TestActionFeedback>
    {
        [DataMember (Name = "feedback")] public int Feedback;
    
        public TestFeedback()
        {
        }
        
        public TestFeedback(int Feedback)
        {
            this.Feedback = Feedback;
        }
        
        public TestFeedback(ref ReadBuffer b)
        {
            b.Deserialize(out Feedback);
        }
        
        public TestFeedback(ref ReadBuffer2 b)
        {
            b.Deserialize(out Feedback);
        }
        
        public TestFeedback RosDeserialize(ref ReadBuffer b) => new TestFeedback(ref b);
        
        public TestFeedback RosDeserialize(ref ReadBuffer2 b) => new TestFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Feedback);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Feedback);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 4;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "actionlib/TestFeedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "49ceb5b32ea3af22073ede4a0328249e";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMKzE2UkhLTU1JSkzO5uICAIj0iCsQAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
