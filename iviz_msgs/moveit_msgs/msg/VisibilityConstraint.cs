/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
    public sealed class VisibilityConstraint : IDeserializable<VisibilityConstraint>, IMessage
    {
        // The constraint is useful to maintain visibility to a disc (the target) in a particular frame.
        // This disc forms the base of a visibiliy cone whose tip is at the origin of the sensor.
        // Maintaining visibility is done by ensuring the robot does not obstruct the visibility cone.
        // Note:
        // This constraint does NOT enforce minimum or maximum distances between the sensor
        // and the target, nor does it enforce the target to be in the field of view of
        // the sensor. A PositionConstraint can (and probably should) be used for such purposes.
        // The radius of the disc that should be maintained visible 
        [DataMember (Name = "target_radius")] public double TargetRadius;
        // The pose of the disc; as the robot moves, the pose of the disc may change as well
        // This can be in the frame of a particular robot link, for example
        [DataMember (Name = "target_pose")] public GeometryMsgs.PoseStamped TargetPose;
        // From the sensor origin towards the target, the disc forms a visibility cone
        // This cone is approximated using many sides. For example, when using 4 sides, 
        // that in fact makes the visibility region be a pyramid.
        // This value should always be 3 or more.
        [DataMember (Name = "cone_sides")] public int ConeSides;
        // The pose in which visibility is to be maintained.
        // The frame id should represent the robot link to which the sensor is attached.
        // It is assumed the sensor can look directly at the target, in any direction.
        // This assumption is usually not true, but additional PositionConstraints
        // can resolve this issue.
        [DataMember (Name = "sensor_pose")] public GeometryMsgs.PoseStamped SensorPose;
        // Even though the disc is maintained visible, the visibility cone can be very small
        // because of the orientation of the disc with respect to the sensor. It is possible
        // for example to view the disk almost from a side, in which case the visibility cone 
        // can end up having close to 0 volume. The view angle is defined to be the angle between
        // the normal to the visibility disc and the direction vector from the sensor origin.
        // The value below represents the minimum desired view angle. For a perfect view,
        // this value will be 0 (the two vectors are exact opposites). For a completely obstructed view
        // this value will be Pi/2 (the vectors are perpendicular). This value defined below 
        // is the maximum view angle to be maintained. This should be a value in the open interval
        // (0, Pi/2). If 0 is set, the view angle is NOT enforced.
        [DataMember (Name = "max_view_angle")] public double MaxViewAngle;
        // This angle is used similarly to max_view_angle but limits the maximum angle
        // between the sensor origin direction vector and the axis that connects the
        // sensor origin to the target frame origin. The value is again in the range (0, Pi/2)
        // and is NOT enforced if set to 0.
        [DataMember (Name = "max_range_angle")] public double MaxRangeAngle;
        // The axis that is assumed to indicate the direction of view for the sensor
        // X = 2, Y = 1, Z = 0
        public const byte SENSOR_Z = 0;
        public const byte SENSOR_Y = 1;
        public const byte SENSOR_X = 2;
        [DataMember (Name = "sensor_view_direction")] public byte SensorViewDirection;
        // A weighting factor for this constraint (denotes relative importance to other constraints. Closer to zero means less important)
        [DataMember (Name = "weight")] public double Weight;
    
        /// Constructor for empty message.
        public VisibilityConstraint()
        {
            TargetPose = new GeometryMsgs.PoseStamped();
            SensorPose = new GeometryMsgs.PoseStamped();
        }
        
        /// Constructor with buffer.
        public VisibilityConstraint(ref ReadBuffer b)
        {
            b.Deserialize(out TargetRadius);
            TargetPose = new GeometryMsgs.PoseStamped(ref b);
            b.Deserialize(out ConeSides);
            SensorPose = new GeometryMsgs.PoseStamped(ref b);
            b.Deserialize(out MaxViewAngle);
            b.Deserialize(out MaxRangeAngle);
            b.Deserialize(out SensorViewDirection);
            b.Deserialize(out Weight);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new VisibilityConstraint(ref b);
        
        public VisibilityConstraint RosDeserialize(ref ReadBuffer b) => new VisibilityConstraint(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(TargetRadius);
            TargetPose.RosSerialize(ref b);
            b.Serialize(ConeSides);
            SensorPose.RosSerialize(ref b);
            b.Serialize(MaxViewAngle);
            b.Serialize(MaxRangeAngle);
            b.Serialize(SensorViewDirection);
            b.Serialize(Weight);
        }
        
        public void RosValidate()
        {
            if (TargetPose is null) BuiltIns.ThrowNullReference();
            TargetPose.RosValidate();
            if (SensorPose is null) BuiltIns.ThrowNullReference();
            SensorPose.RosValidate();
        }
    
        public int RosMessageLength => 37 + TargetPose.RosMessageLength + SensorPose.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "moveit_msgs/VisibilityConstraint";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "62cda903bfe31ff2e5fcdc3810d577ad";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71XTW8bNxC9768g4EPsQnEcNyiKFD4E+Wh9yEfrHJJcBGp3JBHmLjckV7Ly6/tmuFxR" +
                "slP00CZwIGlJPs68efOxJ+rjmlTtuhC9Nl1UJqgh0HKwKjrV8iP8VxsTzMJYE3f8WKvGhFqdRhyN2q8o" +
                "nils0qrXPpp6sNqrpdctnVcnwAek7F863wbFhxY6kHJLnMjAO7aB1HbtsBJNz3boKJudNyugYzv/CtQF" +
                "5xn47Wic6ValfXwbQy12ClsHz8t80LuFi1iioDp8cQt4PNTpiuI4m8Ho71yk59n8gh8BePf+I8DhT02q" +
                "hQXt0MJM0HUnX+Ft1F2NjQuKW6KusByQumvUnroZzPEJ1sQJdb/OhC+I+eVnS0O2YS42hrb4BFzBinqh" +
                "PrhgonHdy73Jte7UKV/agwO9sDsV1m6wzRnjItgNR0aFoV6rfvA9IhDOK3EdrOnGDCGTL2GMawQmITBA" +
                "1ghghEZLqlpap+Mvz0YP5gkkQ/IFJeBvSociQq3bUJjJg+OduAsBWutuRXxmS9ZOEYKPBUssviSwQpIJ" +
                "35rudiYe051ue0vVilxL0e/mbViFJyCQbiJW4NBoP9vB1r/xri3ozsqMbqt9Ew5iOpmcRK+PJVYoi0Tr" +
                "PYID9eiIa4fAom11h0iZBtFQb/bmzpAkUFTa8yxtmCnRAeICc5Yaqm71LYVjbXtaQRnME3jZgSPTTBm6" +
                "0XagHFZtt3rH6lU/i66dR04gzD9fisFzufQgnrh3uzZQ0GEmJu3uJXI+nkkBMk2+0FPvCazGQggcKAZI" +
                "uAXtUhqirtcJ71qKlg5haKkp97EmrHO3CISnOkL3Y0XJQeKaBY7TMpiZyBCwnh+lejhoi9NcNlAzEIHF" +
                "EJVuGkk0bR/IuQAkvh5OObvhdAaOASp4/L7ckt2T3F5vpHK4YbXe6wk49zNu9lAVyzmxIQ8dtVqSZUG1" +
                "HvZZBQWDdC2elom2NXHNxvdUSwEqa0ziG0bK1cAscon3SmUakW4hpdaFiIAjdbSodbYXS8194CHTR/oI" +
                "RWvo1VpvWOy1lebg1IXaOIton4uW5D7UBCuJ1NBSmEnKY+y0NFbisV6i5IKQ7Flxu3ifC/QkDHBYR+eT" +
                "F/cKQBZ1SqEFWbfdCzolYW4SSBtgNoXNKbeRj+SXTDavzMTKKSm3xlp25mLsuFs32gOhemLqcc71PauQ" +
                "wllGrB1HJBKUm5vdePPD8B/Mk8t0Q4kOu3qEIZXQs/OyWGSqk8cANaOzYxssAnOvDiScfRfRI+ZYwB3u" +
                "xPdIHo+BfHoxE/tgwPUSRPDZXGYP41+0ZlSH3Ihg0pz3zWVfNeV5PiVdMJjWwEm7S6NPeUIy3mI9HrqY" +
                "4E4e6PO5OdzTUBYXEEKq2VB8hzVBBtZxcylngbGxJdkVomNfVjyojfx56ZETa+PQcUSPMktmUTLqkCo5" +
                "XnJVmlsWW4cLoQ10raOEyRMK14aD4eeTulKXM/UZH09n6gs+LqoBkf5V3bx+d/P+r/mXq6MHn6+eHj74" +
                "dHU5PhgLpsRpupstfoHhwKzWkcsG90POXbHkcJg7bQhFHY3Sk0URRKE2be+8DG/snIPpvjiATvySi5Dn" +
                "xW/kIRPSXVCWQpiOxrOJy2REVV39x/+qtze/P1ffbSRCAP/OdXxJntil2jnfmI7DlZQkYjQtBT5Y/UG6" +
                "gW9r+agEIPWi/8f+EJtkeroXVsP+rsEsBVqjbnTUErU1SCT/2NKGrApjr5TVuOt5Vh2zGX8r6shLs5aM" +
                "RpRQBduhGyWaPc3nuWYdvbccU8TokpRfB6Hw+tVzEQTVAwsGN5mu9qRlHLt+pUSZmJNwoDr5uHWPuYqt" +
                "WDH58imL6I57BNupA79q/JScOwc2yCHcgqHyVJ7N8TOcoUKxCdQ79M5TWP5hh/EgZfxGe6MXqZrVYACo" +
                "j/jQo7MCuRPoTncuwyfE/R3/BrabcNmnx5jHG8veh2GlZTrDKLtBo2/4HYxBastTBurnwmu/q/hUurI6" +
                "eSMylLyUiKTi4mojgzDLt0LuSRrzzrlpflw2SRpNjXwakvK8u/QEN3pdY6LhXus4kmldpkFJrmLAOldI" +
                "Ka46eUP15wAvfSe4+30/ykGYUryFcF8O+a3LZF+RGmLygbtTebubvu2mb99+jPl76rIPxcSlD/g8NJ5/" +
                "fd3zzq9neNn9Z4+mcl5VfwOOEI6MMBEAAA==";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
