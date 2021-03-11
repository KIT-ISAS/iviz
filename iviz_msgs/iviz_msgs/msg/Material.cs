/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Material")]
    public sealed class Material : IDeserializable<Material>, IMessage
    {
        public const byte BLEND_DEFAULT = 0;
        public const byte BLEND_ADDITIVE = 1;
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "ambient")] public Color32 Ambient { get; set; }
        [DataMember (Name = "diffuse")] public Color32 Diffuse { get; set; }
        [DataMember (Name = "emissive")] public Color32 Emissive { get; set; }
        [DataMember (Name = "opacity")] public float Opacity { get; set; }
        [DataMember (Name = "bump_scaling")] public float BumpScaling { get; set; }
        [DataMember (Name = "shininess")] public float Shininess { get; set; }
        [DataMember (Name = "shininess_strength")] public float ShininessStrength { get; set; }
        [DataMember (Name = "reflectivity")] public float Reflectivity { get; set; }
        [DataMember (Name = "blend_mode")] public byte BlendMode { get; set; }
        [DataMember (Name = "textures")] public Texture[] Textures { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Material()
        {
            Name = string.Empty;
            Textures = System.Array.Empty<Texture>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Material(string Name, in Color32 Ambient, in Color32 Diffuse, in Color32 Emissive, float Opacity, float BumpScaling, float Shininess, float ShininessStrength, float Reflectivity, byte BlendMode, Texture[] Textures)
        {
            this.Name = Name;
            this.Ambient = Ambient;
            this.Diffuse = Diffuse;
            this.Emissive = Emissive;
            this.Opacity = Opacity;
            this.BumpScaling = BumpScaling;
            this.Shininess = Shininess;
            this.ShininessStrength = ShininessStrength;
            this.Reflectivity = Reflectivity;
            this.BlendMode = BlendMode;
            this.Textures = Textures;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Material(ref Buffer b)
        {
            Name = b.DeserializeString();
            Ambient = new Color32(ref b);
            Diffuse = new Color32(ref b);
            Emissive = new Color32(ref b);
            Opacity = b.Deserialize<float>();
            BumpScaling = b.Deserialize<float>();
            Shininess = b.Deserialize<float>();
            ShininessStrength = b.Deserialize<float>();
            Reflectivity = b.Deserialize<float>();
            BlendMode = b.Deserialize<byte>();
            Textures = b.DeserializeArray<Texture>();
            for (int i = 0; i < Textures.Length; i++)
            {
                Textures[i] = new Texture(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Material(ref b);
        }
        
        Material IDeserializable<Material>.RosDeserialize(ref Buffer b)
        {
            return new Material(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            Ambient.RosSerialize(ref b);
            Diffuse.RosSerialize(ref b);
            Emissive.RosSerialize(ref b);
            b.Serialize(Opacity);
            b.Serialize(BumpScaling);
            b.Serialize(Shininess);
            b.Serialize(ShininessStrength);
            b.Serialize(Reflectivity);
            b.Serialize(BlendMode);
            b.SerializeArray(Textures, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Textures is null) throw new System.NullReferenceException(nameof(Textures));
            for (int i = 0; i < Textures.Length; i++)
            {
                if (Textures[i] is null) throw new System.NullReferenceException($"{nameof(Textures)}[{i}]");
                Textures[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 41;
                size += BuiltIns.UTF8.GetByteCount(Name);
                foreach (var i in Textures)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Material";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "dea645939e7a51f77d59181b714cdac1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr2UXW/bIBSG7y3lP/gfbGvXrZvUC2KTBA2w5Y900TQhkpAUKf6QwVnbXz+wTUrU22m+" +
                "sM3zHsF7jn1OL2t9H84xpDGL4QKUuAgfwo9B73EQx6hAa2iET0GgdCfrY1jzSgRRc2q625uQV1span1Z" +
                "7+Xh0Ks3XVRSKXkWweHUcG1A0/Kd1C+X9bavWqZ2/GS2vkD1JGtZC6XeE2ZciPqony5SJw4nsdPybLcd" +
                "3W9Pot6zqtmLoBDPuu/Er9+hHt9UMAse/vE1C0i+/B4aC6+sUkf1YUp/NvnppufR+Zue/H94mSrgvBSb" +
                "FDKaUOh97YHFaLEo8/FbezhPYVRikBl+43NA5ghS+8/c+hgSlOfjL/PZ5yuIlisbfXftIyMA5wZ/uTpz" +
                "hSiiMLfCV19IUhChYmPw/bX1PMUggmQ09M3XsD2XgNTmdZVvBhcYRgVKqJWuci7pD5o8DvwmmASzRYro" +
                "ki2yhLBy7VXPKXm6gplfPydEG4xoDP0SOmme/PQq6KhJhvoVdPzN152zlaSMmNZFKd54lgw1vetZMSAv" +
                "50UGosJzYWiM1iiGngcbSZKkWE07OA+WoyWFw1TwHTxmIGX25p0/sAgDMpTdhwRlWeJXYqAxjAAeTLgp" +
                "03LT4ibANLis9+J5itYvrZheK962dmiMQf2ZjXGXwTLMgAPf6ca1X9OKjmvZ1NP6T8fbYUqw/h05m9b8" +
                "C50kwhcjBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
