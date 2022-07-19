/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [DataContract]
    public sealed class Vertices : IDeserializableRos1<Vertices>, IDeserializableRos2<Vertices>, IMessageRos1, IMessageRos2
    {
        // List of point indices
        [DataMember (Name = "vertices")] public uint[] Vertices_;
    
        /// Constructor for empty message.
        public Vertices()
        {
            Vertices_ = System.Array.Empty<uint>();
        }
        
        /// Explicit constructor.
        public Vertices(uint[] Vertices_)
        {
            this.Vertices_ = Vertices_;
        }
        
        /// Constructor with buffer.
        public Vertices(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out Vertices_);
        }
        
        /// Constructor with buffer.
        public Vertices(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(out Vertices_);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new Vertices(ref b);
        
        public Vertices RosDeserialize(ref ReadBuffer b) => new Vertices(ref b);
        
        public Vertices RosDeserialize(ref ReadBuffer2 b) => new Vertices(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Vertices_);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Vertices_);
        }
        
        public void RosValidate()
        {
            if (Vertices_ is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + 4 * Vertices_.Length;
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Vertices_);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "pcl_msgs/Vertices";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "39bd7b1c23763ddd1b882b97cb7cfe11";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1NW8MksLlHIT1MoyM/MK1HIzEvJTE4t5ioFcoyNomMVylKLSsAiXAA/hR0KKwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
