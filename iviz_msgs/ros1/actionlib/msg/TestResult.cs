/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestResult : IDeserializable<TestResult>, IMessage, IResult<TestActionResult>
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
            b.Deserialize(out Result);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new TestResult(ref b);
        
        public TestResult RosDeserialize(ref ReadBuffer b) => new TestResult(ref b);
        
        public TestResult RosDeserialize(ref ReadBuffer2 b) => new TestResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Result);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Result);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 4;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Result);
        }
    
        public const string MessageType = "actionlib/TestResult";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "034a8e20d6a306665e3a5b340fab3f09";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMKzE2UihKLS7NKeECAJhJ6VgNAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
