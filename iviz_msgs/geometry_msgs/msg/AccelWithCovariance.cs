/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class AccelWithCovariance : IDeserializable<AccelWithCovariance>, IMessage
    {
        // This expresses acceleration in free space with uncertainty.
        [DataMember (Name = "accel")] public Accel Accel;
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        [DataMember (Name = "covariance")] public double[/*36*/] Covariance;
    
        /// Constructor for empty message.
        public AccelWithCovariance()
        {
            Accel = new Accel();
            Covariance = new double[36];
        }
        
        /// Explicit constructor.
        public AccelWithCovariance(Accel Accel, double[] Covariance)
        {
            this.Accel = Accel;
            this.Covariance = Covariance;
        }
        
        /// Constructor with buffer.
        internal AccelWithCovariance(ref Buffer b)
        {
            Accel = new Accel(ref b);
            Covariance = b.DeserializeStructArray<double>(36);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new AccelWithCovariance(ref b);
        
        AccelWithCovariance IDeserializable<AccelWithCovariance>.RosDeserialize(ref Buffer b) => new AccelWithCovariance(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Accel.RosSerialize(ref b);
            b.SerializeStructArray(Covariance, 36);
        }
        
        public void RosValidate()
        {
            if (Accel is null) throw new System.NullReferenceException(nameof(Accel));
            Accel.RosValidate();
            if (Covariance is null) throw new System.NullReferenceException(nameof(Covariance));
            if (Covariance.Length != 36) throw new RosInvalidSizeForFixedArrayException(nameof(Covariance), Covariance.Length, 36);
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 336;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/AccelWithCovariance";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "ad5a718d699c6be72a02b8d6a139f334";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1Ty2rcQBC8z1cU+GKDrEAc9mDIIafgQyAkJuRBCL1SSzuxNC16Rl7JX5/Ww7KX5BII" +
                "WVgYzXRVd9XUnOH24CN46JRj5AgqCm5YKXkJ8AGVMiN2VDCOPh3Qh4I1kQ9pzJ17M1UvGOfO8EGOly39" +
                "FIXyRMghLURSIR0Yu2GHQu5JPRkNWkrqB8Pd2pmo38o7Umo5sUb0kUGo/MDlJQ026ilzbugb49eSNZt7" +
                "PMOS8rWdnw8ZxgwPGVTWBrSXPuEzJsbftr/8efvrvH3hqkYo7V59u9p9fybGudf/+OfefXx7jZrF1Oj4" +
                "o411fDH77f7m0vYqdzxtJoFPEY0PTAoKpf3rvrG1OZZi7j5xkUSvsJY8fa91/0fh2vVR43bbJhL389mp" +
                "wBxTAhKsVkIzomUKCSZ2Qxqw9GrQKS5T1JQrUc7MDpRi7gVJxtHSnVFysLwZmrrOyAhJKcRmMXZ2EOec" +
                "13mG48Fcnat8qK3QGGoOrL6A+tqXC9IaWchXMGEVZ0GtXtpzappl5qWZhddIHkN3keOmwig9jpMgWyhK" +
                "SjaRYG8jrnPRvpnmlWx+KAvFqaHvxe7ebImRajbvYmIq7emuMcawrcZt9eB+AcTwcdwYBAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
