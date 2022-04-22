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
            Label = "";
        }
        
        /// Explicit constructor.
        public MeshFaceCluster(uint[] FaceIndices, string Label)
        {
            this.FaceIndices = FaceIndices;
            this.Label = Label;
        }
        
        /// Constructor with buffer.
        public MeshFaceCluster(ref ReadBuffer b)
        {
            FaceIndices = b.DeserializeStructArray<uint>();
            Label = b.DeserializeString();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MeshFaceCluster(ref b);
        
        public MeshFaceCluster RosDeserialize(ref ReadBuffer b) => new MeshFaceCluster(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(FaceIndices);
            b.Serialize(Label);
        }
        
        public void RosValidate()
        {
            if (FaceIndices is null) BuiltIns.ThrowNullReference();
            if (Label is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 8 + 4 * FaceIndices.Length + BuiltIns.GetStringSize(Label);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshFaceCluster";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9e0f40b9dcf1de10d00e57182c9d138f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1N2ziktLkkt4irNzCsxNoqOVUhLTE6Nz8xLyUxOLeZSzi8oyczPS8zhKi4pysxLV8hJ" +
                "TErN4eICAKZztFU3AAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
