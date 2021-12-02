/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
        [DataMember (Name = "header")] public StdMsgs.Header Header; // timestamp in the header is the time the ranger
        // returned the distance reading
        // Radiation type enums
        // If you want a value added to this list, send an email to the ros-users list
        public const byte ULTRASOUND = 0;
        public const byte INFRARED = 1;
        [DataMember (Name = "radiation_type")] public byte RadiationType; // the type of radiation used by the sensor
        // (sound, IR, etc) [enum]
        [DataMember (Name = "field_of_view")] public float FieldOfView; // the size of the arc that the distance reading is
        // valid for [rad]
        // the object causing the range reading may have
        // been anywhere within -field_of_view/2 and
        // field_of_view/2 at the measured range. 
        // 0 angle corresponds to the x-axis of the sensor.
        [DataMember (Name = "min_range")] public float MinRange; // minimum range value [m]
        [DataMember (Name = "max_range")] public float MaxRange; // maximum range value [m]
        // Fixed distance rangers require min_range==max_range
        [DataMember (Name = "range")] public float Range_; // range data [m]
        // (Note: values < range_min or > range_max
        // should be discarded)
        // Fixed distance rangers only output -Inf or +Inf.
        // -Inf represents a detection within fixed distance.
        // (Detection too close to the sensor to quantify)
        // +Inf represents no detection within the fixed distance.
        // (Object out of range)
    
        /// Constructor for empty message.
        public Range()
        {
        }
        
        /// Explicit constructor.
        public Range(in StdMsgs.Header Header, byte RadiationType, float FieldOfView, float MinRange, float MaxRange, float Range_)
        {
            this.Header = Header;
            this.RadiationType = RadiationType;
            this.FieldOfView = FieldOfView;
            this.MinRange = MinRange;
            this.MaxRange = MaxRange;
            this.Range_ = Range_;
        }
        
        /// Constructor with buffer.
        internal Range(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            RadiationType = b.Deserialize<byte>();
            FieldOfView = b.Deserialize<float>();
            MinRange = b.Deserialize<float>();
            MaxRange = b.Deserialize<float>();
            Range_ = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Range(ref b);
        
        Range IDeserializable<Range>.RosDeserialize(ref Buffer b) => new Range(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(RadiationType);
            b.Serialize(FieldOfView);
            b.Serialize(MinRange);
            b.Serialize(MaxRange);
            b.Serialize(Range_);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 17 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/Range";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "c005c34273dc426c67a020a87bc24148";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1W247bNhB911cM4IfYzdrJpg8tjLpAAGdbA+kmsDdPi8CgJcpiS5EOSfmSr+8ZUpbt" +
                "zdZtgAqLtUTOnBmeuXB6tFBmrSU5Ydb4L0WBbyqdrUkYEnlQ23bTUahEIFmr4Eka6dYHiBTQ2VgXfNYj" +
                "a57iRA3laSu0KkhoizWGdTlhI1SSCuWDMLmkWgrfOFmMCEgPFZRq6b0AGF7J2EBis3F245QIkkrrSAsP" +
                "p3wuDJzxI1pIGSHf8/oCywDqMEo62AaGJe2s+4t926lQkbhEGWVPjQvtLWGTj+mklwZ+UKn2shh2rvdX" +
                "ygh3GLQ8jShCAAniHo7ulNZUCRBZK7OMQpPJpBb77r2DsiUVMkjQbs0o+gKbLY7HqbW2O5q/+0i3tz9F" +
                "9iO2bcKmCTScmZJPyiTY1Z9AYe4SniyAxgovnxUCgldFtB+jcvQhHQm0/I6Igqcq/ZyeHgUFroKoN6RM" +
                "VG5lgMpfvB1fEjnZme7F0wPDoXFGFpd50aYSB2aONxG9CoeNRA42NbM8S7HdiRgbpFqDsBUFA1lgwQ8N" +
                "sBtmETlokMFC6bQHeOuHDTIgCWWNMuFn+vT+Yf528eHT/XTyul2a3d/N387fTSe3Wbvijt4sozeJCz4w" +
                "f4HHbp+AX9DqEHdTKK+w0Pe2McUNzeY3JEM+oEc+5+csK7UV4cc3SD6pi6Utl1sld51Vr7520ePyipX3" +
                "HJGIyxXrqVC5uh7h/+crkmf5k4vGp2p/Wv+1OMTEv4KzkhINwRx2leTiRFEijYYXh3z1hjP3CsY30uno" +
                "x5bSJjFdQXgNC9wGc+tQ5RtrCn/MkP1Q7LlCyrP4jU7h6Eq6w8KKqpu6pSLl4yMi2GkcC/+kAQvPafyz" +
                "v3fcgc5iG2vLg/cvjXIXfaazdnL53HiCSyuFCOJf7PbvbZDj5KKnX5LiEuYIKfPr8VPsr0D4yjYa9RBT" +
                "MxcOhTr4/pNaow8XfQ/2ubWNrkBFua6PezSLU59rE6+8sHYNrD/tdIPFFaEtGnWbMm3fx9eXBl1JlYdr" +
                "J3z5xC1jv/WLUb/Dtw+pMMFP6kTgbJDhnvlfn+yPxW9j8qFY1n7tX6UbAv14Af8KxBX1F0TMKe4nlVqj" +
                "wodabqWmeF/wYXiXG6ZPlx3qDH9rni6ERoBj5wSNua3rxqicr/7uvjnqQxMMCdoIF1TeaOEgbx0aUJwU" +
                "nKhBVo/FPMpDchbNpmPIGC/zhgccWFImR9OKbWw2pdjiUSlQyHoPOzvEp4wjUHfZHUcbuefAsZ/Cj2Hj" +
                "h3S4EbBBjsxjK+nHtSU+/QCt2sAFTE15RX14/vEQKs4ihHgrMN6s0IcAnIMBoL5gpReDM2R2e0xGGGRc" +
                "gk+IJxv/BZZREi6faVghZppP75s1CIQghq0tRoLu5sq14ulHq5XDsJPFez2azHp3zHG6amNE8Cu8tzmP" +
                "ajyihCrzwaXBEpJLVWTZ36SIO5J6CgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
