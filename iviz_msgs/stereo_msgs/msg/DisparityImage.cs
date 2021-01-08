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
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                "H4sIAAAAAAAACr1Y247bNhB9D5B/GGQfuk73EqQvwRZBizbddoEUKZIFCqRoDVocWUwoUiUpX/L1PUNK" +
                "srxJmjykNbBrm+IM53LmzNAn9Io7FVRialhpDlT7QJVvO5XMyliT9rQ1qaGqD4FdolvT8qu9q5rgnXnH" +
                "4eL+vRN6bt6y3VPytGIK3PoNazKOFFloDliyrCKfUR+ZTKvWfDGcZlxM+AQtv5SFsn7/nqi9th5WuDV1" +
                "3uBobSJMFYuKCrpteFo0HEkFpi7wudJveqjV2Rfl9qJrd+7rOnKChWnL7ChBuAvGVaZTtpwQydd5PW09" +
                "VarloCKdwg9Zq+CAKEqNSrKwz8dtOKxZL8QWLJt4RZqe0m5p6Rz/A/6fVuUb3sLi/r3ILvqwbOM6Xt6I" +
                "G8WZ4vArBIs9rdm3nML+gq7hwMFtfZYt0dwhIXXw7WCYGIqz6TWOrm8vJZq1hO6bx1QTwugreGjZrVNz" +
                "Rp3ZsY2HHbfY8QN8s8YhQVsfrKbemRQHk/rV1jjttxKb084ngMAoa/cL2ihr9Mw8fO85Xhz7+JLXxrsX" +
                "9Y0T32IqUsuisxwhaQzKIRQ4Yp7PyCpUDYs/J3RT0nAHBGeS39mi5RglRY5a45azzRFQy0dnZXPk7Idz" +
                "Bhs014iEKEFJ+OA72H1GyMM3z2jjbd/yBAHRFEvKlF176GpapMPRg8j84IJ+K6DK9fOafJ+i0eLklQjK" +
                "6/USVuak0SW1ancweLZD7aYdc5dkR+V7ZMt5QTXA3rt56u/snpaPj8k5bpFQyQ3e/BZ1M4ubq1DPyHmp" +
                "tjjtrBrDG7WyIxxz8EQZkowoJSRdgq7ZJrUUYJ6+/uvxZX27eFiW5pYOK2LM0y/8un/v11c/XyFLusCx" +
                "sEypNeW0CppQakqrpDJbNGbdcDi3vGELKdV2Eg55mvZdBreAR9AUUaYOhYdSEFrTwn7gzRa1UwmdJhDl" +
                "kQIRzZyI0CZT9VYJ0fqgjZP9dUAdZ/0ZVvx3z65iunl2hV0ucoWYwqghJSoKL948w+YeIEMQIQHB260/" +
                "x3deg0onCwpgYTHvQI9RjFUxg/Bh8fEC6hEkxkEajJfXlvgaF4RzYAV3HgUiVPjbPjVIrVTHBhDJCIBm" +
                "MIyF2q9E6KvFXLWYfkVOOT/qLyoPh3yOXndQLG6do8I1GGtNsV8jjtjZBb9BeWla7QstWiP9yppVUAFA" +
                "F7FyKJRcS7CxTRAqucG7itFXBpnQuWDBYgndYV3ysjT/KTrv9oQJZUhglB6BwCWFXgm2AzlLex7yODSP" +
                "Ezp9dEaPFtkRcJPvAOE6QS4Ao8Kr40Zx46jVZprB64SG5QNsYpP5Bdyiqr97E8HKkiIJ5EHjKH/0mpSN" +
                "0ZvpAqOiQmx5JIpKA/uoJvDqGgDBxiLxvqYKiS5efkLV17tRukwUKFmBCg5oUB8Fff/u1tf7Yw1oYu4z" +
                "Jd8dS+IPp3vqrHI5DJ9WcVM2TTEFUyE1Th+vDqp+zKG4cbX/qL4RXXegf7BEcFdbU6WPqpCtK27UxoA8" +
                "AT60oNw/c7kMzNRwju9BqOguy2cjOZ2R69tVSWPw2ziJb42GTe+J5+UPSlfSpd0wv0jXsrwGSsp4klke" +
                "zOpBu+s8wiF7tQHbxFBdZtXL8XG8qDphCwn83ve0VQUzcWgdmH9B5463NHKFD62CS2+Q4dwLfTxHbwjx" +
                "e2tiihfR96Fi7ML06jCLSu5Q/KBjR9wqgzkUA4fP3F4Uj6agMUyMNFk/BuSncQHOl/GOzjFwgiMduljL" +
                "yuGhzDCoyPwpwvKPpFRyqt5iPp7mSzFdNJfTZY6SFmR7zZdz4robOjBoSeETpGe5MuiW2sDPksI4I9/p" +
                "2XeHZpa4O7LpurdWYDGMsWLEap94QMmTP/4sqmYSqko9so58BLPLT4vfcvZp1v8w42zxPxH73UH4PY6X" +
                "6hnmiNhxZWrcMDBNiVSm23GAlhKVOcIN16BM6if0e4Ok3VUg+Xv54gaGpHyRGrhhuDJsReRQ7FuFcA7p" +
                "LzeNoXIFpqUKa8NWOnghMoYlKDmEGMPB8X5RdCQiZSdbZlyTT/1WRutB7ukg8pQelWFJ51EqTgM31RkG" +
                "swlzNBwudakP+a4woWi3HO58gMNztMPWI3y5QMZAIDYfqAPppmSmHSPcy50g91XW5bYyBW8xHbqfHXrr" +
                "uy9yJvr5vx15IFjpu/kzNuZz3uNQAMXI53FDhmHoobMG3DD4AydVQoyrZJA6XeAztVy0W5lc8+V6JIgH" +
                "QW0fiJ5id6nsAdW4Nuw7adLy84CsH1RdKyu/Bgw9TRI7DTN58Cv5pFPRKlccwfZC7nt6ZnCcrqd3ZURP" +
                "FgEiVt5btOpl8Srfev4BsQbk2vkQAAA=";
                
    }
}
