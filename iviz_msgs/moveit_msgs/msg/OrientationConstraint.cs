/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class OrientationConstraint : IDeserializable<OrientationConstraint>, IMessage
    {
        // This message contains the definition of an orientation constraint.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // The desired orientation of the robot link specified as a quaternion
        [DataMember (Name = "orientation")] public GeometryMsgs.Quaternion Orientation;
        // The robot link this constraint refers to
        [DataMember (Name = "link_name")] public string LinkName;
        // optional axis-angle error tolerances specified
        [DataMember (Name = "absolute_x_axis_tolerance")] public double AbsoluteXAxisTolerance;
        [DataMember (Name = "absolute_y_axis_tolerance")] public double AbsoluteYAxisTolerance;
        [DataMember (Name = "absolute_z_axis_tolerance")] public double AbsoluteZAxisTolerance;
        // A weighting factor for this constraint (denotes relative importance to other constraints. Closer to zero means less important)
        [DataMember (Name = "weight")] public double Weight;
    
        /// Constructor for empty message.
        public OrientationConstraint()
        {
            LinkName = string.Empty;
        }
        
        /// Explicit constructor.
        public OrientationConstraint(in StdMsgs.Header Header, in GeometryMsgs.Quaternion Orientation, string LinkName, double AbsoluteXAxisTolerance, double AbsoluteYAxisTolerance, double AbsoluteZAxisTolerance, double Weight)
        {
            this.Header = Header;
            this.Orientation = Orientation;
            this.LinkName = LinkName;
            this.AbsoluteXAxisTolerance = AbsoluteXAxisTolerance;
            this.AbsoluteYAxisTolerance = AbsoluteYAxisTolerance;
            this.AbsoluteZAxisTolerance = AbsoluteZAxisTolerance;
            this.Weight = Weight;
        }
        
        /// Constructor with buffer.
        internal OrientationConstraint(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Orientation);
            LinkName = b.DeserializeString();
            AbsoluteXAxisTolerance = b.Deserialize<double>();
            AbsoluteYAxisTolerance = b.Deserialize<double>();
            AbsoluteZAxisTolerance = b.Deserialize<double>();
            Weight = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new OrientationConstraint(ref b);
        
        OrientationConstraint IDeserializable<OrientationConstraint>.RosDeserialize(ref Buffer b) => new OrientationConstraint(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ref Orientation);
            b.Serialize(LinkName);
            b.Serialize(AbsoluteXAxisTolerance);
            b.Serialize(AbsoluteYAxisTolerance);
            b.Serialize(AbsoluteZAxisTolerance);
            b.Serialize(Weight);
        }
        
        public void RosValidate()
        {
            if (LinkName is null) throw new System.NullReferenceException(nameof(LinkName));
        }
    
        public int RosMessageLength => 68 + Header.RosMessageLength + BuiltIns.GetStringSize(LinkName);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/OrientationConstraint";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "ab5cefb9bc4c0089620f5eb4caf4e59a";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUTW/UQAy9z6+wtIe2SLtIgDhU4oCogB6QQOW+8iZOMmIyk3om201/Pc9Z7UeLEBxg" +
                "FW0yM/bz87M9C/re+Uy95MytUJViYR8zlU6olsZHX3yKlBpi/KsXnM87sMxFYVtWzn0WrkWpm1/OLQBq" +
                "7tmr1E+8gGPImjapUPDxB+VBKt942HEmpvuRi2iErWsl9VJ0Wve5zS+/HQ/OAQ+xzgCL5XNiRyqNKBJK" +
                "Djs+trPVOnIv5pwGg+FAvPN5ybENQqKaFA5BlGMl+cTRNSFxefuGeJNTGIusd2tzXB+Nf7WY/mjx+NwC" +
                "vN7Tg/i2K0a44aqAUGOkniV3WUtMBRRVAgTZCvl+SFoMBhlQgtp65pBX9CGkjD0cPoomVJ5R7oD6H13L" +
                "1ZHjnoRz7/7xz325+3RNudT74u77B2nfIXzNWoNW4ZoLz1l3ICG6DLKVACfuB7TLfFqmQfJqbgIIg6eV" +
                "CBFDmGjMMEKWVer7MfoK7UPFo9HP/eHpI9puYC2+GgObWElrH828UXSJoePJcj+KqXp7cz0LKtVogiOS" +
                "j5UKZyvV7Q25ETq/fmUObvH9IS2xlNYUPwRHFbkYWdkNCuHn3r9GjBf75FbAhjiCKHWmy3lvjWW+IgQB" +
                "BRlS1dElmH+dSoeJsJnasnreoH2tQ6AAUC/M6eLqDNloX1PkmA7we8RTjL+BjUdcy2nZoWYYqpby2EJA" +
                "GA6atr6G6WaaQapgE4vJ2yjr5MxrH9ItPprG+76urSJ4c86p8ihATQ++dIe5naux9vX/6sbf3TeH7lKx" +
                "aiEPMHx6F0KxRgUpDQwtsTrdYta/PW7Iw0Dtjl/T8evxNG7O/QTZnX/SkQUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
