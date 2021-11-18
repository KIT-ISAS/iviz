/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/MagneticField")]
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
    
        /// <summary> Constructor for empty message. </summary>
        public MagneticField()
        {
            MagneticFieldCovariance = new double[9];
        }
        
        /// <summary> Explicit constructor. </summary>
        public MagneticField(in StdMsgs.Header Header, in GeometryMsgs.Vector3 MagneticField_, double[] MagneticFieldCovariance)
        {
            this.Header = Header;
            this.MagneticField_ = MagneticField_;
            this.MagneticFieldCovariance = MagneticFieldCovariance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MagneticField(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out MagneticField_);
            MagneticFieldCovariance = b.DeserializeStructArray<double>(9);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MagneticField(ref b);
        }
        
        MagneticField IDeserializable<MagneticField>.RosDeserialize(ref Buffer b)
        {
            return new MagneticField(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(MagneticField_);
            b.SerializeStructArray(MagneticFieldCovariance, 9);
        }
        
        public void RosValidate()
        {
            if (MagneticFieldCovariance is null) throw new System.NullReferenceException(nameof(MagneticFieldCovariance));
            if (MagneticFieldCovariance.Length != 9) throw new RosInvalidSizeForFixedArrayException(nameof(MagneticFieldCovariance), MagneticFieldCovariance.Length, 9);
        }
    
        public int RosMessageLength => 96 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/MagneticField";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "2f3b0b43eed0c9501de0fa3ff89a45aa";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVVXYvbRhR9F/g/XOKH7BatW7Kh0IU+FELafdgQmqUvJZhr6UqaRJpRZ0a2tb8+544s" +
                "R10acKE1C2tLc8/9OucMrelBOAxeOrGRXEWxEXrg2ko0Bb010pa0lyI6TxyJKfRSmAqvWldwNM5usozW" +
                "dD8FFm7P3rAtZIbqFugm0GfrDjYnEyk0bgD2TqgybSslGatAV6Yiblsa3ZAOa5DiLHGFi2YJnJNs6g1V" +
                "3nXpbMmRQyMScyB+GkKkfoh444IA29l6OmW4dpbba037y7L0jqM3R82klTyJd4EOKFKLNTaK771EVMyB" +
                "XizCBpu6e6FZiW1J0dGAlHNJmN7idAJseI/3ToE5hKEDKAZduxjFTv0E14niOaB4/Bp8IRlG/ptwiQfN" +
                "9O8bnzVF00mI3PXzHPWBfsm+FfMcoUoUOKDX08TLy0M9d7I15Zx7pkwajvMGq0u/LwY8cWoqaUEAzKMW" +
                "DCr6cduFOnz/R2LsLTY5EXk7hazpmNOYp/xPWEbXO4vwcAL+lzM5ycJYepTQ8sXR0ArYjWWKDYgvnQSy" +
                "DuIbovL0lvgoIb8YTmPe8bughUwaPLelqF5658FW1WnVOo4/vv7zp4/PJrNdEHNNv0N2HX9Sye9Q1Glo" +
                "T6mui8v6Qbf+TC3PpbLKfv6PP6vs4cOvdxRiORFhkskqW9OHiK2zV95ETnKs0GBjaujqppW9tJSEgkrT" +
                "2zj2EjYIfGy0kQCGWfEwhFFFncSNSXeDNSD1pKu/xSMSC2Hq2WPMQ8se550vjdXjSRqKjr8gfw2iU7l/" +
                "c4czNkgxRIOCRiAUHjQ3sKz7N5QNmOftKw3I1o8Hd6PjrSH/ryqPDVwaxcoRUw9aJ4c75Phuam4DbExH" +
                "kKUMdJWebfEzXBOSoASQBd56hcrfjzDMiVFpbbsW5heowASA+lKDXl4vkLXsO7Js3Qw/IX7NcQmsPeNq" +
                "TzcNdtZq92GoMUAc7L3bmxJHd+PE9lZthFqz8+zHLNlbSpmt3+qMcQhRaSP4D5N1hcEC4GgmNlmA0QN9" +
                "NqrsfyPkP9rTamYXNIptJcnywlYqL2im50I2ypP7tFlnwQt4H5oGBc+RCCyNR6heyUAVL+C3pKt2dhhg" +
                "dPxZkvGka4f7HmDgumcLB0vejMcIudIbNadDg3sondIxJVInGeD+96aGs6dIJMKVeQpmOnWXU6xeTddc" +
                "qnlKpja7Ju8m57/enNwQFwx6SLY4qS/diXNdiSXRuXy+T1Mdy4m+d9ACxhIC13pJhwjhw/JOjkfH87fx" +
                "/O1plX0BFt4bG/8IAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
