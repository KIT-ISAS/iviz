/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class AveragingGoal : IDeserializable<AveragingGoal>, IGoal<AveragingActionGoal>
    {
        //goal definition
        [DataMember (Name = "edges")] public int Edges;
        [DataMember (Name = "radius")] public float Radius;
    
        /// Constructor for empty message.
        public AveragingGoal()
        {
        }
        
        /// Explicit constructor.
        public AveragingGoal(int Edges, float Radius)
        {
            this.Edges = Edges;
            this.Radius = Radius;
        }
        
        /// Constructor with buffer.
        internal AveragingGoal(ref Buffer b)
        {
            Edges = b.Deserialize<int>();
            Radius = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new AveragingGoal(ref b);
        
        AveragingGoal IDeserializable<AveragingGoal>.RosDeserialize(ref Buffer b) => new AveragingGoal(ref b);
    
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
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "3b9202ab7292cebe5a95ab2bf6b9c091";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACuPKzCsxNlJITUlPLeZKy8lPBPGKElMyS4u5AJplSSgcAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
