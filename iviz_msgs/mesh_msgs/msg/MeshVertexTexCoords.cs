using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class MeshVertexTexCoords : IMessage
    {
        // Mesh Attribute Type
        public float u { get; set; }
        public float v { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshVertexTexCoords()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshVertexTexCoords(float u, float v)
        {
            this.u = u;
            this.v = v;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshVertexTexCoords(Buffer b)
        {
            this.u = BuiltIns.DeserializeStruct<float>(b);
            this.v = BuiltIns.DeserializeStruct<float>(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new MeshVertexTexCoords(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.u, b);
            BuiltIns.Serialize(this.v, b);
        }
        
        public void Validate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 8;
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "mesh_msgs/MeshVertexTexCoords";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "4f5254e0e12914c461d4b17a0cd07f7f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1NW8E0tzlBwLCkpykwqLUlVCKksSOVKy8lPLDE2UiiFs8q4uADIua4VKwAAAA==";
                
    }
}
