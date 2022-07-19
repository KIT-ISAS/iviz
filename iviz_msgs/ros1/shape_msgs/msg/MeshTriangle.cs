/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ShapeMsgs
{
    [DataContract]
    public sealed class MeshTriangle : IDeserializableRos1<MeshTriangle>, IDeserializableRos2<MeshTriangle>, IMessageRos1, IMessageRos2
    {
        // Definition of a triangle's vertices
        [DataMember (Name = "vertex_indices")] public uint[/*3*/] VertexIndices;
    
        /// Constructor for empty message.
        public MeshTriangle()
        {
            VertexIndices = new uint[3];
        }
        
        /// Explicit constructor.
        public MeshTriangle(uint[] VertexIndices)
        {
            this.VertexIndices = VertexIndices;
        }
        
        /// Constructor with buffer.
        public MeshTriangle(ref ReadBuffer b)
        {
            b.DeserializeStructArray(3, out VertexIndices);
        }
        
        /// Constructor with buffer.
        public MeshTriangle(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(3, out VertexIndices);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new MeshTriangle(ref b);
        
        public MeshTriangle RosDeserialize(ref ReadBuffer b) => new MeshTriangle(ref b);
        
        public MeshTriangle RosDeserialize(ref ReadBuffer2 b) => new MeshTriangle(ref b);
    
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
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 12;
        
        public int RosMessageLength => RosFixedMessageLength;
        /// <summary> Constant size of this message. </summary> 
        public const int Ros2FixedMessageLength = 12;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, VertexIndices, 3);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "shape_msgs/MeshTriangle";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "23688b2e6d2de3d32fe8af104a903253";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1NWcElNy8zLLMnMz1PIT1NIVCgpykzMS89JVS9WKEstKslMTi3mKs3MKzE2ijaOBQul" +
                "VsRn5qWAJbgAjDTRWEAAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
