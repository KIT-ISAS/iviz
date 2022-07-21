/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshVertexTexCoords : IDeserializable<MeshVertexTexCoords>, IMessage
    {
        // Mesh Attribute Type
        [DataMember (Name = "u")] public float U;
        [DataMember (Name = "v")] public float V;
    
        public MeshVertexTexCoords()
        {
        }
        
        public MeshVertexTexCoords(float U, float V)
        {
            this.U = U;
            this.V = V;
        }
        
        public MeshVertexTexCoords(ref ReadBuffer b)
        {
            b.Deserialize(out U);
            b.Deserialize(out V);
        }
        
        public MeshVertexTexCoords(ref ReadBuffer2 b)
        {
            b.Deserialize(out U);
            b.Deserialize(out V);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new MeshVertexTexCoords(ref b);
        
        public MeshVertexTexCoords RosDeserialize(ref ReadBuffer b) => new MeshVertexTexCoords(ref b);
        
        public MeshVertexTexCoords RosDeserialize(ref ReadBuffer2 b) => new MeshVertexTexCoords(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(U);
            b.Serialize(V);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(U);
            b.Serialize(V);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 8;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, U);
            WriteBuffer2.AddLength(ref c, V);
        }
    
        public const string MessageType = "mesh_msgs/MeshVertexTexCoords";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "4f5254e0e12914c461d4b17a0cd07f7f";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1NW8E0tzlBwLCkpykwqLUlVCKksSOVKy8lPLDE2UiiFs8q4uADIua4VKwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
