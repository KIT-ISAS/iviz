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
        [DataMember (Name = "package")] public string Package { get; set; } // If uri has a model scheme, this indicates the package to search
    
        /// <summary> Constructor for empty message. </summary>
        public Include()
        {
            Uri = "";
            Pose = new Matrix4();
            Package = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public Include(string Uri, Matrix4 Pose, string Package)
        {
            this.Uri = Uri;
            this.Pose = Pose;
            this.Package = Package;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Include(Buffer b)
        {
            Uri = b.DeserializeString();
            Pose = new Matrix4(b);
            Package = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Include(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Uri);
            Pose.RosSerialize(b);
            b.Serialize(Package);
        }
        
        public void RosValidate()
        {
            if (Uri is null) throw new System.NullReferenceException(nameof(Uri));
            if (Pose is null) throw new System.NullReferenceException(nameof(Pose));
            Pose.RosValidate();
            if (Package is null) throw new System.NullReferenceException(nameof(Package));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 72;
                size += BuiltIns.UTF8.GetByteCount(Uri);
                size += BuiltIns.UTF8.GetByteCount(Package);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Include";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "39357b5e088cc0f3ccc5668047d0227b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE62PPQvCQBBE69yvGEgriB9YCNZiERDFSkSWc5Oc5u7C7anBX+8lpLF3m2nePGZzHLjk" +
                "wE4zogc5cBc5OGpAIhyVkhiMq/AMBlmW45TSl4g1j0BBCeiWaL1wD+xT/hKjoSX9oGpgduXgq0lAsP7G" +
                "DUTXbHmSakZg3M1oiiyDZSz2+4Qp6FpBqc2fTxXH7RrmZT5XK5VMx7dU2XiKi/l5trrAIkfwb1i6+5A2" +
                "fAGX2RvzPQEAAA==";
                
    }
}
