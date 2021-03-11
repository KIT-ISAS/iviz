/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/PointStamped")]
    public sealed class PointStamped : IDeserializable<PointStamped>, IMessage
    {
        // This represents a Point with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "point")] public Point Point { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PointStamped()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public PointStamped(in StdMsgs.Header Header, in Point Point)
        {
            this.Header = Header;
            this.Point = Point;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PointStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Point = new Point(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PointStamped(ref b);
        }
        
        PointStamped IDeserializable<PointStamped>.RosDeserialize(ref Buffer b)
        {
            return new PointStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Point.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength
        {
            get {
                int size = 24;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/PointStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c63aecb41bfdfd6b7e1fac37c7cbe7bf";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVTTWvcMBC9C/Y/DOwhSWFTaEsPC72VfhwKgeS+zEpje0CWXM14E/fXdyQ3bqGXHlJj" +
                "/CXNm/fePO/hYWCBQlMhoaQCCHeZk8Ij62DfOyqUPIHPuQROqARdwZEAUwDlkURxnNwXwkAFhnZzK8JU" +
                "r27nPrzwsXPf7j8fQTScRunl9dp75/Zwr8YKS4CRFAMqQpeNFPcDlUOkC0VodClAW9VlIrl1v0yws6dE" +
                "BWNcYBbbpNl0j+Oc2Ffhm9zneqvkZI5NWJT9HLH85VNFt1Po+9x8/PrxaHuSkJ+VjdBiCL4QCqfeFsHN" +
                "ZtrbN7XA7R8e88FeqTdrt+agA2olS091aJUnytF6vFrF3Rq2uUPWJQhct28ne5UbsCZGgabsB7g25neL" +
                "DjkZIMEFC+M5UgX25oChXtWiq5s/kCvtIyRM+Rl+Rfzd419g04ZbNR0Gm1ms6mXuzUDbOJV84WBbz0sD" +
                "8ZEtmhD5XLAsrlatLd3+U8ui1vG1idgdRbJnG0BoGXaipaK3aZw4/L9A9pQtd2VZU9n+gRrKli1zS5GT" +
                "ND1TFlY2h3JXw9N+FrOtK2S6JvTkuphR37+Dp+1p2Z5+mIKfcMPfsbcDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
