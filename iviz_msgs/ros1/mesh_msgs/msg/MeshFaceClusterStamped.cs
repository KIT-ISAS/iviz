/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshFaceClusterStamped : IDeserializable<MeshFaceClusterStamped>, IMessage
    {
        // header
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // mesh uuid
        [DataMember (Name = "uuid")] public string Uuid;
        // Cluster
        [DataMember (Name = "cluster")] public MeshFaceCluster Cluster;
        // overwrite existing labeled faces
        [DataMember (Name = "override")] public bool @override;
    
        public MeshFaceClusterStamped()
        {
            Uuid = "";
            Cluster = new MeshFaceCluster();
        }
        
        public MeshFaceClusterStamped(in StdMsgs.Header Header, string Uuid, MeshFaceCluster Cluster, bool @override)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.Cluster = Cluster;
            this.@override = @override;
        }
        
        public MeshFaceClusterStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out Uuid);
            Cluster = new MeshFaceCluster(ref b);
            b.Deserialize(out @override);
        }
        
        public MeshFaceClusterStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align4();
            b.DeserializeString(out Uuid);
            Cluster = new MeshFaceCluster(ref b);
            b.Deserialize(out @override);
        }
        
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.AddLength(c, Uuid);
            c = Cluster.AddRos2MessageLength(c);
            c += 1; // @override
            return c;
        }
    
        public const string MessageType = "mesh_msgs/MeshFaceClusterStamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "e9b5993e06e78f5ff36e4050fa2e88c6";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VTTWvcMBC961cM+JCksCltbws9NaTNIVCa3EJYZqVZe0CWXEnejf99n+w4DTn1kBqB" +
                "9fHem++GOmEnyeTidn1u88cf83m9Ng31kjsaR3XAJA3tssfDNz/mAswtANds5flM9vkekHiUdEpahORJ" +
                "c6lsz3vx4ugARjb7GP2MSurEmK/v/Jnbu+9behMb/LorHBwnh+AKOy5Mh4iYte0kbbwcxYPE/QA/59cy" +
                "DZIvQbzvNBNWK0ESez/RmAEqkWzs+zGoZQRbFEl7zQdTAzENnIra0TOyFGNyGir8kLiXqo6V5fcowQrd" +
                "XG2BCVnsWBQOTVCwSTjXJN5ckRk1lC+fK4EaevgV86dH09yf4gb30qIOL15Q6bhUr+VpSJKrw5y3MPZh" +
                "ifISRpAlgTmX6Xy+2+GYLwjW4IsM0XZ0jhB+TqWLAYJCR07Key9V2CIVUD2rpLOLV8phlg4c4iq/KP61" +
                "8S+y4UW3xrTpUDxf05DHFpkEcEjxiAZytJ9mEetVQiGv+8RpMpW1mDTNdU02QGDNpcGfc45WUQlHJy3d" +
                "2udzWXa11/9PW9bBWvryzQSZZt0sNX54nKdlp8FpHZomDkVjYL96Os+UMX8AzWNdIs0DAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
