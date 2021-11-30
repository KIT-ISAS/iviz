/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MeshFaceCluster : IDeserializable<MeshFaceCluster>, IMessage
    {
        //Cluster
        [DataMember (Name = "face_indices")] public uint[] FaceIndices;
        //optional
        [DataMember (Name = "label")] public string Label;
    
        /// Constructor for empty message.
        public MeshFaceCluster()
        {
            FaceIndices = System.Array.Empty<uint>();
            Label = string.Empty;
        }
        
        /// Explicit constructor.
        public MeshFaceCluster(uint[] FaceIndices, string Label)
        {
            this.FaceIndices = FaceIndices;
            this.Label = Label;
        }
        
        /// Constructor with buffer.
        internal MeshFaceCluster(ref Buffer b)
        {
            FaceIndices = b.DeserializeStructArray<uint>();
            Label = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MeshFaceCluster(ref b);
        
        MeshFaceCluster IDeserializable<MeshFaceCluster>.RosDeserialize(ref Buffer b) => new MeshFaceCluster(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(FaceIndices);
            b.Serialize(Label);
        }
        
        public void RosValidate()
        {
            if (FaceIndices is null) throw new System.NullReferenceException(nameof(FaceIndices));
            if (Label is null) throw new System.NullReferenceException(nameof(Label));
        }
    
        public int RosMessageLength => 8 + 4 * FaceIndices.Length + BuiltIns.GetStringSize(Label);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshFaceCluster";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "9e0f40b9dcf1de10d00e57182c9d138f";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1N2ziktLkkt4irNzCsxNoqOVUhLTE6Nz8xLyUxOLeZSzi8oyczPS8zhKi4pysxLV8hJ" +
                "TErN4eICAKZztFU3AAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
