/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/JointConstraint")]
    public sealed class JointConstraint : IDeserializable<JointConstraint>, IMessage
    {
        // Constrain the position of a joint to be within a certain bound
        [DataMember (Name = "joint_name")] public string JointName { get; set; }
        // the bound to be achieved is [position - tolerance_below, position + tolerance_above]
        [DataMember (Name = "position")] public double Position { get; set; }
        [DataMember (Name = "tolerance_above")] public double ToleranceAbove { get; set; }
        [DataMember (Name = "tolerance_below")] public double ToleranceBelow { get; set; }
        // A weighting factor for this constraint (denotes relative importance to other constraints. Closer to zero means less important)
        [DataMember (Name = "weight")] public double Weight { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public JointConstraint()
        {
            JointName = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public JointConstraint(string JointName, double Position, double ToleranceAbove, double ToleranceBelow, double Weight)
        {
            this.JointName = JointName;
            this.Position = Position;
            this.ToleranceAbove = ToleranceAbove;
            this.ToleranceBelow = ToleranceBelow;
            this.Weight = Weight;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public JointConstraint(ref Buffer b)
        {
            JointName = b.DeserializeString();
            Position = b.Deserialize<double>();
            ToleranceAbove = b.Deserialize<double>();
            ToleranceBelow = b.Deserialize<double>();
            Weight = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new JointConstraint(ref b);
        }
        
        JointConstraint IDeserializable<JointConstraint>.RosDeserialize(ref Buffer b)
        {
            return new JointConstraint(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(JointName);
            b.Serialize(Position);
            b.Serialize(ToleranceAbove);
            b.Serialize(ToleranceBelow);
            b.Serialize(Weight);
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
                int size = 36;
                size += BuiltIns.UTF8.GetByteCount(JointName);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/JointConstraint";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c02a15146bec0ce13564807805b008f0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE22QzWrEMAyE74G8w8BeWvpzKr2XfYyyLIqjbFQca7HUBPr0dZI2WUoPvmi+kWZ8wFGT" +
                "eSZJ8J5xVRMXTdAOhA+V5HBFw5jE+wIRAmef8UY/U1tXxSzpsqLnRAPXVV0dlmUL8WOn0AuP3EIM79uV" +
                "p6JGzpQCnxuOOj3uCR5uNGp05FNddVHJX182aJ/8Yf8TlgNrujdMLJfe5+QdBdeMrrzS0BB+P8Rx13JS" +
                "Z0PmSC4jQ4arlvpl3dxLS8t8Y7BnHKNamRXxi7NiYEqGyGab1e/3cGuK6hvCbxEPhgEAAA==";
                
    }
}
