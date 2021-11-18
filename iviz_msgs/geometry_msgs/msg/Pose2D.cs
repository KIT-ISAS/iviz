/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/Pose2D")]
    public sealed class Pose2D : IDeserializable<Pose2D>, IMessage
    {
        // Deprecated
        // Please use the full 3D pose.
        // In general our recommendation is to use a full 3D representation of everything and for 2D specific applications make the appropriate projections into the plane for their calculations but optimally will preserve the 3D information during processing.
        // If we have parallel copies of 2D datatypes every UI and other pipeline will end up needing to have dual interfaces to plot everything. And you will end up with not being able to use 3D tools for 2D use cases even if they're completely valid, as you'd have to reimplement it with different inputs and outputs. It's not particularly hard to plot the 2D pose or compute the yaw error for the Pose message and there are already tools and libraries that can do this for you.
        // This expresses a position and orientation on a 2D manifold.
        [DataMember (Name = "x")] public double X;
        [DataMember (Name = "y")] public double Y;
        [DataMember (Name = "theta")] public double Theta;
    
        /// <summary> Constructor for empty message. </summary>
        public Pose2D()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Pose2D(double X, double Y, double Theta)
        {
            this.X = X;
            this.Y = Y;
            this.Theta = Theta;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Pose2D(ref Buffer b)
        {
            X = b.Deserialize<double>();
            Y = b.Deserialize<double>();
            Theta = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Pose2D(ref b);
        }
        
        Pose2D IDeserializable<Pose2D>.RosDeserialize(ref Buffer b)
        {
            return new Pose2D(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(X);
            b.Serialize(Y);
            b.Serialize(Theta);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 24;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Pose2D";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "938fa65709584ad8e77d238529be13b8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAClVSvY7bMAzeA/QdCNyQpbihLboXyJLthvYBGIuO2cqSIFHJ+e37Uc4F7WCApvjz/fCF" +
                "TlKqTGwSDi/0FoWbUMdni9DcY6SvJyq5yesB7+dEV0lSOVLuldCY11VSYNOcSBtZHs38bK0+vkmyvSTP" +
                "JDepmy2arsQp0JwrfTlRKzLprBNxKVGnUd1o5T87EmRrLlWBkxD9lmkv0ISNXlAiJxnD8KeVJo5Tj48x" +
                "l26Ui+nKMW50V0AbqOptnw6cmtC77iBDr44OeyZpDeHOfaa70MLoKQwFokSaclFpzgoUoALbVvA/KNKv" +
                "8yCYsaFS0SJRAXFsh2TUCyWR4JvAYcwNHcKCktSZsdrzJWb7R7JX+oHWLff/5tzVFkoovMiQ9RLlwwlQ" +
                "s5xj+9DZcxM8HiBh2ewCbMeKbF5LFBModOOo4TNx803HsIPDwCrqNXDcSG1fG3SepY5MKt3azrmbx690" +
                "tmMbyCCZqTtSMX/hGp7k3AAA8xsjYHQYHS57euM7Sa3IPoylN69a4QpfcRTY5OIi8i9W4bA96Ppb1Evl" +
                "6gbZwgbasNavBXfq88ANxsLZn56Rdz8JF4Ydi45LGFww4Xm+SDnYlZPOOQb0zzGzff9G789oe0YAZ3z4" +
                "dPgLC2rvdmYDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
