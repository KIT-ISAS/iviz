/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestGoal : IDeserializable<TestGoal>, IHasSerializer<TestGoal>, IMessage, IGoal<TestActionGoal>
    {
        [DataMember (Name = "goal")] public int Goal;
    
        public TestGoal()
        {
        }
        
        public TestGoal(int Goal)
        {
            this.Goal = Goal;
        }
        
        public TestGoal(ref ReadBuffer b)
        {
            b.Deserialize(out Goal);
        }
        
        public TestGoal(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out Goal);
        }
        
        public TestGoal RosDeserialize(ref ReadBuffer b) => new TestGoal(ref b);
        
        public TestGoal RosDeserialize(ref ReadBuffer2 b) => new TestGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Goal);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Goal);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 4;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "actionlib/TestGoal";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "18df0149936b7aa95588e3862476ebde";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMKzE2UkjPT8zhAgAyerI5CwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<TestGoal> CreateSerializer() => new Serializer();
        public Deserializer<TestGoal> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TestGoal>
        {
            public override void RosSerialize(TestGoal msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(TestGoal msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(TestGoal msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(TestGoal msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<TestGoal>
        {
            public override void RosDeserialize(ref ReadBuffer b, out TestGoal msg) => msg = new TestGoal(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out TestGoal msg) => msg = new TestGoal(ref b);
        }
    }
}
