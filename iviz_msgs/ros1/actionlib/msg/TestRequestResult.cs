/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestRequestResult : IDeserializableRos1<TestRequestResult>, IMessageRos1, IResult<TestRequestActionResult>
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
        public TestRequestResult(ref ReadBuffer b)
        {
            b.Deserialize(out TheResult);
            b.Deserialize(out IsSimpleServer);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestRequestResult(ref b);
        
        public TestRequestResult RosDeserialize(ref ReadBuffer b) => new TestRequestResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(TheResult);
            b.Serialize(IsSimpleServer);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 5;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib/TestRequestResult";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "61c2364524499c7c5017e2f3fce7ba06";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMKzE2UijJSI0vSi0uzSnhSsrPz1HILI4vzswtyEmNL04tKkst4gIAoJWh1ycAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
