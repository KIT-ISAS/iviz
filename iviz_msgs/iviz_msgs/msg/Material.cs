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
                "H4sIAAAAAAAAE72Uy27bMBBF9wL8D/qDtk7SpgWyoCXaJkpRhB5OjaIgaJt2CFgPSJSb5OtLvSyyybKo" +
                "FwJx7nh4Z6SZRubq3l1gSHzmwyVIceI+uB9nTmMIwPdRgjZQK59mzsypVSXzk5vzTMwcrzgX1c3c5dlO" +
                "ilxN4CCPx6Y2IkQm61peNDmeC640KUq+l+plArsmK1m952edf6L1k8xlLur6HcS0GZGf1NOkVeJ4Fnsl" +
                "L13qvpDdWeQHlhUHfXsinlVTiZ+/XNWfdF7n4R//nCBefXO1h1eW1af6w9CEwU41+jpdDY4H/h/MDB0Y" +
                "bky2FDISEmi++Q76aLlM4+G9Gzym0EsxiLQwtwQQLBAk7Sd0Y3EYoDjuv6BbS1hDtFq38Xcz200UABxr" +
                "/tm+eI0IIjBulS+WElLgoWSr+f1fJcQUAw8Gva2vlojbywNA2wLtyiO4xNBLUEhaza4+Jd9J+NgJ83Yc" +
                "ekmnoYis2DIKA5ZuzF6OUkzXMLK6OSreFiPiQ6uho7YIf5j9HLGuilj9HIXJ3t3kLqQs0LONKN6azjTW" +
                "w2060iROF0kEvMQ0o7GPNsiHppU2NgjDZD0kuTUFtCLQHwTDyGMEKGsfpo0OehgE1LTS0QBFUWj1pcM+" +
                "9ADuvVz3UcnbLaBj9A6Q+UE8j/9QL6UYzxkvy2659HHNhQ2h1xXUbYoj36viOqVFKSquZJGP4HfFy26b" +
                "sOYtuugJ/gMjjxTyWAUAAA==";
                
    }
}
