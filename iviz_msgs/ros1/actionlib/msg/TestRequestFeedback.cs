/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestRequestFeedback : IHasSerializer<TestRequestFeedback>, IMessage, IFeedback<TestRequestActionFeedback>
    {
    
        public TestRequestFeedback()
        {
        }
        
        public TestRequestFeedback(ref ReadBuffer b)
        {
        }
        
        public TestRequestFeedback(ref ReadBuffer2 b)
        {
        }
        
        public TestRequestFeedback RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public TestRequestFeedback RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static TestRequestFeedback? singleton;
        public static TestRequestFeedback Singleton => singleton ??= new TestRequestFeedback();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 0;
        
        [IgnoreDataMember] public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 0;
        
        [IgnoreDataMember] public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => c;
    
        public const string MessageType = "actionlib/TestRequestFeedback";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = BuiltIns.EmptyMd5Sum;
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember] public string RosDependenciesBase64 => BuiltIns.EmptyDependenciesBase64;
    
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<TestRequestFeedback> CreateSerializer() => new Serializer();
        public Deserializer<TestRequestFeedback> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TestRequestFeedback>
        {
        }
    
        sealed class Deserializer : Deserializer<TestRequestFeedback>
        {
            public override void RosDeserialize(ref ReadBuffer _, out TestRequestFeedback msg) => msg = Singleton;
            public override void RosDeserialize(ref ReadBuffer2 _, out TestRequestFeedback msg) => msg = Singleton;
        }
    }
}
