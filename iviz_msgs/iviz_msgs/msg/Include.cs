/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Include")]
    public sealed class Include : IMessage
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
        internal Include(ref Buffer b)
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
                "H4sIAAAAAAAAE71STUsDMRA9N79iYK+C2IqI4smDeCiIH6dSZLo7uxndJEtmti3+etM1EcSrNZeXR968" +
                "eZOkgkdqKZKvCTQAeqC9UvTYA4qQGiMa2XcwRobZrIKXhKEFtZQFS0yC/TkMQeggeEj4S0GRk6PLm+I5" +
                "YP2O3VR1304dLAoguNBQD1JbcnSSjFiAfcN1KpfJNxceEgthrK0BY27+eJnl090V8JY/Xp10cpoHNW0f" +
                "UBfz1dnFGhxUEMMuTfYW4v9k+HGDHh2Z29Cn5ug2TF4za7htRyln5FiEt2Se0+uOkcpxpsfPPcUwI3u9" +
                "hIJdxk1GPH6MMu/3/1N7PXVfzGHHjdpCLHFntbDNMHxlXK2hQU1BPwGIelWuNwMAAA==";
                
    }
}
