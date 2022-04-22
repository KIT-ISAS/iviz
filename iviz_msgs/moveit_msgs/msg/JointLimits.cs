/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class JointLimits : IDeserializable<JointLimits>, IMessage
    {
        // This message contains information about limits of a particular joint (or control dimension)
        [DataMember (Name = "joint_name")] public string JointName;
        // true if the joint has position limits
        [DataMember (Name = "has_position_limits")] public bool HasPositionLimits;
        // min and max position limits
        [DataMember (Name = "min_position")] public double MinPosition;
        [DataMember (Name = "max_position")] public double MaxPosition;
        // true if joint has velocity limits
        [DataMember (Name = "has_velocity_limits")] public bool HasVelocityLimits;
        // max velocity limit
        [DataMember (Name = "max_velocity")] public double MaxVelocity;
        // min_velocity is assumed to be -max_velocity
        // true if joint has acceleration limits
        [DataMember (Name = "has_acceleration_limits")] public bool HasAccelerationLimits;
        // max acceleration limit
        [DataMember (Name = "max_acceleration")] public double MaxAcceleration;
        // min_acceleration is assumed to be -max_acceleration
    
        /// Constructor for empty message.
        public JointLimits()
        {
            JointName = "";
        }
        
        /// Constructor with buffer.
        public JointLimits(ref ReadBuffer b)
        {
            JointName = b.DeserializeString();
            b.Deserialize(out HasPositionLimits);
            b.Deserialize(out MinPosition);
            b.Deserialize(out MaxPosition);
            b.Deserialize(out HasVelocityLimits);
            b.Deserialize(out MaxVelocity);
            b.Deserialize(out HasAccelerationLimits);
            b.Deserialize(out MaxAcceleration);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new JointLimits(ref b);
        
        public JointLimits RosDeserialize(ref ReadBuffer b) => new JointLimits(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
        
        public void RosValidate()
        {
            if (JointName is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 39 + BuiltIns.GetStringSize(JointName);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/JointLimits";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8ca618c7329ea46142cbc864a2efe856";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE22QQW6EMAxF95ziS7NpF91VPUn3yIQwuEriUWyqmds3HWAggmX8/4uffMH3yIroVenq" +
                "4SQZcVJwGiRHMpYE6mQyBI5sChlAuFE2dlOgjB/hZHiT/GSzBPQcfdICvjdqmdN17rSJom+aCyxPHjzA" +
                "Rr/QIyluovzcNu9pOilflaBdg3YJyg+Ri1TqEel+4IYgZF+f/50Xug3pvg13KpvGrw/i2B4HjTXYa5T1" +
                "db9atEaz8OuJcm5SnaLvYYLO46Nqn2qRcz74TKcX2oer3mx3xCrDfbxYVsS5aUU1f+p1quFBAgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
