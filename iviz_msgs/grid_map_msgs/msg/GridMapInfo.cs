namespace Iviz.Msgs.grid_map_msgs
{
    public sealed class GridMapInfo : IMessage
    {
        // Header (time and frame)
        public std_msgs.Header header;
        
        // Resolution of the grid [m/cell].
        public double resolution;
        
        // Length in x-direction [m].
        public double length_x;
        
        // Length in y-direction [m].
        public double length_y;
        
        // Pose of the grid map center in the frame defined in `header` [m].
        public geometry_msgs.Pose pose;
    
        /// <summary> Constructor for empty message. </summary>
        public GridMapInfo()
        {
            header = new std_msgs.Header();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out resolution, ref ptr, end);
            BuiltIns.Deserialize(out length_x, ref ptr, end);
            BuiltIns.Deserialize(out length_y, ref ptr, end);
            pose.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(resolution, ref ptr, end);
            BuiltIns.Serialize(length_x, ref ptr, end);
            BuiltIns.Serialize(length_y, ref ptr, end);
            pose.Serialize(ref ptr, end);
        }
    
        public int GetLength()
        {
            int size = 80;
            size += header.GetLength();
            return size;
        }
    
        public IMessage Create() => new GridMapInfo();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "grid_map_msgs/GridMapInfo";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "43ee5430e1c253682111cb6bedac0ef9";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
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
