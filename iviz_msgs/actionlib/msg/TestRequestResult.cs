/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TestRequestResult : IDeserializable<TestRequestResult>, IResult<TestRequestActionResult>
    {
        [DataMember (Name = "the_result")] public int TheResult;
        [DataMember (Name = "is_simple_server")] public bool IsSimpleServer;
    
        /// Constructor for empty message.
        public TestRequestResult()
        {
        }
        
        /// Explicit constructor.
        public TestRequestResult(int TheResult, bool IsSimpleServer)
        {
            this.TheResult = TheResult;
            this.IsSimpleServer = IsSimpleServer;
        }
        
        /// Constructor with buffer.
        internal TestRequestResult(ref Buffer b)
        {
            TheResult = b.Deserialize<int>();
            IsSimpleServer = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestRequestResult(ref b);
        
        TestRequestResult IDeserializable<TestRequestResult>.RosDeserialize(ref Buffer b) => new TestRequestResult(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(TheResult);
            b.Serialize(IsSimpleServer);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 5;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib/TestRequestResult";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "61c2364524499c7c5017e2f3fce7ba06";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzE2UijJSI0vSi0uzSnhSsrPz1HILI4vzswtyEmNL04tKkst4gIAoJWh1ycAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
