/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class PointCloud2 : IDeserializableRos1<PointCloud2>, IMessageRos1
    {
        // This message holds a collection of N-dimensional points, which may
        // contain additional information such as normals, intensity, etc. The
        // point data is stored as a binary blob, its layout described by the
        // contents of the "fields" array.
        // The point cloud data may be organized 2d (image-like) or 1d
        // (unordered). Point clouds organized as 2d images may be produced by
        // camera depth sensors such as stereo or time-of-flight.
        // Time of sensor data acquisition, and the coordinate frame ID (for 3d
        // points).
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // 2D structure of the point cloud. If the cloud is unordered, height is
        // 1 and width is the length of the point cloud.
        [DataMember (Name = "height")] public uint Height;
        [DataMember (Name = "width")] public uint Width;
        // Describes the channels and their layout in the binary data blob.
        [DataMember (Name = "fields")] public PointField[] Fields;
        /// <summary> Is this data bigendian? </summary>
        [DataMember (Name = "is_bigendian")] public bool IsBigendian;
        /// <summary> Length of a point in bytes </summary>
        [DataMember (Name = "point_step")] public uint PointStep;
        /// <summary> Length of a row in bytes </summary>
        [DataMember (Name = "row_step")] public uint RowStep;
        /// <summary> [Rent] Actual point data, size is (row_step*height) </summary>
        [DataMember (Name = "data")] public Tools.SharedRent<byte> Data;
        /// <summary> True if there are no invalid points </summary>
        [DataMember (Name = "is_dense")] public bool IsDense;
    
        /// Constructor for empty message.
        public PointCloud2()
        {
            Fields = System.Array.Empty<PointField>();
            Data = Tools.SharedRent<byte>.Empty;
        }
        
        /// Constructor with buffer.
        public PointCloud2(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Height);
            b.Deserialize(out Width);
            b.DeserializeArray(out Fields);
            for (int i = 0; i < Fields.Length; i++)
            {
                Fields[i] = new PointField(ref b);
            }
            b.Deserialize(out IsBigendian);
            b.Deserialize(out PointStep);
            b.Deserialize(out RowStep);
            b.DeserializeStructRent(out Data);
            b.Deserialize(out IsDense);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PointCloud2(ref b);
        
        public PointCloud2 RosDeserialize(ref ReadBuffer b) => new PointCloud2(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Height);
            b.Serialize(Width);
            b.SerializeArray(Fields);
            b.Serialize(IsBigendian);
            b.Serialize(PointStep);
            b.Serialize(RowStep);
            b.SerializeStructArray(Data);
            b.Serialize(IsDense);
        }
        
        public void RosValidate()
        {
            if (Fields is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Fields.Length; i++)
            {
                if (Fields[i] is null) BuiltIns.ThrowNullReference(nameof(Fields), i);
                Fields[i].RosValidate();
            }
            if (Data is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 26;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetArraySize(Fields);
                size += Data.Length;
                return size;
            }
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/PointCloud2";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "1158d486dd51d683ce2f1be655c3c181";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VV32/bNhB+nv6KQ/1Qu7A9JGmzIIAxFAuyBujSos2eisKgxLNNjCJdkkqm/fX7jpRs" +
                "Z9vDHjbBhqTT3Xff/eSEHnYmUssxqi3TzlsdSVHjreUmGe/Ib+h+oU3LLuJVWdp741Kc09PONDtqVV9N" +
                "oO+SMo6U1iYVNeM2PrQqY8QOmiqSE4mFLRAEL/Vz4tQsQYKBkpFJq6QInGLygbWYKaqNU6Gn2voaximS" +
                "Vb3voMuxCaaGWt1TyiBChUFQiENCLzaGEdQLUiGofllBBd4GX431nS4eEQjVTD5slTN/APFc09S0yMrC" +
                "mt94hi90pmE97RCGZnCbLenjESae2II0zLN1HJH3weuuyVSFpmo5KASwTzuKSIYP8ZCnmADvxWNC4hd+" +
                "s9hYs92lwh4iCa4YFfKq+daZmFM/J+V0jrzx4InEJaZNgDu6u6EpikIXekx2nC2rd6wQDe3yTRyc34BA" +
                "6JrUBR6zeJKuJd0VWUkeKnVIyBwowhNC4JxlJk9GI0JoiYllt8XbP4BWHZ4vzgeA8S0bC6ebodAFptkp" +
                "59jGMVQTxoZAD4rC0C85N9I0yyoX6lZa4ctXKi1RVbX3lnCZuK7Nlp02ytGE7sQLGBfz8cOPI6nCe40i" +
                "7WE7ofeHoNQQEljUfeJ4sAj+adT/qwU+Pde/AkHx/N2g/OUT2vkrvUU9xunL3+cU0WuS2ekI/6pkbzZE" +
                "hrA0moRpuNA7oYNFzj5qq/B3Ht4flTV66IiqWv3HV/XL55+v0VJ63cZt/L70G2r6OaF8Kmhsn6RyrqU5" +
                "d4iAw8LyI1sYqXbPw4Smfs9xmecXQeOHwmCGrO2pi1BKHi3ftp0zjfS8jM4ze1jKiqK9Csk0nVXhbyMi" +
                "6PhF/taxa2RirmWhRG66ZECoB0ITWEXjtjJOY4FhUE0envxCFtuWw9E5Uq1kHoh/3wds2bwcruHjVQlu" +
                "CWwkh+EFG2SaZWu8xhnKKxR477EUpmD+sU87X/r7UQWjapur3yADQH0pRi9nJ8guQzvl/AhfEI8+/g2s" +
                "O+DmXYTJ01aij90WCYQi9tqj0YcVjHk2aFiypg6YwUqsistqcpvX0HG2cFcx+sagALIpMOvYPIKeq7E2" +
                "+n/rxrw8S0MeV8PYW8+PQwmqHDT78UT0btxeCDX0w9qBecb6STba+QGlnINlw13R3f3DlQzjis4Gya+D" +
                "aEXnR52zyyy5ONER0YpeH3Vks0Dy5kRHRCu6HCS37z+8FdGKfjiVXL6G5Koac+2kKMOCuFflcMkbcuxu" +
                "v9lETkXhQ3neBN9KVUMS7ZKKcmgMjnKBZWLF6GZ8ZtfJsVfOqchYQbV/5NFP4zuXBiLvsBZb5Xpiy20+" +
                "z4fVXphVfwK7taIQuQgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            Data.Dispose();
        }
    }
}
