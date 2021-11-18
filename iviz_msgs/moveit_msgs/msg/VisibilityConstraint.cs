/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/VisibilityConstraint")]
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
    
        /// <summary> Constructor for empty message. </summary>
        public VisibilityConstraint()
        {
            TargetPose = new GeometryMsgs.PoseStamped();
            SensorPose = new GeometryMsgs.PoseStamped();
        }
        
        /// <summary> Explicit constructor. </summary>
        public VisibilityConstraint(double TargetRadius, GeometryMsgs.PoseStamped TargetPose, int ConeSides, GeometryMsgs.PoseStamped SensorPose, double MaxViewAngle, double MaxRangeAngle, byte SensorViewDirection, double Weight)
        {
            this.TargetRadius = TargetRadius;
            this.TargetPose = TargetPose;
            this.ConeSides = ConeSides;
            this.SensorPose = SensorPose;
            this.MaxViewAngle = MaxViewAngle;
            this.MaxRangeAngle = MaxRangeAngle;
            this.SensorViewDirection = SensorViewDirection;
            this.Weight = Weight;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal VisibilityConstraint(ref Buffer b)
        {
            TargetRadius = b.Deserialize<double>();
            TargetPose = new GeometryMsgs.PoseStamped(ref b);
            ConeSides = b.Deserialize<int>();
            SensorPose = new GeometryMsgs.PoseStamped(ref b);
            MaxViewAngle = b.Deserialize<double>();
            MaxRangeAngle = b.Deserialize<double>();
            SensorViewDirection = b.Deserialize<byte>();
            Weight = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new VisibilityConstraint(ref b);
        }
        
        VisibilityConstraint IDeserializable<VisibilityConstraint>.RosDeserialize(ref Buffer b)
        {
            return new VisibilityConstraint(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
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
            if (TargetPose is null) throw new System.NullReferenceException(nameof(TargetPose));
            TargetPose.RosValidate();
            if (SensorPose is null) throw new System.NullReferenceException(nameof(SensorPose));
            SensorPose.RosValidate();
        }
    
        public int RosMessageLength => 37 + TargetPose.RosMessageLength + SensorPose.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/VisibilityConstraint";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "62cda903bfe31ff2e5fcdc3810d577ad";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1XTW/bOBC9C8h/IJBDk4WbpmmxWGSRQ9GP3Ryadjc9tL0YtDS2iUiiSlJ23F/fN0NR" +
                "ot10sYfdFilsU+TjzJs3HzpWH9akStv64LRpgzJe9Z6Wfa2CVQ0v4b/aGG8WpjZhx8taVcaX6iTgaNBu" +
                "ReFUYZNWnXbBlH2tnVo63dBZcQx8QMr+pXWNV3xooT0pu8SJBLxjG0ht1xZPgunYDh1ks3VmBXRs51+e" +
                "Wm8dA78djDPtKrePb2OoxU5ha+/4MR90dmEDHpFXLb7YBTzuy3hFdpzNYPQbG+gymZ/xIwA37z4AHP6U" +
                "pBpY0PQNzARd9/IV3gbdlti4oLAlajPLAanbSk3UzWCOi7AmjKjTcyZ8Qcwvry0N1RVzsTG0xSfgMlbU" +
                "C/XeehOMbV9OJpe6VSd8aQcO9KLeKb+2fV2dMi6CXXFklO/Ltep61yEC/qwQ18GarkzvE/kSxrBGYCIC" +
                "AySNAEZorEkVy9rq8OvzwYN5BEmQfEEO+LvSPotQYzfkZ7JwuBN3IUBr3a6Iz2yprscIwceMJRZfFFgm" +
                "yYhfm/ZuJh7TvW66mooV2YaC280bv/JPQCDdBjyBQ4P9bAdb/8bZJqM7KTPYrXaV34vpaHIUvT6UWKYs" +
                "WI0NHYID9eiAa3vPom10i0iZCtFQbyZzZ0gSKCrueR43zJToAHGBOUsNVTf6DoI60LajFZTBPIGXHTgy" +
                "1ZihG133lMKq663esXrVM9G1dcgJhPnZhRg8l0v34ol7t2sDBe1nYtTuJJF4XQqQqdKFjjpHYDWm4xQo" +
                "Boi4Ge1SGoIu1xHvWoqW9r5vOGTTPtZEbe0dAuGoDND9UFFSkLhmgeP4GMyMZAhYx0uxHva6xmkuG6gZ" +
                "iMCiD0pXlSSarh/IOQ8kvh5O2XrD6QwcA1Tw+GO5RbtHub3eSOWw/Wo96Qk432dc1NuBxFJObMhBRw1c" +
                "AOaCSt1PWQUFg3QtnuaJtjVhzcZ3IIZjkNeYyDeMlKuBmeUS75XKNCDdQUqN9QEBR+poUavwHoNach94" +
                "yPSBPkLR6ju11hsWe1lLc7DqXG1sjWifiZbkPtQE3M61n5bCTFQeY8dHQyUe6iVKLghJnmW3i/epQI/C" +
                "AIdlgJfLBwtAEnVMoQXVdjsJOiZhahJIG2BWmc0xt5GP5JZMNj+ZiZVjUm5NXbMz50PH3drBHgjVEVOP" +
                "c7ZDREwgf5oQS8sRCQTlpmY33Pww/Hvz5CLekKPDrg5hiCUU2FmxSFRHjwFqBmeHNpgF5rs6EHGmLqIH" +
                "zKGAW9yJ74EcloF8cj4T+2DA9RJE8NlUZvfjn7VmVIfUiGDSnPfNZV8x5nk6JV3Qm8bASfAlo09+QjK+" +
                "xvOw72KEO36gz6fm8J2GkriAwFgoSVB8i2eCDKzD5pLPAkNji7LLRMe+rHhQG/hz0iNH1oah44AeZZbM" +
                "omTUPlVyPOcqNzcvthYXQhvoWgcJkyYUrg17w89HdaUuZuoTPp7O1Gd8nBc9Iv2bun19c/vu7/nnq4OF" +
                "T1dP9xc+Xl0MC0PBlDiNd7PFLzAcmNU6cNngfsi5K5bsD3MnFaGoo1E6qlEEUahN01knwxs7Z2E6+shU" +
                "1s/USy5CQLLqKznIhHTrVU3ej0fD6chlNKI4Kq7+439HxdvbPy7VD1vJkXDAC6mUL8kRe1Va6yrTcsSi" +
                "mESPpiGMrE1X/Em6gntr+SgEQNrR/+aCD1W0Pt7MhsOHtsJEBXKDrnTQErs1qCT3uKYN1TgVO6Y8DbuO" +
                "J9Yhp/G3opactGzJa8QKtbDp20Goydl0nivXwdvLIUuMLqn5pRcWr19diiyo7Fk2uMm0pSMtQ9n1KyX6" +
                "xLSEA8Xxh619zLVsxbpJl4+5RPfcKdhO7fmF45fo3BmwwQ7hFoyWJ7I2x09/ijrFJlBn0UFPYPn7HYaE" +
                "mPcb7QwmfCkIJRgA6iM+9Og0Q2azL1WrW5vgI+J0x7+BbUdc9ukxpvIKE9sK7xErEMgTgrMbtHuUdxRU" +
                "fsmsedZAFV047XYFn4pXFsdvRImSnRKRWGJsaWQcZgUXyEBJZt45N9XPzKmYTGNHH6elNPguHcGTTpcY" +
                "bbjpYlle0aQh815OsWzSOlNILC4/aUPxVw9HXSu4076f5yOMOcreSLhHxz43uhDfpcTqPY+LVOrux2+7" +
                "8dvXn+XBxN/oRjaAYZDM59w9+/nXl4l9flvDu+8/OzVWd7j3DYENulZAEQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
