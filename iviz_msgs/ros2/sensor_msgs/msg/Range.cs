/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class Range : IDeserializable<Range>, IMessageRos2
    {
        // Single range reading from an active ranger that emits energy and reports
        // one range reading that is valid along an arc at the distance measured.
        // This message is  not appropriate for laser scanners. See the LaserScan
        // message if you are working with a laser scanner.
        //
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
        // (Note: values < range_min or > range_max should be discarded)
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
        public Range(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out RadiationType);
            b.Deserialize(out FieldOfView);
            b.Deserialize(out MinRange);
            b.Deserialize(out MaxRange);
            b.Deserialize(out Range_);
        }
        
        public Range RosDeserialize(ref ReadBuffer2 b) => new Range(ref b);
    
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
    
        public void GetRosMessageLength(ref int c)
        {
            Header.GetRosMessageLength(ref c);
            WriteBuffer2.Advance(ref c, RadiationType);
            WriteBuffer2.Advance(ref c, FieldOfView);
            WriteBuffer2.Advance(ref c, MinRange);
            WriteBuffer2.Advance(ref c, MaxRange);
            WriteBuffer2.Advance(ref c, Range_);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/Range";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
