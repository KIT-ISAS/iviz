/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PolygonStamped : IDeserializable<PolygonStamped>, IMessage
    {
        // This represents a Polygon with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "polygon")] public Polygon Polygon;
    
        /// Constructor for empty message.
        public PolygonStamped()
        {
            Polygon = new Polygon();
        }
        
        /// Explicit constructor.
        public PolygonStamped(in StdMsgs.Header Header, Polygon Polygon)
        {
            this.Header = Header;
            this.Polygon = Polygon;
        }
        
        /// Constructor with buffer.
        internal PolygonStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Polygon = new Polygon(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PolygonStamped(ref b);
        
        PolygonStamped IDeserializable<PolygonStamped>.RosDeserialize(ref Buffer b) => new PolygonStamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Polygon.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Polygon is null) throw new System.NullReferenceException(nameof(Polygon));
            Polygon.RosValidate();
        }
    
        public int RosMessageLength => 0 + Header.RosMessageLength + Polygon.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/PolygonStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "c6be8f7dc3bee7fe9e8d296070f53340";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTW/UMBC9+1eMtIe2iBYJbitxQCCgB6RK7Q2hyutMkhGOHTzOtuHX8+wkS6VeOACr" +
                "lfLh+Xhv5r3s6K4XpcRjYuWQlSzdRD93MdCD5B4nLScOjsnFmBoJNjO1yQ5MNjSUZWDNdhjNZ7YNJ+rr" +
                "xWw1xuVqzNu//DNfbj/tSXNzP2inr5buZke3GbBsamjgbBubLbURqKTrOV16PrKnipcbqqd5HlmvzDoH" +
                "/DsOnKz3M02KoBxBfBimIK4wP/Hd8pEpAUMbbcriJm/Ts0GV6vgr/5jqIK8/7BETlN2UBYBmVHCJrUro" +
                "cEhmkpDfvC4JZnf3EC/xyB1me2pOube5gOXHsreC0+oePV4s5K5QG8NhdGmUzuu7ezzqBaEJIPAYXU/n" +
                "QH4z5x57yj3T0SaxB8+lsMMEUPWsJJ1dPKlcYO8p2BC38kvF3z3+pGw41S2cLnvszBf2OnUYIALHFI/S" +
                "IPQw1yLOC9RJXg7JptmUrKWl2X2sYsxlfXUjuFrV6AQLaKqIjeZUqtdt3Evzr9TYcYTq0rxIcrWA2b0j" +
                "HdlJWxQkGEpsi142k0GXBT20IklzNZW3uBkj1g4qOAWdaVi0eCg+DIEduMFkVShfv63B/4tX7bpZBnCy" +
                "laCVwxhVnnJEZLFHmxjrGq3j8/pRgbgPAnKIgn6dKFIuik2uq6rxCpbjZqEMG1LtuczqCCOgjUqVVNAM" +
                "55dCK6wrKl7bwK2V8EWoqPAGBaGtIcKcxVYpjnD7QbzkuaZumXCa2q6KtmGVLixgsv3ONI3kcbwwKqgC" +
                "vBZg+Q7ZPq7E1v1livDHSyyxTKJq2YJRHVDF/N7HqSm9TeujLcZ/PN3Np7uf5hfG0nZTqwUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
