/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class GetMapGoal : IDeserializable<GetMapGoal>, IHasSerializer<GetMapGoal>, IMessage, IGoal<GetMapActionGoal>
    {
        // Get the map as a nav_msgs/OccupancyGrid
    
        public GetMapGoal()
        {
        }
        
        public GetMapGoal(ref ReadBuffer b)
        {
        }
        
        public GetMapGoal(ref ReadBuffer2 b)
        {
        }
        
        public GetMapGoal RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public GetMapGoal RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static GetMapGoal? singleton;
        public static GetMapGoal Singleton => singleton ??= new GetMapGoal();
    
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
    
        public const string MessageType = "nav_msgs/GetMapGoal";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = BuiltIns.EmptyMd5Sum;
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 => BuiltIns.EmptyDependenciesBase64;
    
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<GetMapGoal> CreateSerializer() => new Serializer();
        public Deserializer<GetMapGoal> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<GetMapGoal>
        {
            public override void RosSerialize(GetMapGoal msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(GetMapGoal msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(GetMapGoal msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(GetMapGoal msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<GetMapGoal>
        {
            public override void RosDeserialize(ref ReadBuffer b, out GetMapGoal msg) => msg = new GetMapGoal(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out GetMapGoal msg) => msg = new GetMapGoal(ref b);
        }
    }
}
