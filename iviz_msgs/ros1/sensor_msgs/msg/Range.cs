/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class Range : IHasSerializer<Range>, IMessage
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
    
        public Range()
        {
        }
        
        public Range(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out RadiationType);
            b.Deserialize(out FieldOfView);
            b.Deserialize(out MinRange);
            b.Deserialize(out MaxRange);
            b.Deserialize(out Range_);
        }
        
        public Range(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out RadiationType);
            b.Align4();
            b.Deserialize(out FieldOfView);
            b.Deserialize(out MinRange);
            b.Deserialize(out MaxRange);
            b.Deserialize(out Range_);
        }
        
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
    
        public int RosMessageLength
        {
            get
            {
                int size = 17;
                size += Header.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size += 1; // RadiationType
            size = WriteBuffer2.Align4(size);
            size += 4; // FieldOfView
            size += 4; // MinRange
            size += 4; // MaxRange
            size += 4; // Range_
            return size;
        }
    
        public const string MessageType = "sensor_msgs/Range";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "c005c34273dc426c67a020a87bc24148";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61W224bNxB9368YQA+RGkuJ04cWRlUggONWQOoEkvNkGAK1pLRsuaRMcnXp1/cMuVpZ" +
                "jqM2QBeGtUtyzsycuXB6NNN2ZRR5YVf4r4TENy29q0lYEmXUm3bTU6xEJFXrGEhZ5Vd7HJGQWTsfQ9Ej" +
                "Z5/jJAkdaCOMliSMwxrD+pKwEStFUocobKmoViI0XskRAemuglCtQhAAwytZF0ms196tvRZR0dJ5MiLA" +
                "qFAKC2PCiGZKJciPvD7DMoA6jCXtXQPFirbO/8W2bXWsSJyijIrnyoUJjrDJbnoVlIUdtNQ7JYed6f2F" +
                "tsLvBy1PI0oQQMLxAEO32hiqBIistZ2nQ+PxuBa77r2DckuSKirQ7uwo2QKdLU6A18a4LU0/fKbLy58S" +
                "+wnbNXHdRBpO7JI9ZRLc4k+gMHcZT0mgscDrFw8BIWiZ9KeoHGzILoGW3xFR8FTln+PTo6jBVRT1mrRN" +
                "wu0ZoPIXb6eXTE5B33h6YDg23ip5mhdtKnFgpngTyaq4XyvkYFMzy5Mc261IsUGqNQiblAzkgAU7DMAu" +
                "mEXJ2adqoU3eA7wLwwYZkA8VjbbxZ/ry8W76fvbpy+31+G27NLm9mb6ffrgeXxbtij9YM0/WZC7YYf4C" +
                "j90+AV/SYp92cyjPsNAPrrHygibTC1KxHNA9+/lQFEvjRPzxHZJPGTl3y/lGq22nNei/u+hxeaXKe4lI" +
                "xOWM9lyoXF33sP/hzMkn+VOKJuRqf17/tdinxD+Ds1AKDcHut5Xi4kRRIo2GJ06+eceZewbjq9PZ9UNL" +
                "aZOYziC8hQZug6XzqPK1szIcMmQ3FDuukOWT+I2O4ehKusPCiq6buqUi5+M9IthJHAr/KAENL0l8294b" +
                "7kBPYptqK4D3x0b7kz7TaTua/FR5hssrUkTxL3r7ty6qq2xioF+y4BzqCCnz6+FT7M5AhMo1BvWQUrMU" +
                "HoU6+H5PnTX7k74H/dzaRmeg0rmujwc0i2OfaxNveaLtHFj/upONDleEcWjUbcq0fR9fjw26kl7uz3n4" +
                "+plZ1n1tF6N+h22fcmGCn9yJwNmgGP/PT/HH7LcrClHO67AKb/INgX48g30ScUX9RZFyivtJpVeo8KFR" +
                "G2Uo3RfsDO9ywwyjw8WLvxVPF8IgwKlzgsbS1XVjdclXf3ffHOQhCYYErYWPumyM8DjvPBpQmhS8qBWj" +
                "p/v4sVGcRZPrK5yxQZUNDzjQpG2JppXa2OSaUotHpUAAdN5PXbh8KHp3WzfEukqzUHfrHWYcteMIssEi" +
                "XEHZD9nLEZSAJVWmntJPa3N8hgF6NtuC8amsqA8XPu9j5XKsNwJzzsKk4acEFUB9xUKvBk+QbYK2wroD" +
                "fEY86vgvsLbDZZ+GFYJnmIbQrES6xTF1bbQ8XmGl0TwGGb3wmHqKdMEnlUXvhsnOd24KDX5FCK7kmU2m" +
                "XC5C9HnCxMm5lkXxD+wVcbaDCgAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Range> CreateSerializer() => new Serializer();
        public Deserializer<Range> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Range>
        {
            public override void RosSerialize(Range msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Range msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Range msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Range msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Range msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Range>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Range msg) => msg = new Range(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Range msg) => msg = new Range(ref b);
        }
    }
}
