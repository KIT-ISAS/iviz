/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Material : IDeserializable<Material>, IMessage
    {
        public const byte BLEND_DEFAULT = 0;
        public const byte BLEND_ADDITIVE = 1;
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "ambient")] public Color32 Ambient;
        [DataMember (Name = "diffuse")] public Color32 Diffuse;
        [DataMember (Name = "emissive")] public Color32 Emissive;
        [DataMember (Name = "opacity")] public float Opacity;
        [DataMember (Name = "bump_scaling")] public float BumpScaling;
        [DataMember (Name = "shininess")] public float Shininess;
        [DataMember (Name = "shininess_strength")] public float ShininessStrength;
        [DataMember (Name = "reflectivity")] public float Reflectivity;
        [DataMember (Name = "blend_mode")] public byte BlendMode;
        [DataMember (Name = "textures")] public Texture[] Textures;
    
        public Material()
        {
            Name = "";
            Textures = System.Array.Empty<Texture>();
        }
        
        public Material(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.Deserialize(out Ambient);
            b.Deserialize(out Diffuse);
            b.Deserialize(out Emissive);
            b.Deserialize(out Opacity);
            b.Deserialize(out BumpScaling);
            b.Deserialize(out Shininess);
            b.Deserialize(out ShininessStrength);
            b.Deserialize(out Reflectivity);
            b.Deserialize(out BlendMode);
            b.DeserializeArray(out Textures);
            for (int i = 0; i < Textures.Length; i++)
            {
                Textures[i] = new Texture(ref b);
            }
        }
        
        public Material(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Name);
            b.Deserialize(out Ambient);
            b.Deserialize(out Diffuse);
            b.Deserialize(out Emissive);
            b.Deserialize(out Opacity);
            b.Deserialize(out BumpScaling);
            b.Deserialize(out Shininess);
            b.Deserialize(out ShininessStrength);
            b.Deserialize(out Reflectivity);
            b.Deserialize(out BlendMode);
            b.DeserializeArray(out Textures);
            for (int i = 0; i < Textures.Length; i++)
            {
                Textures[i] = new Texture(ref b);
            }
        }
        
        public Material RosDeserialize(ref ReadBuffer b) => new Material(ref b);
        
        public Material RosDeserialize(ref ReadBuffer2 b) => new Material(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(in Ambient);
            b.Serialize(in Diffuse);
            b.Serialize(in Emissive);
            b.Serialize(Opacity);
            b.Serialize(BumpScaling);
            b.Serialize(Shininess);
            b.Serialize(ShininessStrength);
            b.Serialize(Reflectivity);
            b.Serialize(BlendMode);
            b.SerializeArray(Textures);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Name);
            b.Serialize(in Ambient);
            b.Serialize(in Diffuse);
            b.Serialize(in Emissive);
            b.Serialize(Opacity);
            b.Serialize(BumpScaling);
            b.Serialize(Shininess);
            b.Serialize(ShininessStrength);
            b.Serialize(Reflectivity);
            b.Serialize(BlendMode);
            b.SerializeArray(Textures);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Textures is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Textures.Length; i++)
            {
                if (Textures[i] is null) BuiltIns.ThrowNullReference(nameof(Textures), i);
                Textures[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 41 + WriteBuffer.GetStringSize(Name) + WriteBuffer.GetArraySize(Textures);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = WriteBuffer2.AddLength(c, Name);
            c += 4;  // Ambient
            c += 4;  // Diffuse
            c += 4;  // Emissive
            c = WriteBuffer2.Align4(c);
            c += 4;  // Opacity
            c += 4;  // BumpScaling
            c += 4;  // Shininess
            c += 4;  // ShininessStrength
            c += 4;  // Reflectivity
            c += 1;  // BlendMode
            c = WriteBuffer2.AddLength(c, Textures);
            return c;
        }
    
        public const string MessageType = "iviz_msgs/Material";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "dea645939e7a51f77d59181b714cdac1";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7WU247bIBCG73kKv0HbPbTbSntBbJKgArZ8yDaqKkQSkkWKDzI43e3TF9vYS7S3rS9s" +
                "8/0j+Gdg6FRlHoIFQSziEVrCguTBY/ARdB6HUYRzvEFW+ASANq2qTkElSgnC+ly3tzeBKHdKVmYeH9Tx" +
                "2Ok3XZZKa3WR4HiuhbGgbsRemdd5vOvKhuu9ONupZ6ifVaUqqfV7wq0LWZ3M8yy18niWe6Mu/bSj+91Z" +
                "Vgde1gcJcvliulb+/BWY8U8D8PiPH0Cz1bfAGvjDS33SH1zyzkzrvqfJnPuK/2/EJe8WzLcJ4ixmyNvn" +
                "gUV4uSyycZc9nCUoLAhMLb/xOaQLjFh/Wm59jCjOsvGw3Pl8jfBq3UffX/tIKSSZxZ+v1lxjhhnKeuGL" +
                "L8QJDHG+tfjh2nqWEBgiOhr66mukX5fCpM/rKt8ULQkKcxyzXrrKuWDfWfw08BvgBDtFgtmKL9OY8mLj" +
                "VW9SsmSNUr9+kxBuCWYR8ks4SYv4h1fBidpkmF/Bib/5up9sxQmntmlxQraeJUtt13pWLMiKRZ7CMPdc" +
                "WBrhDY6Q56GPpHGcr90Mdx7HK4Yix2cHTylMeP/y1h9YSCBNPA8DpDhNY78SA41QCMlgYrpfGmGb2wbY" +
                "1lbVQb64aPPaTAe5FE3TXxdjUHfhY9x8pQzdfxR7U0+9VzeyFUbVlRv/bkUz3A+8e0cuAPwFjGxOhxwF" +
                "AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
