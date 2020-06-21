using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Link")]
    public sealed class Link : IMessage
    {
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "origin_xyz")] public Vector3 OriginXyz { get; set; }
        [DataMember (Name = "origin_rpy")] public Vector3 OriginRpy { get; set; }
        [DataMember (Name = "visual_origin_xyz")] public Vector3 VisualOriginXyz { get; set; }
        [DataMember (Name = "visual_origin_rpy")] public Vector3 VisualOriginRpy { get; set; }
        [DataMember (Name = "geometry_shape")] public string GeometryShape { get; set; }
        [DataMember (Name = "geometry_params")] public Vector3 GeometryParams { get; set; }
        [DataMember (Name = "geometry_mesh")] public string GeometryMesh { get; set; }
        [DataMember (Name = "material_color")] public Color MaterialColor { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Link()
        {
            Name = "";
            GeometryShape = "";
            GeometryMesh = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public Link(string Name, in Vector3 OriginXyz, in Vector3 OriginRpy, in Vector3 VisualOriginXyz, in Vector3 VisualOriginRpy, string GeometryShape, in Vector3 GeometryParams, string GeometryMesh, in Color MaterialColor)
        {
            this.Name = Name;
            this.OriginXyz = OriginXyz;
            this.OriginRpy = OriginRpy;
            this.VisualOriginXyz = VisualOriginXyz;
            this.VisualOriginRpy = VisualOriginRpy;
            this.GeometryShape = GeometryShape;
            this.GeometryParams = GeometryParams;
            this.GeometryMesh = GeometryMesh;
            this.MaterialColor = MaterialColor;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Link(Buffer b)
        {
            Name = b.DeserializeString();
            OriginXyz = new Vector3(b);
            OriginRpy = new Vector3(b);
            VisualOriginXyz = new Vector3(b);
            VisualOriginRpy = new Vector3(b);
            GeometryShape = b.DeserializeString();
            GeometryParams = new Vector3(b);
            GeometryMesh = b.DeserializeString();
            MaterialColor = new Color(b);
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Link(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Name);
            OriginXyz.RosSerialize(b);
            OriginRpy.RosSerialize(b);
            VisualOriginXyz.RosSerialize(b);
            VisualOriginRpy.RosSerialize(b);
            b.Serialize(GeometryShape);
            GeometryParams.RosSerialize(b);
            b.Serialize(GeometryMesh);
            MaterialColor.RosSerialize(b);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException();
            if (GeometryShape is null) throw new System.NullReferenceException();
            if (GeometryMesh is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 76;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += BuiltIns.UTF8.GetByteCount(GeometryShape);
                size += BuiltIns.UTF8.GetByteCount(GeometryMesh);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Link";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b302830c17da873399a033cf1ddd5320";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VPuw7CMAzc/RX5AyS6ICQmBiYmJNbIVCG11CSVnVZNvh4KJYjHCF7ufD6/JDJ5qzw6" +
                "A0dTx8CVCkyWvB5Tfpe4S0UaSHps9Rfza2XqkfsWa4IzkZOWBrvnviJ3yOjkw+2MNLANbWDlMBqm6/B6" +
                "SmHz44D9YbdWNFDWTqws5gvh3AaM1VKNhaXCMvz9jNvv0JOPK/VAO+NpRgS4ALE5ruDMAQAA";
                
    }
}
