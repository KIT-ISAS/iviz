/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/MeshFaceClusterStamped")]
    public sealed class MeshFaceClusterStamped : IDeserializable<MeshFaceClusterStamped>, IMessage
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
        public MeshFaceClusterStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Uuid = b.DeserializeString();
            Cluster = new MeshFaceCluster(ref b);
            @override = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeshFaceClusterStamped(ref b);
        }
        
        MeshFaceClusterStamped IDeserializable<MeshFaceClusterStamped>.RosDeserialize(ref Buffer b)
        {
            return new MeshFaceClusterStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            Cluster.RosSerialize(ref b);
            b.Serialize(@override);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
            if (Cluster is null) throw new System.NullReferenceException(nameof(Cluster));
            Cluster.RosValidate();
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
    
        public string RosType => RosMessageType;
    
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
