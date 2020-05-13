using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    [DataContract]
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
        [DataMember] public std_msgs.Header header { get; set; }
        
        // 2D structure of the point cloud. If the cloud is unordered, height is
        // 1 and width is the length of the point cloud.
        [DataMember] public uint height { get; set; }
        [DataMember] public uint width { get; set; }
        
        // Describes the channels and their layout in the binary data blob.
        [DataMember] public PointField[] fields { get; set; }
        
        [DataMember] public bool is_bigendian { get; set; } // Is this data bigendian?
        [DataMember] public uint point_step { get; set; } // Length of a point in bytes
        [DataMember] public uint row_step { get; set; } // Length of a row in bytes
        [DataMember] public byte[] data { get; set; } // Actual point data, size is (row_step*height)
        
        [DataMember] public bool is_dense { get; set; } // True if there are no invalid points
    
        /// <summary> Constructor for empty message. </summary>
        public PointCloud2()
        {
            header = new std_msgs.Header();
            fields = System.Array.Empty<PointField>();
            data = System.Array.Empty<byte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PointCloud2(std_msgs.Header header, uint height, uint width, PointField[] fields, bool is_bigendian, uint point_step, uint row_step, byte[] data, bool is_dense)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.height = height;
            this.width = width;
            this.fields = fields ?? throw new System.ArgumentNullException(nameof(fields));
            this.is_bigendian = is_bigendian;
            this.point_step = point_step;
            this.row_step = row_step;
            this.data = data ?? throw new System.ArgumentNullException(nameof(data));
            this.is_dense = is_dense;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PointCloud2(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.height = b.Deserialize<uint>();
            this.width = b.Deserialize<uint>();
            this.fields = b.DeserializeArray<PointField>();
            for (int i = 0; i < this.fields.Length; i++)
            {
                this.fields[i] = new PointField(b);
            }
            this.is_bigendian = b.Deserialize<bool>();
            this.point_step = b.Deserialize<uint>();
            this.row_step = b.Deserialize<uint>();
            this.data = b.DeserializeStructArray<byte>();
            this.is_dense = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new PointCloud2(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.header);
            b.Serialize(this.height);
            b.Serialize(this.width);
            b.SerializeArray(this.fields, 0);
            b.Serialize(this.is_bigendian);
            b.Serialize(this.point_step);
            b.Serialize(this.row_step);
            b.SerializeStructArray(this.data, 0);
            b.Serialize(this.is_dense);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            header.Validate();
            if (fields is null) throw new System.NullReferenceException();
            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i] is null) throw new System.NullReferenceException();
                fields[i].Validate();
            }
            if (data is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 26;
                size += header.RosMessageLength;
                for (int i = 0; i < fields.Length; i++)
                {
                    size += fields[i].RosMessageLength;
                }
                size += 1 * data.Length;
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/PointCloud2";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1158d486dd51d683ce2f1be655c3c181";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VV32/bNhB+119xqB9qF7GHpF0WBDCGYkHWAF1aoNnTMASUeLaJUaRLUsm0v37fkaLt" +
                "bHvYwybYkHS6++73xxk97EyknmNUW6adtzqSos5by10y3pHf0P1Sm55dxKuytPfGpXhGzzvT7ahXYzOD" +
                "vkvKOFJam1TUjNv40KuMEQdoqkhOJBa2QBC8NJ4Rp26FIBgoGZm0SooQU0w+sBYzRa1xKozUWt/COEWy" +
                "avQDdDl2wbRQa0dKGURCYQQogUNCrzaGkdQrUiGocdVABd4mX531gy4ekQi1TD5slTN/APFC09z0qMrS" +
                "mt94gS90rmE9H5CGZsS2WNHnI0w8sUXQMM/WsSLvg9dDl0OVMFXPQSGBfdpRRDF8iIc6xQR4Lx4TCr/0" +
                "m+XGmu0uleghkuSKUQledV8HE3Ppz0g5nTPvPOJE4RLTJsAd3d3QHE2ht7oWOy5WzQdWyIZ2+SYOLm4Q" +
                "QBi6NASuVTwp14ruiqwUD506FOQMKBInhMA5z5E8G40MoSUmlt0Wb/8A2gx4fnsxAdS3bCwx3UyNLjDd" +
                "TjnHNtZUTagDgRkUhWlecm1kaFZNbtStjMIvv1IZiaZpvbeEy8TH1mzZaaMczehOvCDiYl4/fF+DKnE/" +
                "okl72M7o4yEpNaWEKNoxcTxYBP9c9f9qgU8v9a8QYPZcrxm9Ry/q5uVvZxQxZ1LVeYV+Uyq3mLJCShoD" +
                "wkeQhzDAIlcefVX4Ow/PT8oaPU1D06z/46v56cuP1xgn/djHbfymzBr6+SWhdSpoME9SOVsZzB0y4LC0" +
                "/MQWRqrf87SdadxzXOXdRdL4oSnYH2tHGiKUkse49/3gTCfzLmvzwh6WQk+0VyGZbrAq/G09BB2/yF8H" +
                "dp1sy7WQSeRuSAYBjUDoAqto3FZWqTYXBs3s4dkvhdS2HI7OUWolu0D8+z6AYTMxXMPHm5LcCtgoDsML" +
                "2GOeZY94jQu0V0LgvQchzBH55zHtfJntJxWMam3ufocKAPW1GL1enCC7DO2U8xW+IB59/BtYd8DNPISt" +
                "01ayj8MWBYQiOO3J6AP9YpcNuJesaQP2rxGr4rKZ3WYKOu4V7ipG3xk0QFgCew7WEfTcjUej/7dpzMRZ" +
                "BvJIC3W2Xh6FklQ5ZPb1NPSuMhdSDeNEOTDPWD8Im10cUMoZWNjtiu7uH65kGdd0Pkl+nkRrujjqnF9m" +
                "ydsTHRGt6d1RR1gFkm9PdES0pstJcvvx03sRrem7U8nlO0iumlprJ02ZCOJelYMls2Odbr/ZRE5F4VN5" +
                "3gTfS1dDEu1SinJgTI5yg2VjxeimPrMb5MgrZ1RkUFDrn7j66fzg0hTIB1Bir9xIbLnPZ/lE6yWy5k8H" +
                "yKIrtQgAAA==";
                
    }
}
