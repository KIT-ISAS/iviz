/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
        
        /// Explicit constructor.
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
        
        /// Constructor with buffer.
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new VisibilityConstraint(ref b);
        
        VisibilityConstraint IDeserializable<VisibilityConstraint>.RosDeserialize(ref Buffer b) => new VisibilityConstraint(ref b);
    
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/VisibilityConstraint";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "62cda903bfe31ff2e5fcdc3810d577ad";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1XTXPbNhC981dgxofYHcVx3Eyn444PmXy0PuSjdQ5JLhqIXFEYkwQDgJKVX9+3C4KE" +
                "FKfTQ5uMM5II4GH37dsPnqgPG1Kl7Xxw2nRBGa8GT+uhUcGqlh/hv9oab1amMWHPj7WqjC/VacDRoF1N" +
                "4Uxhk1a9dsGUQ6OdWjvd0nlxAnxAyv61da1XfGilPSm7xokEvGcbSO02FivB9GyHDrLZOlMDHdv5l6fO" +
                "W8fAb0bjTFfn9vFtDLXaK2wdHC/zQWdXNmCJvOrwxa7g8VDGK7LjbAajv7WBrpL5GT8C8PbdB4DDn5JU" +
                "CwvaoYWZoOtevsLboLsSG1cUdkRdZjkgdVepmboFzHER1oQJdV5nwlfE/PKztaGmYi62hnb4BFzGinqu" +
                "3ltvgrHdi9nkUnfqlC/twYFeNXvlN3ZoqjPGRbArjozyQ7lR/eB6RMCfF+I6WNOVGXwiX8IYNghMRGCA" +
                "pBHACI0NqWLdWB1+eTZ6sIwgCZIvyAF/U9pnEWrtlvxCHhzvxF0I0EZ3NfGZHTXNFCH4mLHE4osCyyQZ" +
                "8RvT3S3EY7rXbd9QUZNtKbj9svW1fwIC6TZgBQ6N9rMdbP1rZ9uM7qTMYHfaVf4gppPJUfT6WGKZsmA1" +
                "NvQIDtSjA64dPIu21R0iZSpEQ72ezV0gSaCouOdZ3LBQogPEBeasNVTd6jsI6kjbjmoog3kCL3twZKop" +
                "Q7e6GSiFVTc7vWf1qp9F19YhJxDmny/F4KVcehBP3LvbGCjoMBOjdmeJxOtSgEyVLnTUOwKrMR3nQDFA" +
                "xM1ol9IQdLmJeDdStLT3Q8shm/exJhpr7xAIR2WA7seKkoLENQscx2UwM5EhYD0/ivVw0A1Oc9lAzUAE" +
                "VkNQuqok0XTzQM55IPH1cMo2W05n4Biggsfvyy3aPcnt1VYqhx3qzawn4HybcVFvRxJLObElBx21cAGY" +
                "Kyr1MGcVFAzStXiaJ9rOhA0b34MYjkFeYyLfMFKuBmaWS7xXKtOIdAcptdYHBBypo0WtwnsMasl94CHT" +
                "R/oIRWvo1UZvWexlI83Bqgu1tQ2ifS5akvtQE3A7135aCzNReYwdl8ZKPNZLlFwQkjzLbhfvU4GehAEO" +
                "ywAv1w8WgCTqmEIrauxuFnRMwtQkkDbArDKbY24jH8mtmWxeWYiVU1LuTNOwMxdjx93Z0R4I1RFTj3O2" +
                "R0RMIH+WEEvLEQkE5aZmN978MPx78+Qy3pCjw64eYYglFNhZsUhUR48BakZnxzaYBeabOhBx5i6iR8yx" +
                "gFvcie+BHB4D+fRiIfbBgJs1iOCzqcwexj9rzagOqRHBpCXvW8q+YsrzdEq6oDetgZPgS0af/IRkfIP1" +
                "cOhihDt5oM+n5vCNhpK4gMBYKElQfIc1QQbWcXPJZ4GxsUXZZaJjX2oe1Eb+nPTIibVx6DiiR5k1sygZ" +
                "dUiVHM+5ys3Ni63FhdAGutZRwqQJhWvDwfDzUV2ry4X6hI+nC/UZHxfFgEj/qm5fvb1999fy8/XRg0/X" +
                "Tw8ffLy+HB+MBVPiNN3NFj/HcGDqTeCywf2Qc1csORzmTitCUUejdNSgCKJQm7a3ToY3ds7CdPSRuayf" +
                "qxdchIBk1VdykAnpzquGvJ+OhrOJy2hEUVz/x/+KN7e/X6nvNhIhgH+nOr4mR+xSaa2rTMfhikoSMZqW" +
                "MK+2ffEH6Qq+beSjEIDYi/4f+32oounxXlgN+7sKsxRoDbrSQUvUNiCR3OOGttTgUOyVshr2Pc+qYzbj" +
                "r6aOnDRryWhECVWwHbpRosnTdJ5r1tF7yzFFjC5J+WUQCm9eXokgqBxYMLjJdKUjLePYzUslysSchAPF" +
                "yYedfcxVrGbFpMunLKJ77hFsp/b8qvFTdO4c2CCHcAuGylN5tsRPf4YKxSZQb9E7T2H5+z3Gg5jxW+0M" +
                "ZnspBSUYAOojPvToLENms69Upzub4CPifMe/ge0mXPbpMebxCrNajTeIGgTybODsFo0ehR2llF8vG54y" +
                "UD9XTrt9wafilcXJa5Gh5KVEJBYXWxoZhFm+BXJP0ph3Lk3147JJ0mhq5NOQlObdtSO40esSEw33WjyW" +
                "NzPpw7yXkysbsM4VUoqrTtpQ/DnAS9cJ7rzvRzkIU7K3EO7LsbdN9sf3JzH5wN0ilbf76dt++vb1x5g/" +
                "U5d8yCYuTI75YHtgPP/6MvPOr2d42f1nj6ZyXhR/A44QjowwEQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
