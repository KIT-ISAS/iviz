/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshVertexCostsStamped : IHasSerializer<MeshVertexCostsStamped>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "type")] public string Type;
        [DataMember (Name = "mesh_vertex_costs")] public MeshMsgs.MeshVertexCosts MeshVertexCosts;
    
        public MeshVertexCostsStamped()
        {
            Uuid = "";
            Type = "";
            MeshVertexCosts = new MeshMsgs.MeshVertexCosts();
        }
        
        public MeshVertexCostsStamped(in StdMsgs.Header Header, string Uuid, string Type, MeshMsgs.MeshVertexCosts MeshVertexCosts)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.Type = Type;
            this.MeshVertexCosts = MeshVertexCosts;
        }
        
        public MeshVertexCostsStamped(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Uuid = b.DeserializeString();
            Type = b.DeserializeString();
            MeshVertexCosts = new MeshMsgs.MeshVertexCosts(ref b);
        }
        
        public MeshVertexCostsStamped(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align4();
            Uuid = b.DeserializeString();
            b.Align4();
            Type = b.DeserializeString();
            MeshVertexCosts = new MeshMsgs.MeshVertexCosts(ref b);
        }
        
        public MeshVertexCostsStamped RosDeserialize(ref ReadBuffer b) => new MeshVertexCostsStamped(ref b);
        
        public MeshVertexCostsStamped RosDeserialize(ref ReadBuffer2 b) => new MeshVertexCostsStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            b.Serialize(Type);
            MeshVertexCosts.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Uuid);
            b.Align4();
            b.Serialize(Type);
            MeshVertexCosts.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Uuid, nameof(Uuid));
            BuiltIns.ThrowIfNull(Type, nameof(Type));
            BuiltIns.ThrowIfNull(MeshVertexCosts, nameof(MeshVertexCosts));
            MeshVertexCosts.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Uuid);
                size += WriteBuffer.GetStringSize(Type);
                size += MeshVertexCosts.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Uuid);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Type);
            size = MeshVertexCosts.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "mesh_msgs/MeshVertexCostsStamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "f65d52b48920bc9c2a071d643ab7b6b3";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VTTWvcMBC961cM+JCksCltbws9lIakOQRKE3oJYZmVZm2BLLkaaRP/+z55u2koBHpo" +
                "jUDW6L03X5qObkQH+lRK9ttapB2VezFa3GbUXt9+EXaSaVg2mLOPPdXq3fG/zJOYESoHfNP7LrnI0+ek" +
                "RWm52S+GjW0WYz7+48/c3F6t6Y+ITUe3haPj7BBDYceFaZeQie8Hyasgewkg8TiJo+W2ZaLnIN4NXgmr" +
                "lyiZQ5ipKkAlkU3jWKO3jFIVj9xe8sH0kZgmzsXbGjgDn7LzscF3mUdp6lgqP6pEK3R9sQYmqthaPAKa" +
                "oWCzsLbKXl+QqT6WD+8bgTq6/5b03YPp7h7TCnbp0ZjnKKgMXFrU8jRldBFRsa7h7M0hy3M4QZUE7pzS" +
                "6WLb4KhnBG+IRaZkBzpFCl/nMqQIQaE9Z8/bIE3YohRQPWmkk7MXynGRjhzTUf6g+NvH38jGZ92W02pA" +
                "80Irg9YelQRwymnvHaDbeRGxwUssFPw2c55NYx1cmu6yFRsgsJbWYGfVZD064ejRl+H4fJe2bPCc/9Oz" +
                "fHUykOcrs7cLidH0+wf6NTA/AfuiUGGmAwAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<MeshVertexCostsStamped> CreateSerializer() => new Serializer();
        public Deserializer<MeshVertexCostsStamped> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MeshVertexCostsStamped>
        {
            public override void RosSerialize(MeshVertexCostsStamped msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MeshVertexCostsStamped msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MeshVertexCostsStamped msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MeshVertexCostsStamped msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(MeshVertexCostsStamped msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<MeshVertexCostsStamped>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MeshVertexCostsStamped msg) => msg = new MeshVertexCostsStamped(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MeshVertexCostsStamped msg) => msg = new MeshVertexCostsStamped(ref b);
        }
    }
}
