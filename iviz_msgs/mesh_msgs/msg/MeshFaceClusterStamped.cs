using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    [DataContract]
    public sealed class MeshFaceClusterStamped : IMessage
    {
        // header
        [DataMember] public std_msgs.Header header { get; set; }
        
        // mesh uuid
        [DataMember] public string uuid { get; set; }
        
        // Cluster
        [DataMember] public MeshFaceCluster cluster { get; set; }
        
        // overwrite existing labeled faces
        [DataMember] public bool @override { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshFaceClusterStamped()
        {
            header = new std_msgs.Header();
            uuid = "";
            cluster = new MeshFaceCluster();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshFaceClusterStamped(std_msgs.Header header, string uuid, MeshFaceCluster cluster, bool @override)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.uuid = uuid ?? throw new System.ArgumentNullException(nameof(uuid));
            this.cluster = cluster ?? throw new System.ArgumentNullException(nameof(cluster));
            this.@override = @override;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshFaceClusterStamped(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.uuid = b.DeserializeString();
            this.cluster = new MeshFaceCluster(b);
            this.@override = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new MeshFaceClusterStamped(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.header);
            b.Serialize(this.uuid);
            b.Serialize(this.cluster);
            b.Serialize(this.@override);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            header.Validate();
            if (uuid is null) throw new System.NullReferenceException();
            if (cluster is null) throw new System.NullReferenceException();
            cluster.Validate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(uuid);
                size += cluster.RosMessageLength;
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshFaceClusterStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e9b5993e06e78f5ff36e4050fa2e88c6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VTTWvcMBC961cM+JCksCm0t4WeGtLmECgkt1KWsTRrD8iSK8m78b/vkx2nIaceGiOw" +
                "Pt57891QL+wkmVzcYchd/vh9OW/XpqFBck/TpA6YpKFb93j46qdcgLkH4JatPJ/JPt8DEk+SzkmLkDxp" +
                "LpXtuRUvjo5gZNPG6BdUUifGfPnPn7l/+LanN7HBr4fCwXFyCK6w48J0jIhZu17SzstJPEg8jPBzeS3z" +
                "KPkaxMdeM2F1EiSx9zNNGaASycZhmIJaRrBFkbTXfDA1ENPIqaidPCNLMSanocKPiQep6lhZfk8SrNDd" +
                "zR6YkMVOReHQDAWbhHNN4t0NmUlD+fypEkzzeI47HKVD+l+MU+m5VGflaUySq5+c97DxYQ3uGtpIjsCK" +
                "y3S53B1wzFcEI3BBxmh7uoTnP+bSxwBBoRMn5dZLFbbIAFQvKuni6pVyWKQDh7jJr4p/bfyLbHjRrTHt" +
                "etTM1+jz1CGBAI4pntA3jtp5EbFeJRTy2iZOs6ms1aRpbmuOAQJrqQj+nHO0igI4Omvpt/ZeqnGoLf4+" +
                "3VjnaW3HN4Njmm2zlvbnr2VIDhqc1llp4lg0Bvabp8soGfMHCtWNf8QDAAA=";
                
    }
}
