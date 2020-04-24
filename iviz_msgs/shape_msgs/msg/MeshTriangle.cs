namespace Iviz.Msgs.shape_msgs
{
    public sealed class MeshTriangle : IMessage
    {
        // Definition of a triangle's vertices
        public uint[/*3*/] vertex_indices;
    
        /// <summary> Constructor for empty message. </summary>
        public MeshTriangle()
        {
            vertex_indices = new uint[3];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out vertex_indices, ref ptr, end, 3);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(vertex_indices, ref ptr, end, 3);
        }
    
        public int GetLength() => 12;
    
        public IMessage Create() => new MeshTriangle();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "shape_msgs/MeshTriangle";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "23688b2e6d2de3d32fe8af104a903253";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
                "H4sIAAAAAAAAE1NWcElNy8zLLMnMz1PIT1NIVCgpykzMS89JVS9WKEstKslMTi3mKs3MKzE2ijaOBQul" +
                "VsRn5qWAJbgAjDTRWEAAAAA=";
                
    }
}
