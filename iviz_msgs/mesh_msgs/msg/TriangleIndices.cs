/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/TriangleIndices")]
    public sealed class TriangleIndices : IDeserializable<TriangleIndices>, IMessage
    {
        // Definition of a triangle's vertices
        [DataMember (Name = "vertex_indices")] public uint[/*3*/] VertexIndices { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TriangleIndices()
        {
            VertexIndices = new uint[3];
        }
        
        /// <summary> Explicit constructor. </summary>
        public TriangleIndices(uint[] VertexIndices)
        {
            this.VertexIndices = VertexIndices;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TriangleIndices(ref Buffer b)
        {
            VertexIndices = b.DeserializeStructArray<uint>(3);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TriangleIndices(ref b);
        }
        
        TriangleIndices IDeserializable<TriangleIndices>.RosDeserialize(ref Buffer b)
        {
            return new TriangleIndices(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(VertexIndices, 3);
        }
        
        public void RosValidate()
        {
            if (VertexIndices is null) throw new System.NullReferenceException(nameof(VertexIndices));
            if (VertexIndices.Length != 3) throw new System.IndexOutOfRangeException();
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 12;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
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
