/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ShapeMsgs
{
    [Preserve, DataContract (Name = "shape_msgs/MeshTriangle")]
    public sealed class MeshTriangle : IDeserializable<MeshTriangle>, IMessage
    {
        // Definition of a triangle's vertices
        [DataMember (Name = "vertex_indices")] public uint[/*3*/] VertexIndices;
    
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
        internal MeshTriangle(ref Buffer b)
        {
            VertexIndices = b.DeserializeStructArray<uint>(3);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeshTriangle(ref b);
        }
        
        MeshTriangle IDeserializable<MeshTriangle>.RosDeserialize(ref Buffer b)
        {
            return new MeshTriangle(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(VertexIndices, 3);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (VertexIndices is null) throw new System.NullReferenceException(nameof(VertexIndices));
            if (VertexIndices.Length != 3) throw new RosInvalidSizeForFixedArrayException(nameof(VertexIndices), VertexIndices.Length, 3);
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 12;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "shape_msgs/MeshTriangle";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "23688b2e6d2de3d32fe8af104a903253";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1NWcElNy8zLLMnMz1PIT1NIVCgpykzMS89JVS9WKEstKslMTi3mKs3MKzE2ijaOBQul" +
                "VsRn5qWAJbgAjDTRWEAAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
