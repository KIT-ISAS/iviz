using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class MeshMaterial : IMessage
    {
        public uint texture_index { get; set; }
        public std_msgs.ColorRGBA color { get; set; }
        public bool has_texture { get; set; }
    
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
            this.texture_index = BuiltIns.DeserializeStruct<uint>(b);
            this.color = new std_msgs.ColorRGBA(b);
            this.has_texture = BuiltIns.DeserializeStruct<bool>(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new MeshMaterial(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.texture_index, b);
            this.color.Serialize(b);
            BuiltIns.Serialize(this.has_texture, b);
        }
        
        public void Validate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 21;
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "mesh_msgs/MeshMaterial";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "6ad79583de5735994d239e1d0f34371b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCsxNlIoSa0oKS1Kjc/MS0mt4CouSYnPLU4v1nfOz8kvCnJ3clRIBrG4kvLzcxQy" +
                "Eovjoeq5uGypDLh8g92tFDAdwJWWk58IcmkRnJUOZyXBWYlcXABOlNZm0gAAAA==";
                
    }
}
