/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class GetMapFeedback : IDeserializable<GetMapFeedback>, IHasSerializer<GetMapFeedback>, IMessage, IFeedback<GetMapActionFeedback>
    {
        // no feedback
    
        public GetMapFeedback()
        {
        }
        
        public GetMapFeedback(ref ReadBuffer b)
        {
        }
        
        public GetMapFeedback(ref ReadBuffer2 b)
        {
        }
        
        public GetMapFeedback RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public GetMapFeedback RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static GetMapFeedback? singleton;
        public static GetMapFeedback Singleton => singleton ??= new GetMapFeedback();
    
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
        
        public const int Ros2FixedMessageLength = 0;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => c;
    
        public const string MessageType = "nav_msgs/GetMapFeedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = BuiltIns.EmptyMd5Sum;
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 => BuiltIns.EmptyDependenciesBase64;
    
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<GetMapFeedback> CreateSerializer() => new Serializer();
        public Deserializer<GetMapFeedback> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<GetMapFeedback>
        {
            public override void RosSerialize(GetMapFeedback msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(GetMapFeedback msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(GetMapFeedback _) => RosFixedMessageLength;
            public override int Ros2MessageLength(GetMapFeedback _) => Ros2FixedMessageLength;
        }
        sealed class Deserializer : Deserializer<GetMapFeedback>
        {
            public override void RosDeserialize(ref ReadBuffer _, out GetMapFeedback msg) => msg = Singleton;
            public override void RosDeserialize(ref ReadBuffer2 _, out GetMapFeedback msg) => msg = Singleton;
        }
    }
}
