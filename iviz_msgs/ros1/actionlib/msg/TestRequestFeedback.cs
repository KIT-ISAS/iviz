/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestRequestFeedback : IDeserializable<TestRequestFeedback>, IMessage, IFeedback<TestRequestActionFeedback>
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
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => 0;
        
        public int AddRos2MessageLength(int c) => c;
    
        public const string MessageType = "actionlib/TestRequestFeedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = BuiltIns.EmptyMd5Sum;
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 => BuiltIns.EmptyDependenciesBase64;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
