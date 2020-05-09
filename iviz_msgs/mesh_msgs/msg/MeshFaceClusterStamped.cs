using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class MeshFaceClusterStamped : IMessage
    {
        // header
        public std_msgs.Header header { get; set; }
        
        // mesh uuid
        public string uuid { get; set; }
        
        // Cluster
        public MeshFaceCluster cluster { get; set; }
        
        // overwrite existing labeled faces
        public bool @override { get; set; }
    
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
            this.uuid = BuiltIns.DeserializeString(b);
            this.cluster = new MeshFaceCluster(b);
            this.@override = BuiltIns.DeserializeStruct<bool>(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new MeshFaceClusterStamped(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            BuiltIns.Serialize(this.uuid, b);
            this.cluster.Serialize(b);
            BuiltIns.Serialize(this.@override, b);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            if (uuid is null) throw new System.NullReferenceException();
            if (cluster is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
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
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "mesh_msgs/MeshFaceClusterStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "e9b5993e06e78f5ff36e4050fa2e88c6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
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
