/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Vertices : IDeserializable<Vertices>, IMessage
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
            Vertices_ = b.DeserializeStructArray<uint>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Vertices(ref b);
        
        public Vertices RosDeserialize(ref ReadBuffer b) => new Vertices(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Vertices_);
        }
        
        public void RosValidate()
        {
            if (Vertices_ is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + 4 * Vertices_.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "pcl_msgs/Vertices";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "39bd7b1c23763ddd1b882b97cb7cfe11";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1NW8MksLlHIT1MoyM/MK1HIzEvJTE4t5ioFcoyNomMVylKLSsAiXAA/hR0KKwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
