/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StereoMsgs
{
    [Preserve, DataContract (Name = "stereo_msgs/DisparityImage")]
    public sealed class DisparityImage : IDeserializable<DisparityImage>, IMessage
    {
        // Separate header for compatibility with current TimeSynchronizer.
        // Likely to be removed in a later release, use image.header instead.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Floating point disparity image. The disparities are pre-adjusted for any
        // x-offset between the principal points of the two cameras (in the case
        // that they are verged). That is: d = x_l - x_r - (cx_l - cx_r)
        [DataMember (Name = "image")] public SensorMsgs.Image Image { get; set; }
        // Stereo geometry. For disparity d, the depth from the camera is Z = fT/d.
        [DataMember (Name = "f")] public float F { get; set; } // Focal length, pixels
        [DataMember] public float T { get; set; } // Baseline, world units
        // Subwindow of (potentially) valid disparity values.
        [DataMember (Name = "valid_window")] public SensorMsgs.RegionOfInterest ValidWindow { get; set; }
        // The range of disparities searched.
        // In the disparity image, any disparity less than min_disparity is invalid.
        // The disparity search range defines the horopter, or 3D volume that the
        // stereo algorithm can "see". Points with Z outside of:
        //     Z_min = fT / max_disparity
        //     Z_max = fT / min_disparity
        // could not be found.
        [DataMember (Name = "min_disparity")] public float MinDisparity { get; set; }
        [DataMember (Name = "max_disparity")] public float MaxDisparity { get; set; }
        // Smallest allowed disparity increment. The smallest achievable depth range
        // resolution is delta_Z = (Z^2/fT)*delta_d.
        [DataMember (Name = "delta_d")] public float DeltaD { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public DisparityImage()
        {
            Image = new SensorMsgs.Image();
            ValidWindow = new SensorMsgs.RegionOfInterest();
        }
        
        /// <summary> Explicit constructor. </summary>
        public DisparityImage(in StdMsgs.Header Header, SensorMsgs.Image Image, float F, float T, SensorMsgs.RegionOfInterest ValidWindow, float MinDisparity, float MaxDisparity, float DeltaD)
        {
            this.Header = Header;
            this.Image = Image;
            this.F = F;
            this.T = T;
            this.ValidWindow = ValidWindow;
            this.MinDisparity = MinDisparity;
            this.MaxDisparity = MaxDisparity;
            this.DeltaD = DeltaD;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public DisparityImage(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Image = new SensorMsgs.Image(ref b);
            F = b.Deserialize<float>();
            T = b.Deserialize<float>();
            ValidWindow = new SensorMsgs.RegionOfInterest(ref b);
            MinDisparity = b.Deserialize<float>();
            MaxDisparity = b.Deserialize<float>();
            DeltaD = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new DisparityImage(ref b);
        }
        
        DisparityImage IDeserializable<DisparityImage>.RosDeserialize(ref Buffer b)
        {
            return new DisparityImage(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Image.RosSerialize(ref b);
            b.Serialize(F);
            b.Serialize(T);
            ValidWindow.RosSerialize(ref b);
            b.Serialize(MinDisparity);
            b.Serialize(MaxDisparity);
            b.Serialize(DeltaD);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Image is null) throw new System.NullReferenceException(nameof(Image));
            Image.RosValidate();
            if (ValidWindow is null) throw new System.NullReferenceException(nameof(ValidWindow));
            ValidWindow.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 37;
                size += Header.RosMessageLength;
                size += Image.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "stereo_msgs/DisparityImage";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "04a177815f75271039fa21f16acad8c9";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1XUY/jNg5+N7D/gdg8dLKdySzal2KKxRW9vbkboIcW3QEKbNELFJuO1cqSK8lJ3F9/" +
                "HyXbcWa62z70LsBMYlmkSH7kR2pF77hTXkWmhlXFnmrnqXRtp6LeaaPjQEcdGyp779lGetQtvxts2Xhn" +
                "9W/sN8WKvtG/sBkoOtoxeW7dgSvSlhQZKPZYMqwCX1MfmHSr9rwZD9M2RPzaFP/Kz3m5gM5742CB3VPn" +
                "NI6tdICZYk2Wp8eG50XNgZRn6jzfqOrnHjqr5IeyA1SdblxdB46wLh6ZLUXIdl7bUnfK5AMCuTqtx6Oj" +
                "UrXsVaAr+CBrJYyHntioKM9DOuzAfs/VWizBsg53VNEbOm0N3eC/x/+rMj/hy6+LwDY4v23DPtw+iA/Z" +
                "E/H1HYLEjvbsWo5+2NA9bD97XF0nKyrugEPtXTsaJUbiYHqPc+vHW0SxlqB9/hnVhAC6Es4ZtvvYXFOn" +
                "T2zCvOERG76GV0ZbwHJ03lTUWx1DMqffHbWt3FFictW5CNy1MmZY00EZXS1Mw3PPYXPh3Pe8185+Wz9Y" +
                "cSvELLTNKkW/QOeVRQSgf4lhYOXLhuHIih5y6J/gfi2QLhYNhyC4WGq13S42B6RWOld0LXNlGE8ZLai4" +
                "RghEBwrAedfB5mtC+D9/Swdn+pZn2KEoZKCU2TuoalqAYOllYH65oe9yGqVieU+uj0FX4uEd5OTzfgsT" +
                "E1J0S606na09b1CnecPSHWwoXQ+IrJMkRmr3doH25d559eIIgbUFhoIHvtwRFbIIly1RtoA511WYd5aN" +
                "5oPamSn7UtCgC7giOBE4S6grNlFtJQ2v3v/ns9v6cf0qLy2MHBeKF8Wbv/jzovj3u3/eAZsq51+mkhep" +
                "rpStlK8IZaUqFVUihUbvG/Y3hg9sIKXaTmIhb+PQSTJLwkgCBVSkRY0h84W5KiE4MGOLQimFMCOo8EIe" +
                "kon1ENWoy94oYVLnK21le+1RsaJdEol/7dmWTA9v77DHBi4RTRg0YqGCUN/DWyp6ZBXCB4Fi9Xh0N3jk" +
                "PZhyPjznJ4zlE/gviJ0qSNK9ys5toBvRYZxSgdLS2haPYU04BCZw51AOwnXfDbEBolILByRGwh2KwSMG" +
                "Wj8RoU/WC81i9h1ZZd2kPms8n/Fn1NpZr/h0g2KuQEt7Cv0eAcTGzrsDaqmi3ZCZz2jpREbvvPJDIVL5" +
                "yGJ1LzHGJklKQQTfKgRXagBQpdosQgTz7zMaW/2/TMinhP9iyiwgF6QBIGZRoQWC1MC+0nRHAHNnWNHV" +
                "62t6vU5egIJch6StI8Q80lLIc9xXXLbPxCb4rGhcPudKaBKPgENU+WuvA3hXkJEIzuom8YvPrGuK20IV" +
                "SBMFYfIr0ZNb04cUgTr3yArsywLPFZWANzv4cU2fnibhPCSgPiU/oL9BReSM+6hPnw6XCtCg7J8T/O1S" +
                "EH8421FnlE0h+EMND3nPHE5QEkCx1eXqqOnvKQwPtnYfUjel1JNsP9shyVYbXcYPaZCdO27UQYMjkXFo" +
                "Mak3VsXEQQ2nwJ5FsuK8fD3x0DXZvt1l+Lw7hkn6qCvY80w6Lf+ucCnt16aJRJqS4T1SIw8cicfBnw7U" +
                "uk/TGECrNXgl+PI2Kd5Or8Om7MAMEvDB9XRUOVHC2Bsww4KxLR9pIgbnWwV3fgaw0ulcuAH5+/CV0SGG" +
                "TXC9LxmbMIRazJQCGSodpGuJW6UxT2KKcIm/s97JkE0xcc9s+RSKf0wLcDtPanSDuRFUaNGjWlYWL2Us" +
                "QQWmXwFm/z6QgqT6BUPuPCiK3aI4Hy6DkfQY01d8u6Sop1FrMu5fAJXtTqMTVhouZuTCgmLnd3+be1Xk" +
                "7sKg+94YyYVxGhULdkPknBpf/PhTVrQQUGXsATZw8PqU3maX5eSrpP5Vyq31/4e9n060z4hcymUcEELH" +
                "pa5xRcCMJFKJV6dJWEpSJgQ73mJA3Sv6oQFaT8UFuO+/fYAZMd2CRiIYh/6jiJxL+6hCMcKerwpjpUpu" +
                "5rKrNRtpz5mzGGagxhBeNP7L/dBzISGFJjsWvJLO/FJm5FHszSjyhl6nCahK41GYB2eqE/6LkXGyGv50" +
                "sfcy8U/Jc9qOlzWkwTfodq1D3FJNTDFAWJ6nvvRK0vOGKcPzYJ+6Jlf5wjGHbT0dOSyOfHTdX3EievVH" +
                "DjwTqbTV9Bv75JRnXLmiH9Lv8b0knu+hr0aCYX5HbpQRoS2jBmBVTpm5n6KXyhSabsMTG7z06vgSarLJ" +
                "uY7HNMbwP3TSgOUuL+tnTffKyNV97FkC5zSipEEug0hXolTuKJLNa7mrVQtzw3ytfCoDNUliU+ycM+jC" +
                "2+zRgPL+Lw4pSn6hEAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
