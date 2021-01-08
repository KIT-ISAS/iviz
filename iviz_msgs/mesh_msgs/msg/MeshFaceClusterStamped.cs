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
                "H4sIAAAAAAAACrVTTWsbMRC9G/wfBnxIUnAK7c3QU0PaHAqF5FaKGUvj3QGttNVIdvbfd0abbEJOPTSL" +
                "l7Wk9948zccGekJPeb2S4veDdPLxe9tY9terDQwkPdTK3mCZY/e0sLOvoUox4A8F3aKjpw1wzweGSifK" +
                "58yFgB5ZikkEPFAgD0flyHp1SCk0WGZPRvrynx91eP9tB2/uaebuC0aP2es9C3osCMekCeCup7wNdKKg" +
                "LBxGNdtOyzSSXBvzoWcB/XUUKWMIE1RRVEng0jDUyA71yoU1ga8FjMoREEbMhV0NqOlKKXuOhj9mHKjp" +
                "2yv0p1J0BHc3O0VFIVcLq6lJNVwmFMvm3Y2CK8fy+ZMxlPhwTltdU6e1WBxA6bGYY3ocM4mZRdlZmA/z" +
                "Ha9VXpNEGsgLXLa9vS7lCjSOuqAxuR4u1f7PqfQpqiLBCTPjIZApO82Dyl4Y6eLqtbRZ30HEmJ71Z8mX" +
                "IP+iG1+E7VrbXosXLAVSO82jIsecTtpEHg5TU3GBKRYIfMiYp/XKaHNQFbm1ZCtMea02+kWR5Fgr4eHM" +
                "pV+avtVlPzf+O3WnTdrcnm/GSZ0u/+Yy//rdRmfP0XOboE0aC6eIYTHcRszc/gUh8mK95wMAAA==";
                
    }
}
