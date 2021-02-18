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
                "H4sIAAAAAAAAE7VUTW/TQBC9r5T/sFIObZESJEAcKnFAVEAPSKBytyb22F6x3nVn12ncX88bW3FCAcEB" +
                "Iiv+mnnz5s0br+3X1iXbcUrUsC1jyORCsrllW3HtgssuBhtrS/gXx3g/PUFkyoLYvF2ZlfnIVLHYdjrp" +
                "gzWAFSI54eqHTGApusRdzNa78M2mnktXO8RRsmTvB8osAbEr03DsOMtYdKlJz78sb84RT+XOMLO2dSJp" +
                "hWsW9BVXBo9caKawIlDHc37sFYu8pYNLGwqNZ8siUZDjWSiUnE5MV6b2kfLrV5Z2Kfohc3EoNLNYon8R" +
                "Mv455PGnECX31j6wa9qsvGsqM1jVyuxJk5cVh5jBU9hDmj1b1/VRsgKhDRshvJwlpK1952Ni7dE+skQY" +
                "gTB9DzssqfnqRHNmsTLmzT/+mU93H65tytU86NlOaPwOBCqSCsQyVZRp6rsFC5aN5z17JFHXwzvT2zz2" +
                "nLaTHSANjoYDhPR+tENCEPosY9cNwZWwks0Ozj/PR6YL8GBPkl05eFK5olQuaHgtsIui40h8P7Dqentz" +
                "PUnK5aCSo5ILpTAlHdbtjTUDlH75QhPM+utD3OCWG9X8WBxzpKxk+dALpJ8W4Ro1ns3NbYENcRhVqmQv" +
                "p2cFbtOVRRFQ4D6Wrb0E889jbrEdumB7Ekc7uFg9AgWAeqFJF1dnyGGCDhTiEX5GPNX4G9iw4GpPmxYz" +
                "89p9GhoIiMBe4t5VCN2NE0jpdXuxgjshGY1mzSXN+r1qPDt7mgjOlFIsHQZQ2QeX2+MCT9MoXPW/3Pi7" +
                "b8/RXcI6LfSRnn4coVgtjJZ6gpa4O33S1L/d1iwbdViuxuXq8bRvxnwHQa6v66IFAAA=";
                
    }
}
