using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/MeshVertexTexCoords")]
    public sealed class MeshVertexTexCoords : IMessage
    {
        // Mesh Attribute Type
        [DataMember (Name = "u")] public float U { get; set; }
        [DataMember (Name = "v")] public float V { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshVertexTexCoords()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshVertexTexCoords(float U, float V)
        {
            this.U = U;
            this.V = V;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshVertexTexCoords(Buffer b)
        {
            U = b.Deserialize<float>();
            V = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new MeshVertexTexCoords(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(U);
            b.Serialize(V);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 8;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshVertexTexCoords";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4f5254e0e12914c461d4b17a0cd07f7f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1NW8E0tzlBwLCkpykwqLUlVCKksSOVKy8lPLDE2UiiFs8q4uADIua4VKwAAAA==";
                
    }
}
