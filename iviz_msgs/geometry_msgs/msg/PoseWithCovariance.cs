/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/PoseWithCovariance")]
    public sealed class PoseWithCovariance : IDeserializable<PoseWithCovariance>, IMessage
    {
        // This represents a pose in free space with uncertainty.
        [DataMember (Name = "pose")] public Pose Pose { get; set; }
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        [DataMember (Name = "covariance")] public double[/*36*/] Covariance { get; set; }
    
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
        public PoseWithCovariance(ref Buffer b)
        {
            Pose = new Pose(ref b);
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
            Pose.RosSerialize(ref b);
            b.SerializeStructArray(Covariance, 36);
        }
        
        public void RosValidate()
        {
            if (Covariance is null) throw new System.NullReferenceException(nameof(Covariance));
            if (Covariance.Length != 36) throw new System.IndexOutOfRangeException();
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 344;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/PoseWithCovariance";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c23e848cf1b7533a8d7c259073a97e6f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1TPU/DMBDdI+U/nJQFpLQMoAyVGJgQAxIfHfgQQtfk0hoRO5wdmvTXc07bJKWVWFA9" +
                "nc/Pd/fesyOYLpQFppLJknYWEEpjCZSGnInAlpgSLJVbQKVTYodKu2YcBmFw53Ee7DcRPJjlqMAPw301" +
                "dMpoMDm4BUFSJ5Cab2SFUggKdKxqf3Eqh4ZVhy+RsSBHbKGSDgi5qikbYT0ctIXKGBHcSAfOiOO2y+Ay" +
                "Mk084KSOoYlhFQObTQucmcrBE/iae+nnw+mXNn0aBvmnQZdcvJ4nbwNCXoTLf15hcPt4PYE5GaHEzXth" +
                "5/bMy+5pXR3Qed+6WEYsfDrbnKs1JZ0NNR+Dt1Oc7RBhcF+hyKjbyj3yiDRlnPXzEN9TI/2VtmuPtyyE" +
                "kX+ufu4d0p1HIA9sGzZ9uDoai17Ejsrwr+1I++vPye6rtyA3XLS/7k9m23Dp0T/5eC/l4AMAAA==";
                
    }
}
