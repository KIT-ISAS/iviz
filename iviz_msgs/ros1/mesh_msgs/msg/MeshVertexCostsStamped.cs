/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshVertexCostsStamped : IDeserializable<MeshVertexCostsStamped>, IMessage
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
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out Uuid);
            b.DeserializeString(out Type);
            MeshVertexCosts = new MeshMsgs.MeshVertexCosts(ref b);
        }
        
        public MeshVertexCostsStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out Uuid);
            b.DeserializeString(out Type);
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
            b.Serialize(Uuid);
            b.Serialize(Type);
            MeshVertexCosts.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) BuiltIns.ThrowNullReference();
            if (Type is null) BuiltIns.ThrowNullReference();
            if (MeshVertexCosts is null) BuiltIns.ThrowNullReference();
            MeshVertexCosts.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Uuid);
                size += WriteBuffer.GetStringSize(Type);
                size += MeshVertexCosts.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Uuid);
            WriteBuffer2.AddLength(ref c, Type);
            MeshVertexCosts.AddRos2MessageLength(ref c);
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
    }
}
