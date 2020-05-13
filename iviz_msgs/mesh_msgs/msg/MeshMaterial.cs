using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    [DataContract]
    public sealed class MeshMaterial : IMessage
    {
        [DataMember] public uint texture_index { get; set; }
        [DataMember] public std_msgs.ColorRGBA color { get; set; }
        [DataMember] public bool has_texture { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshMaterial()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshMaterial(uint texture_index, std_msgs.ColorRGBA color, bool has_texture)
        {
            this.texture_index = texture_index;
            this.color = color;
            this.has_texture = has_texture;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshMaterial(Buffer b)
        {
            this.texture_index = b.Deserialize<uint>();
            this.color = new std_msgs.ColorRGBA(b);
            this.has_texture = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new MeshMaterial(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.texture_index);
            b.Serialize(this.color);
            b.Serialize(this.has_texture);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 21;
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshMaterial";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6ad79583de5735994d239e1d0f34371b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCsxNlIoSa0oKS1Kjc/MS0mt4CouSYnPLU4v1nfOz8kvCnJ3clRIBrG4kvLzcxQy" +
                "Eovjoeq5uGypDLh8g92tFDAdwJWWk58IcmkRnJUOZyXBWYlcXABOlNZm0gAAAA==";
                
    }
}
