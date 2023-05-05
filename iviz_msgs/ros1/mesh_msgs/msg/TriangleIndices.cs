/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct TriangleIndices : IMessage, IHasSerializer<TriangleIndices>
    {
        // Definition of a triangle's vertices
        [DataMember (Name = "vertex_indices")] public fixed uint VertexIndices[3];
    
        public TriangleIndices(uint[] VertexIndices)
        {
            if (VertexIndices is null) BuiltIns.ThrowArgumentNull(nameof(VertexIndices));
            for (int i = 0; i < 3; i++)
            {
                this.VertexIndices[i] = VertexIndices[i];
            }
        }
        
        public TriangleIndices(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        public TriangleIndices(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
        public readonly TriangleIndices RosDeserialize(ref ReadBuffer b) => new TriangleIndices(ref b);
        
        public readonly TriangleIndices RosDeserialize(ref ReadBuffer2 b) => new TriangleIndices(ref b);
    
        public readonly void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 12;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 12;
        
        public readonly int Ros2MessageLength => Ros2FixedMessageLength;
        
        public readonly int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "mesh_msgs/TriangleIndices";
    
        public readonly string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "23688b2e6d2de3d32fe8af104a903253";
    
        public readonly string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public readonly string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1NWcElNy8zLLMnMz1PIT1NIVCgpykzMS89JVS9WKEstKslMTi3mKs3MKzE2ijaOBQul" +
                "VsRn5qWAJbgAjDTRWEAAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<TriangleIndices> CreateSerializer() => new Serializer();
        public Deserializer<TriangleIndices> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TriangleIndices>
        {
            public override void RosSerialize(TriangleIndices msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(TriangleIndices msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(TriangleIndices _) => RosFixedMessageLength;
            public override int Ros2MessageLength(TriangleIndices _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<TriangleIndices>
        {
            public override void RosDeserialize(ref ReadBuffer b, out TriangleIndices msg) => msg = new TriangleIndices(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out TriangleIndices msg) => msg = new TriangleIndices(ref b);
        }
    }
}
