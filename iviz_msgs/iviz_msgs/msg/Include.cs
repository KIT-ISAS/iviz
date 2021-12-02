/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Include : IDeserializable<Include>, IMessage
    {
        // Reference to an external asset
        [DataMember (Name = "uri")] public string Uri; // Uri of the asset
        [DataMember (Name = "pose")] public Matrix4 Pose; // Pose of the asset
        [DataMember (Name = "material")] public Material Material;
        [DataMember (Name = "package")] public string Package; // If uri has a model scheme, this indicates the package to search
    
        /// Constructor for empty message.
        public Include()
        {
            Uri = string.Empty;
            Pose = new Matrix4();
            Material = new Material();
            Package = string.Empty;
        }
        
        /// Explicit constructor.
        public Include(string Uri, Matrix4 Pose, Material Material, string Package)
        {
            this.Uri = Uri;
            this.Pose = Pose;
            this.Material = Material;
            this.Package = Package;
        }
        
        /// Constructor with buffer.
        internal Include(ref Buffer b)
        {
            Uri = b.DeserializeString();
            Pose = new Matrix4(ref b);
            Material = new Material(ref b);
            Package = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Include(ref b);
        
        Include IDeserializable<Include>.RosDeserialize(ref Buffer b) => new Include(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Uri);
            Pose.RosSerialize(ref b);
            Material.RosSerialize(ref b);
            b.Serialize(Package);
        }
        
        public void RosValidate()
        {
            if (Uri is null) throw new System.NullReferenceException(nameof(Uri));
            if (Pose is null) throw new System.NullReferenceException(nameof(Pose));
            Pose.RosValidate();
            if (Material is null) throw new System.NullReferenceException(nameof(Material));
            Material.RosValidate();
            if (Package is null) throw new System.NullReferenceException(nameof(Package));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 72;
                size += BuiltIns.GetStringSize(Uri);
                size += Material.RosMessageLength;
                size += BuiltIns.GetStringSize(Package);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/Include";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "89c6a6240009410a08d4bbcad467b364";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UyW7bMBA9R19BINcCbZylaYEcZIm2iWqDlqRGEAiMTNlstYGkHKdf39Hm0Mi1qQ+m" +
                "+N5w+GY4M+coZDkTrMoYUjWiFWIHxURFC0SlZMowpBK82qJWcHR2do4SWOscqR0bDVwKBocr1NSSdQYB" +
                "rO8smODgsRw/Jp8NzX7TbX+K5P0NOyoRRWW9YQWS2Y6V7BM44hLxasMzOC57v+PBTrFkVGQ7AxnG3T/+" +
                "GW60/I74nv9JS7mVn8dAjbyoqbqcPV7cPKESnSNRv0Bkv2rxfzQMGWx5pW7R3MGendp4YSZOjO7QlxPc" +
                "tG0Sk3sMxMXxHStaMsOqi1pczhAtnzmr1HG/4XneyjeelVxKvmdTzKiGzHP1etw/t2WTyowW4PoIyh2v" +
                "eMWkfI+koIJVW7U7UoLlBcsUhAhuB/XPBas2aVcERgzV2Ar2+ITU8CU/Psdj8KMYMa7bSdy40o8XMgY/" +
                "XhivA5x6voe1d+4xmywWSTS8sgZHAbYSxwwBn+m46c4J9rpqudRh7JIoGorlSsdXmCxXnfX1qY7QNZ0I" +
                "4JuTO1fEIx6OOuKrTviBaZF4DfDtqfQocEwLu4OgbzrndPe6ZtDFdRJviBcOtmLiex11EnPi/fD8hx6f" +
                "GSMBLgLiLdNF6Ltpcq9lb2KiYIVDPX8TYa0d4tlYT+FEzf2fWgYnFILx9AxO+Juu60mWH6QuNC0JnLUm" +
                "CVDoWk0KAFEyj0PTijUVgNrknthY09BZur4fr0YPk4YOJ0sP9/NAV/AQmkHa/Wn395jlmG6fdh10SRj6" +
                "eiZ61MaW6fQi3mY6NDcYQGvDzGaH0Vq9NlMhl7RpunExGLX7dLA7jpS++3OaqXrqvbphgipeV+P+RdCm" +
                "nw9p+w7ZG8ZfQ8GNmdQGAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
