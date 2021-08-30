/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Include")]
    public sealed class Include : IDeserializable<Include>, IMessage
    {
        // Reference to an external asset
        [DataMember (Name = "uri")] public string Uri; // Uri of the asset
        [DataMember (Name = "pose")] public Matrix4 Pose; // Pose of the asset
        [DataMember (Name = "material")] public Material Material;
        [DataMember (Name = "package")] public string Package; // If uri has a model scheme, this indicates the package to search
    
        /// <summary> Constructor for empty message. </summary>
        public Include()
        {
            Uri = string.Empty;
            Pose = new Matrix4();
            Material = new Material();
            Package = string.Empty;
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
        
        public void Dispose()
        {
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
        [Preserve] public const string RosMd5Sum = "89c6a6240009410a08d4bbcad467b364";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE72UyW7bMBCGz9FTEMi1QBtnaVogB1mibaLaoCWpEQQCI1M2W2uBSDlOn74jiVJo5NrU" +
                "B1P8Zjj8Z8jhOQpZzhpWZgzJCtESsaNkTUn3iArBpGEI2fByi9qGo7Ozc5TAWOVI7phycCk4HK9QXQnW" +
                "OQQwvvNgDYeIhfoYY9Y0+023/SqS9zvsqEAUFdWG7ZHIdqxgnyAQF4iXG57BctHHVQs7xYLRJtsZyDDu" +
                "/vHPcKPld8QP/E9aiK34rBI18n1F5eXs8eLmCRXoHDXVC2T2q2r+j4ahgi0v5S2aO9izUxsvzMSJ0R36" +
                "csJN2yYxucdguJjOsaQFM6xqXzWXM0SLZ85KOc03PM9b8WZnBReCH9iYM6qg8ly+TvPntqhTkdE9hJ6g" +
                "2PGSl0yI9yQFFazcyt1kali+Z5mEFCHsoP55z8pN2l0CI4bb2Dbs8QnJ4Ut8fI1V8kpMo8btKE6N9OOF" +
                "qOTVhvE6wKnne1g7557ZZLFIouGUNRwF2EocMwQ+07npzgn2uttyqWPskigaLsuVzleYLFed9/WpjtA1" +
                "nQjwzcmeK+IRD0ed4atu8APTIvEa8O2p9ChwTAu7g6Bvus3p9nXNoMvrJN8QLxxsxcT3OtNJzon3w/Mf" +
                "ej4zlAFCBMRbpovQd9PkXqveaImCFQ71+o0Ga+0Qz8Z6CUfT3P+pVXCkkIynV3Dkb7quR1l+kLrQtCRw" +
                "1pokoNC1mhQAUTKPQ9OKNRVAbXJPbKxp6Dxd349XKsKVxsnSw7bik4KH0AzS7k/bv2eWY7qBpqGHLglD" +
                "X69ET21smU4v4u1Nh+YGB2hteLPZUXnL13q8yAWt6+65GJzaQzr4TU9K3/05zWQ19l5Vs4ZKXpVq/tLQ" +
                "un8f0vYdORjGX0PBjZnUBgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
