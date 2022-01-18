/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PoseWithCovariance : IDeserializable<PoseWithCovariance>, IMessage
    {
        // This represents a pose in free space with uncertainty.
        [DataMember (Name = "pose")] public Pose Pose;
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        [DataMember (Name = "covariance")] public double[/*36*/] Covariance;
    
        /// Constructor for empty message.
        public PoseWithCovariance()
        {
            Covariance = new double[36];
        }
        
        /// Explicit constructor.
        public PoseWithCovariance(in Pose Pose, double[] Covariance)
        {
            this.Pose = Pose;
            this.Covariance = Covariance;
        }
        
        /// Constructor with buffer.
        public PoseWithCovariance(ref ReadBuffer b)
        {
            b.Deserialize(out Pose);
            Covariance = b.DeserializeStructArray<double>(36);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PoseWithCovariance(ref b);
        
        public PoseWithCovariance RosDeserialize(ref ReadBuffer b) => new PoseWithCovariance(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in Pose);
            b.SerializeStructArray(Covariance, 36);
        }
        
        public void RosValidate()
        {
            if (Covariance is null) throw new System.NullReferenceException(nameof(Covariance));
            if (Covariance.Length != 36) throw new RosInvalidSizeForFixedArrayException(nameof(Covariance), Covariance.Length, 36);
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 344;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/PoseWithCovariance";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "c23e848cf1b7533a8d7c259073a97e6f";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71TyU7DQAy9z1dY6gWktBxAOVTiwAlxQGLpgUUImcRpB5GZ4JnQpF+PJ22WLuKEmpPj" +
                "5Y3fsz2C2UI7YCqYHBnvAKGwjkAbyJgIXIEJwVL7BZQmIfaoja8nSt2FrJCq1Age7HKc46flHgm9tgZs" +
                "Bn5BEFcxJPYHWaOAQI6edSV1M4lZ1l16gYw5eWIHpcAjZLqidIzVsMcmdSLVN4LPKXHUvDGoRaapxE+q" +
                "COoIVhGw3TyAH7b08AQBcc/9fNj90rhPVfZl0ccXr+fx24CMUpf//Knbx+spzMkKG67fczd3Z0FtYXR1" +
                "QN/9cUXSXh7c6Sau12xMOhR7AjJDGWaXoO5LFPlMg9vnHYugtNJshIw6sSbsmVvPte1fuITlDC1v0W0H" +
                "A1Vn1Z21Ok77vXQth+FJbem5c1ry993rnlnO5bj+ZtRaS6V+AcIN90zAAwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
