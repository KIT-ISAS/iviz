/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class PointCloud2 : IDeserializableCommon<PointCloud2>, IMessageCommon
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
    
        public PointCloud2()
        {
            Fields = System.Array.Empty<PointField>();
            Data = Tools.SharedRent<byte>.Empty;
        }
        
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
        
        public PointCloud2(ref ReadBuffer2 b)
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
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new PointCloud2(ref b);
        
        public PointCloud2 RosDeserialize(ref ReadBuffer b) => new PointCloud2(ref b);
        
        public PointCloud2 RosDeserialize(ref ReadBuffer2 b) => new PointCloud2(ref b);
    
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
        
        public void RosSerialize(ref WriteBuffer2 b)
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
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Height);
            WriteBuffer2.AddLength(ref c, Width);
            WriteBuffer2.AddLength(ref c, Fields);
            WriteBuffer2.AddLength(ref c, IsBigendian);
            WriteBuffer2.AddLength(ref c, PointStep);
            WriteBuffer2.AddLength(ref c, RowStep);
            WriteBuffer2.AddLength(ref c, Data);
            WriteBuffer2.AddLength(ref c, IsDense);
        }
    
        public const string MessageType = "sensor_msgs/PointCloud2";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "1158d486dd51d683ce2f1be655c3c181";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VV32/bNhB+nv6KQ/1Qu7A9OOmyIIAxFAuyBujSYs2eiiCgxLNNjCJdkkqm/fX7jpRs" +
                "Z9vDHjbBhqTT3Xff/eSE7ncmUssxqi3TzlsdSVHjreUmGe/Ib+huoU3LLuJVWdp741Kc0/PONDtqVV9N" +
                "oO+SMo6U1iYVNeM2PrQqY8QOmiqSE4mFLRAEL/Vz4tQsQYKBkpFJq6QInGLygbWYKaqNU6Gn2voaximS" +
                "Vb3voMuxCaaGWt1TyiBChUFQiENCrzaGEdQrUiGofllBBd4GX431nS4eEQjVTD5slTN/APFM09S0yMrC" +
                "mt94hi+00rCedghDM7jNlvTpCBNPbEEa5tk6jsj74HXXZKpCU7UcFALYpx1FJMOHeMhTTID34jEh8Qu/" +
                "WWys2e5SYQ+RBFeMCnnVfO1MzKmfk3I6R9548ETiEtMmwB3dXtMURaFzPSY7zpbVe1aIhnb5Jg7OrkEg" +
                "dE3qAo9ZPEnXkm6LrCQPlTokZA4U4QkhcFaZybPRiBBaYmLZbfH2D6BVh+fzswFgfMvGwul6KHSBaXbK" +
                "ObZxDNWEsSHQg6Iw9EvOjTTNssqFupFW+PJApSWqqvbeEi4TH2uzZaeNcjShW/ECxsV8/PDDSKrwfkSR" +
                "9rCd0IdDUGoICSzqPnE8WAT/POr/1QKfXupfgqB4/mZQ/vIL2vmB3qEe4/Tl73OK6DXJ7HSEf1OyNxsi" +
                "Q1gaTcI0XOid0MEiZx+1Vfg7D+9Pyho9dERVrf/jq/r5809XaCn92MZt/Lb0G2r6OaF8Kmhsn6RyrqU5" +
                "d4iAw8LyE1sYqXbPw4Smfs9xmecXQeOHwmCGrO2pi1BKHi3ftp0zjfS8jM4Le1jKiqK9Csk0nVXhbyMi" +
                "6PhF/tqxa2RirmShRG66ZECoB0ITWEXjtjJOY4FhkCvl4+qhmtw/+4VsuC2HIwvkXMlgEP++D1i3eUtc" +
                "wdmbEuUSTpAlhjuskmmWPeI1zlBn4cJ7j+0wRQif+rTzpdGfVDCqtrkNGqQCqK/F6PXsBNllaKecH+EL" +
                "4tHHv4F1B9y8lDCC2koaYrdFJqGIBfdk9GEXY7ANOpesqQOGsRKr4rKa3OR9dBwy3FWMvjGohKwMDD1W" +
                "kKDnsjwa/b+1Zd6ipTOPO2JsspfnogRVTpz9eDR6N64xhBr6Yf/APGP9KKvt7IBSDsSy6i7p9u7+UqZy" +
                "TatB8usgWtPZUWd1kSXnJzoiWtPbo46sGEi+O9ER0ZouBsnNh4/vRLSm708lF28huazGXDspyrAp7lQ5" +
                "ZfKqHNvcbzaRU1H4WJ43wbdS1ZBEu6SinB6Do1xgGV0xuh6f2XVy/pUDKzJ2Ue2fePTT+M6lgch77MdW" +
                "uZ7YcpsP9mHHF2bVnwMxVrzCCAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            Data.Dispose();
        }
    }
}
