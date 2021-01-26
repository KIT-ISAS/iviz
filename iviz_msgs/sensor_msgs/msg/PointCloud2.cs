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
