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
                "H4sIAAAAAAAAE71V2W6cMBR9DhL/YCmvldpMlqaV8sCAZ8Yqm1iSjqIIOYyZccsmbCZJv76XLUDTxzY8" +
                "gHXO9fW5x/blFHksYRXLY4ZkgWiO2LNkVU5TRIVgUlGErHi+R3XF0cnJKQrhWyRIHlgfYFEIeL5AZSFY" +
                "E+DC900EqzhkzPrBkLOk8U+6b2eRpF3hQAWiKCt2LEUiPrCMfYBEXCCe73gM00Wbt5/YKBaMVvFBQYpy" +
                "848fxfLXXxE/8l9RJvbiY1+okqQFleeL+7OrB5ShU1QVT1DZj6J6Hw2dgzXP5TVamtg2IgOvtNAM0A36" +
                "pM4IzTBIQG4xMGeqog625zRjqqIXaVGdLxDNHjnL5QjseJLUYhLBMi4EPwLSl44K2AAuX0bgsc7KSMQ0" +
                "hfwjKg485zkT4i9QBGJYvpeHkatYkrJYQrVN6q6Qx5Tlu6g5EKoSwNGsK3b/gGQ3grz/3fHehF5ONeja" +
                "vwocBvQdxPQO9CsGWxdHtmPj6c63oEFWq9Dv932C+y7WQ1PzgFjMCM1aEmw3R+h8hmOL+H53gi5mxAaT" +
                "9aaJv1TnajxLM33Ar+YLb4hNbOw3zOcZ47iaToIt4Nd/lOC7pqZjq5P1ZUaazeKW5jYFziv38MrEekAc" +
                "u+Hm1Yf2N9u5a4lFcx06CtK4xF5HK8+xovB26uVA+e4GezM3B0bfmsQ28MzQgVs636d+DjBUZc/8HIhR" +
                "3uWoznEjC+42cc3tVBnAcLmnigDxw2XgaXowFQOwQW6JgadSmljLcYJNn+RiSpC1jY2emAi58zQ3al5T" +
                "GS2om5rlTqW0qEU8z5n50sIG1jWz06KOv4GmC0AM9ADo8+x5mCFfSjaMM1qWbXPp4upj1Ie+tqC2UyQ0" +
                "lsXrLS1KVlHJi3wAnipatt0kqt9CR7jBvwEepvnLEAcAAA==";
                
    }
}
