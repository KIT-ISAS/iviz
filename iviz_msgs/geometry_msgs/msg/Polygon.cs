using System.Runtime.Serialization;

namespace Iviz.Msgs.geometry_msgs
{
    public sealed class Polygon : IMessage
    {
        //A specification of a polygon where the first and last points are assumed to be connected
        public Point32[] points { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Polygon()
        {
            points = System.Array.Empty<Point32>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Polygon(Point32[] points)
        {
            this.points = points ?? throw new System.ArgumentNullException(nameof(points));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Polygon(Buffer b)
        {
            this.points = b.DeserializeArray<Point32>(0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new Polygon(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(this.points, 0);
        }
        
        public void Validate()
        {
            if (points is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 12 * points.Length;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "geometry_msgs/Polygon";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "cd60a26494a087f577976f0329fa120e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61RzUrEMBC+5ykG9qIgK+zeBA/iQTwIgt5EJG2m7WCaCZlZ1/r0Ttru4gPY0zSZ7zeb" +
                "O5CMLXXUeiVOwB14yByn3n6OAxYEHRA6KqLgU4DobchMSQW83XqRw4gBlKFBaDklbBWDe64r+93b+7rs" +
                "3O0/f+7p5eEGeuQRtUwfo/Ryvaq6DbwOJNWOekoyZ8gs9DejbQIl6AqileBbvDiSDrDfQUMWzrZysWrE" +
                "IJdbY3y0dQE74tECL5EPgjBrLl19YakyQk1E4xZFHyrRamsLYDwncytTCkvzdmKEufDIWsGKhTMW31Ak" +
                "nWboCTmiiO+xQgIK9Wkxo/4T4ZAh2vWSqLpKIKZBqTd05DXY+n4KnFq8skesTdSSWm+J5oJmz/eRD6Fq" +
                "uy6ytwjwfZ6m8/TjfgGp0g7/SAIAAA==";
                
    }
}
