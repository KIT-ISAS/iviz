/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/JointLimits")]
    public sealed class JointLimits : IDeserializable<JointLimits>, IMessage
    {
        // This message contains information about limits of a particular joint (or control dimension)
        [DataMember (Name = "joint_name")] public string JointName { get; set; }
        // true if the joint has position limits
        [DataMember (Name = "has_position_limits")] public bool HasPositionLimits { get; set; }
        // min and max position limits
        [DataMember (Name = "min_position")] public double MinPosition { get; set; }
        [DataMember (Name = "max_position")] public double MaxPosition { get; set; }
        // true if joint has velocity limits
        [DataMember (Name = "has_velocity_limits")] public bool HasVelocityLimits { get; set; }
        // max velocity limit
        [DataMember (Name = "max_velocity")] public double MaxVelocity { get; set; }
        // min_velocity is assumed to be -max_velocity
        // true if joint has acceleration limits
        [DataMember (Name = "has_acceleration_limits")] public bool HasAccelerationLimits { get; set; }
        // max acceleration limit
        [DataMember (Name = "max_acceleration")] public double MaxAcceleration { get; set; }
        // min_acceleration is assumed to be -max_acceleration
    
        /// <summary> Constructor for empty message. </summary>
        public JointLimits()
        {
            JointName = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public JointLimits(string JointName, bool HasPositionLimits, double MinPosition, double MaxPosition, bool HasVelocityLimits, double MaxVelocity, bool HasAccelerationLimits, double MaxAcceleration)
        {
            this.JointName = JointName;
            this.HasPositionLimits = HasPositionLimits;
            this.MinPosition = MinPosition;
            this.MaxPosition = MaxPosition;
            this.HasVelocityLimits = HasVelocityLimits;
            this.MaxVelocity = MaxVelocity;
            this.HasAccelerationLimits = HasAccelerationLimits;
            this.MaxAcceleration = MaxAcceleration;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public JointLimits(ref Buffer b)
        {
            JointName = b.DeserializeString();
            HasPositionLimits = b.Deserialize<bool>();
            MinPosition = b.Deserialize<double>();
            MaxPosition = b.Deserialize<double>();
            HasVelocityLimits = b.Deserialize<bool>();
            MaxVelocity = b.Deserialize<double>();
            HasAccelerationLimits = b.Deserialize<bool>();
            MaxAcceleration = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new JointLimits(ref b);
        }
        
        JointLimits IDeserializable<JointLimits>.RosDeserialize(ref Buffer b)
        {
            return new JointLimits(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(JointName);
            b.Serialize(HasPositionLimits);
            b.Serialize(MinPosition);
            b.Serialize(MaxPosition);
            b.Serialize(HasVelocityLimits);
            b.Serialize(MaxVelocity);
            b.Serialize(HasAccelerationLimits);
            b.Serialize(MaxAcceleration);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (JointName is null) throw new System.NullReferenceException(nameof(JointName));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 39;
                size += BuiltIns.UTF8.GetByteCount(JointName);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/JointLimits";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8ca618c7329ea46142cbc864a2efe856";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACm2RQW6EMAxF90i9w5dm0y66q3qS7pFhzOAqiUexqejtmw4wwDDL+P8XPyUnfPViiGxG" +
                "F0aryUmSQVKnOZKLJlCjgyNIFDdoB8KVsks7BMr4VkmOV803NmvAWSInK+BbZZ4lXaZOnShyVZ3geWBI" +
                "B+95pnsyXNXktm3aUzVaripBvQT1HJQbohSpdEak8cB1Qck/P/47d3Qd0rgONyqrxg8HbcV/DxpLsNWg" +
                "8aG/W7REk/D9iPLcZDZEPsMVDeN9136qRW3LgfP0H49q23DRm+yO2M5wG8+WO+K56Y56qf4ARzGoPUIC" +
                "AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
