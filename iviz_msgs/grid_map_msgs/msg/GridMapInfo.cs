/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GridMapMsgs
{
    [Preserve, DataContract (Name = "grid_map_msgs/GridMapInfo")]
    public sealed class GridMapInfo : IDeserializable<GridMapInfo>, IMessage
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
        public GridMapInfo(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Resolution = b.Deserialize<double>();
            LengthX = b.Deserialize<double>();
            LengthY = b.Deserialize<double>();
            Pose = new GeometryMsgs.Pose(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GridMapInfo(ref b);
        }
        
        GridMapInfo IDeserializable<GridMapInfo>.RosDeserialize(ref Buffer b)
        {
            return new GridMapInfo(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Resolution);
            b.Serialize(LengthX);
            b.Serialize(LengthY);
            Pose.RosSerialize(ref b);
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
                "H4sIAAAAAAAACr1UTW/bMAy9G/B/INBD22FpgW3YIcAOA4Z9ABvQrb0VRapKjC1AllxJbuP9+j3KTdxs" +
                "h/WwNUhiWSIf+fhIHdBnVoYjHWXbMSlvaB1Vx8d19XDQlkdd1dUB/eAU3JBt8BTWlFumJlpDl92pZueu" +
                "Tupq7YLKb99Q3FlOnl/ZN7kl62mzMDayLiCX3WMfV2xWm989xr95jJPHWUi8l1enetLsM1gARrYLNzK8" +
                "tp6NbF5P9K4fgBsOHec4rrrUpNMC2OOvrt79409dfTv/tKSUzRRqKrawOM8QQUVkz1kZlRWtA1SwTctx" +
                "4fiOHbxU1yP/cprHnhNSP6CL1ibCt2HPUTk30pBglQPp0HWDt1plJtF5D0BcUQlFvYrZ6sGpCIcQjfVi" +
                "X0pW8OWX+HZgr5m+fFjCyifWUBlJjcDQkVWyvsEhjAfr8+tX4gHHi/uwwDs30GKXASRRWTLmTY+GkWRV" +
                "WkqYFxPHE8CjSIxAJtFR2VvhNR0T4iAL7oNu6Qjpn425DZPIdypadeNYkDXqANhDcTo8fgwtqS/JKx+2" +
                "+BPkHOQpuH4GFlqLFuI5KUEaGtQRln0Md9bA9mYsKNpZtCQ5exNVROeWuStBAfKx9GcWIYs2eKqUgrZQ" +
                "wtC9zW1dpRwlQNFlZY30/n/qzj+nQXi+x3CLXGChtleBDIk00ToyyPRK80tpOtk2D+e22MoFE6Ld+p6g" +
                "T84CGmNnUVffB5CNviDPls9IE+nsxgmdkZX1qUi3YwFGGJeS9x7p+WbazEtovF3+fDYWcxF3VHaqoaf2" +
                "SrvPQd5uZwlw+XSY/icw2y7vxfoXDwKv0VgGAAA=";
                
    }
}
