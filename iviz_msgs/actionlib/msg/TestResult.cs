/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TestResult : IDeserializable<TestResult>, IResult<TestActionResult>
    {
        [DataMember (Name = "result")] public int Result;
    
        /// Constructor for empty message.
        public TestResult()
        {
        }
        
        /// Explicit constructor.
        public TestResult(int Result)
        {
            this.Result = Result;
        }
        
        /// Constructor with buffer.
        public TestResult(ref ReadBuffer b)
        {
            b.Deserialize(out Result);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestResult(ref b);
        
        public TestResult RosDeserialize(ref ReadBuffer b) => new TestResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Result);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TestResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "034a8e20d6a306665e3a5b340fab3f09";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzE2UihKLS7NKeECAJhJ6VgNAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
