/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class Range : IDeserializableRos1<Range>, IDeserializableRos2<Range>, IMessageRos1, IMessageRos2
    {
        // Single range reading from an active ranger that emits energy and reports
        // one range reading that is valid along an arc at the distance measured. 
        // This message is  not appropriate for laser scanners. See the LaserScan
        // message if you are working with a laser scanner.
        // This message also can represent a fixed-distance (binary) ranger.  This
        // sensor will have min_range===max_range===distance of detection.
        // These sensors follow REP 117 and will output -Inf if the object is detected
        // and +Inf if the object is outside of the detection range.
        /// <summary> Timestamp in the header is the time the ranger </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // returned the distance reading
        // Radiation type enums
        // If you want a value added to this list, send an email to the ros-users list
        public const byte ULTRASOUND = 0;
        public const byte INFRARED = 1;
        /// <summary> The type of radiation used by the sensor </summary>
        [DataMember (Name = "radiation_type")] public byte RadiationType;
        // (sound, IR, etc) [enum]
        /// <summary> The size of the arc that the distance reading is </summary>
        [DataMember (Name = "field_of_view")] public float FieldOfView;
        // valid for [rad]
        // the object causing the range reading may have
        // been anywhere within -field_of_view/2 and
        // field_of_view/2 at the measured range. 
        // 0 angle corresponds to the x-axis of the sensor.
        /// <summary> Minimum range value [m] </summary>
        [DataMember (Name = "min_range")] public float MinRange;
        /// <summary> Maximum range value [m] </summary>
        [DataMember (Name = "max_range")] public float MaxRange;
        // Fixed distance rangers require min_range==max_range
        /// <summary> Range data [m] </summary>
        [DataMember (Name = "range")] public float Range_;
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
        
        /// Constructor with buffer.
        public Range(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out RadiationType);
            b.Deserialize(out FieldOfView);
            b.Deserialize(out MinRange);
            b.Deserialize(out MaxRange);
            b.Deserialize(out Range_);
        }
        
        /// Constructor with buffer.
        public Range(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out RadiationType);
            b.Deserialize(out FieldOfView);
            b.Deserialize(out MinRange);
            b.Deserialize(out MaxRange);
            b.Deserialize(out Range_);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new Range(ref b);
        
        public Range RosDeserialize(ref ReadBuffer b) => new Range(ref b);
        
        public Range RosDeserialize(ref ReadBuffer2 b) => new Range(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(RadiationType);
            b.Serialize(FieldOfView);
            b.Serialize(MinRange);
            b.Serialize(MaxRange);
            b.Serialize(Range_);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
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
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, RadiationType);
            WriteBuffer2.AddLength(ref c, FieldOfView);
            WriteBuffer2.AddLength(ref c, MinRange);
            WriteBuffer2.AddLength(ref c, MaxRange);
            WriteBuffer2.AddLength(ref c, Range_);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/Range";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "c005c34273dc426c67a020a87bc24148";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61W224bNxB9368YQA+RGkuJ04cGRlQggONWQOoEkvNkBAK1pLRsuaRMcnXp1/cMuVpZ" +
                "jqM2QBeGtUtyzsycuXB6NNN2ZRR5YVf4r4TENy29q0lYEmXUm3bTU6xEJFXrGEhZ5Vd7HJGQWTsfQ9Ej" +
                "Z5/iJAkdaCOMliSMwxrD+pKwEStFUocobKmoViI0XskRAemuglCtQhAAwytZF0ms196tvRZR0dJ5MiLA" +
                "qFAKC2PCiGZKJciPvD7DMoA6jCXtXQPFirbO/8W2bXWsSJyijIqnyoUJjrDJbnoVlIUdtNQ7JYed6f2F" +
                "tsLvBy1PI0oQQMLxAEO32hiqBIistZ2nQ+PxuBa77r2DckuSKirQ7uwo2QKdLU6A18a4LU0/fKbLy18S" +
                "+wnbNXHdRBpO7JI9ZRLc4k+gMHcZT0mgscDLZw8BIWiZ9KeoHGzILoGW3xFR8FTln+PTo6jBVRT1mrRN" +
                "wu0ZoPIXb6eXTE5B33l6YDg23ip5mhdtKnFgpngTyaq4XyvkYFMzy5Mc261IsUGqNQiblAzkgAU7DMAu" +
                "mEXJ2adqoU3eA7wLwwYZkA8VjbbxLX35eDd9P/v05fZ6/LpdmtzeTN9PP1yPL4t2xR+smSdrMhfsMH+B" +
                "x26fgC9psU+7OZRnWOgH11h5QZPpBalYDuie/fxaFEvjRPz5DZJPGTl3y/lGq22nNei/u+hxeaXKe45I" +
                "xOWM9lyoXF33sP/rmZOP8qcUTcjV/rT+a7FPiX8GZ6EUGoLdbyvFxYmiRBoNT5x89YYz9wzGN6ez64eW" +
                "0iYxnUF4DQ3cBkvnUeVrZ2U4ZMhuKHZcIctH8Rsdw9GVdIeFFV03dUtFzsd7RLCTOBT+UQIanpP4vr03" +
                "3IEexTbVVgDvD432J32m03Y0+bHyDJdXpIjiX/T2b11UV9nEQO+y4BzqCCnz6+FT7M5AhMo1BvWQUrMU" +
                "HoU6+HFPnTX7k74H/dzaRmeg0rmujwc0i2OfaxNveaLtHFj/upONDleEcWjUbcq0fR9fDw26kl7uz3n4" +
                "8olZ1n1rF6P+gG2fcmGCn9yJwNmgGP/PT/HH7LcrClHO67AKr/INgX48g30ScUX9RZFyivtJpVeo8KFR" +
                "G2Uo3RfsDO9ywwyjw8WLvxVPF8IgwKlzgsbS1XVjdclXf3ffHOQhCYYErYWPumyM8DjvPBpQmhS8qBWj" +
                "p/v4oVGcRZPrK5yxQZUNDzjQpG2JppXa2OSaUotHpUCg6N1t3RCfKo1A3WV3GG3UjgPHdopwBR0/ZedG" +
                "wAY5qkytpJ/W5vgMA7RqNgFTU1lRH5Z/3sfK5RBvBMabhUkzTwkGgPqChV4MHiHbBG2FdQf4jHjU8V9g" +
                "bYfLPg0rxMyw96FZiXR5Y9jaaHm8uUqjefoxeuEx7BTpXk8qi94Nc5yv2hQR/IoQXMmjmkwpXITo82CJ" +
                "k3Mti+IfpIg7knoKAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
