/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/OrientationConstraint")]
    public sealed class OrientationConstraint : IDeserializable<OrientationConstraint>, IMessage
    {
        // This message contains the definition of an orientation constraint.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // The desired orientation of the robot link specified as a quaternion
        [DataMember (Name = "orientation")] public GeometryMsgs.Quaternion Orientation { get; set; }
        // The robot link this constraint refers to
        [DataMember (Name = "link_name")] public string LinkName { get; set; }
        // optional axis-angle error tolerances specified
        [DataMember (Name = "absolute_x_axis_tolerance")] public double AbsoluteXAxisTolerance { get; set; }
        [DataMember (Name = "absolute_y_axis_tolerance")] public double AbsoluteYAxisTolerance { get; set; }
        [DataMember (Name = "absolute_z_axis_tolerance")] public double AbsoluteZAxisTolerance { get; set; }
        // A weighting factor for this constraint (denotes relative importance to other constraints. Closer to zero means less important)
        [DataMember (Name = "weight")] public double Weight { get; set; }
    
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
        public OrientationConstraint(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Orientation = new GeometryMsgs.Quaternion(ref b);
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
            Orientation.RosSerialize(ref b);
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
    
        public int RosMessageLength
        {
            get {
                int size = 68;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(LinkName);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/OrientationConstraint";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ab5cefb9bc4c0089620f5eb4caf4e59a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUwW7TQBC9W+o/jJRDW6QECRCHSBwQFdADEqjco4k9tlesd93ZdRr363njKE5ahOAA" +
                "kZXEuzNv37x5swv63rpEnaTEjVAZQ2YXEuVWqJLaBZddDBRrYnyrE+xPK4hMWRGbV0XxWbgSpXb6KYoF" +
                "QC09OZXqSRZwDFnjNmbyLvyg1Evpaoc4TsR0P3AWDYgtGomdZB03XWrSy2/zxjng8awzwGz1nNiRSi2K" +
                "gmKBFReaKWoTuBNLjr3BsCfeu7Tk0HghUY2KBC/KoZR04ljUPnJ++4Z4m6Ifsmz2G0vczMG/Rox/jHh8" +
                "HgFe7+lBXNNmI1xzmUGoNlLPiruqJMQMiioeguyEXNdHzQaDCihCbT1LSCv64GPCGjYfRSM6z2i3R//n" +
                "1Hw9czyQKC6Kd//4c1F8ufu0ppSrQ3sPDrpA5XdgULFWYJa54sxT4S14iC697MQji7sejpl289hLWk0+" +
                "gDZ4GgnQ0fuRhoQgFFrGrhuCK+Egyg5eP89HpgtwXs+aXTl4Nr2iVi5YeK0wiqHjSXI/iAl7e7OeNJVy" +
                "MM1xkgulCifr1u0NFQOkfv3KEorF94e4xKs0JvrxcDSSs5GVfa/QfrL/Gme8OBS3AjbUEZxSJbqa1jZ4" +
                "TdeEQ0BB+li2dAXmX8fcYihsrHasjrdwsJkECgD10pIur8+QjfaaAod4hD8gns74G9gw41pNyxY9w1w1" +
                "lIYGAiKw17hzFUK34wRSehtaDN9WWcfCsg5HFouPpvHB2lNHHC6ClGLp0ICKHlxuj6M7dWPjqv9nyN9d" +
                "OubMyWAq1jCUApJPb0SIVqugqp4hJ95Od5lZuMM9eRyr/fxvnP89noYO5f0EkFBTiJgFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
