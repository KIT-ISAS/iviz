/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshFaceClusterStamped : IDeserializableRos1<MeshFaceClusterStamped>, IDeserializableRos2<MeshFaceClusterStamped>, IMessageRos1, IMessageRos2
    {
        // header
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // mesh uuid
        [DataMember (Name = "uuid")] public string Uuid;
        // Cluster
        [DataMember (Name = "cluster")] public MeshFaceCluster Cluster;
        // overwrite existing labeled faces
        [DataMember (Name = "override")] public bool @override;
    
        /// Constructor for empty message.
        public MeshFaceClusterStamped()
        {
            Uuid = "";
            Cluster = new MeshFaceCluster();
        }
        
        /// Explicit constructor.
        public MeshFaceClusterStamped(in StdMsgs.Header Header, string Uuid, MeshFaceCluster Cluster, bool @override)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.Cluster = Cluster;
            this.@override = @override;
        }
        
        /// Constructor with buffer.
        public MeshFaceClusterStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out Uuid);
            Cluster = new MeshFaceCluster(ref b);
            b.Deserialize(out @override);
        }
        
        /// Constructor with buffer.
        public MeshFaceClusterStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out Uuid);
            Cluster = new MeshFaceCluster(ref b);
            b.Deserialize(out @override);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new MeshFaceClusterStamped(ref b);
        
        public MeshFaceClusterStamped RosDeserialize(ref ReadBuffer b) => new MeshFaceClusterStamped(ref b);
        
        public MeshFaceClusterStamped RosDeserialize(ref ReadBuffer2 b) => new MeshFaceClusterStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            Cluster.RosSerialize(ref b);
            b.Serialize(@override);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            Cluster.RosSerialize(ref b);
            b.Serialize(@override);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) BuiltIns.ThrowNullReference();
            if (Cluster is null) BuiltIns.ThrowNullReference();
            Cluster.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Uuid);
                size += Cluster.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Uuid);
            Cluster.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, @override);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "mesh_msgs/MeshFaceClusterStamped";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "e9b5993e06e78f5ff36e4050fa2e88c6";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VTTWvcMBC961cM+JCksCm0t4WeGtLmECgkt1KWsTRrD8iSK8m78b/vkx2nIaceGiOw" +
                "Pt57891QL+wkmVzcYchd/vh9OW/XpqFBck/TpA6YpKFb93j46qdcgLkH4JatPJ/JPt8DEk+SzkmLkDxp" +
                "LpXtuRUvjo5gZNPG6BdUUifGfPnPn7l/+LanN7HBr4fCwXFyCK6w48J0jIhZu17SzstJPEg8jPBzeS3z" +
                "KPkaxMdeM2F1EiSx9zNNGaASycZhmIJaRrBFkbTXfDA1ENPIqaidPCNLMSanocKPiQep6lhZfk8SrNDd" +
                "zR6YkMVOReHQDAWbhHNN4t0NmUlD+fypEkzzeI47HKVD+l+MU+m5VGflaUySq5+c97DxYQ3uGtpIjsCK" +
                "y3S53B1wzFcEI3BBxmh7uoTnP+bSxwBBoRMn5dZLFbbIAFQvKuni6pVyWKQDh7jJr4p/bfyLbHjRrTHt" +
                "etTM1+jz1CGBAI4pntA3jtp5EbFeJRTy2iZOs6ms1aRpbmuOAQJrqQj+nHO0igI4Omvpt/ZeqnGoLf4+" +
                "3VjnaW3HN4Njmm2zlvbnr2VIDhqc1llp4lg0Bvabp8soGfMHCtWNf8QDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
