/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/PointCloud2")]
    public sealed class PointCloud2 : IDeserializable<PointCloud2>, IMessage
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
        [DataMember (Name = "is_bigendian")] public bool IsBigendian; // Is this data bigendian?
        [DataMember (Name = "point_step")] public uint PointStep; // Length of a point in bytes
        [DataMember (Name = "row_step")] public uint RowStep; // Length of a row in bytes
        [DataMember (Name = "data")] public byte[] Data; // Actual point data, size is (row_step*height)
        [DataMember (Name = "is_dense")] public bool IsDense; // True if there are no invalid points
    
        /// <summary> Constructor for empty message. </summary>
        public PointCloud2()
        {
            Fields = System.Array.Empty<PointField>();
            Data = System.Array.Empty<byte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PointCloud2(in StdMsgs.Header Header, uint Height, uint Width, PointField[] Fields, bool IsBigendian, uint PointStep, uint RowStep, byte[] Data, bool IsDense)
        {
            this.Header = Header;
            this.Height = Height;
            this.Width = Width;
            this.Fields = Fields;
            this.IsBigendian = IsBigendian;
            this.PointStep = PointStep;
            this.RowStep = RowStep;
            this.Data = Data;
            this.IsDense = IsDense;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PointCloud2(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Height = b.Deserialize<uint>();
            Width = b.Deserialize<uint>();
            Fields = b.DeserializeArray<PointField>();
            for (int i = 0; i < Fields.Length; i++)
            {
                Fields[i] = new PointField(ref b);
            }
            IsBigendian = b.Deserialize<bool>();
            PointStep = b.Deserialize<uint>();
            RowStep = b.Deserialize<uint>();
            Data = b.DeserializeStructArray<byte>();
            IsDense = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PointCloud2(ref b);
        }
        
        PointCloud2 IDeserializable<PointCloud2>.RosDeserialize(ref Buffer b)
        {
            return new PointCloud2(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Height);
            b.Serialize(Width);
            b.SerializeArray(Fields, 0);
            b.Serialize(IsBigendian);
            b.Serialize(PointStep);
            b.Serialize(RowStep);
            b.SerializeStructArray(Data, 0);
            b.Serialize(IsDense);
        }
        
        public void RosValidate()
        {
            if (Fields is null) throw new System.NullReferenceException(nameof(Fields));
            for (int i = 0; i < Fields.Length; i++)
            {
                if (Fields[i] is null) throw new System.NullReferenceException($"{nameof(Fields)}[{i}]");
                Fields[i].RosValidate();
            }
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 26;
                size += Header.RosMessageLength;
                size += BuiltIns.GetArraySize(Fields);
                size += Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/PointCloud2";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1158d486dd51d683ce2f1be655c3c181";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVWXW/bNhR9F5D/cFE/1C5sD3G6LAhgDMWCrAG6tECzp2EwKJGWiFGkSlJOtV+/c0nJ" +
                "drY+7GETbFi6uvfc70PP6KnRgVoVgqgVNc7IQIIqZ4yqonaW3J4eV1K3ygY8CkOd0zaGJT03umqoFUMx" +
                "g76NQlsSUuqY1bTdO9+KhBF6aIpAliUGtkBgvDgsScVqjSAUUBIySREFIaYQnVeSzQSV2go/UGlcCeMY" +
                "yIjB9dBVofK6hFo5UEwgHIpCgBw4JPRqrxWSekXCezGsC6jA2+irMq6X2SMSoVKR87Ww+k8gbiTNdYuq" +
                "rIz+Qy3whi4lrOc90pAKsS3W9OkEE85sETTMk3WYkDvvZF+lUDlM0SovkEAXGwoohvPhWKcQAe/YY0Th" +
                "V26/2htdNzFHDxEnl41y8KL60mvUE8VekrAyZV45xInCRUV7D3f0cEdzNIWuOI3cxsW6eK8EsqEm/bCD" +
                "zR0C8H0Ve58cxZflWtNDluXioVPHgiyBwnFCCJzLFMmzlsgQWmxilK3x9A3Qosf91WYEmJ6SMcd0NzY6" +
                "w1SNsFaZMKWq/TQQmEFWGOcl1YaHZl2kRt3zKPz2O+WRKIrSOUO4dNiVulZWamFpRg/sBRFn8+nFj1NQ" +
                "Oe4dmtTBdkYfjkmJMSVEUQ5RhaOFd8+T/t8t8Oql/g0CTJ6na0bv0Itp89K7JQXMGVd1PkG/yZVbjFkh" +
                "JYkBUSeQJ9/DIlUefRX4WgfPB2G0HKehuCi2//F1Ufzy+edbDJTctaEO3+Vpu0BLP0d0T3gJ8okiJcyz" +
                "2SAJ5VdGHZSBlWg7bEx6G4dOhXVaX+SND/qCFTJmoD5AKTpMfNv2Vlc88rw5L+xhyQxFnfBRV70R/h8b" +
                "wuj4BPWlV7bihbllPgmq6qNGQAMQKq9E0LbmbZr6C4Ni9vTsVsxrNZbp6BzVFrwOpL52HiSbuOEWPt7k" +
                "5NbARnUUvIBA5km2w2NYoMMcguocOGGOyD8NsXF5vA/Ca1GaNAAVKgDU12z0enGGzGHfkhXWTfAZ8eTj" +
                "38DaI26iIiyeNJx96GsUEIqgtYOWRwbGOmvQLxldeqxgwVbZZTG7Tyx0Wi38ihBcpdEAJgqsOoiH0VM3" +
                "dlr+jwOZ2DPP5IkbeC6/cSByXvmo6aYz0dmJv5AtqCYTD8wT2E/MaZsjSj4JM8fd0MPj0w2v5JYuR8mv" +
                "o2hLm5PO5XWSXJ3psGhLb086V+AWSL4/02HRlq5Hyf2Hj+9YtKUfziXXbyG5KaZyW+7LSBOPfI8EE0dO" +
                "A+72+6BiVviY7/fetdxYH1k7lyIfG6Oj1GNeWja6m+6V7fngyydVUCCi0h3U5KdyPWCyn/cgxlbYgZRR" +
                "+PeBE30k9xzZRfEX+3XW+LwIAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
