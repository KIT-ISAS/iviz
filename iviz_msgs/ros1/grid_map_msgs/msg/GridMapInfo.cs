/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [DataContract]
    public sealed class GridMapInfo : IDeserializableRos1<GridMapInfo>, IMessageRos1
    {
        // Header (time and frame)
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Resolution of the grid [m/cell].
        [DataMember (Name = "resolution")] public double Resolution;
        // Length in x-direction [m].
        [DataMember (Name = "length_x")] public double LengthX;
        // Length in y-direction [m].
        [DataMember (Name = "length_y")] public double LengthY;
        // Pose of the grid map center in the frame defined in `header` [m].
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
    
        /// Constructor for empty message.
        public GridMapInfo()
        {
        }
        
        /// Constructor with buffer.
        public GridMapInfo(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Resolution);
            b.Deserialize(out LengthX);
            b.Deserialize(out LengthY);
            b.Deserialize(out Pose);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new GridMapInfo(ref b);
        
        public GridMapInfo RosDeserialize(ref ReadBuffer b) => new GridMapInfo(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Resolution);
            b.Serialize(LengthX);
            b.Serialize(LengthY);
            b.Serialize(in Pose);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 80 + Header.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "grid_map_msgs/GridMapInfo";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "43ee5430e1c253682111cb6bedac0ef9";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
