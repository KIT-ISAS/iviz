/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MagneticField : IDeserializable<MagneticField>, IMessage
    {
        // Measurement of the Magnetic Field vector at a specific location.
        // If the covariance of the measurement is known, it should be filled in
        // (if all you know is the variance of each measurement, e.g. from the datasheet,
        //just put those along the diagonal)
        // A covariance matrix of all zeros will be interpreted as "covariance unknown",
        // and to use the data a covariance will have to be assumed or gotten from some
        // other source
        [DataMember (Name = "header")] public StdMsgs.Header Header; // timestamp is the time the
        // field was measured
        // frame_id is the location and orientation
        // of the field measurement
        [DataMember (Name = "magnetic_field")] public GeometryMsgs.Vector3 MagneticField_; // x, y, and z components of the
        // field vector in Tesla
        // If your sensor does not output 3 axes,
        // put NaNs in the components not reported.
        [DataMember (Name = "magnetic_field_covariance")] public double[/*9*/] MagneticFieldCovariance; // Row major about x, y, z axes
        // 0 is interpreted as variance unknown
    
        /// Constructor for empty message.
        public MagneticField()
        {
            MagneticFieldCovariance = new double[9];
        }
        
        /// Explicit constructor.
        public MagneticField(in StdMsgs.Header Header, in GeometryMsgs.Vector3 MagneticField_, double[] MagneticFieldCovariance)
        {
            this.Header = Header;
            this.MagneticField_ = MagneticField_;
            this.MagneticFieldCovariance = MagneticFieldCovariance;
        }
        
        /// Constructor with buffer.
        internal MagneticField(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out MagneticField_);
            MagneticFieldCovariance = b.DeserializeStructArray<double>(9);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MagneticField(ref b);
        
        MagneticField IDeserializable<MagneticField>.RosDeserialize(ref Buffer b) => new MagneticField(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ref MagneticField_);
            b.SerializeStructArray(MagneticFieldCovariance, 9);
        }
        
        public void RosValidate()
        {
            if (MagneticFieldCovariance is null) throw new System.NullReferenceException(nameof(MagneticFieldCovariance));
            if (MagneticFieldCovariance.Length != 9) throw new RosInvalidSizeForFixedArrayException(nameof(MagneticFieldCovariance), MagneticFieldCovariance.Length, 9);
        }
    
        public int RosMessageLength => 96 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/MagneticField";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "2f3b0b43eed0c9501de0fa3ff89a45aa";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVVXYvbRhR916+4xA/ZDV63ZEuhC30ohLT7sCE0S15KMdfSlTSJNKPOjGxrf33PnbEc" +
                "ZWnAhdYsrC3NPffrnDO0ogfhMHrpxUZyNcVW6IEbK9GU9NZIV9Feyug8cSSmMEhparzqXMnROLspClrR" +
                "fQ4s3Z69YVvKDNUv0E2gz9Yd7JpMpNC6Edg7odp0nVRkrAJdmZq462hyYzqsQYqzxBUu2yXwmmTTbKj2" +
                "rk9nK44cWpG4BuKnMUQaxog3LgiwnW3yKcONs9xda9pflqX3HL05aiat5Em8C3RAkVqssVH84CWiYg70" +
                "YhE22tTdC81KbCuKjkaknEvC9BanE2DLe7x3CswhjD1AMejGxSg29xNcL4rngOLxa/SlFBj5b8IVHrT5" +
                "3zc+K4qmlxC5H+Y56gP9Unwr5jlCnShwQK+niVeXh3ruZWuqOfdMmTQc5w1Wl35fDHjiVC5pQQDMoxEM" +
                "Kvpp24cmfPcxMfYWm8xE3uaQFR3XNK1T/icsox+cRXg4Af/LmZxkYSw9Suj44mhoBezGMsUGxFdOAlkH" +
                "8Y1ReXpLfJSwvhhOY97xu6CFZA2e21JUL4PzYKvqtO4cxx9/+OOnP59NZrsg5op+h+x6/qSS36Go09Ce" +
                "Ul0Xl/W9bv2ZWp5Lpfj5P/4UDx9+vaMQq0yDLJJiRR8iVs5eSRM5abFGd61pIKqbTvbSUVIJykxv4zRI" +
                "2CDwsdUuAuhlxcMNJlV0UjbG3I/WgNFZVF/FIxLbYBrYY8Zjxx7nna+M1eNJF4qOvyB/jaIjuX9zhzM2" +
                "SDlGg4ImIJQeHDfwq/s3VIwY5u1rDShWjwd3o7NtoP0vEo8tLBrFyhEjD1onhzvkeJWb2wAbwxFkqQJd" +
                "pWdb/AzXhCQoAUyBsV6h8vcT3DLTKe1s18H5ApWYAFBfatDL6wWyln1Hlq2b4TPilxyXwNozrvZ002Jn" +
                "nXYfxgYDxMHBu72pcHQ3Zap36iHUmZ1nPxXJ21LKYvVWZ4xDiEobwX84rCsNFgA7M7EtAlwe6LNLFf8T" +
                "G//RmWZqQZ1YVRIrLwyl9oJOBi5loyS5T2t1FqSA66Fj8O8cicDKeITqZQxU8QJyS7pkZ28BRs+fJVmO" +
                "aDQPA8BAdM8W3pVcGY8RcqV36ZoOLW6gdEpnlBidNICb35sGnp4ikQiX5SmY6dTcmmL9Ol9wqeacTA12" +
                "Rd5lz7/enHwQVwt6SIaYpZduw7muRJHo3Hq+SVMdy4G+dxACxhICN3o9hwjRw+xOXkfH87fp/O2p+BvL" +
                "5/4J+AgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
