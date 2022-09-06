/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class PoseWithCovariance : IDeserializable<PoseWithCovariance>, IHasSerializer<PoseWithCovariance>, IMessage
    {
        // This represents a pose in free space with uncertainty.
        [DataMember (Name = "pose")] public Pose Pose;
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        [DataMember (Name = "covariance")] public double[/*36*/] Covariance;
    
        public PoseWithCovariance()
        {
            Covariance = new double[36];
        }
        
        public PoseWithCovariance(in Pose Pose, double[] Covariance)
        {
            this.Pose = Pose;
            this.Covariance = Covariance;
        }
        
        public PoseWithCovariance(ref ReadBuffer b)
        {
            b.Deserialize(out Pose);
            unsafe
            {
                var array = new double[36];
                b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), 36 * 8);
                Covariance = array;
            }
        }
        
        public PoseWithCovariance(ref ReadBuffer2 b)
        {
            b.Align8();
            b.Deserialize(out Pose);
            unsafe
            {
                var array = new double[36];
                b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), 36 * 8);
                Covariance = array;
            }
        }
        
        public PoseWithCovariance RosDeserialize(ref ReadBuffer b) => new PoseWithCovariance(ref b);
        
        public PoseWithCovariance RosDeserialize(ref ReadBuffer2 b) => new PoseWithCovariance(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in Pose);
            b.SerializeStructArray(Covariance, 36);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(in Pose);
            b.SerializeStructArray(Covariance, 36);
        }
        
        public void RosValidate()
        {
            if (Covariance is null) BuiltIns.ThrowNullReference();
            if (Covariance.Length != 36) BuiltIns.ThrowInvalidSizeForFixedArray(Covariance.Length, 36);
        }
    
        public const int RosFixedMessageLength = 344;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 344;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "geometry_msgs/PoseWithCovariance";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "c23e848cf1b7533a8d7c259073a97e6f";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71TyU7DQAy9z1dY6gWktBxAOVTiwAlxQGLpgUUImcRpB5GZ4JnQpF+PJ22WLuKEmpPj" +
                "5Y3fsz2C2UI7YCqYHBnvAKGwjkAbyJgIXIEJwVL7BZQmIfaoja8nSt2FrJCq1Age7HKc46flHgm9tgZs" +
                "Bn5BEFcxJPYHWaOAQI6edSV1M4lZ1l16gYw5eWIHpcAjZLqidIzVsMcmdSLVN4LPKXHUvDGoRaapxE+q" +
                "COoIVhGw3TyAH7b08AQBcc/9fNj90rhPVfZl0ccXr+fx24CMUpf//Knbx+spzMkKG67fczd3Z0FtYXR1" +
                "QN/9cUXSXh7c6Sau12xMOhR7AjJDGWaXoO5LFPlMg9vnHYugtNJshIw6sSbsmVvPte1fuITlDC1v0W0H" +
                "A1Vn1Z21Ok77vXQth+FJbem5c1ry993rnlnO5bj+ZtRaS6V+AcIN90zAAwAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<PoseWithCovariance> CreateSerializer() => new Serializer();
        public Deserializer<PoseWithCovariance> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<PoseWithCovariance>
        {
            public override void RosSerialize(PoseWithCovariance msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(PoseWithCovariance msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(PoseWithCovariance _) => RosFixedMessageLength;
            public override int Ros2MessageLength(PoseWithCovariance _) => Ros2FixedMessageLength;
        }
        sealed class Deserializer : Deserializer<PoseWithCovariance>
        {
            public override void RosDeserialize(ref ReadBuffer b, out PoseWithCovariance msg) => msg = new PoseWithCovariance(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out PoseWithCovariance msg) => msg = new PoseWithCovariance(ref b);
        }
    }
}
