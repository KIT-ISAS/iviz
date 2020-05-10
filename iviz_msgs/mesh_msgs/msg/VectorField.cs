using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class VectorField : IMessage
    {
        public geometry_msgs.Point[] positions { get; set; }
        public geometry_msgs.Vector3[] vectors { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public VectorField()
        {
            positions = System.Array.Empty<geometry_msgs.Point>();
            vectors = System.Array.Empty<geometry_msgs.Vector3>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public VectorField(geometry_msgs.Point[] positions, geometry_msgs.Vector3[] vectors)
        {
            this.positions = positions ?? throw new System.ArgumentNullException(nameof(positions));
            this.vectors = vectors ?? throw new System.ArgumentNullException(nameof(vectors));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal VectorField(Buffer b)
        {
            this.positions = b.DeserializeStructArray<geometry_msgs.Point>(0);
            this.vectors = b.DeserializeStructArray<geometry_msgs.Vector3>(0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new VectorField(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeStructArray(this.positions, 0);
            b.SerializeStructArray(this.vectors, 0);
        }
        
        public void Validate()
        {
            if (positions is null) throw new System.NullReferenceException();
            if (vectors is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += 24 * positions.Length;
                size += 24 * vectors.Length;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "mesh_msgs/VectorField";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "9da8d62df10048ede4a91e419a35679d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71SwUrFQAy871cEvCiUCioeBM/yDoKgeBGRvDbdt9gmZZPns369afusiIIXsad0d2Z2" +
                "ZnYjSUeWh6dOox7fSGJ7eIReNFkS1hC/7N9TZZJPHfEyTRrC5R9/4fr26gLid1vhAO42SaESNkysYBta" +
                "jII0gP7nOEgMTSYC7bGi0LSCdn4Gr8s0LNPb/9jft/YRIFOfSYlN3fLc41fPJTh05UEUhNsBOkKPZfLJ" +
                "dGKdslM9eumqlKmRTAUkg1pIgWXsq8NnlyRWGtnY9y6GYBlZW5xq82WnHFIZywJ2G+IZlTg60BUiMeVU" +
                "QU4x1TPTD+oWMsI+XAHWnMAute3seT7Mr8hFsthEOCph1cAgW9iNgXzIUKPhKLSmxReu29GvFLAdjU8S" +
                "P7wHr0UVI3l3aoR1GX6563c9vu0P6wIAAA==";
                
    }
}
