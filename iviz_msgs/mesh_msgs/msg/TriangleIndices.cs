using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/TriangleIndices")]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct TriangleIndices : IMessage
    {
        // Definition of a triangle's vertices
        [DataMember (Name = "vertex_indices")] fixed uint VertexIndices[3];
        public uint VertexIndices0
        {
            get => VertexIndices[0];
            set => VertexIndices[0] = value;
        }
        public uint VertexIndices1
        {
            get => VertexIndices[1];
            set => VertexIndices[1] = value;
        }
        public uint VertexIndices2
        {
            get => VertexIndices[2];
            set => VertexIndices[2] = value;
        }
    
        /// <summary> Explicit constructor. </summary>
        public TriangleIndices(uint[] VertexIndices)
        {
            if (VertexIndices is null) throw new System.ArgumentNullException(nameof(VertexIndices));
            for (int i = 0; i < 3; i++)
            {
                this.VertexIndices[i] = VertexIndices[i];
            }
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TriangleIndices(Buffer b)
        {
            b.Deserialize(out this);
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new TriangleIndices(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 12;
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/TriangleIndices";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "23688b2e6d2de3d32fe8af104a903253";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1NWcElNy8zLLMnMz1PIT1NIVCgpykzMS89JVS9WKEstKslMTi3mKs3MKzE2ijaOBQul" +
                "VsRn5qWAJbgAjDTRWEAAAAA=";
                
    }
}
