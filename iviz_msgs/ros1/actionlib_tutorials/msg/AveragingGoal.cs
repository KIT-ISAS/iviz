/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class AveragingGoal : IDeserializable<AveragingGoal>, IHasSerializer<AveragingGoal>, IMessage, IGoal<AveragingActionGoal>
    {
        //goal definition
        [DataMember (Name = "edges")] public int Edges;
        [DataMember (Name = "radius")] public float Radius;
    
        public AveragingGoal()
        {
        }
        
        public AveragingGoal(int Edges, float Radius)
        {
            this.Edges = Edges;
            this.Radius = Radius;
        }
        
        public AveragingGoal(ref ReadBuffer b)
        {
            b.Deserialize(out Edges);
            b.Deserialize(out Radius);
        }
        
        public AveragingGoal(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out Edges);
            b.Deserialize(out Radius);
        }
        
        public AveragingGoal RosDeserialize(ref ReadBuffer b) => new AveragingGoal(ref b);
        
        public AveragingGoal RosDeserialize(ref ReadBuffer2 b) => new AveragingGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Edges);
            b.Serialize(Radius);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Edges);
            b.Serialize(Radius);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 8;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "actionlib_tutorials/AveragingGoal";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "3b9202ab7292cebe5a95ab2bf6b9c091";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+PKzCsxNlJITUlPLeZKy8lPBPGKElMyS4u5AJplSSgcAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<AveragingGoal> CreateSerializer() => new Serializer();
        public Deserializer<AveragingGoal> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<AveragingGoal>
        {
            public override void RosSerialize(AveragingGoal msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(AveragingGoal msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(AveragingGoal _) => RosFixedMessageLength;
            public override int Ros2MessageLength(AveragingGoal _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<AveragingGoal>
        {
            public override void RosDeserialize(ref ReadBuffer b, out AveragingGoal msg) => msg = new AveragingGoal(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out AveragingGoal msg) => msg = new AveragingGoal(ref b);
        }
    }
}
