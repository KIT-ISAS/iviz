/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MeshMaterial : IDeserializable<MeshMaterial>, IMessage
    {
        [DataMember (Name = "texture_index")] public uint TextureIndex;
        [DataMember (Name = "color")] public StdMsgs.ColorRGBA Color;
        [DataMember (Name = "has_texture")] public bool HasTexture;
    
        /// Constructor for empty message.
        public MeshMaterial()
        {
        }
        
        /// Explicit constructor.
        public MeshMaterial(uint TextureIndex, in StdMsgs.ColorRGBA Color, bool HasTexture)
        {
            this.TextureIndex = TextureIndex;
            this.Color = Color;
            this.HasTexture = HasTexture;
        }
        
        /// Constructor with buffer.
        public MeshMaterial(ref ReadBuffer b)
        {
            b.Deserialize(out TextureIndex);
            b.Deserialize(out Color);
            b.Deserialize(out HasTexture);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MeshMaterial(ref b);
        
        public MeshMaterial RosDeserialize(ref ReadBuffer b) => new MeshMaterial(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(TextureIndex);
            b.Serialize(in Color);
            b.Serialize(HasTexture);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 21;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshMaterial";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6ad79583de5735994d239e1d0f34371b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCsxNlIoSa0oKS1Kjc/MS0mt4CouSYnPLU4v1nfOz8kvCnJ3clRIBrG4kvLzcxQy" +
                "Eovjoeq5uGypDLh8g92tFDAdwJWWk58IcmkRnJUOZyXBWYlcXABOlNZm0gAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
