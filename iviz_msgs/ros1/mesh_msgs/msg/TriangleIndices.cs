/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class TriangleIndices : IDeserializable<TriangleIndices>, IHasSerializer<TriangleIndices>, IMessage
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
            unsafe
            {
                var array = new uint[3];
                b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), 3 * 4);
                VertexIndices = array;
            }
        }
        
        public TriangleIndices(ref ReadBuffer2 b)
        {
            unsafe
            {
                b.Align4();
                var array = new uint[3];
                b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), 3 * 4);
                VertexIndices = array;
            }
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
        
        public const int Ros2FixedMessageLength = 12;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
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
