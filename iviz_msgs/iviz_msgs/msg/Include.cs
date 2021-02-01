/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Include")]
    public sealed class Include : IDeserializable<Include>, IMessage
    {
        // Reference to an external asset
        [DataMember (Name = "uri")] public string Uri { get; set; } // Uri of the asset
        [DataMember (Name = "pose")] public Matrix4 Pose { get; set; } // Pose of the asset
        [DataMember (Name = "material")] public Material Material { get; set; }
        [DataMember (Name = "package")] public string Package { get; set; } // If uri has a model scheme, this indicates the package to search
    
        /// <summary> Constructor for empty message. </summary>
        public Include()
        {
            Uri = "";
            Pose = new Matrix4();
            Material = new Material();
            Package = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public Include(string Uri, Matrix4 Pose, Material Material, string Package)
        {
            this.Uri = Uri;
            this.Pose = Pose;
            this.Material = Material;
            this.Package = Package;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Include(ref Buffer b)
        {
            Uri = b.DeserializeString();
            Pose = new Matrix4(ref b);
            Material = new Material(ref b);
            Package = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Include(ref b);
        }
        
        Include IDeserializable<Include>.RosDeserialize(ref Buffer b)
        {
            return new Include(ref b);
        }
    
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
                size += BuiltIns.UTF8.GetByteCount(Uri);
                size += Material.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Package);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Include";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "39837fcbb846cd5b31941b250794d8ae";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71STUsDMRA9N79iYK+C2IqI4MmDeCiIH6dSZHZ3koxukiWTbYu/3nTNtohXay4vj7x5" +
                "8yZJBU+kKZJvCFIA9EC7RNFjByhCSSlJkb2BITLMZhW8ZgwakqUiWGIW7C6hD0J7wWPGXwqKnB1d2Uye" +
                "PTYfaMaqBz12sCiA4EJLHUhjydFZNmIB9i03uVxG31K4TyyEsbEKlLr946WWz/c3wBv+fHNi5LwMqnQX" +
                "MC3mq4urNTioIIZtnuw9xP/J8OMGPTpSd6ELcTEHdDWTTwfestaDHM/JsQhvSL3kNx4iTYJCT5++BFED" +
                "+3QNsaApWBfE0weZJj78w2TH5vmSttweiSU2Nk2s7vvviKs1tJhyzi8IGh8xPgMAAA==";
                
    }
}
