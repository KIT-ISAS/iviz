/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/TwistWithCovariance")]
    public sealed class TwistWithCovariance : IDeserializable<TwistWithCovariance>, IMessage
    {
        // This expresses velocity in free space with uncertainty.
        [DataMember (Name = "twist")] public Twist Twist;
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        [DataMember (Name = "covariance")] public double[/*36*/] Covariance;
    
        /// <summary> Constructor for empty message. </summary>
        public TwistWithCovariance()
        {
            Covariance = new double[36];
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwistWithCovariance(in Twist Twist, double[] Covariance)
        {
            this.Twist = Twist;
            this.Covariance = Covariance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TwistWithCovariance(ref Buffer b)
        {
            b.Deserialize(out Twist);
            Covariance = b.DeserializeStructArray<double>(36);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwistWithCovariance(ref b);
        }
        
        TwistWithCovariance IDeserializable<TwistWithCovariance>.RosDeserialize(ref Buffer b)
        {
            return new TwistWithCovariance(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Twist);
            b.SerializeStructArray(Covariance, 36);
        }
        
        public void RosValidate()
        {
            if (Covariance is null) throw new System.NullReferenceException(nameof(Covariance));
            if (Covariance.Length != 36) throw new RosInvalidSizeForFixedArrayException(nameof(Covariance), Covariance.Length, 36);
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 336;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/TwistWithCovariance";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1fe8a28e6890a4cc3ae4c3ca5c7d82e6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1TTWvbQBC9L/g/PMglAUWFpvgQ6LnkUCitKf2glLE0kreRdszsOJby6zuyVSemPfRQ" +
                "KpAYzc578/X2AqtNzOBhq5wzZzxwJ1W0ETGhUWbkLVWMfbQNdqliNYrJxjKE1T5mg03fEC7wXvbXPf0Q" +
                "hfJExsnIoiRIA9swlsMSlTyQRnIa9GQaB8et/Ew0nsK3pNSzsWbsMoPQxIHraxq8zHPm0tF3zq81a3HI" +
                "8QxLyrd+fjkUGAs8FlCZE9BadoZPmBh/c3/+s/vLwX0Vmk7Ilq++3iy/PWsmLMLrf/wswtsPb27Rsng/" +
                "On7vc5tfHCa+CH+7s7XKPSd3miBaRhcTk4JS7W+769z2gVkuw0euTPQGc8jT/xz3vxqc855aPO3b9+ld" +
                "TofnPZaYNGDwWEndiJ4puSblCenAOqpDJ8FMYlNuRLnwiaAWH14Sc46e7p2SkyvOBLTdOhnBlFLujkJw" +
                "t0MuuWzLAvuND/YQFVPrgc7QcmKNFTS2sT4iPZHLfAYT5u5cqs1Lv1Bdd6z5mMzl6yS/ZHdV4q7BKDvs" +
                "p4bcUNRkXpFg7SXOddG6m+qV4nBVjhTnE30nvn4fS87Uss8uG1Ptl3cWMoaTNZ6sx0X4Cf1kjn0XBAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
