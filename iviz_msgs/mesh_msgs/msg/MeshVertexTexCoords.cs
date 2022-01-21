/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
            U = b.Deserialize<float>();
            V = b.Deserialize<float>();
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
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshVertexTexCoords";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4f5254e0e12914c461d4b17a0cd07f7f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1NW8E0tzlBwLCkpykwqLUlVCKksSOVKy8lPLDE2UiiFs8q4uADIua4VKwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
