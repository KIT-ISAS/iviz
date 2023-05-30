/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.ShapeMsgs
{
    [DataContract]
    public sealed class MeshTriangle : IHasSerializer<MeshTriangle>, IMessage
    {
        // Definition of a triangle's vertices
        [DataMember (Name = "vertex_indices")] public uint[/*3*/] VertexIndices;
    
        public MeshTriangle()
        {
            VertexIndices = new uint[3];
        }
        
        public MeshTriangle(uint[] VertexIndices)
        {
            this.VertexIndices = VertexIndices;
        }
        
        public MeshTriangle(ref ReadBuffer b)
        {
            {
                var array = new uint[3];
                b.DeserializeStructArray(array);
                VertexIndices = array;
            }
        }
        
        public MeshTriangle(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                var array = new uint[3];
                b.DeserializeStructArray(array);
                VertexIndices = array;
            }
        }
        
        public MeshTriangle RosDeserialize(ref ReadBuffer b) => new MeshTriangle(ref b);
        
        public MeshTriangle RosDeserialize(ref ReadBuffer2 b) => new MeshTriangle(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(VertexIndices, 3);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.SerializeStructArray(VertexIndices, 3);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(VertexIndices, nameof(VertexIndices));
            BuiltIns.ThrowIfWrongSize(VertexIndices, nameof(VertexIndices), 3);
        }
    
        public const int RosFixedMessageLength = 12;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 12;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "shape_msgs/MeshTriangle";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "23688b2e6d2de3d32fe8af104a903253";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1NWcElNy8zLLMnMz1PIT1NIVCgpykzMS89JVS9WKEstKslMTi3mKs3MKzE2ijaOBQul" +
                "VsRn5qWAJbgAjDTRWEAAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<MeshTriangle> CreateSerializer() => new Serializer();
        public Deserializer<MeshTriangle> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MeshTriangle>
        {
            public override void RosSerialize(MeshTriangle msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MeshTriangle msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MeshTriangle _) => RosFixedMessageLength;
            public override int Ros2MessageLength(MeshTriangle _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<MeshTriangle>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MeshTriangle msg) => msg = new MeshTriangle(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MeshTriangle msg) => msg = new MeshTriangle(ref b);
        }
    }
}
