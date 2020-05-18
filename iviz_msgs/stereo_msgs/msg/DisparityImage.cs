using System.Runtime.Serialization;

namespace Iviz.Msgs.StereoMsgs
{
    [DataContract (Name = "stereo_msgs/DisparityImage")]
    public sealed class DisparityImage : IMessage
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
            Header = new StdMsgs.Header();
            Image = new SensorMsgs.Image();
            ValidWindow = new SensorMsgs.RegionOfInterest();
        }
        
        /// <summary> Explicit constructor. </summary>
        public DisparityImage(StdMsgs.Header Header, SensorMsgs.Image Image, float F, float T, SensorMsgs.RegionOfInterest ValidWindow, float MinDisparity, float MaxDisparity, float DeltaD)
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
        internal DisparityImage(Buffer b)
        {
            Header = new StdMsgs.Header(b);
            Image = new SensorMsgs.Image(b);
            F = b.Deserialize<float>();
            T = b.Deserialize<float>();
            ValidWindow = new SensorMsgs.RegionOfInterest(b);
            MinDisparity = b.Deserialize<float>();
            MaxDisparity = b.Deserialize<float>();
            DeltaD = b.Deserialize<float>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new DisparityImage(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Header);
            b.Serialize(Image);
            b.Serialize(this.F);
            b.Serialize(this.T);
            b.Serialize(ValidWindow);
            b.Serialize(this.MinDisparity);
            b.Serialize(this.MaxDisparity);
            b.Serialize(this.DeltaD);
        }
        
        public void Validate()
        {
            if (Header is null) throw new System.NullReferenceException();
            Header.Validate();
            if (Image is null) throw new System.NullReferenceException();
            Image.Validate();
            if (ValidWindow is null) throw new System.NullReferenceException();
            ValidWindow.Validate();
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
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "stereo_msgs/DisparityImage";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "04a177815f75271039fa21f16acad8c9";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71X247bNhB911cQuw9ZJ15v0LwEKRYt2nTbBVIkyC5QIEVr0OLIYkKRCkn5kq/vGVKS" +
                "ZefSPKQ1kKxNzY0zZ86MzsUdtdLLSKImqciLynlRuqaVUa+00XEvtjrWouy8JxvFvW7obm/L2jurP5Bf" +
                "FOfihX5HZi+iEysSnhq3ISW0FVIYGPY4MiQDzUUXSOhGrmnRO9M2RHxbFL/l3/m4gM0b4xCBXYvWabhV" +
                "OiBMjibri/uaxkNNQUhPovV0KdXbDjZVuoe0e5jaXbqqChQRXdwSWRFrltW21K002UEQrkrncetEKRvy" +
                "MogLnWVLBA87sZaRf++Tsw35NakZR4JjHZ4JJa7FbmnEJf73+P+izL/wx8+KQDY4v2zCOlzd8h3yTfiu" +
                "d0gSObEm11D0+4W4QeyHG6t5ikJRizpU3jV9UBwkHIs38FvdXyGLFSftyXeiEkigK3E5Q3Yd67lo9Y5M" +
                "GAXuIfATbmW0RVm2zhslOqtjSOF0q622ym05Jxeti6i7lsbsZ2IjjVaT0PC7o7A4utxrWmtnX1a3lq8V" +
                "YlZaZpNsn0vnpUUGYH9aw0DSlzUpxtRtTv1J3edc0smhoRC4LlY02i4nwgHQSn4XvcPDs+ylj0BRhRSE" +
                "5Kt23rWIeS6Q/ifPxcaZrqGx7DAUcqGkWTuYqhsUwYqzQHS2EK8yjFKzvBGui0ErvuEz6PHnzRIhpkqJ" +
                "K9HI3SHag4DcjQLT60CgdB1KZB2DGNDu7KTax7Lj6ZELLmuDGnI98MdtaVpGtALaFmXOfRVGybLWtJEr" +
                "M6AvJQ22UFckJ6LOnGpFJsolw/Dizd/fXVX3s4f5aBJkf1AU19/4U/x+9+szVEZl9GUiSU0lrZJeCfSU" +
                "VDLKxAi1XtfkLw1tyEBJNi0ngp/GfctIZrQwegLa0aLBAHumLcXsBlps0CUls2UEDx7pQzNRHlIaddkZ" +
                "yTTqvNKWxSuPdmXrjCJ635EtSdw+fwYZG6hEKjdMoakQMjDv3T4XRQdIIXdQKM7vt+4SP2kNmhydZ3Ai" +
                "WNqB/ALHKQMj7mG+3AK2kRyCFwU+S2dL/AwzAScIgVqHXmCie7WPtctNtwEqUtFhuGQoKPGAlR7MJpZt" +
                "Mm2ldYP5bPHg42vM2tEu3+kSnawM3z50ayQQgq13GzSSEqt9pj2jeQwZvfLS7wvWyi6L8xvOMYQYkVwR" +
                "/JUhuFJLngfcmEWInq2naiz1f4fGU64fYIWyBaZ+JCxKDD/QGXiXx21fPd1LXzyei8ezdAWQj2uB2CpC" +
                "zQOTTJu9XHE8OEX/ORf98QEooU4MAvaQ5ftOB52aN6VvNCc+9RltDUmbmAJdap4y6RHbyUPpc4ZAmmtA" +
                "AnJZ4WNDJfHQ+HdLj3aDcl4P0JwMDtivo+in9hfv9Gh/bACjyX6d4odjRfxz7L410tKwRHzRwm2WGdMJ" +
                "PkJRrDo+7S39nNJwayv3OXMDpE6gfoiDwVYZXcbPWWDJFdVyo11qOQyXNBXRHj0B1ZQSe1DJhvPxfCCh" +
                "ubBds8rl824bBu2tVojnI+10/EnlkgevDcOuYGgNaORVI5E4yNMp7mPew1C0SoNUgi+vkuHl8DgsyrYt" +
                "UsL3rhNbmYES+sGA7RV0bWkrBlZwvpG4zlsUlmecC5dgfh9+NDrEsAiu8yVBCOunxTbJJUOjK+5haqQ2" +
                "zFWtS+Sd7Q6BLIqBeMbIh1T8Mhzg2nlHE5fYGMGDFgOqIWnxkBcSdGD6FhD2pwvJlZTvsN6OKyLHzYaz" +
                "c16JeMCYTtHVlKFOs1bnuj9FVZYrjTGoNK6YKxcm/Do++2EcVJHao4BuOmMYC/0eyhGs9pEyNJ7++Vc2" +
                "NFGQZexQbNTB6116mq/Mni+S+YcJW7P/g7pPN9lTFude6VeD0FKpK7wZYDVipUSqwwLM/ci7ge1fXsDb" +
                "5+KPGqU6VeeqvX55iyhi7FEx2fW3NU0YCoAORV/z/IbQtykDM/dcpcnwYM6ERQgDDYbclvWJPOwcaXCX" +
                "scSEVJLP73k17tWue5Vr8TjtPiotRmHcl0WVij/ZFIeocZ82dp4X/QE5u2X/jgYMvMCoaxzylhpiyAHS" +
                "8jHueVAKPQoM8M77fBqZpNbHtDwbXO4nLu9d+y08YlB/weGBRXmmpu+QYy8fESXgkb73zxl4viP2K3lt" +
                "BzbKiNSWUaNgKkNmHKYYpLx/ppfggQrOvNyewUwOOTdxD2Ps/PuWpy+/wvP5wdKNNIGG26ZyDvuJPhRR" +
                "XLBRfjVhNM/mCX+HcMP4NnmqAzNJY1GsnDMYwct8I7yt/AModVbPlxAAAA==";
                
    }
}
