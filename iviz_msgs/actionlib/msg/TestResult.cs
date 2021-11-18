/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TestResult")]
    public sealed class TestResult : IDeserializable<TestResult>, IResult<TestActionResult>
    {
        [DataMember (Name = "result")] public int Result;
    
        /// <summary> Constructor for empty message. </summary>
        public TestResult()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestResult(int Result)
        {
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestResult(ref Buffer b)
        {
            Result = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestResult(ref b);
        }
        
        TestResult IDeserializable<TestResult>.RosDeserialize(ref Buffer b)
        {
            return new TestResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
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
                "H4sIAAAAAAAACsvMKzE2UihKLS7NKeHlAgBrYhqDDgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
