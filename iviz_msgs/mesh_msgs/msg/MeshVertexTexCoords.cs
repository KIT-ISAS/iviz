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
        internal MeshVertexTexCoords(ref Buffer b)
        {
            U = b.Deserialize<float>();
            V = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MeshVertexTexCoords(ref b);
        
        MeshVertexTexCoords IDeserializable<MeshVertexTexCoords>.RosDeserialize(ref Buffer b) => new MeshVertexTexCoords(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(U);
            b.Serialize(V);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshVertexTexCoords";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "4f5254e0e12914c461d4b17a0cd07f7f";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAClNW8E0tzlBwLCkpykwqLUlVCKksSOVKy8lPLDE2UiiFs8q4uADIua4VKwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
