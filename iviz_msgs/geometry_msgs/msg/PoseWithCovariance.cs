
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class PoseWithCovariance : IMessage
    {
        // This represents a pose in free space with uncertainty.
        
        public Pose pose;
        
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        public double[/*36*/] covariance;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/PoseWithCovariance";
    
        public IMessage Create() => new PoseWithCovariance();
    
        public int GetLength() => 344;
    
        /// <summary> Constructor for empty message. </summary>
        public PoseWithCovariance()
        {
            covariance = new double[36];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            pose.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out covariance, ref ptr, end, 36);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            pose.Serialize(ref ptr, end);
            BuiltIns.Serialize(covariance, ref ptr, end, 36);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "c23e848cf1b7533a8d7c259073a97e6f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr2TPU/DQAyG95P6Hyx1ASktAyhDJQYmxIDERwc+hJCbOO0hchd8F5r01+NL2ySlFRNq" +
                "Jsf22X5e3w1hutAOmAomR8Y7QCisI9AGMiYCV2BCsNR+AaVJiD1q4+uxUnchK6QqNYQHuxzl+GG5q4Re" +
                "WwM2A78giKsYEvuNrFGKQI6edSXnphKzrNv0Ahlz8sQOSimPkOmK0hFW/Rmb1LGcvpH6nBJHTY/eWWSa" +
                "SPykiqCOYBUB200DnNnSwxOEinvu58Pul8Z9qrJPiz6+eD2P33owaqAu//kbqNvH6wnMyQoP1++5m7uz" +
                "oPdAoK4OSLy/sUgmzIM73cT1Gsikfb3HIGuUfbYJ6r5EUdA0dbu84zHKMAGyuZSJlfbauPV2twiCE65o" +
                "mHqHWG3WA1Vr1a21OhZBp1+L0X9bO6r+emPy99Wpn1nO5ZX9DbW1loL3A5Uc8YvKAwAA";
                
    }
}
