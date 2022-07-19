/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class AveragingGoal : IDeserializableRos1<AveragingGoal>, IDeserializableRos2<AveragingGoal>, IMessageRos1, IMessageRos2, IGoal<AveragingActionGoal>
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
        public AveragingGoal(ref ReadBuffer b)
        {
            b.Deserialize(out Edges);
            b.Deserialize(out Radius);
        }
        
        /// Constructor with buffer.
        public AveragingGoal(ref ReadBuffer2 b)
        {
            b.Deserialize(out Edges);
            b.Deserialize(out Radius);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new AveragingGoal(ref b);
        
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
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        /// <summary> Constant size of this message. </summary> 
        public const int Ros2FixedMessageLength = 8;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Edges);
            WriteBuffer2.AddLength(ref c, Radius);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib_tutorials/AveragingGoal";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "3b9202ab7292cebe5a95ab2bf6b9c091";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+PKzCsxNlJITUlPLeZKy8lPBPGKElMyS4u5AJplSSgcAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
