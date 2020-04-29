using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class MeshFaceCluster : IMessage
    {
        //Cluster
        public uint[] face_indices;
        //optional
        public string label;
    
        /// <summary> Constructor for empty message. </summary>
        public MeshFaceCluster()
        {
            face_indices = System.Array.Empty<uint>();
            label = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out face_indices, ref ptr, end, 0);
            BuiltIns.Deserialize(out label, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(face_indices, ref ptr, end, 0);
            BuiltIns.Serialize(label, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += 4 * face_indices.Length;
                size += BuiltIns.UTF8.GetByteCount(label);
                return size;
            }
        }
    
        public IMessage Create() => new MeshFaceCluster();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "mesh_msgs/MeshFaceCluster";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "9e0f40b9dcf1de10d00e57182c9d138f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1N2ziktLkkt4irNzCsxNoqOVUhLTE6Nz8xLyUxOLeZSzi8oyczPS8zhKi4pysxLV8hJ" +
                "TErN4eICAKZztFU3AAAA";
                
    }
}
