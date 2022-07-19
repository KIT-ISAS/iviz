/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshMaterial : IDeserializableRos1<MeshMaterial>, IDeserializableRos2<MeshMaterial>, IMessageRos1, IMessageRos2
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
        
        /// Constructor with buffer.
        public MeshMaterial(ref ReadBuffer2 b)
        {
            b.Deserialize(out TextureIndex);
            b.Deserialize(out Color);
            b.Deserialize(out HasTexture);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new MeshMaterial(ref b);
        
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
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 21;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        /// <summary> Constant size of this message. </summary> 
        public const int Ros2FixedMessageLength = 21;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, TextureIndex);
            WriteBuffer2.AddLength(ref c, Color);
            WriteBuffer2.AddLength(ref c, HasTexture);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "mesh_msgs/MeshMaterial";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "6ad79583de5735994d239e1d0f34371b";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvNzCsxNlIoSa0oKS1Kjc/MS0mt4CouSYnPLU4v1nfOz8kvCnJ3clRIBrG4kvLzcxQy" +
                "Eovjoeq5uGypDLh8g92tFDAdwJWWk58IcmkRnJUOZyXBWYlcXABOlNZm0gAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
