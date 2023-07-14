/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class GetMapGoal : IHasSerializer<GetMapGoal>, IMessage, IGoal<GetMapActionGoal>
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
        
        [IgnoreDataMember] public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 0;
        
        [IgnoreDataMember] public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => c;
    
        public const string MessageType = "nav_msgs/GetMapGoal";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = BuiltIns.EmptyMd5Sum;
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember] public string RosDependenciesBase64 => BuiltIns.EmptyDependenciesBase64;
    
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<GetMapGoal> CreateSerializer() => new Serializer();
        public Deserializer<GetMapGoal> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<GetMapGoal>
        {
        }
    
        sealed class Deserializer : Deserializer<GetMapGoal>
        {
            public override void RosDeserialize(ref ReadBuffer _, out GetMapGoal msg) => msg = Singleton;
            public override void RosDeserialize(ref ReadBuffer2 _, out GetMapGoal msg) => msg = Singleton;
        }
    }
}
