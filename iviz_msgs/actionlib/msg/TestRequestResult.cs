/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TestRequestResult")]
    public sealed class TestRequestResult : IDeserializable<TestRequestResult>, IResult<TestRequestActionResult>
    {
        [DataMember (Name = "the_result")] public int TheResult { get; set; }
        [DataMember (Name = "is_simple_server")] public bool IsSimpleServer { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestRequestResult()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestRequestResult(int TheResult, bool IsSimpleServer)
        {
            this.TheResult = TheResult;
            this.IsSimpleServer = IsSimpleServer;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestRequestResult(ref Buffer b)
        {
            TheResult = b.Deserialize<int>();
            IsSimpleServer = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestRequestResult(ref b);
        }
        
        TestRequestResult IDeserializable<TestRequestResult>.RosDeserialize(ref Buffer b)
        {
            return new TestRequestResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(TheResult);
            b.Serialize(IsSimpleServer);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 5;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TestRequestResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "61c2364524499c7c5017e2f3fce7ba06";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzE2UijJSI0vSi0uzSnhSsrPz1HILI4vzswtyEmNL04tKkst4gIAoJWh1ycAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
