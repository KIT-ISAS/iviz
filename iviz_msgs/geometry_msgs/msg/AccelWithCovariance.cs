/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/AccelWithCovariance")]
    public sealed class AccelWithCovariance : IDeserializable<AccelWithCovariance>, IMessage
    {
        // This expresses acceleration in free space with uncertainty.
        [DataMember (Name = "accel")] public Accel Accel;
        // Row-major representation of the 6x6 covariance matrix
        // The orientation parameters use a fixed-axis representation.
        // In order, the parameters are:
        // (x, y, z, rotation about X axis, rotation about Y axis, rotation about Z axis)
        [DataMember (Name = "covariance")] public double[/*36*/] Covariance;
    
        /// <summary> Constructor for empty message. </summary>
        public AccelWithCovariance()
        {
            Accel = new Accel();
            Covariance = new double[36];
        }
        
        /// <summary> Explicit constructor. </summary>
        public AccelWithCovariance(Accel Accel, double[] Covariance)
        {
            this.Accel = Accel;
            this.Covariance = Covariance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AccelWithCovariance(ref Buffer b)
        {
            Accel = new Accel(ref b);
            Covariance = b.DeserializeStructArray<double>(36);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AccelWithCovariance(ref b);
        }
        
        AccelWithCovariance IDeserializable<AccelWithCovariance>.RosDeserialize(ref Buffer b)
        {
            return new AccelWithCovariance(ref b);
        }
    
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
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 336;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/AccelWithCovariance";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ad5a718d699c6be72a02b8d6a139f334";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1TTWvcQAy9D+x/eJBLAo4LTdlDoIecSg6F0obSD0rR2rJ3GntkNLNZO7++8kfdLO2l" +
                "ELqwIGv0nvQ0b85wt/cR3HfKMXIEFQU3rJS8BPiASpkROyoYR5/2OISCNZEPaciduxmrZ4xzZ3gvx8uW" +
                "fohCeSTkkGYiqZD2jG2/RSEPpJ6MBi0l9b3h7uxM1K/lHSm1nFgjDpFBqHzP5SX1Nuopc27oW+PXkjWb" +
                "ejzBkvK1nZ/3GYYMjxlUlga0k0PCJ4yMf6Q//z39ZUpfuKoRSttXX6+2356IcRv3+pl/G/f2w5tr1Cym" +
                "R4fvbazji2njG/cv97ZTuecxmQQ+RTQ+MCkolPavD43FtrQUc/eRiyR6haXk9/dS979ELn1Xmeudm048" +
                "TIenGnOMPkiwWgnNgJYpJJjeFWnA0qtBR9OMhlOuRDmzjaAUW2CQZBwt3RslB3NdElDXGRkhKYXYzLu1" +
                "tEHOOa/zDMe9LXaq8qG2QmOoObD6AuprX85Ia2RWX8CERZ3ZtXppj6pp5pnnZmZhI/llvYsctxUGOeA4" +
                "CrJAUVKyiQQ7G3GZi3bNOK9k03OZKU43+k7s+m0tMVLNtruYmEp7wIuZ0a/RsEaPG/cTZs8JRh8EAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
