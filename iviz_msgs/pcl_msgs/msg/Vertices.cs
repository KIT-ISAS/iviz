/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.PclMsgs
{
    [Preserve, DataContract (Name = "pcl_msgs/Vertices")]
    public sealed class Vertices : IDeserializable<Vertices>, IMessage
    {
        // List of point indices
        [DataMember (Name = "vertices")] public uint[] Vertices_;
    
        /// <summary> Constructor for empty message. </summary>
        public Vertices()
        {
            Vertices_ = System.Array.Empty<uint>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Vertices(uint[] Vertices_)
        {
            this.Vertices_ = Vertices_;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Vertices(ref Buffer b)
        {
            Vertices_ = b.DeserializeStructArray<uint>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Vertices(ref b);
        }
        
        Vertices IDeserializable<Vertices>.RosDeserialize(ref Buffer b)
        {
            return new Vertices(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Vertices_, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Vertices_ is null) throw new System.NullReferenceException(nameof(Vertices_));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 4 * Vertices_.Length;
                return size;
            }
        }
    
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
