/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [DataContract]
    public sealed class ShapeGoal : IHasSerializer<ShapeGoal>, IMessage, IGoal<ShapeActionGoal>
    {
        //goal definition
        [DataMember (Name = "edges")] public int Edges;
        [DataMember (Name = "radius")] public float Radius;
    
        public ShapeGoal()
        {
        }
        
        public ShapeGoal(int Edges, float Radius)
        {
            this.Edges = Edges;
            this.Radius = Radius;
        }
        
        public ShapeGoal(ref ReadBuffer b)
        {
            b.Deserialize(out Edges);
            b.Deserialize(out Radius);
        }
        
        public ShapeGoal(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out Edges);
            b.Deserialize(out Radius);
        }
        
        public ShapeGoal RosDeserialize(ref ReadBuffer b) => new ShapeGoal(ref b);
        
        public ShapeGoal RosDeserialize(ref ReadBuffer2 b) => new ShapeGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Edges);
            b.Serialize(Radius);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
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
        
    
        public const string MessageType = "turtle_actionlib/ShapeGoal";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "3b9202ab7292cebe5a95ab2bf6b9c091";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+PKzCsxNlJITUlPLeZKy8lPBPGKElMyS4u5AJplSSgcAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<ShapeGoal> CreateSerializer() => new Serializer();
        public Deserializer<ShapeGoal> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<ShapeGoal>
        {
            public override void RosSerialize(ShapeGoal msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(ShapeGoal msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(ShapeGoal _) => RosFixedMessageLength;
            public override int Ros2MessageLength(ShapeGoal _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<ShapeGoal>
        {
            public override void RosDeserialize(ref ReadBuffer b, out ShapeGoal msg) => msg = new ShapeGoal(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out ShapeGoal msg) => msg = new ShapeGoal(ref b);
        }
    }
}
