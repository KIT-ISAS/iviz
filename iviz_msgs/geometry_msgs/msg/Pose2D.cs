
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class Pose2D : IMessage
    {
        // Deprecated
        // Please use the full 3D pose.
        
        // In general our recommendation is to use a full 3D representation of everything and for 2D specific applications make the appropriate projections into the plane for their calculations but optimally will preserve the 3D information during processing.
        
        // If we have parallel copies of 2D datatypes every UI and other pipeline will end up needing to have dual interfaces to plot everything. And you will end up with not being able to use 3D tools for 2D use cases even if they're completely valid, as you'd have to reimplement it with different inputs and outputs. It's not particularly hard to plot the 2D pose or compute the yaw error for the Pose message and there are already tools and libraries that can do this for you.
        
        
        // This expresses a position and orientation on a 2D manifold.
        
        public double x;
        public double y;
        public double theta;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Pose2D";
    
        public IMessage Create() => new Pose2D();
    
        public int GetLength() => 24;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out x, ref ptr, end);
            BuiltIns.Deserialize(out y, ref ptr, end);
            BuiltIns.Deserialize(out theta, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(x, ref ptr, end);
            BuiltIns.Serialize(y, ref ptr, end);
            BuiltIns.Serialize(theta, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "938fa65709584ad8e77d238529be13b8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAAClVSvY7bMAzeA/QdCNyQpbihLboXyJLthvYBGIuO2cqSIFHJ+e37Uc4F7WCApvjz/fCF" +
                "TlKqTGwSDi/0FoWbUMdni9DcY6SvJyq5yesB7+dEV0lSOVLuldCY11VSYNOcSBtZHs38bK0+vkmyvSTP" +
                "JDepmy2arsQp0JwrfTlRKzLprBNxKVGnUd1o5T87EmRrLlWBkxD9lmkv0ISNXlAiJxnD8KeVJo5Tj48x" +
                "l26Ui+nKMW50V0AbqOptnw6cmtC77iBDr44OeyZpDeHOfaa70MLoKQwFokSaclFpzgoUoALbVvA/KNKv" +
                "8yCYsaFS0SJRAXFsh2TUCyWR4JvAYcwNHcKCktSZsdrzJWb7R7JX+oHWLff/5tzVFkoovMiQ9RLlwwlQ" +
                "s5xj+9DZcxM8HiBh2ewCbMeKbF5LFBModOOo4TNx803HsIPDwCrqNXDcSG1fG3SepY5MKt3azrmbx690" +
                "tmMbyCCZqTtSMX/hGp7k3AAA8xsjYHQYHS57euM7Sa3IPoylN69a4QpfcRTY5OIi8i9W4bA96Ppb1Evl" +
                "6gbZwgbasNavBXfq88ANxsLZn56Rdz8JF4Ydi45LGFww4Xm+SDnYlZPOOQb0zzGzff9G789oe0YAZ3z4" +
                "dPgLC2rvdmYDAAA=";
                
    }
}
