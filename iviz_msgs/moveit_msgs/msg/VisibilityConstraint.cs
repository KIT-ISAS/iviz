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
        [DataMember (Name = "target_radius")] public double TargetRadius { get; set; }
        // The pose of the disc; as the robot moves, the pose of the disc may change as well
        // This can be in the frame of a particular robot link, for example
        [DataMember (Name = "target_pose")] public GeometryMsgs.PoseStamped TargetPose { get; set; }
        // From the sensor origin towards the target, the disc forms a visibility cone
        // This cone is approximated using many sides. For example, when using 4 sides, 
        // that in fact makes the visibility region be a pyramid.
        // This value should always be 3 or more.
        [DataMember (Name = "cone_sides")] public int ConeSides { get; set; }
        // The pose in which visibility is to be maintained.
        // The frame id should represent the robot link to which the sensor is attached.
        // It is assumed the sensor can look directly at the target, in any direction.
        // This assumption is usually not true, but additional PositionConstraints
        // can resolve this issue.
        [DataMember (Name = "sensor_pose")] public GeometryMsgs.PoseStamped SensorPose { get; set; }
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
        [DataMember (Name = "max_view_angle")] public double MaxViewAngle { get; set; }
        // This angle is used similarly to max_view_angle but limits the maximum angle
        // between the sensor origin direction vector and the axis that connects the
        // sensor origin to the target frame origin. The value is again in the range (0, Pi/2)
        // and is NOT enforced if set to 0.
        [DataMember (Name = "max_range_angle")] public double MaxRangeAngle { get; set; }
        // The axis that is assumed to indicate the direction of view for the sensor
        // X = 2, Y = 1, Z = 0
        public const byte SENSOR_Z = 0;
        public const byte SENSOR_Y = 1;
        public const byte SENSOR_X = 2;
        [DataMember (Name = "sensor_view_direction")] public byte SensorViewDirection { get; set; }
        // A weighting factor for this constraint (denotes relative importance to other constraints. Closer to zero means less important)
        [DataMember (Name = "weight")] public double Weight { get; set; }
    
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
        public VisibilityConstraint(ref Buffer b)
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
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (TargetPose is null) throw new System.NullReferenceException(nameof(TargetPose));
            TargetPose.RosValidate();
            if (SensorPose is null) throw new System.NullReferenceException(nameof(SensorPose));
            SensorPose.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 37;
                size += TargetPose.RosMessageLength;
                size += SensorPose.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/VisibilityConstraint";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "62cda903bfe31ff2e5fcdc3810d577ad";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71XTY/bNhC9C9j/QGAP2S2czSeKIsUegny0e2iSdnNIclnQ0tgmQokKSdlxfn3fDEWJ" +
                "9m7QHtoEG9gWyceZN28+dKreb0jVrgvRa9NFZYIaAq0Gq6JTLT/Cf7U1wSyNNXHPj7VqTKjVWcTRqP2a" +
                "4rnCJq167aOpB6u9Wnnd0sVJdYoLgCkHVs63QfGppQ6k3ApHMvKejSC12zisRNOzITrKZufNGvDYzr8C" +
                "dcF5Qf5jNM9069JCvo6xlnuFvYPnZT7p3dJFLFFQHb64JXwe6nRHcZztEPg3LtKzyYOCI4F48/Y94OFS" +
                "TaqFDe3QwlJQ9lW+wuGouxoblxR3RF1hPGPqrlEzfwtY5BOuiRPsvM6sL4lJ5mcrQ7ZhPraGdvhkvIIa" +
                "9Vy9c8FE47oXs9G17tQZ39qDB720exU2brDNOQMj5A2HR4Wh3qh+8D3CEMBCch/c6cYMIcdAohk3iE/C" +
                "YIisFQAJmZbUSbWyTsefn45e3CSUGZVvKTF/VToUoWrdlsJCHhzvxHWI1EZ3a+IzO7J2jhQ8LchiISat" +
                "FfJMF1jTfV6I3/RVt72lk2pNrqXo9zdtWIcH4JGuI5bg1egDW5I8eO1dW/CedRrdTvsmHER3MjulgD7W" +
                "W6kyEun3CBOUpCNuHgJLuNUdYmYaxEW9nk1eIGegrrTnadqwUEkSCBAMWmmIvNWfKRxL3dMaImGyQM4e" +
                "RJlmTtmttgPlAGu703vWsnoiKneecwQRf/JYbL6Re48ii7t3GwNBHSZn0vKsl4t8KIXKNPlST70nkBsL" +
                "TXDIGCEBF+xLvYi63oyAV1LLdAhDS025keVhnfuMgHiqIxJhrDM5WFzKQHVaBj8zJYLW87NUJwdtcZyL" +
                "CSoJIrEcotJNI6mn7R1ZGBiKDYBfzm45xQFkAMt0fl97yfRCe6+2UlHcsN7M4gLU7Sxc3FXfco5syUNT" +
                "rU7Zs6RaD3OeQc/gXou7ZertTNywAz3VUpjK0pNYh51yN4MW2cWbpWSNUJ8hq9aFiMAjk7RodzGrpuYm" +
                "cZfxmURCNRt6tdFb1n5tpXU49VBtnUXUL0RUciHqhJW8amgl5CQNMnhaGot0rqSoxiAlO1fcLwTk2j0p" +
                "BDzW0fnkx62KMOk7ZdSSrNvN2k5JmTsIkgigTWF1SnbkJ/kVE84ri2TnlKQ7Yy3783DsyTs3WgTJemL6" +
                "cdD1PeuRwnmGrB1HJRI0nJvhePV38N+ZB4/TFSU8LOsRilRZzy/K6pHpTj4zqhn9HdtkEZ1bZSEBzR1G" +
                "j6BjYXe4FN8jeTxm6LOHC7EQJlytwAUfzsX3UAVF7+ZikbsUjLrhjTeyMRczzvt8UPpkMK2Bp3afRqTy" +
                "jFQAi/V46OYIeHrHMJDbxi0xZZUBIqRaDvF3WBNoBjvuO+XAMLa9JMBCfezNmke6kUUvLXSiLo8mRyQp" +
                "s2IuJbuOCBOAQ8ZKm8sa7HApZIKedpQ+eZThWnE4Jn1Ql+rxQn3Ex6OF+oSPhyfVgKj/oq5fvbl++9fN" +
                "p8vjJx8vHx09+XD5OD8ZK6mEbDIgGf4cY4RZbyIXE+6ZnNBi0OH0d9YQKj6aqSeL4ogibtreeZn22EcH" +
                "D3xxAO36BZcmz4vfyEMzpLugLIUwHY3nM6vJipOquvyP/1V/XP/2TH23ywgF/DtX+BV5Yqdq53xjOo5b" +
                "kpVI07QU+GD1O+kG3m3koxIAblT/l/0hNsn0dC+shv1dg5kLxEbd6KglbhuwSP6+pS1ZFcZGKqtx32OI" +
                "ysmNvzV15KWXS4IjTiiN7dCNWs2e5vNcxo5ed44pYnTJ0C+DUHj18plIguqBJYObTFd70jK1Xb1Uok7M" +
                "UjhQnb7fuftc2NasmXz5lE70lTsH26nDM9zxU3LuAtggh3ALhs8zeXaDn+EcBYtNoN6hqZ7B8nd7TA4p" +
                "/bfaG71Mxa0GA0C9x4funRfInUB3unMZPiHOd/wb2G7CZZ/uY3ZvLHsfhrWW8Q0T7xYTQMMvbgxSW54/" +
                "UE6XXvt9xafSldXpa5GhZKZEJFUZVxuZl1m+FbJPEpl33pjmx2WTpNHU3qfxKU/EK09wo9c1Rh1uwI4j" +
                "mdZlWJTkKkavC4WU4rqTN1R/DvDSd4I77/tRDsKU4mWFW3XIb2gm+4rUEJMP3J3q29fp23769u3HmD9T" +
                "l30o5jB9wOeh8fzry8w7v8ZdVP/g0VTPq+pvS5Hg6GcRAAA=";
                
    }
}
