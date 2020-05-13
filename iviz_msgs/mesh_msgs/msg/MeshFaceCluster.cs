using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    [DataContract]
    public sealed class MeshFaceCluster : IMessage
    {
        //Cluster
        [DataMember] public uint[] face_indices { get; set; }
        //optional
        [DataMember] public string label { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshFaceCluster()
        {
            face_indices = System.Array.Empty<uint>();
            label = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshFaceCluster(uint[] face_indices, string label)
        {
            this.face_indices = face_indices ?? throw new System.ArgumentNullException(nameof(face_indices));
            this.label = label ?? throw new System.ArgumentNullException(nameof(label));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshFaceCluster(Buffer b)
        {
            this.face_indices = b.DeserializeStructArray<uint>();
            this.label = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new MeshFaceCluster(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeStructArray(this.face_indices, 0);
            b.Serialize(this.label);
        }
        
        public void Validate()
        {
            if (face_indices is null) throw new System.NullReferenceException();
            if (label is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += 4 * face_indices.Length;
                size += BuiltIns.UTF8.GetByteCount(label);
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshFaceCluster";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9e0f40b9dcf1de10d00e57182c9d138f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1N2ziktLkkt4irNzCsxNoqOVUhLTE6Nz8xLyUxOLeZSzi8oyczPS8zhKi4pysxLV8hJ" +
                "TErN4eICAKZztFU3AAAA";
                
    }
}
