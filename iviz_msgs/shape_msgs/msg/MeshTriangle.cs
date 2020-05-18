using System.Runtime.Serialization;

namespace Iviz.Msgs.ShapeMsgs
{
    [DataContract (Name = "shape_msgs/MeshTriangle")]
    public sealed class MeshTriangle : IMessage
    {
        // Definition of a triangle's vertices
        [DataMember (Name = "vertex_indices")] public uint[/*3*/] VertexIndices { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshTriangle()
        {
            VertexIndices = new uint[3];
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshTriangle(uint[] VertexIndices)
        {
            this.VertexIndices = VertexIndices;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshTriangle(Buffer b)
        {
            VertexIndices = b.DeserializeStructArray<uint>(3);
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new MeshTriangle(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeStructArray(VertexIndices, 3);
        }
        
        public void Validate()
        {
            if (VertexIndices is null) throw new System.NullReferenceException();
            if (VertexIndices.Length != 3) throw new System.IndexOutOfRangeException();
        }
    
        public int RosMessageLength => 12;
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "shape_msgs/MeshTriangle";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "23688b2e6d2de3d32fe8af104a903253";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1NWcElNy8zLLMnMz1PIT1NIVCgpykzMS89JVS9WKEstKslMTi3mKs3MKzE2ijaOBQul" +
                "VsRn5qWAJbgAjDTRWEAAAAA=";
                
    }
}
