using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [DataContract (Name = "grid_map_msgs/GridMapInfo")]
    public sealed class GridMapInfo : IMessage
    {
        // Header (time and frame)
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Resolution of the grid [m/cell].
        [DataMember (Name = "resolution")] public double Resolution { get; set; }
        // Length in x-direction [m].
        [DataMember (Name = "length_x")] public double LengthX { get; set; }
        // Length in y-direction [m].
        [DataMember (Name = "length_y")] public double LengthY { get; set; }
        // Pose of the grid map center in the frame defined in `header` [m].
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GridMapInfo()
        {
            Header = new StdMsgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public GridMapInfo(StdMsgs.Header Header, double Resolution, double LengthX, double LengthY, in GeometryMsgs.Pose Pose)
        {
            this.Header = Header;
            this.Resolution = Resolution;
            this.LengthX = LengthX;
            this.LengthY = LengthY;
            this.Pose = Pose;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GridMapInfo(Buffer b)
        {
            Header = new StdMsgs.Header(b);
            Resolution = b.Deserialize<double>();
            LengthX = b.Deserialize<double>();
            LengthY = b.Deserialize<double>();
            Pose = new GeometryMsgs.Pose(b);
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new GridMapInfo(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            Header.RosSerialize(b);
            b.Serialize(Resolution);
            b.Serialize(LengthX);
            b.Serialize(LengthY);
            Pose.RosSerialize(b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 80;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "grid_map_msgs/GridMapInfo";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "43ee5430e1c253682111cb6bedac0ef9";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
