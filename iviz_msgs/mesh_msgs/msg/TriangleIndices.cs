/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TriangleIndices : IDeserializable<TriangleIndices>, IMessage
    {
        // Definition of a triangle's vertices
        [DataMember (Name = "vertex_indices")] public uint[/*3*/] VertexIndices;
    
        /// Constructor for empty message.
        public TriangleIndices()
        {
            VertexIndices = new uint[3];
        }
        
        /// Explicit constructor.
        public TriangleIndices(uint[] VertexIndices)
        {
            this.VertexIndices = VertexIndices;
        }
        
        /// Constructor with buffer.
        public TriangleIndices(ref ReadBuffer b)
        {
            VertexIndices = b.DeserializeStructArray<uint>(3);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TriangleIndices(ref b);
        
        public TriangleIndices RosDeserialize(ref ReadBuffer b) => new TriangleIndices(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(VertexIndices, 3);
        }
        
        public void RosValidate()
        {
            if (VertexIndices is null) throw new System.NullReferenceException(nameof(VertexIndices));
            if (VertexIndices.Length != 3) throw new RosInvalidSizeForFixedArrayException(nameof(VertexIndices), VertexIndices.Length, 3);
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 12;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "mesh_msgs/TriangleIndices";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "23688b2e6d2de3d32fe8af104a903253";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1NWcElNy8zLLMnMz1PIT1NIVCgpykzMS89JVS9WKEstKslMTi3mKs3MKzE2ijaOBQul" +
                "VsRn5qWAJbgAjDTRWEAAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
