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
            Uuid = string.Empty;
            Cluster = new MeshFaceCluster();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshFaceClusterStamped(in StdMsgs.Header Header, string Uuid, MeshFaceCluster Cluster, bool @override)
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
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
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
                "H4sIAAAAAAAACrVTTWvbQBC9C/wfBnRIUnAK7c3QU0PaHAKF5FaKGe+OpYHVrrofdvTv+1aK0pBTD40R" +
                "WLt6782br5Z6YSuxSdnuh9Slj9/n83rdtDRI6qkUtcBE9d3yjg9fXUkZmHsAbtnI85nM8z0g4STxHDUL" +
                "yZOmXNmOD+LE0hGM1BxCcDMqqpVm03z5z79Nc//wbUdvstvA2kNmbzla5JfZcmY6BqStXS9x6+QkDiwe" +
                "Rlidv+ZplHQN4mOvifB04iWycxOVBFAOZMIwFK+GkW9W1O01H0z1xDRyzGqKYxQqhGjVV/gx8iBVHU+S" +
                "30W8Ebq72QHjk5iSFYYmKJgonGod726oKerz50+V0LSP57DFUTp04CU45Z5zNStPY5RUfXLaIcaHJblr" +
                "aKM6gig20eV8t8cxXRGCwIKMwfR0Cec/ptwHD0GhE0flg5MqbFABqF5U0sXVK+Vqe0eefVjlF8W/Mf5F" +
                "1r/o1py2PXrmavapdCgggGMMJ4yOpcM0ixin4jM5PUSOU1NZS8imva01BgisuSP455SCUTTA0llzv074" +
                "3I09pvzdBrIu1TKRb7YHo7nu1dLdn7/mVdmrt1o3pg1j1uDZrWbnhYLTP6lMa8TLAwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
