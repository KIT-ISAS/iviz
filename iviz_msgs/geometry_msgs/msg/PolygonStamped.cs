/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/PolygonStamped")]
    public sealed class PolygonStamped : IDeserializable<PolygonStamped>, IMessage
    {
        // This represents a Polygon with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "polygon")] public Polygon Polygon { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PolygonStamped()
        {
            Header = new StdMsgs.Header();
            Polygon = new Polygon();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PolygonStamped(StdMsgs.Header Header, Polygon Polygon)
        {
            this.Header = Header;
            this.Polygon = Polygon;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PolygonStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Polygon = new Polygon(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PolygonStamped(ref b);
        }
        
        PolygonStamped IDeserializable<PolygonStamped>.RosDeserialize(ref Buffer b)
        {
            return new PolygonStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Polygon.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Polygon is null) throw new System.NullReferenceException(nameof(Polygon));
            Polygon.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Polygon.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/PolygonStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c6be8f7dc3bee7fe9e8d296070f53340";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTW/UMBC9R8p/GKmHtogtEtxW4oBAQA9IldobQtWsPUksHDvYzrbh1/PG2d1W4sIB" +
                "WGWVD3vevJl5z2d0N7hMSaYkWULJxHQT/dLHQA+uDFjpJEkwQibGZF3gItQlHoU4WCpulFx4nNrms7CV" +
                "REO9tc0RZVrvbdM2b//yr22+3H7aUi72fsx9frUyaJszui0gx8nSKIUtF6YugprrB0kbL3vxVFmLpbpa" +
                "lknylUbWduDqJUhi7xeaM3aViPrHcQ7OaANOZR8BNNQFNG/iVJyZPaffGlbx9Z/lx1xbev1hi10hi5mL" +
                "A6kFGCYJZxd6LGLz7EJ581ojEHj3EDd4lx5tPjGgMnBRxvKoM1SynLea5sVa4xXg0SRBIpvpon67x2u+" +
                "JOQBC5miGegC9G+WMmBkZRDac3K886LIBn0A7LkGnV8+h1bqWwoc4hF/hXxK8ie44QlYy9oMGJ7XFuS5" +
                "Rx+xc0px7yz27paKYryDWMm7XeK0tI2GrUkB8rGqs+gg62xw55yjcZiErapum1ySJqhzuXf2H6qzlwgR" +
                "pmWV6MEVYPmO8iTGdSooh+bETtVztB50qiVAOS7lUq3mGQ9ThABQD1ZR0zyu0typO0MQgwLVeVU0X78d" +
                "dv/H4mrik43AqbALuRYyxeyeF4qt6pguCSY3sZGLet5A7DuHCrELcjYuI+SyOue6qhzf4EOxa+HwJo4r" +
                "haod28MYyJNdFVjIBQeCIh2IXVG134nfAQsnRSWGL4CE1MYIy6rRUpxwCOycd2VZg0+xcF/mvsrYSnZ9" +
                "WAkV/i40T+SxvJalzALsF3AU9Bru46G8wygLRVjmJeap/ajqZpRV21SJv/dxtjV923Q+sh4Ij0+PkP/x" +
                "8Wfb/AJ8cMgS0QUAAA==";
                
    }
}
