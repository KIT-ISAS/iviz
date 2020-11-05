/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Material")]
    public sealed class Material : IDeserializable<Material>, IMessage
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
                "H4sIAAAAAAAAE7WQsQoCMQyG9zxF3kDQRQQnBycn3USOlubagO2VS3qKT6/ctfgCmuXn4x/yJaIjJ4/J" +
                "RILDcB9GNNEyJa3kuO+LtI4ii/BEcKGnlpFaXRFg/+OB0/m4Q5741UXxspo1oHDSLbb0NW1N83+Ndq8s" +
                "38tGw7x8s8YHuy8EYh+0kc15Ubze0Bn9eL4BQkK3Hn4BAAA=";
                
    }
}
