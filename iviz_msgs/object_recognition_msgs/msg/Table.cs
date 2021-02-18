/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/Table")]
    public sealed class Table : IDeserializable<Table>, IMessage
    {
        // Informs that a planar table has been detected at a given location
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // The pose gives you the transform that take you to the coordinate system
        // of the table, with the origin somewhere in the table plane and the 
        // z axis normal to the plane
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose { get; set; }
        // There is no guarantee that the table does NOT extend further than the
        // convex hull; this is just as far as we've observed it.
        // The origin of the table coordinate system is inside the convex hull
        // Set of points forming the convex hull of the table
        [DataMember (Name = "convex_hull")] public GeometryMsgs.Point[] ConvexHull { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Table()
        {
            ConvexHull = System.Array.Empty<GeometryMsgs.Point>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Table(in StdMsgs.Header Header, in GeometryMsgs.Pose Pose, GeometryMsgs.Point[] ConvexHull)
        {
            this.Header = Header;
            this.Pose = Pose;
            this.ConvexHull = ConvexHull;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Table(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Pose = new GeometryMsgs.Pose(ref b);
            ConvexHull = b.DeserializeStructArray<GeometryMsgs.Point>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Table(ref b);
        }
        
        Table IDeserializable<Table>.RosDeserialize(ref Buffer b)
        {
            return new Table(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Pose.RosSerialize(ref b);
            b.SerializeStructArray(ConvexHull, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (ConvexHull is null) throw new System.NullReferenceException(nameof(ConvexHull));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 60;
                size += Header.RosMessageLength;
                size += 24 * ConvexHull.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/Table";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "39efebc7d51e44bd2d72f2df6c7823a2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UwW7UMBC9R+o/jNRDC6KLBIhDEQekCugBKGpvCFWzyWxiSOzUdnY3/Xre2Ju0Sw9w" +
                "gK5WSuzMvDfveTyHdG5XzneBYsORmPqWLXuKvGyFGg60FLFUSZQySkUppjZr7LWu5GicPSgOio/ClXhq" +
                "0kM3DumqEepdkBQdaHQDKISiZxuUMRNG/in5m0ufS+d8ZSxHoTCGKJ1CuVVO1Zqe0cbEJq2dN7WxFFwn" +
                "m0a8EBZzXBIixLZKewpzS7w1gSzIuZ0IU9hBUQtQoh+vu1CH5xdatxY/S1F0TaV6YCiIIrv6Z77KQeXn" +
                "L1ck2yhgXQ0eH72GpbIUqHR2LVtqhrZ9gz0g4v9jCDA10Aq247GRozW0LYP4NQw3cTG5udN7342Hfimi" +
                "scFUsvNzZsxaLiUqQu+MjeCEF8bWv4fucTw0B6nfvu/irzN08fYf/4pPlx9OKcQqk+YG0/ojjpR9RaiI" +
                "K46sGqgxNbw+aWUtLZK462Fd+hrHXsIiOZjtrsWK57YdaQgIQhuUrusGa0o1MZpO9vKRCc9xL9hHUw4t" +
                "Dume5yvPnSg6/kFuBrGl0PnZqZoTpBwieh9MxpZeOKjT52dUDDDw5QtNKA6vNu4ES6m1VSby3FsoVra9" +
                "l6B1cjgFx9MsbgFsmCNgqQIdp71rLMMTAglKkN6VDR2j8osxNi7fizV7k5oGwCUcAOqRJh09uYdsE7Rl" +
                "6yb4jHjH8TewdsZVTSe4AlWr6sNQw0AE9t6t0aMVLcfcfK0RG6k1S89+LDQrUxaH79XjfFvSieDJIbjS" +
                "sA4knQZFiF7R02lcm+p/dePDIQGB78iLHhLKT+Mw366QptHKY06EnkuMLXSZble77ybF6njCtZ5yF1Sk" +
                "2zUHFF8HqPQ24d7FPZZAlDLdHPRCZAyWPDSn+qGF8yzZl1usWsfx9Svazm/j/Hb7OOXfWTdpmA8KHbTn" +
                "537xurq5811n5KL4g6LpbVMUvwDP7mNfUwcAAA==";
                
    }
}
