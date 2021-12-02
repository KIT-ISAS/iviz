/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StereoMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class DisparityImage : IDeserializable<DisparityImage>, IMessage
    {
        // Separate header for compatibility with current TimeSynchronizer.
        // Likely to be removed in a later release, use image.header instead.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Floating point disparity image. The disparities are pre-adjusted for any
        // x-offset between the principal points of the two cameras (in the case
        // that they are verged). That is: d = x_l - x_r - (cx_l - cx_r)
        [DataMember (Name = "image")] public SensorMsgs.Image Image;
        // Stereo geometry. For disparity d, the depth from the camera is Z = fT/d.
        [DataMember (Name = "f")] public float F; // Focal length, pixels
        [DataMember] public float T; // Baseline, world units
        // Subwindow of (potentially) valid disparity values.
        [DataMember (Name = "valid_window")] public SensorMsgs.RegionOfInterest ValidWindow;
        // The range of disparities searched.
        // In the disparity image, any disparity less than min_disparity is invalid.
        // The disparity search range defines the horopter, or 3D volume that the
        // stereo algorithm can "see". Points with Z outside of:
        //     Z_min = fT / max_disparity
        //     Z_max = fT / min_disparity
        // could not be found.
        [DataMember (Name = "min_disparity")] public float MinDisparity;
        [DataMember (Name = "max_disparity")] public float MaxDisparity;
        // Smallest allowed disparity increment. The smallest achievable depth range
        // resolution is delta_Z = (Z^2/fT)*delta_d.
        [DataMember (Name = "delta_d")] public float DeltaD;
    
        /// Constructor for empty message.
        public DisparityImage()
        {
            Image = new SensorMsgs.Image();
            ValidWindow = new SensorMsgs.RegionOfInterest();
        }
        
        /// Explicit constructor.
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
        
        /// Constructor with buffer.
        internal DisparityImage(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Image = new SensorMsgs.Image(ref b);
            F = b.Deserialize<float>();
            T = b.Deserialize<float>();
            ValidWindow = new SensorMsgs.RegionOfInterest(ref b);
            MinDisparity = b.Deserialize<float>();
            MaxDisparity = b.Deserialize<float>();
            DeltaD = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new DisparityImage(ref b);
        
        DisparityImage IDeserializable<DisparityImage>.RosDeserialize(ref Buffer b) => new DisparityImage(ref b);
    
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
            if (Image is null) throw new System.NullReferenceException(nameof(Image));
            Image.RosValidate();
            if (ValidWindow is null) throw new System.NullReferenceException(nameof(ValidWindow));
            ValidWindow.RosValidate();
        }
    
        public int RosMessageLength => 37 + Header.RosMessageLength + Image.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "stereo_msgs/DisparityImage";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "04a177815f75271039fa21f16acad8c9";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1XUY/bNgx+z68gmoddurtc0b4UNxQbtu62Azp0WA8o0GELFJuOtcmSJ8lJ3F+/j5Lt" +
                "ONe128O2AHeJZZEi+ZEfqSW94VZ5FZlqViV7qpynwjWtinqrjY49HXSsqei8ZxvpXjf8prdF7Z3V79mv" +
                "F0t6pX9n01N0tGXy3Lg9l6QtKTJQ7LFkWAW+pC4w6UbteD0cpm2I+LVefJ+f8/ICOm+NgwV2R63TOLbU" +
                "AWaKNVme7mueFjUHUp6p9Xylyt866CyTH8r2UHW8clUVOMK6eGC2FCHbem0L3SqTDwjkqrQeD44K1bBX" +
                "gS7gg6wVMB56Yq2iPPfpsD37HZcrsQTLOtxQSS/ouDF0hf8e/y+K/IQvv1oEtsH5TRN24fpOfMieiK9v" +
                "ECR2tGPXcPT9mm5h+8nj8jJZUXILHCrvmsEoMRIH0zucW91fI4qVBO3ZU6oIAXQFnDNsd7G+pFYf2YRp" +
                "wz02fA2vjLaA5eC8KamzOoZkTrc9aFu6g8TkonURuGtlTL+ivTK6nJmG547D+sy5n3innX1d3VlxK8Qs" +
                "tMkqRb9A55VFBKB/jmFg5Yua4ciS7nLoH+B+KZDOFg2HILhYarTdzDYHpFY6V3TNc6UfThksKLlCCEQH" +
                "CsB518LmS0L4n72kvTNdwxPsUBQyUMrsHFTVDUCw9CgwP1rTjzmNUrG8I9fFoEvx8AZy8nm3gYkJKbqm" +
                "Rh1P1p42qOO0Ye4ONhSuA0TWSRIjtTs7Q/t877R6doTA2gBDwQNf7oAKmYXLFihbwJzrKkw7i1rzXm3N" +
                "mH0paNAFXBGcCJwl1CWbqDaShhfvfn16Xd2vHuelmZHDwmLx4l/+LH54890NkClz9mUiSUWlbKl8Sagp" +
                "VaqoEiPUelezvzK8ZwMh1bQSCHkb+1YyWbJFsiegHC0KDGkvtFUKu4EWG1RJIWwZwYNn8pBMlIeQRl10" +
                "RgmNOl9qK9srj3IV7ZJF/EfHtmC6e3mDPTZwgVDCoAEIFYT37l7SokNKIXYQWCzvD+4Kj7wDTU6H5+SE" +
                "sXwE+QWxUwXJuMfZuTV0IziMU0rwWVrb4DGsCIfABG4dakGI7sc+1oBTCmGPrEigQzFIxEDrZyL02Wqm" +
                "Wcy+IausG9Vnjacz/olaO+kVn65QySU4aUeh2yGA2Nh6t0chlbTtM+0ZLW3I6K1Xvl+IVD5ysbyVGGOT" +
                "ZKQggm8Vgis0AChTYS5CBO3vMhob/d9l40OuH9MKsAWhfgQsKjQ/0Bl4V9rtgF7uCUu6eHJJT1bJBZCP" +
                "a5GxVYSYR04KbQ77FueNM/EIPksalk+JEurEIGAPVfzR6QDGFVgkfJO6UfzsM+kagzZTBbpENZj8SvTk" +
                "pvQxRSDNHVIC+7LAh4oKYJsd/LSmz4+jcB4PUJySHNBfoxxyun3Sp8/7cwVoTfafCb4/F8QfznbUGmVT" +
                "CP5Ww13eM4UTfARQbHm+Omj6JoXhzlbuY+rGlHqQ6ic7JNkqo4v4MQ2yc8u12msQJDIOzSV1RZTHQEA1" +
                "p8CeRLLivHw5ktAl2a7ZZvi8O4RR+qBL2POBdFr+S+FCGq9Ns4i0I8M7pEYeNRKJgzwdeHWX5jCAVmmQ" +
                "SvDFdVK8GV+HddGCFiTgvevooHKihKExYHoFXVs+0MgKzjcK7vwGYKXHuXAF5vfhK6NDDOvgOl8wNmH8" +
                "tJgmBTIUOhjXEjdKY5LE/OASeWe9oyHrxUg8k+VjKL4dF+B2ntHoChMjeNCiQTWsLF7KQIIKTL8CzP5r" +
                "IAVJ9TvG22lEFLtFcT5cRiJpMKYr+XrOUA+jVmfcnwOVzVajDZYaLmbkwoxfp3dfTo0qcntm0G1njOTC" +
                "MIeKBds+ck6N5z//khXNBFQRO4ANHLw+prfZZTn5Iql/nHJr9X9Q98NJ9iGLS60Mo0FoudAVbgYYjUQo" +
                "keo4AEs9ymxgh8sLeHtJb2tA9VBcUPvp9R2siOnyM7DAMOsfRORU1wcVFgPm+YYwlKkkZq65SrORxpwJ" +
                "i2EGCgyxRcs/3w89ZxJSZbJjRirpzC9kNB7EXgwiL+hJmn3KNBiFaV6mKoE/mxRHq+FPGzsvg/6YOcfN" +
                "cEdDDrxCq2sc4pYKYowBwvJh3kujJD1tGNM7z/OpZXKZ7xlT2Fbjkf3syHvX/hsnolF/4sATi0pPTb+x" +
                "T075gCiX9Db9Ht5L4vkO+iokGMZ25EYREdoiagBW5pSZmikaqcyf6RI8UsEjrw6PoCabnIt4SGPM/H0r" +
                "3Veu8LJ+0nSrjNzYh4YlcI7zSRrhMoh0IUrlaiLZvJIrWjkzN0y3yYcyUJMk1outcwYteJM9wm3lTyh1" +
                "Vs+XEAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
