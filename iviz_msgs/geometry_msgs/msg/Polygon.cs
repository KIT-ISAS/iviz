
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class Polygon : IMessage
    {
        //A specification of a polygon where the first and last points are assumed to be connected
        public Point32[] points;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Polygon";
    
        public IMessage Create() => new Polygon();
    
        public int GetLength()
        {
            int size = 4;
            size += 12 * points.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public Polygon()
        {
            points = new Point32[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeArray(out points, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeArray(points, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "cd60a26494a087f577976f0329fa120e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACq1RzUrEMBC+F/YdBvaiICvs3gQP4kE8CILeRCRNp+1gmgmZqWt9eidtd/EB7GmafPP9" +
                "ZXsHktBTS94pcQRuwUHiMHX2c+wxI2iP0FIWBRcbCM6GxBRVwNmtExkHbEAZagTPMaJXbKrnAjns395X" +
                "cLWpbv/521RPLw830CEPqHn6GKST61V3U23htScpjtRRlDlGYqG/MQ0KFKHNiNaD83hxJO3hsIeaLJ+h" +
                "UrZ2xFYud8b4aHABO+LBMi+pR0GYRZe6vjAXGaE6oHGLomsK0eprB2A8J3MrU2yW8u3ECFPmgbUsK2ZO" +
                "mF1NgXSaV0+bA4q4zkACDQp1cTGj7hNhTBDseklUXEUQ06DY2XbgNdj6hAocPV7ZO5YmSkneWaK5oNnz" +
                "feCxKdpVG9hZBPg+T9N5+tlUv1GMfyFMAgAA";
                
    }
}
