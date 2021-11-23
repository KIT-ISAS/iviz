/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ShapeGoal : IDeserializable<ShapeGoal>, IGoal<ShapeActionGoal>
    {
        //goal definition
        [DataMember (Name = "edges")] public int Edges;
        [DataMember (Name = "radius")] public float Radius;
    
        /// Constructor for empty message.
        public ShapeGoal()
        {
        }
        
        /// Explicit constructor.
        public ShapeGoal(int Edges, float Radius)
        {
            this.Edges = Edges;
            this.Radius = Radius;
        }
        
        /// Constructor with buffer.
        internal ShapeGoal(ref Buffer b)
        {
            Edges = b.Deserialize<int>();
            Radius = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ShapeGoal(ref b);
        
        ShapeGoal IDeserializable<ShapeGoal>.RosDeserialize(ref Buffer b) => new ShapeGoal(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Edges);
            b.Serialize(Radius);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "3b9202ab7292cebe5a95ab2bf6b9c091";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+PKzCsxNlJITUlPLeZKy8lPBPGKElMyS4u5AJplSSgcAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
