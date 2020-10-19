/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Material")]
    public sealed class Material : IMessage
    {
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "ambient")] public Color Ambient { get; set; }
        [DataMember (Name = "diffuse")] public Color Diffuse { get; set; }
        [DataMember (Name = "emissive")] public Color Emissive { get; set; }
        [DataMember (Name = "diffuseTexture")] public Texture DiffuseTexture { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Material()
        {
            Name = "";
            DiffuseTexture = new Texture();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Material(string Name, in Color Ambient, in Color Diffuse, in Color Emissive, Texture DiffuseTexture)
        {
            this.Name = Name;
            this.Ambient = Ambient;
            this.Diffuse = Diffuse;
            this.Emissive = Emissive;
            this.DiffuseTexture = DiffuseTexture;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Material(ref Buffer b)
        {
            Name = b.DeserializeString();
            Ambient = new Color(ref b);
            Diffuse = new Color(ref b);
            Emissive = new Color(ref b);
            DiffuseTexture = new Texture(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Material(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            Ambient.RosSerialize(ref b);
            Diffuse.RosSerialize(ref b);
            Emissive.RosSerialize(ref b);
            DiffuseTexture.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (DiffuseTexture is null) throw new System.NullReferenceException(nameof(DiffuseTexture));
            DiffuseTexture.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += DiffuseTexture.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Material";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "73d4b822c17551ea49c3de1364f2e6d4";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7WQMQ7CMAxFd5/CN0CCBYGYGJiYYEMVShQ3sUTSqHYK4vSgNrkBePl6+oOfLTpy8phM" +
                "JDgOj2FEEy1T0kqO+75I6yiyCE8EV3ppGanVFQEOPx44X0475Inf9yheVrMGFE66xZa+pq1p/q/R7pXl" +
                "e9lo2M/bN2t8stPQIBD7oI1szovjrUNn9Cv6AV9/IHZ/AQAA";
                
    }
}
