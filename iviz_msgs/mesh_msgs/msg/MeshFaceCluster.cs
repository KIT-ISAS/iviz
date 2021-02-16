/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/MeshFaceCluster")]
    public sealed class MeshFaceCluster : IDeserializable<MeshFaceCluster>, IMessage
    {
        //Cluster
        [DataMember (Name = "face_indices")] public uint[] FaceIndices { get; set; }
        //optional
        [DataMember (Name = "label")] public string Label { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshFaceCluster()
        {
            FaceIndices = System.Array.Empty<uint>();
            Label = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshFaceCluster(uint[] FaceIndices, string Label)
        {
            this.FaceIndices = FaceIndices;
            this.Label = Label;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MeshFaceCluster(ref Buffer b)
        {
            FaceIndices = b.DeserializeStructArray<uint>();
            Label = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeshFaceCluster(ref b);
        }
        
        MeshFaceCluster IDeserializable<MeshFaceCluster>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(FaceIndices, 0);
            b.Serialize(Label);
        }
        
        public void RosValidate()
        {
            if (FaceIndices is null) throw new System.NullReferenceException(nameof(FaceIndices));
            if (Label is null) throw new System.NullReferenceException(nameof(Label));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += 4 * FaceIndices.Length;
                size += BuiltIns.UTF8.GetByteCount(Label);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
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
