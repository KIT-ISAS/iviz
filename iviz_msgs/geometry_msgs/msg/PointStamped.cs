
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class PointStamped : IMessage
    {
        // This represents a Point with reference coordinate frame and timestamp
        public std_msgs.Header header;
        public Point point;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/PointStamped";
    
        public IMessage Create() => new PointStamped();
    
        public int GetLength()
        {
            int size = 24;
            size += header.GetLength();
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public PointStamped()
        {
            header = new std_msgs.Header();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            point.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            point.Serialize(ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "c63aecb41bfdfd6b7e1fac37c7cbe7bf";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACrVTTWvcMBC9C/Y/DOwhSWFTaEsPC72VfhwKgeS+zEpje0CWXM14E/fXdyQ3bqGXHlJj" +
                "/CXNm/fePO/hYWCBQlMhoaQCCHeZk8Ij62DfOyqUPIHPuQROqARdwZEAUwDlkURxnNwXwkAFhnZzK8JU" +
                "r27nPrzwsXPf7j8fQTScRunl9dp75/Zwr8YKS4CRFAMqQpeNFPcDlUOkC0VodClAW9VlIrl1v0yws6dE" +
                "BWNcYBbbpNl0j+Oc2Ffhm9zneqvkZI5NWJT9HLH85VNFt1Po+9x8/PrxaHuSkJ+VjdBiCL4QCqfeFsHN" +
                "ZtrbN7XA7R8e88FeqTdrt+agA2olS091aJUnytF6vFrF3Rq2uUPWJQhct28ne5UbsCZGgabsB7g25neL" +
                "DjkZIMEFC+M5UgX25oChXtWiq5s/kCvtIyRM+Rl+Rfzd419g04ZbNR0Gm1ms6mXuzUDbOJV84WBbz0sD" +
                "8ZEtmhD5XLAsrlatLd3+U8ui1vG1idgdRbJnG0BoGXaipaK3aZw4/L9A9pQtd2VZU9n+gRrKli1zS5GT" +
                "ND1TFlY2h3JXw9N+FrOtK2S6JvTkuphR37+Dp+1p2Z5+mIKfcMPfsbcDAAA=";
                
    }
}
