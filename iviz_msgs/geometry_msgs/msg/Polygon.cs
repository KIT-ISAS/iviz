/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Polygon : IDeserializable<Polygon>, IMessage
    {
        //A specification of a polygon where the first and last points are assumed to be connected
        [DataMember (Name = "points")] public Point32[] Points;
    
        /// Constructor for empty message.
        public Polygon()
        {
            Points = System.Array.Empty<Point32>();
        }
        
        /// Explicit constructor.
        public Polygon(Point32[] Points)
        {
            this.Points = Points;
        }
        
        /// Constructor with buffer.
        internal Polygon(ref Buffer b)
        {
            Points = b.DeserializeStructArray<Point32>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Polygon(ref b);
        
        Polygon IDeserializable<Polygon>.RosDeserialize(ref Buffer b) => new Polygon(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Points);
        }
        
        public void RosValidate()
        {
            if (Points is null) throw new System.NullReferenceException(nameof(Points));
        }
    
        public int RosMessageLength => 4 + 12 * Points.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/Polygon";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "cd60a26494a087f577976f0329fa120e";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1RzUrEMBC+5ykG9qIgK+zeBA/iQTwIgt5EJG2m7WCaCZlZ1/r0Ttru4gPY0zT55vvL" +
                "5g4kY0sdtV6JE3AHHjLHqbef44AFQQeEjooo+BQgehsyU1IBb7de5DBiAGVoEFpOCVvF4J4rZL97e1/B" +
                "zt3+8+eeXh5uoEceUcv0MUov16uq28DrQFLtqKckc4bMQn8zGhIoQVcQrQTf4sWRdID9DhqycIbKxaoR" +
                "W7ncGuOjwQXsiEcLvEQ+CMKsuXT1haXKCDURjVsUfahEq60tgPGczK1MKSzN24kR5sIja11WLJyx+IYi" +
                "6TSvnjZHFPG9gQQCCvVpMaP+E+GQIdr1kqi6SiCmQam37chrsPX9FDi1eGWPWJuoJbXeEs0FzZ7vIx9C" +
                "1XZdZG8R4Ps8Tefpx/0CqdIO/0gCAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
