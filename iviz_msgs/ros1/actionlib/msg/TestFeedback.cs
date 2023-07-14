/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestFeedback : IHasSerializer<TestFeedback>, IMessage, IFeedback<TestActionFeedback>
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
            b.Align4();
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
            b.Align4();
            b.Serialize(Feedback);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 4;
        
        [IgnoreDataMember] public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 4;
        
        [IgnoreDataMember] public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "actionlib/TestFeedback";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "49ceb5b32ea3af22073ede4a0328249e";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMKzE2UkhLTU1JSkzO5uICAIj0iCsQAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<TestFeedback> CreateSerializer() => new Serializer();
        public Deserializer<TestFeedback> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TestFeedback>
        {
            public override void RosSerialize(TestFeedback msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(TestFeedback msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(TestFeedback _) => RosFixedMessageLength;
            public override int Ros2MessageLength(TestFeedback _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<TestFeedback>
        {
            public override void RosDeserialize(ref ReadBuffer b, out TestFeedback msg) => msg = new TestFeedback(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out TestFeedback msg) => msg = new TestFeedback(ref b);
        }
    }
}
