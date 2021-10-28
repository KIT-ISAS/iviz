/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/OrientationConstraint")]
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
    
        /// <summary> Constructor for empty message. </summary>
        public OrientationConstraint()
        {
            LinkName = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
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
        
        /// <summary> Constructor with buffer. </summary>
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new OrientationConstraint(ref b);
        }
        
        OrientationConstraint IDeserializable<OrientationConstraint>.RosDeserialize(ref Buffer b)
        {
            return new OrientationConstraint(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Orientation);
            b.Serialize(LinkName);
            b.Serialize(AbsoluteXAxisTolerance);
            b.Serialize(AbsoluteYAxisTolerance);
            b.Serialize(AbsoluteZAxisTolerance);
            b.Serialize(Weight);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (LinkName is null) throw new System.NullReferenceException(nameof(LinkName));
        }
    
        public int RosMessageLength => 68 + Header.RosMessageLength + BuiltIns.GetStringSize(LinkName);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/OrientationConstraint";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ab5cefb9bc4c0089620f5eb4caf4e59a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VUTW/TQBC9768YKYe2SAkSIA6VOCAqoAckUHuPJvbYXrHedWfXadxfzxtH+aAIwQEi" +
                "K/7YmTdv3rzdBd13PlMvOXMrVKVY2MdMpROqpfHRF58ipYYY/+oF6/MXROaiiC0r5z4L16LUzTfnFgC1" +
                "9OxV6p+ygGPImjapUPDxO+VBKt94xHEmpoeRi2hErGsl9VJ0Wve5zS+/HRfOAQ+1zgCL9XNiRyqNKBpK" +
                "Dl98bOeodeReLDkNBsOBeOfzkmMbhEQ1KRKCKMdK8omja0Li8vYN8SanMBZZ79aWuD4G/xox/THi6XkE" +
                "eL2nR/FtV4xww1UBocZIPWvuspaYCiiqBAiyFfL9kLQYDDqgBLX1LCGv6ENIWaw9ehJNmDxj3AHzP6aW" +
                "qyPHPQnn3v3jn/ty9+macqn3w937B23foXzNWoNW4ZoLz113ICG6DLKVgCTuB9hlXi3TIHk1mwDC4Gol" +
                "QsQQJhozgtBllfp+jL6Cfah4GP08H5k+wnYDa/HVGNjESlr7aOGNwiWGjivLwyim6u3N9SyoVKMJjko+" +
                "ViqcbVS3N+RG6Pz6lSW4xf1jWuJVWlP8UBxT5GJkZTcohJ+9f40aL/bNrYANcQRV6kyX87c1XvMVoQgo" +
                "yJCqji7B/OtUOuwI21NbVs8b2NccAgWAemFJF1dnyHGGjhzTAX6PeKrxN7DxiGs9LTvMLFj3eWwhIAIH" +
                "TVtfI3QzzSBVsB2LnbdR1slZ1r6kW3w0jfe+nieCO+ecKo8B1PToS3fYt/M01r7+X2783XlzcJeKTQt9" +
                "5OdnIRRrVNDSwNASb6dTzPzb44Q8bKjd8Wk6Pj2dtptzPwDZnX/SkQUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
