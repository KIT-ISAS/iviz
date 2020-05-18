using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/MeshFaceClusterStamped")]
    public sealed class MeshFaceClusterStamped : IMessage
    {
        // header
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // mesh uuid
        [DataMember (Name = "uuid")] public string Uuid { get; set; }
        // Cluster
        [DataMember (Name = "cluster")] public MeshFaceCluster Cluster { get; set; }
        // overwrite existing labeled faces
        [DataMember (Name = "override")] public bool @override { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshFaceClusterStamped()
        {
            Header = new StdMsgs.Header();
            Uuid = "";
            Cluster = new MeshFaceCluster();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshFaceClusterStamped(StdMsgs.Header Header, string Uuid, MeshFaceCluster Cluster, bool @override)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.Cluster = Cluster;
            this.@override = @override;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshFaceClusterStamped(Buffer b)
        {
            Header = new StdMsgs.Header(b);
            Uuid = b.DeserializeString();
            Cluster = new MeshFaceCluster(b);
            @override = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new MeshFaceClusterStamped(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Header);
            b.Serialize(this.Uuid);
            b.Serialize(Cluster);
            b.Serialize(this.@override);
        }
        
        public void Validate()
        {
            if (Header is null) throw new System.NullReferenceException();
            Header.Validate();
            if (Uuid is null) throw new System.NullReferenceException();
            if (Cluster is null) throw new System.NullReferenceException();
            Cluster.Validate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Uuid);
                size += Cluster.RosMessageLength;
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
