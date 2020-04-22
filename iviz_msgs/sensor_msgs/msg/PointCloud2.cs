
namespace Iviz.Msgs.sensor_msgs
{
    public sealed class PointCloud2 : IMessage
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
        public std_msgs.Header header;
        
        // 2D structure of the point cloud. If the cloud is unordered, height is
        // 1 and width is the length of the point cloud.
        public uint height;
        public uint width;
        
        // Describes the channels and their layout in the binary data blob.
        public PointField[] fields;
        
        public bool is_bigendian; // Is this data bigendian?
        public uint point_step; // Length of a point in bytes
        public uint row_step; // Length of a row in bytes
        public byte[] data; // Actual point data, size is (row_step*height)
        
        public bool is_dense; // True if there are no invalid points
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/PointCloud2";
    
        public IMessage Create() => new PointCloud2();
    
        public int GetLength()
        {
            int size = 26;
            size += header.GetLength();
            for (int i = 0; i < fields.Length; i++)
            {
                size += fields[i].GetLength();
            }
            size += 1 * data.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public PointCloud2()
        {
            header = new std_msgs.Header();
            fields = new PointField[0];
            data = new byte[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out height, ref ptr, end);
            BuiltIns.Deserialize(out width, ref ptr, end);
            BuiltIns.DeserializeArray(out fields, ref ptr, end, 0);
            BuiltIns.Deserialize(out is_bigendian, ref ptr, end);
            BuiltIns.Deserialize(out point_step, ref ptr, end);
            BuiltIns.Deserialize(out row_step, ref ptr, end);
            BuiltIns.Deserialize(out data, ref ptr, end, 0);
            BuiltIns.Deserialize(out is_dense, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(height, ref ptr, end);
            BuiltIns.Serialize(width, ref ptr, end);
            BuiltIns.SerializeArray(fields, ref ptr, end, 0);
            BuiltIns.Serialize(is_bigendian, ref ptr, end);
            BuiltIns.Serialize(point_step, ref ptr, end);
            BuiltIns.Serialize(row_step, ref ptr, end);
            BuiltIns.Serialize(data, ref ptr, end, 0);
            BuiltIns.Serialize(is_dense, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "1158d486dd51d683ce2f1be655c3c181";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
