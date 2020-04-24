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
    
        public int GetLength() => 344;
    
        public IMessage Create() => new PoseWithCovariance();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "geometry_msgs/PoseWithCovariance";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "c23e848cf1b7533a8d7c259073a97e6f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
                "H4sIAAAAAAAAE71TyU7DQAy9z1dY6gWktBxAOVTiwAlxQGLpgUUImcRpB5GZ4JnQpF+PJ22WLuKEmpPj" +
                "5Y3fsz2C2UI7YCqYHBnvAKGwjkAbyJgIXIEJwVL7BZQmIfaoja8nSt2FrJCq1Age7HKc46flHgm9tgZs" +
                "Bn5BEFcxJPYHWaOAQI6edSV1M4lZ1l16gYw5eWIHpcAjZLqidIzVsMcmdSLVN4LPKXHUvDGoRaapxE+q" +
                "COoIVhGw3TyAH7b08AQBcc/9fNj90rhPVfZl0ccXr+fx24CMUpf//Knbx+spzMkKG67fczd3Z0FtYXR1" +
                "QN/9cUXSXh7c6Sau12xMOhR7AjJDGWaXoO5LFPlMg9vnHYugtNJshIw6sSbsmVvPte1fuITlDC1v0W0H" +
                "A1Vn1Z21Ok77vXQth+FJbem5c1ry993rnlnO5bj+ZtRaS6V+AcIN90zAAwAA";
                
    }
}
