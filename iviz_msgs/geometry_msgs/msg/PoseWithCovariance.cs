/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/PoseWithCovariance")]
    public sealed class PoseWithCovariance : IDeserializable<PoseWithCovariance>, IMessage
    {
        // This represents a pose in free space with uncertainty.
        [DataMember (Name = "pose")] public Pose Pose;
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        [DataMember (Name = "covariance")] public double[/*36*/] Covariance;
    
        /// <summary> Constructor for empty message. </summary>
        public PoseWithCovariance()
        {
            Covariance = new double[36];
        }
        
        /// <summary> Explicit constructor. </summary>
        public PoseWithCovariance(in Pose Pose, double[] Covariance)
        {
            this.Pose = Pose;
            this.Covariance = Covariance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PoseWithCovariance(ref Buffer b)
        {
            b.Deserialize(out Pose);
            Covariance = b.DeserializeStructArray<double>(36);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PoseWithCovariance(ref b);
        }
        
        PoseWithCovariance IDeserializable<PoseWithCovariance>.RosDeserialize(ref Buffer b)
        {
            return new PoseWithCovariance(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Pose);
            b.SerializeStructArray(Covariance, 36);
        }
        
        public void RosValidate()
        {
            if (Covariance is null) throw new System.NullReferenceException(nameof(Covariance));
            if (Covariance.Length != 36) throw new RosInvalidSizeForFixedArrayException(nameof(Covariance), Covariance.Length, 36);
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 344;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/PoseWithCovariance";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c23e848cf1b7533a8d7c259073a97e6f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr2TPU/DQAyG95P6Hyx1ASktAyhDJQYmxIDERwc+hJCbOO0hchd8F5r01+NL2ySlFRNq" +
                "Jsf22X5e3w1hutAOmAomR8Y7QCisI9AGMiYCV2BCsNR+AaVJiD1q4+uxUnchK6QqNYQHuxzl+GG5q4Re" +
                "WwM2A78giKsYEvuNrFGKQI6edSXnphKzrNv0Ahlz8sQOSimPkOmK0hFW/Rmb1LGcvpH6nBJHTY/eWWSa" +
                "SPykiqCOYBUB200DnNnSwxOEinvu58Pul8Z9qrJPiz6+eD2P33owaqAu//kbqNvH6wnMyQoP1++5m7uz" +
                "oPdAoK4OSLy/sUgmzIM73cT1Gsikfb3HIGuUfbYJ6r5EUdA0dbu84zHKMAGyuZSJlfbauPV2twiCE65o" +
                "mHqHWG3WA1Vr1a21OhZBp1+L0X9bO6r+emPy99Wpn1nO5ZX9DbW1loL3A5Uc8YvKAwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
