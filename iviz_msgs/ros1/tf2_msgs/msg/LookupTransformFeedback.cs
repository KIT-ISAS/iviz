/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [DataContract]
    public sealed class LookupTransformFeedback : IHasSerializer<LookupTransformFeedback>, IMessage, IFeedback<LookupTransformActionFeedback>
    {
        public LookupTransformFeedback()
        {
        }
        
        public LookupTransformFeedback(ref ReadBuffer b)
        {
        }
        
        public LookupTransformFeedback(ref ReadBuffer2 b)
        {
        }
        
        public LookupTransformFeedback RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public LookupTransformFeedback RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static LookupTransformFeedback? singleton;
        public static LookupTransformFeedback Singleton => singleton ??= new LookupTransformFeedback();
    
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
    
        public const string MessageType = "tf2_msgs/LookupTransformFeedback";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = BuiltIns.EmptyMd5Sum;
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember] public string RosDependenciesBase64 => BuiltIns.EmptyDependenciesBase64;
    
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<LookupTransformFeedback> CreateSerializer() => new Serializer();
        public Deserializer<LookupTransformFeedback> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<LookupTransformFeedback>
        {
        }
    
        sealed class Deserializer : Deserializer<LookupTransformFeedback>
        {
            public override void RosDeserialize(ref ReadBuffer _, out LookupTransformFeedback msg) => msg = Singleton;
            public override void RosDeserialize(ref ReadBuffer2 _, out LookupTransformFeedback msg) => msg = Singleton;
        }
    }
}
