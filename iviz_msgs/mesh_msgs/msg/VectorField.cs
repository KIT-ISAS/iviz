/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class VectorField : IDeserializable<VectorField>, IMessage
    {
        [DataMember (Name = "positions")] public GeometryMsgs.Point[] Positions;
        [DataMember (Name = "vectors")] public GeometryMsgs.Vector3[] Vectors;
    
        /// Constructor for empty message.
        public VectorField()
        {
            Positions = System.Array.Empty<GeometryMsgs.Point>();
            Vectors = System.Array.Empty<GeometryMsgs.Vector3>();
        }
        
        /// Explicit constructor.
        public VectorField(GeometryMsgs.Point[] Positions, GeometryMsgs.Vector3[] Vectors)
        {
            this.Positions = Positions;
            this.Vectors = Vectors;
        }
        
        /// Constructor with buffer.
        public VectorField(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out Positions);
            b.DeserializeStructArray(out Vectors);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new VectorField(ref b);
        
        public VectorField RosDeserialize(ref ReadBuffer b) => new VectorField(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Positions);
            b.SerializeStructArray(Vectors);
        }
        
        public void RosValidate()
        {
            if (Positions is null) BuiltIns.ThrowNullReference();
            if (Vectors is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 8 + 24 * Positions.Length + 24 * Vectors.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/VectorField";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9da8d62df10048ede4a91e419a35679d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71SwUrFQAy871cEvCiUCioeBM/yDoKgeBGRvDbdt9gmZZPns369afusiIIXsad0d2Z2" +
                "ZnYjSUeWh6dOox7fSGJ7eIReNFkS1hC/7N9TZZJPHfEyTRrC5R9/4fr26gLid1vhAO42SaESNkysYBta" +
                "jII0gP7nOEgMTSYC7bGi0LSCdn4Gr8s0LNPb/9jft/YRIFOfSYlN3fLc41fPJTh05UEUhNsBOkKPZfLJ" +
                "dGKdslM9eumqlKmRTAUkg1pIgWXsq8NnlyRWGtnY9y6GYBlZW5xq82WnHFIZywJ2G+IZlTg60BUiMeVU" +
                "QU4x1TPTD+oWMsI+XAHWnMAute3seT7Mr8hFsthEOCph1cAgW9iNgXzIUKPhKLSmxReu29GvFLAdjU8S" +
                "P7wHr0UVI3l3aoR1GX6563c9vu0P6wIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
