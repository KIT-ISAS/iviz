/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class TriangleIndices : IDeserializable<TriangleIndices>, IMessage
    {
        // Definition of a triangle's vertices
        [DataMember (Name = "vertex_indices")] public uint[/*3*/] VertexIndices;
    
        public TriangleIndices()
        {
            VertexIndices = new uint[3];
        }
        
        public TriangleIndices(uint[] VertexIndices)
        {
            this.VertexIndices = VertexIndices;
        }
        
        public TriangleIndices(ref ReadBuffer b)
        {
            b.DeserializeStructArray(3, out VertexIndices);
        }
        
        public TriangleIndices(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(3, out VertexIndices);
        }
        
        public TriangleIndices RosDeserialize(ref ReadBuffer b) => new TriangleIndices(ref b);
        
        public TriangleIndices RosDeserialize(ref ReadBuffer2 b) => new TriangleIndices(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(VertexIndices, 3);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(VertexIndices, 3);
        }
        
        public void RosValidate()
        {
            if (VertexIndices is null) BuiltIns.ThrowNullReference();
            if (VertexIndices.Length != 3) BuiltIns.ThrowInvalidSizeForFixedArray(VertexIndices.Length, 3);
        }
    
        public const int RosFixedMessageLength = 12;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, VertexIndices, 3);
        }
    
        public const string MessageType = "mesh_msgs/TriangleIndices";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "23688b2e6d2de3d32fe8af104a903253";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1NWcElNy8zLLMnMz1PIT1NIVCgpykzMS89JVS9WKEstKslMTi3mKs3MKzE2ijaOBQul" +
                "VsRn5qWAJbgAjDTRWEAAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
