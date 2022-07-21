/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestRequestResult : IDeserializableCommon<TestRequestResult>, IMessageCommon, IResult<TestRequestActionResult>
    {
        [DataMember (Name = "the_result")] public int TheResult;
        [DataMember (Name = "is_simple_server")] public bool IsSimpleServer;
    
        public TestRequestResult()
        {
        }
        
        public TestRequestResult(int TheResult, bool IsSimpleServer)
        {
            this.TheResult = TheResult;
            this.IsSimpleServer = IsSimpleServer;
        }
        
        public TestRequestResult(ref ReadBuffer b)
        {
            b.Deserialize(out TheResult);
            b.Deserialize(out IsSimpleServer);
        }
        
        public TestRequestResult(ref ReadBuffer2 b)
        {
            b.Deserialize(out TheResult);
            b.Deserialize(out IsSimpleServer);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new TestRequestResult(ref b);
        
        public TestRequestResult RosDeserialize(ref ReadBuffer b) => new TestRequestResult(ref b);
        
        public TestRequestResult RosDeserialize(ref ReadBuffer2 b) => new TestRequestResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(TheResult);
            b.Serialize(IsSimpleServer);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(TheResult);
            b.Serialize(IsSimpleServer);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 5;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 5;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, TheResult);
            WriteBuffer2.AddLength(ref c, IsSimpleServer);
        }
    
        public const string MessageType = "actionlib/TestRequestResult";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "61c2364524499c7c5017e2f3fce7ba06";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMKzE2UijJSI0vSi0uzSnhSsrPz1HILI4vzswtyEmNL04tKkst4gIAoJWh1ycAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
