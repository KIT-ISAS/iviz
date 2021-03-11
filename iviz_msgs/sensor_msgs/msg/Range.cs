/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/Range")]
    public sealed class Range : IDeserializable<Range>, IMessage
    {
        // Single range reading from an active ranger that emits energy and reports
        // one range reading that is valid along an arc at the distance measured. 
        // This message is  not appropriate for laser scanners. See the LaserScan
        // message if you are working with a laser scanner.
        // This message also can represent a fixed-distance (binary) ranger.  This
        // sensor will have min_range===max_range===distance of detection.
        // These sensors follow REP 117 and will output -Inf if the object is detected
        // and +Inf if the object is outside of the detection range.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; } // timestamp in the header is the time the ranger
        // returned the distance reading
        // Radiation type enums
        // If you want a value added to this list, send an email to the ros-users list
        public const byte ULTRASOUND = 0;
        public const byte INFRARED = 1;
        [DataMember (Name = "radiation_type")] public byte RadiationType { get; set; } // the type of radiation used by the sensor
        // (sound, IR, etc) [enum]
        [DataMember (Name = "field_of_view")] public float FieldOfView { get; set; } // the size of the arc that the distance reading is
        // valid for [rad]
        // the object causing the range reading may have
        // been anywhere within -field_of_view/2 and
        // field_of_view/2 at the measured range. 
        // 0 angle corresponds to the x-axis of the sensor.
        [DataMember (Name = "min_range")] public float MinRange { get; set; } // minimum range value [m]
        [DataMember (Name = "max_range")] public float MaxRange { get; set; } // maximum range value [m]
        // Fixed distance rangers require min_range==max_range
        [DataMember (Name = "range")] public float Range_ { get; set; } // range data [m]
        // (Note: values < range_min or > range_max
        // should be discarded)
        // Fixed distance rangers only output -Inf or +Inf.
        // -Inf represents a detection within fixed distance.
        // (Detection too close to the sensor to quantify)
        // +Inf represents no detection within the fixed distance.
        // (Object out of range)
    
        /// <summary> Constructor for empty message. </summary>
        public Range()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Range(in StdMsgs.Header Header, byte RadiationType, float FieldOfView, float MinRange, float MaxRange, float Range_)
        {
            this.Header = Header;
            this.RadiationType = RadiationType;
            this.FieldOfView = FieldOfView;
            this.MinRange = MinRange;
            this.MaxRange = MaxRange;
            this.Range_ = Range_;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Range(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            RadiationType = b.Deserialize<byte>();
            FieldOfView = b.Deserialize<float>();
            MinRange = b.Deserialize<float>();
            MaxRange = b.Deserialize<float>();
            Range_ = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Range(ref b);
        }
        
        Range IDeserializable<Range>.RosDeserialize(ref Buffer b)
        {
            return new Range(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(RadiationType);
            b.Serialize(FieldOfView);
            b.Serialize(MinRange);
            b.Serialize(MaxRange);
            b.Serialize(Range_);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength
        {
            get {
                int size = 17;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/Range";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c005c34273dc426c67a020a87bc24148";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1WbW/bNhD+LiD/4QB/qL3GbtN92GDMAwqk2Qy0aWGnn4LCoCXK4kaRLkn5Zb9+z5Gy" +
                "bKeZtwITglgi7547PvfC69FcmZWW5IRZ4b8UBb6pdLYmYUjkQW3aTUehEoFkrYInaaRb7SFSQGdtXfBZ" +
                "j6x5ihM1lKeN0KogoS3WGNblhI1QSSqUD8LkkmopfONkMSIgPVRQqqX3AmB4JWMDifXa2bVTIkgqrSMt" +
                "PJzyuTBwxo9oLmWEfM/rcywDqMMoaW8bGJa0te5P9m2rQkXiHGWUPTUutLeETT6mk14a+EGl2sli2Lne" +
                "Xyoj3H7Q8jSiCAEkiHs4ulVaUyVAZK3MIgpNJpNa7Lr3DsqWVMggQbs1o+gLbLY4HqfW2m5p9u4T3dz8" +
                "FNmP2LYJ6ybQcGpKPimTYJd/AIW5S3iyABorvHxWCAheFdF+jMrBh3Qk0PI7IgqeqvRzfHoUFLgKol6T" +
                "MlG5lQEqf/F2fEnkZCe6Z08PDIfGGVmc50WbShyYGd5E9Crs1xI52NTM8jTFditibJBqDcJWFAxkgQU/" +
                "NMCumUXkoEEGC6XTHuCtHzbIgCSUNcqEn+nz+4fZ2/nHz/e3k9ft0vT+bvZ29u52cpO1K+7gzSJ6k7jg" +
                "A/MXeOz2CfgFLfdxN4XyAgt9bxtTXNN0dk0y5AN65HN+ybJSWxF+fIPkk7pY2HKxUXLbWfXqry56XF6x" +
                "8p4jEnG5YD0VKlfXI/z/ckHyJH9y0fhU7U/rvxb7mPgXcJZSoiGY/baSXJwoSqTR8OyQr95w5l7A+EY6" +
                "Hf3QUtokpgsIr2GB22BuHap8bU3hDxmyG4odV0h5Er/RMRxdSXdYWFF1U7dUpHx8RAQ7jUPhHzVg4TmN" +
                "f/b3jjvQSWxjbXnw/rVR7qzPdNaOLp8aT3BppRBB/Ivd/r0Ncpxc9PRLUlzAHCFlfj18it0FCF/ZRqMe" +
                "YmrmwqFQB99/Umv0/qzvwT63ttEFqCjX9XGPZnHsc23ilWfWLoH1bzvdYHFFaItG3aZM2/fx9bVBV1Ll" +
                "/tIJXz5xy9hv/WLU7/DtYypM8JM6ETgbXGW4aP7X5yr7MP9tTD4Ui9qv/Kt0R1yhJc/hYoHQogSDiGnF" +
                "LaVSKxT5UMuN1BSvDD4P73LP9Om+Q6nhb8UDhtCIcWyeYDK3dd0YlfPt3105B31ogiRBa+GCyhstHOSt" +
                "Qw+Kw4ITNfjqsZhHhUhOpOntGDLGy7zhGQeWlMnRt2Inm95S7PIoFihkvYetHeJTximou+8O043ccezY" +
                "T+HHsPFDOtwI2GBH5rGb9OPaAp9+gG5t4AIGp7yiPjz/tA8VJxKivBGYcJZoRQDOwQBQX7DSi8EJMrs9" +
                "JiMMki7BJ8Sjjf8CyygJl880rBAzzaf3zQoEQhDz1gZTQXd55VrxAKTV0mHeyeLVHk1mvTvmON22MSL4" +
                "Fd7bnKc1nlJClfng0mwJyYUqsqvsb1A7d71+CgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
