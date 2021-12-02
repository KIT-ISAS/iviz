/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
    
        /// Constructor for empty message.
        public Material()
        {
            Name = string.Empty;
            Textures = System.Array.Empty<Texture>();
        }
        
        /// Explicit constructor.
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
        
        /// Constructor with buffer.
        internal Material(ref Buffer b)
        {
            Name = b.DeserializeString();
            b.Deserialize(out Ambient);
            b.Deserialize(out Diffuse);
            b.Deserialize(out Emissive);
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new Material(ref b);
        
        Material IDeserializable<Material>.RosDeserialize(ref Buffer b) => new Material(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(ref Ambient);
            b.Serialize(ref Diffuse);
            b.Serialize(ref Emissive);
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
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Textures is null) throw new System.NullReferenceException(nameof(Textures));
            for (int i = 0; i < Textures.Length; i++)
            {
                if (Textures[i] is null) throw new System.NullReferenceException($"{nameof(Textures)}[{i}]");
                Textures[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 41 + BuiltIns.GetStringSize(Name) + BuiltIns.GetArraySize(Textures);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/Material";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "dea645939e7a51f77d59181b714cdac1";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrWUXW/bIBSG7/kV/gfb0nXrJvWC2CRBA2z5I11UTYgkJEWKP2Rw1vbXD/yREvV2zUVs" +
                "nvcI3nPMOZ2qzF0wJ4hFPEILWJA8uA8+g87jMIpwjtfICl8A0KZV1TGoRClBWJ/q9mYWiHKrZGUu6706" +
                "HDr9pstSaa3OEhxOtTAW1I3YKfNyWW+7suF6J0526wvUT6pSldT6PeHWhayO5ukitfJwkjujzm7bwf32" +
                "JKs9L+u9BLl8Nl0rH/8EZnjTANz/5x+g2fJnYA288lIf9acx+dFMOz6Pk7nxKT7eyJj8eGC+SRBnMUPe" +
                "d+5ZhBeLIhu+soezBIUFganlM59DOseIudty42NEcZYNl+Wrz1cIL1cu+vbaR0ohySz+dnXmCjPMUOaE" +
                "774QJzDE+cbiu2vrWUJgiOhg6IevEXcuhYnL6yrfFC0ICnMcMydd5VywXyx+6PkMjILdIsFsyRdpTHmx" +
                "9qo3KVmyQqlfv0kINwSzCPklnKR5/Nur4ERtMsyv4MTffN1OtuKEU9u0OCEbz5Kltms9KxZkxTxPYZh7" +
                "LiyN8BpHyPPgImkc56txh8mD43jJUD8PfAcPKUy4+/PO71lIIO3L7kOK0zT2K9HTCIWQ9Cam+dII29w2" +
                "wLa2qvbyeYw2L810kUvRNG5cDEHdmQ9xl5HSd/9B7Ew99V7dyFYYVVfj+m8rmn4+8O4dOQPwD4xsTocc" +
                "BQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
