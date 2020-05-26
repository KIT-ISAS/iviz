using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/MeshMaterial")]
    [StructLayout(LayoutKind.Sequential)]
    public struct MeshMaterial : IMessage
    {
        [DataMember (Name = "texture_index")] public uint TextureIndex { get; set; }
        [DataMember (Name = "color")] public StdMsgs.ColorRGBA Color { get; set; }
        [DataMember (Name = "has_texture")] public bool HasTexture { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public MeshMaterial(uint TextureIndex, StdMsgs.ColorRGBA Color, bool HasTexture)
        {
            this.TextureIndex = TextureIndex;
            this.Color = Color;
            this.HasTexture = HasTexture;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshMaterial(Buffer b)
        {
            b.Deserialize(out this);
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new MeshMaterial(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this);
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
