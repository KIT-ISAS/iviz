/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestResult : IHasSerializer<TestResult>, IMessage, IResult<TestActionResult>
    {
        [DataMember (Name = "result")] public int Result;
    
        public TestResult()
        {
        }
        
        public TestResult(int Result)
        {
            this.Result = Result;
        }
        
        public TestResult(ref ReadBuffer b)
        {
            b.Deserialize(out Result);
        }
        
        public TestResult(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out Result);
        }
        
        public TestResult RosDeserialize(ref ReadBuffer b) => new TestResult(ref b);
        
        public TestResult RosDeserialize(ref ReadBuffer2 b) => new TestResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Result);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Result);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 4;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "actionlib/TestResult";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "034a8e20d6a306665e3a5b340fab3f09";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMKzE2UihKLS7NKeECAJhJ6VgNAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<TestResult> CreateSerializer() => new Serializer();
        public Deserializer<TestResult> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TestResult>
        {
            public override void RosSerialize(TestResult msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(TestResult msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(TestResult _) => RosFixedMessageLength;
            public override int Ros2MessageLength(TestResult _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<TestResult>
        {
            public override void RosDeserialize(ref ReadBuffer b, out TestResult msg) => msg = new TestResult(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out TestResult msg) => msg = new TestResult(ref b);
        }
    }
}
