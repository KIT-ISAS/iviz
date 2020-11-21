/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract (Name = "sensor_msgs/PointCloud2")]
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
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // 2D structure of the point cloud. If the cloud is unordered, height is
        // 1 and width is the length of the point cloud.
        [DataMember (Name = "height")] public uint Height { get; set; }
        [DataMember (Name = "width")] public uint Width { get; set; }
        // Describes the channels and their layout in the binary data blob.
        [DataMember (Name = "fields")] public PointField[] Fields { get; set; }
        [DataMember (Name = "is_bigendian")] public bool IsBigendian { get; set; } // Is this data bigendian?
        [DataMember (Name = "point_step")] public uint PointStep { get; set; } // Length of a point in bytes
        [DataMember (Name = "row_step")] public uint RowStep { get; set; } // Length of a row in bytes
        [DataMember (Name = "data")] public byte[] Data { get; set; } // Actual point data, size is (row_step*height)
        [DataMember (Name = "is_dense")] public bool IsDense { get; set; } // True if there are no invalid points
    
        /// <summary> Constructor for empty message. </summary>
        public PointCloud2()
        {
            Header = new StdMsgs.Header();
            Fields = System.Array.Empty<PointField>();
            Data = System.Array.Empty<byte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PointCloud2(StdMsgs.Header Header, uint Height, uint Width, PointField[] Fields, bool IsBigendian, uint PointStep, uint RowStep, byte[] Data, bool IsDense)
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
        public PointCloud2(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
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
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                foreach (var i in Fields)
                {
                    size += i.RosMessageLength;
                }
                size += 1 * Data.Length;
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
                "H4sIAAAAAAAACrVWXW/bNhR9N+D/cFE/1C5sD3G6LAhgDMWCrAHatECzp2IwKJG2iFGkSlJO1V+/c0lJ" +
                "dro+7GETbFi6uvfc70PP6LHSgWoVgjgoqpyRgQSVzhhVRu0suT09rKSulQ14FIYap20MS3qqdFlRLbrp" +
                "ZAYDG4W2JKTUMetpu3e+FgkktFAVgSxLDIwBwYCxW5KK5RpRKIZJ2CRFFISoQnReSbYTVGgrfEeFcQWs" +
                "YyAjOtdCV4XS6wJqRUcxo3AwCjFy7BDRi71WyOsFCe9Ft55OWAkee3elca3MTpENFYqcPwirvwF0I2mu" +
                "a5RmZfRfaoE3dCHZfN4iF6kQ32JNH0844cwYgcM+mYcBuvFOtmUKN4UqauUFsmhiRQElcT6M1QoR+I59" +
                "RtR/5farvdGHKg4ZQMgpZrOcgCi/tBp1RdGXJKxM+ZcOoaJ+UdHewyHd39IczaHLlEpu6AKob5VASlSl" +
                "n+xkc4swfFvG1idn8XnZ1nSfZbmIaNpYlyVwOFoIGegihfOkJRKFGtsYZQ94+gHqdNLi4XLTQ4yPyTwH" +
                "dts3PkOVlbBWmTDkrP0wIBhKVujnJxWJhwguUtfueDQ+/0l5RBi6cM4QLh12hT4oK7WwNKN7doTAM8Lw" +
                "4tcxtBz/Dj1rYDyjd2Nyok8NkRRdVOFk4t3TYPC9CV59Z3CNKJPz4ZrRG7Rl2Mj0bkkBk8f1nQ/Yr3IJ" +
                "F2NqyEtiYtQJ5tG3sEldQJMFvtbB+VEYLfvpYOvtf3xNJ+8//X6D8ZK7OhzCT3n6uLefItoovAQvRZFy" +
                "5mmtkIfyK6OOysBK1A3WKL2NXaPCOm81cscH7cFeGdNRG6AVHZagrlurS94CXqdnAGzK7EWN8FGXrRH+" +
                "H1uT8Pkb1JdW2ZLX6Ia5JqiyjRpBdcAovRJB2wPv2NhoWMDw8cmtmPcOWLExAhRd8IqQ+tp4sHCijRt2" +
                "8yrnuAY8iqTgCOQyT7IdHsMCveYoVONAF3OE/7GLlcvjfhRei8KkUShRB8C+ZKOXi3NoDv2GrLBuwM+Q" +
                "Jyf/BteegBNPYRWl4RKE9oA6QhOkd9Ry5GgsuQY/k9GFx1JOJ2yWnQLkLlHUadfwK0JwpUYnmECYAEBJ" +
                "7CD1Zafl/zmdiVzzgJ4IYxy15wcn55YPpGY4O50dmA0Zg4AyHbF9QvuN6W4zwuQTs6e/a7p/eLzmFd3S" +
                "xSD6o5dtaXOmdXGVRJfnWizb0uszLaYciH4+12LZlq4G0d27D29YtqVfnomuXkN0zXXuS2+5ST1/PPA9" +
                "ck0cOg692++DilnjQ77fe1dzn31k9VyWfLoMzlLLeZ3Z6na4V7blYzKfakGBowp3RBF7T6VrAZQ9vQVv" +
                "1sJ2pIzCfxb8CegPgD646eRvcUsRXfMIAAA=";
                
    }
}
