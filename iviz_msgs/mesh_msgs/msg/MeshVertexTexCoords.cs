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
    
        /// Constructor for empty message.
        public MeshVertexTexCoords()
        {
        }
        
        /// Explicit constructor.
        public MeshVertexTexCoords(float U, float V)
        {
            this.U = U;
            this.V = V;
        }
        
        /// Constructor with buffer.
        public MeshVertexTexCoords(ref ReadBuffer b)
        {
            b.Deserialize(out U);
            b.Deserialize(out V);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MeshVertexTexCoords(ref b);
        
        public MeshVertexTexCoords RosDeserialize(ref ReadBuffer b) => new MeshVertexTexCoords(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(U);
            b.Serialize(V);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "mesh_msgs/MeshVertexTexCoords";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "4f5254e0e12914c461d4b17a0cd07f7f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1NW8E0tzlBwLCkpykwqLUlVCKksSOVKy8lPLDE2UiiFs8q4uADIua4VKwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
