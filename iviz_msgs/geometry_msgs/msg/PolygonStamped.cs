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
                "H4sIAAAAAAAAE71UTW/UMBC9+1eMtIe2iC0S3CpxQCCgB6RK7Q2hymtPkhGOHWxn2/DreZ7sLpV64QCs" +
                "VsqHZ97Mm3kvG7obpFDmKXPhWAtZuklh6VOkB6kDTjrOHB2TSyl7ibYyddmOTDZ6qjJyqXaczGe2njMN" +
                "ejFHjGm9GvP2L//Ml9tPV1Sqvx9LX16t1c2GbivastnTyNV6Wy11CV1JP3DeBt5zIO2XPelpXSYul0jU" +
                "OeDfc+RsQ1hoLgiqCcTHcY7iGvMT32M+MiViaJPNVdwcbH42qIaOf+Efsw7y+sMVYmJhN1dBQwsQXGZb" +
                "JPY4JDNLrG9etwSzuXtIWzxyj9meilMdbG3N8mPbW+vTlivUeLGSuwQ2hsOo4gud67t7PJYLQhG0wFNy" +
                "A52j85ulDthTHZj2NovdBW7ADhMA6llLOrt4ghwVOtqYjvAr4u8afwIbT7iN03bAzkJjX+YeA0TglNNe" +
                "PEJ3i4K4IFAnBdllmxfTstaSZvNRxVjb+nQjuNpSkhMswKuITam5oes27sX/KzX2nKC6vKySPFjAbN5R" +
                "mdhJ1xQkGErqml6OJoMuWSl2kktVUwWLmymJ2hGnoDOPqxZ3zYcxsgM3mEyF8vXbIfh/8dKqR8ugnWol" +
                "FuUwpSJPOSKy2aPLjHVN1vG5flQg7p2AHKKgXycFKRfNJteqaryC5divlGFD0prrrPYwAsoUUUnFUuH8" +
                "BnRo65Jotdvhu6ZI+CJoV3gDQGhrTLUlV85pgtt3EqQumnrMhNOK7VW0nov0cW2m2u9M80QBxyuj1lWE" +
                "1yIs3yM7pAOxw/4qJfjjJZbYJqFatmCkA9Ke34c0+1bbdCHZZvzH091yuvtpfgHG0nZTqwUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
