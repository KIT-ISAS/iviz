/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestActionGoal : IDeserializable<TestActionGoal>, IHasSerializer<TestActionGoal>, IMessage, IActionGoal<TestGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public TestGoal Goal { get; set; }
    
        public TestActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new TestGoal();
        }
        
        public TestActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, TestGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        public TestActionGoal(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new TestGoal(ref b);
        }
        
        public TestActionGoal(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new TestGoal(ref b);
        }
        
        public TestActionGoal RosDeserialize(ref ReadBuffer b) => new TestActionGoal(ref b);
        
        public TestActionGoal RosDeserialize(ref ReadBuffer2 b) => new TestActionGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) BuiltIns.ThrowNullReference();
            GoalId.RosValidate();
            if (Goal is null) BuiltIns.ThrowNullReference();
            Goal.RosValidate();
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + GoalId.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = GoalId.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 4; // Goal
            return c;
        }
    
        public const string MessageType = "actionlib/TestActionGoal";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "348369c5b403676156094e8c159720bf";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VUwYrbMBC9+ysGctjdQrK0vQV6W7qbQ6F0cyslTKSJLSpLrkZO6r/vk5ykW+ihh64x" +
                "GMszb97Me+MnYSuJuvpo2GQXg3f7Xa+t3j9G9psHavHYOdtsRXM5qgfNh/98NZ+eH9ek2c61n2ZGC3rO" +
                "HCwnS71ktpyZDhGEXdtJWno5ikcS94NYql/zNIiukLjtnBLuVoIk9n6iURGUI5nY92NwhrNQdr38kY9M" +
                "F4hp4JSdGT0nxMdkXSjhh8S9FHTcKj9GCUZo87BGTFAxY3YgNAHBJGF1ocVHakYX8vt3JYEW9PVL1Lff" +
                "msX2FJc4lxbzv7Kg3HEurOXnkEQLYdY1ir2Zu1yhCKYkKGeVbuvZDq96R6gGLjJE09EtWvg85S4GAAod" +
                "OTneeynABqMA6k1Jurl7gRwqdOAQL/Az4u8a/wIbrrilp2UH8XwZg44tJonAIcWjswjdTxXEeCchE0yX" +
                "OE1NyZpLNouPZdgIQlaVBk9WjcZBCUsnl7tGcyroVZbi0Vey5V8Xo3rsTJa0i6O3eIlJal+1EWh56hwE" +
                "qU2UvaETK6XiHEUTxUmbqnf1JkbC4VwMIqcjrHHqJJDLhEZFi3vhC+mHTBg4sgumzq45CUpfoWkvh8KF" +
                "yUjKDOUKo5fzPfN39qIJxgt6UylynTMdROyezXcws8iAKUefsYyq3EoVgXQQ4w7OzA2eGejqjF42ZQ4A" +
                "qX7UDGaE9UPU6qJfUe61pbu//L+aeRvrX+wXBfrCcAAFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<TestActionGoal> CreateSerializer() => new Serializer();
        public Deserializer<TestActionGoal> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TestActionGoal>
        {
            public override void RosSerialize(TestActionGoal msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(TestActionGoal msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(TestActionGoal msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(TestActionGoal msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<TestActionGoal>
        {
            public override void RosDeserialize(ref ReadBuffer b, out TestActionGoal msg) => msg = new TestActionGoal(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out TestActionGoal msg) => msg = new TestActionGoal(ref b);
        }
    }
}
