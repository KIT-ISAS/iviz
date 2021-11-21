/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/Polygon")]
    public sealed class Polygon : IDeserializable<Polygon>, IMessage
    {
        //A specification of a polygon where the first and last points are assumed to be connected
        [DataMember (Name = "points")] public Point32[] Points;
    
        /// <summary> Constructor for empty message. </summary>
        public Polygon()
        {
            Points = System.Array.Empty<Point32>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Polygon(Point32[] Points)
        {
            this.Points = Points;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Polygon(ref Buffer b)
        {
            Points = b.DeserializeStructArray<Point32>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Polygon(ref b);
        }
        
        Polygon IDeserializable<Polygon>.RosDeserialize(ref Buffer b)
        {
            return new Polygon(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Points, 0);
        }
        
        public void RosValidate()
        {
            if (Points is null) throw new System.NullReferenceException(nameof(Points));
        }
    
        public int RosMessageLength => 4 + 12 * Points.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Polygon";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "cd60a26494a087f577976f0329fa120e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1RzUrEMBC+F/YdBvaiICvs3gQP4kE8CILeRCRNp+1gmgmZqWt9eidtd/EB7GmafPP9" +
                "ZXsHktBTS94pcQRuwUHiMHX2c+wxI2iP0FIWBRcbCM6GxBRVwNmtExkHbEAZagTPMaJXbKrnAjns395X" +
                "cLWpbv/521RPLw830CEPqHn6GKST61V3U23htScpjtRRlDlGYqG/MQ0KFKHNiNaD83hxJO3hsIeaLJ+h" +
                "UrZ2xFYud8b4aHABO+LBMi+pR0GYRZe6vjAXGaE6oHGLomsK0eprB2A8J3MrU2yW8u3ECFPmgbUsK2ZO" +
                "mF1NgXSaV0+bA4q4zkACDQp1cTGj7hNhTBDseklUXEUQ06DY2XbgNdj6hAocPV7ZO5YmSkneWaK5oNnz" +
                "feCxKdpVG9hZBPg+T9N5+tlUv1GMfyFMAgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
