/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/Table")]
    public sealed class Table : IDeserializable<Table>, IMessage
    {
        // Informs that a planar table has been detected at a given location
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // The pose gives you the transform that take you to the coordinate system
        // of the table, with the origin somewhere in the table plane and the 
        // z axis normal to the plane
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        // There is no guarantee that the table does NOT extend further than the
        // convex hull; this is just as far as we've observed it.
        // The origin of the table coordinate system is inside the convex hull
        // Set of points forming the convex hull of the table
        [DataMember (Name = "convex_hull")] public GeometryMsgs.Point[] ConvexHull;
    
        /// <summary> Constructor for empty message. </summary>
        public Table()
        {
            ConvexHull = System.Array.Empty<GeometryMsgs.Point>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Table(in StdMsgs.Header Header, in GeometryMsgs.Pose Pose, GeometryMsgs.Point[] ConvexHull)
        {
            this.Header = Header;
            this.Pose = Pose;
            this.ConvexHull = ConvexHull;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Table(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Pose);
            ConvexHull = b.DeserializeStructArray<GeometryMsgs.Point>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Table(ref b);
        }
        
        Table IDeserializable<Table>.RosDeserialize(ref Buffer b)
        {
            return new Table(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Pose);
            b.SerializeStructArray(ConvexHull, 0);
        }
        
        public void RosValidate()
        {
            if (ConvexHull is null) throw new System.NullReferenceException(nameof(ConvexHull));
        }
    
        public int RosMessageLength => 60 + Header.RosMessageLength + 24 * ConvexHull.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/Table";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "39efebc7d51e44bd2d72f2df6c7823a2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UwY7TMBC9W+o/jNTDLogtEiAORRyQVsAegEW7N4SqaTJNDImdtZ222a/n2W7Tlj3A" +
                "AbaqlNiZeW/e83imdGVW1rWeQs2BmLqGDTsKvGyEava0FDFUSpAiSEkpptJr7DW24KCtUeqjcCmO6vRQ" +
                "akq3tVBnvaRIT4PtAS8UHBsf2TJZ4J+Sv9n0ubDWldpwEPKDD9ICya5yZiznGW10qNPaOl1pQ962sqnF" +
                "CWExxiUNQmzKtAeUe+Kt9mRAzc2eLkWpSoAR3LBofeWfX8eiY+U7FRE55lHVM4oPIrvSR67SQuDnL7ck" +
                "2yBgXPUOH2Fgzakk4BTWrGVLdd80b7AFQPx/9B5WelrBbDw2craGrKUXt4bNOsx2Nu6UHvvw0KgIqI3X" +
                "ZSxPjgmjjhsJMb+z2gQQwgRtqt8DTxgeuILMb9934YuEO1Fv//Fvoj7dfJiTD2VmzV01iQICzpJdSaiJ" +
                "Sw4cRVCtKxh90chaGmRx28G49DUMnfhsYDa7EiOOm2ag3iMIDVDYtu2NRgdDsm7lJB+ZsBx3gV3QRd/g" +
                "iI4sXzluJaLj7+WuF1MIXV3Ooz1eij6g58GkTeGEfbT66pJUDwtfvogJanq7sRdYShX7ZE+eGwvFyrZz" +
                "4mOd7OfgeJrFzYANdwQspafztLfA0j8hkKAE6WxR0zkqvx5CbfOFWLPTqWcAXMABoJ7FpLMnR8ix7DkZ" +
                "NnYPnxEPHH8Da0bcqOkC/V82Ub3vKxiIwM7ZNVq0pOWQu6/RYgI1eunYDSpmZUo1fR89znclnQie7L0t" +
                "NA6gTGNA+eAiejqNhS7/X0M+nBCxJ9+Rk3hOUJCmYL5hmB4wauUwJ3zHBUYWGi1ul7vvOsXG0YSLvc+d" +
                "kUpXbAxQX3sIdSbhHuIeTyOKmezvDzoiMKZLHpp7CZCDC5KqPlGsVo3l8PoVbce3YXy7fywFB/9GGeNx" +
                "oZVOXD2tP67uDu7HaTlTfxC1f9tA3i8jmfaIUQcAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
