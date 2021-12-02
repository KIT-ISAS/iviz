/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class GridMapInfo : IDeserializable<GridMapInfo>, IMessage
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
        
        /// Explicit constructor.
        public GridMapInfo(in StdMsgs.Header Header, double Resolution, double LengthX, double LengthY, in GeometryMsgs.Pose Pose)
        {
            this.Header = Header;
            this.Resolution = Resolution;
            this.LengthX = LengthX;
            this.LengthY = LengthY;
            this.Pose = Pose;
        }
        
        /// Constructor with buffer.
        internal GridMapInfo(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Resolution = b.Deserialize<double>();
            LengthX = b.Deserialize<double>();
            LengthY = b.Deserialize<double>();
            b.Deserialize(out Pose);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GridMapInfo(ref b);
        
        GridMapInfo IDeserializable<GridMapInfo>.RosDeserialize(ref Buffer b) => new GridMapInfo(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Resolution);
            b.Serialize(LengthX);
            b.Serialize(LengthY);
            b.Serialize(ref Pose);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 80 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "grid_map_msgs/GridMapInfo";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "43ee5430e1c253682111cb6bedac0ef9";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTYvbMBC961cM5LBJabLQlh4CPRRKP6CFtLu3pWS10sQWyJJXkpO4v75P8trZ0MP2" +
                "0G4wsS29efPezMgz+sxSc6B5Mg2TdJp2QTa8EA/rdbkJMaMfHL3tkvGO/I5SzVQFo+mmuVRs7c+V2Fkv" +
                "09s3FCZgDvvKrko1GUfHpTaBVWG4aR4F2ALZHs/h/RPwPsM3PvKZnEa2pNglSAdHXi5+SPPOONZ58Xbw" +
                "dDuwVuwbTqHfNrGKl4WvxZ94949/4tvVpzXFpIdEQ3nh4Cqh6DJAOSepZZK08yi7qWoOS8t7tgiSTQvt" +
                "ZTf1LccVAq9rEwlXxY6DtLanLgKUPCnfNJ0zSiam3NazeESiBpJaGZJRnZUBeB+0cRleipXZcUW+79gp" +
                "pi8f1sC4yApdhaAeDCqwjMZV2CTRGZdev8oBYnZ98Eu8coUOTMnRCJmyWD62GI+sU8Y1crwYzK3AjeIw" +
                "suhI87K2xWtcEJJAArde1TSH8k2faj90di+DkXeWM7FCBcB6kYMuFo+Ys+w1Oen8SD8wnnL8Da2beLOn" +
                "ZY2e2ew+dhUKCGAb/N5oQO/6QqKswRSSNXdBhl6U01VSitnHMpApt690BHcZo1cGDdB0MKkWMYXMXrqx" +
                "NVr8p2n8c/Zh8D0OcG4S5MvxtOcTkcdmFxg2Wqn4ZZ6yvKwf9k3B5g+ID2aMXZHYeEzDBBDfO7gMrvCe" +
                "cM9lEFLGk4NZSNK4WLo16YcXHI0i+cyuGD8+x+mpn55+PY/8U+lGD1OjMEFn9TwXn9/uT3XH96VZiScc" +
                "jU8HIX4Dwlo7ryQGAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
