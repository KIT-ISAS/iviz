/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshMaterial : IDeserializable<MeshMaterial>, IMessage
    {
        [DataMember (Name = "texture_index")] public uint TextureIndex;
        [DataMember (Name = "color")] public StdMsgs.ColorRGBA Color;
        [DataMember (Name = "has_texture")] public bool HasTexture;
    
        public MeshMaterial()
        {
        }
        
        public MeshMaterial(uint TextureIndex, in StdMsgs.ColorRGBA Color, bool HasTexture)
        {
            this.TextureIndex = TextureIndex;
            this.Color = Color;
            this.HasTexture = HasTexture;
        }
        
        public MeshMaterial(ref ReadBuffer b)
        {
            b.Deserialize(out TextureIndex);
            b.Deserialize(out Color);
            b.Deserialize(out HasTexture);
        }
        
        public MeshMaterial(ref ReadBuffer2 b)
        {
            b.Deserialize(out TextureIndex);
            b.Align4();
            b.Deserialize(out Color);
            b.Deserialize(out HasTexture);
        }
        
        public MeshMaterial RosDeserialize(ref ReadBuffer b) => new MeshMaterial(ref b);
        
        public MeshMaterial RosDeserialize(ref ReadBuffer2 b) => new MeshMaterial(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(TextureIndex);
            b.Serialize(in Color);
            b.Serialize(HasTexture);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(TextureIndex);
            b.Serialize(in Color);
            b.Serialize(HasTexture);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 21;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 21;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "mesh_msgs/MeshMaterial";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "6ad79583de5735994d239e1d0f34371b";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvNzCsxNlIoSa0oKS1Kjc/MS0mt4CouSYnPLU4v1nfOz8kvCnJ3clRIBrG4kvLzcxQy" +
                "Eovjoeq5uGypDLh8g92tFDAdwJWWk58IcmkRnJUOZyXBWYlcXABOlNZm0gAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
