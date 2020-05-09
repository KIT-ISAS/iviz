using System.Runtime.Serialization;

namespace Iviz.Msgs.grid_map_msgs
{
    public sealed class GridMapInfo : IMessage
    {
        // Header (time and frame)
        public std_msgs.Header header { get; set; }
        
        // Resolution of the grid [m/cell].
        public double resolution { get; set; }
        
        // Length in x-direction [m].
        public double length_x { get; set; }
        
        // Length in y-direction [m].
        public double length_y { get; set; }
        
        // Pose of the grid map center in the frame defined in `header` [m].
        public geometry_msgs.Pose pose { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GridMapInfo()
        {
            header = new std_msgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GridMapInfo(std_msgs.Header header, double resolution, double length_x, double length_y, geometry_msgs.Pose pose)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.resolution = resolution;
            this.length_x = length_x;
            this.length_y = length_y;
            this.pose = pose;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GridMapInfo(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.resolution = BuiltIns.DeserializeStruct<double>(b);
            this.length_x = BuiltIns.DeserializeStruct<double>(b);
            this.length_y = BuiltIns.DeserializeStruct<double>(b);
            this.pose = new geometry_msgs.Pose(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GridMapInfo(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            BuiltIns.Serialize(this.resolution, b);
            BuiltIns.Serialize(this.length_x, b);
            BuiltIns.Serialize(this.length_y, b);
            this.pose.Serialize(b);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 80;
                size += header.RosMessageLength;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "grid_map_msgs/GridMapInfo";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "43ee5430e1c253682111cb6bedac0ef9";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UTWvcMBC961cM7CFJ6W6gLT0EeiiUfkALaZNbKJuJNGsLZMmR5GTdX98nOevN0kN6" +
                "aLOYtS29efPezMgL+ixsJNJxtp0Qe0ObyJ2cqIf1tt6UWtAPScEN2QZPYUO5FWqiNXTVnWpx7udKbVzg" +
                "/PYNxRlYwr6Kb3JL1tN2aWwUXRmuukcBrkLW20P4+AR8LPDzkORATsc9afEZ0sFRlqsfMrKxXkxZvJ48" +
                "XU+sjYROchzXXWrSaeXr8afe/eOf+nbx6YxSNlOiqbxwcJFRdI5QLpkNZ6ZNQNlt00pcOrkThyDuemiv" +
                "u3nsJa0QeNnaRLga8RLZuZGGBFAOpEPXDd5qzkKlrQfxiEQNmHqO2erBcQQ+RGN9gddiFXZcSW4H8Vro" +
                "y4czYHwSja5C0AgGHYWT9Q02SQ3W59evSoBaXN6HJV6lQQfm5GgE5yJWtj3Go+jkdIYcLyZzK3CjOIIs" +
                "JtFxXVvjNZ0QkkCC9EG3dAzl52Nuw9TZO46Wb5wUYo0KgPWoBB2dPGL2ldqzDzv6iXGf429o/cxbPC1b" +
                "9MwV92loUEAA+xjurAH0Zqwk2llMITl7EzmOqp6umlItPtaBzKV9tSO4c0pBWzTA0L3NrUo5FvbajbU1" +
                "6j9N45+zD4PvcYBLkyCfd6e9nIgyNpsosNGzlpdlysqyedi3FVs+ICHaXeyK1HnANMwA9X2Ay+gr7x73" +
                "XAYhZXdyMAuZrU+1W7N+eMHRqJIP7M4fn+38NM5Pv55H/r50Ow9zozBBB/U8FF/ebvd1x/elW6knHO2e" +
                "7pX6DcJaO68kBgAA";
                
    }
}
