/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PointStamped : IDeserializable<PointStamped>, IMessage
    {
        // This represents a Point with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "point")] public Point Point;
    
        /// Constructor for empty message.
        public PointStamped()
        {
        }
        
        /// Explicit constructor.
        public PointStamped(in StdMsgs.Header Header, in Point Point)
        {
            this.Header = Header;
            this.Point = Point;
        }
        
        /// Constructor with buffer.
        internal PointStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Point);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PointStamped(ref b);
        
        PointStamped IDeserializable<PointStamped>.RosDeserialize(ref Buffer b) => new PointStamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ref Point);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 24 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/PointStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "c63aecb41bfdfd6b7e1fac37c7cbe7bf";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVTwWrcMBC96ysG9pCksCm0pYeF3kqbHgqB5L7MSmN7QJYczXgT9+s7krtuoZceGiMs" +
                "ydK8efPmeQePAwsUmgoJJRVAuM+cFJ5ZB/veUaHkCXzOJXBCJegKjgSYAiiPJIrj5O4IAxUY2uRWhKm+" +
                "nfv0nx/3/eHrAUTDcZRe3q6Z3Q4e1ChhCTCSYkBF6LIx4n6gso90pgiNKwVop7pMJLfulwI2ekpUMMYF" +
                "ZrFLmq3ocZwT+1r1Vusl3iI5mVwTFmU/Ryx/iVTRbQg9zU3Eb58PdicJ+VnZCC2G4AuhcOrtENxsir1/" +
                "VwPc7vE5721Lvem6JQcdUCtZeqkdqzxRDpbjzVrcrWGbOGRZgsB1+3a0rdyAJTEKNGU/wLUxv190yMkA" +
                "Cc5YGE+RKrA3BQz1qgZd3fyBXGkfIGHKF/gV8XeOf4FNG26taT9Yz2KtXubeBLSLU8lnDnb1tDQQH9l8" +
                "CZFPBcviatSa0u2+NCNqbV/riM0okj1bA0IzsBMtFb1148jhtdzYUzbXlWW1ZLP/xVgmlSInacVMWVjZ" +
                "5MlddU77TUyzrpAVNaEn18WM+vEDvGyrZVv9cO4nR2MSRbADAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
