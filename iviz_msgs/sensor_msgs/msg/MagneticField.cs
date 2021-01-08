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
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; } // timestamp is the time the
        // field was measured
        // frame_id is the location and orientation
        // of the field measurement
        [DataMember (Name = "magnetic_field")] public GeometryMsgs.Vector3 MagneticField_ { get; set; } // x, y, and z components of the
        // field vector in Tesla
        // If your sensor does not output 3 axes,
        // put NaNs in the components not reported.
        [DataMember (Name = "magnetic_field_covariance")] public double[/*9*/] MagneticFieldCovariance { get; set; } // Row major about x, y, z axes
        // 0 is interpreted as variance unknown
    
        /// <summary> Constructor for empty message. </summary>
        public MagneticField()
        {
            Header = new StdMsgs.Header();
            MagneticFieldCovariance = new double[9];
        }
        
        /// <summary> Explicit constructor. </summary>
        public MagneticField(StdMsgs.Header Header, in GeometryMsgs.Vector3 MagneticField_, double[] MagneticFieldCovariance)
        {
            this.Header = Header;
            this.MagneticField_ = MagneticField_;
            this.MagneticFieldCovariance = MagneticFieldCovariance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MagneticField(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            MagneticField_ = new GeometryMsgs.Vector3(ref b);
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
            MagneticField_.RosSerialize(ref b);
            b.SerializeStructArray(MagneticFieldCovariance, 9);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (MagneticFieldCovariance is null) throw new System.NullReferenceException(nameof(MagneticFieldCovariance));
            if (MagneticFieldCovariance.Length != 9) throw new System.IndexOutOfRangeException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 96;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/MagneticField";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "2f3b0b43eed0c9501de0fa3ff89a45aa";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVWTWscRxC9L+x/KLwHS2G1CVYIRJBDwDjRQcbEIpcQRO1M7UzbM92T7p6VVr/er6p3" +
                "pIlIYA3JgNj56Hpd/erVK9GKboTTGKUXnynsKLdCN9x4ya6id066mvZS5RCJMzGlQSq3w6cuVJxd8Jvl" +
                "YrmgFV2X0CrsOTr2lUxg/QzfJfrsw71fk8uU2jACfSu0c10nNTlvSGduR9x1dAijrdYoBZoDC1ftHHlN" +
                "smk2tIuht7U1Z06tSF4r5KcxZRrGjE8hCcCDb8oyx03w3J3bxj/Ps+85R/ege2kujxJDonvkqfk6nyUO" +
                "UTKS5kSvZmGjtwO+sn2JfU050IhNp6zA4Wy5Iba8x/egyJzS2AMVdDchZ/HlSCn0YoABMBGPY6zwwqj/" +
                "VbjGu7b8/Mu1oux6SZn7YaJTX+gNIE66ViiTquEeJz4yX39FbORe7lw97T7JxygK0aGG9nw64lFeJamZ" +
                "FIyURsBYjoe7PjXp299NwJeoadH1XQla0cOaDmtL4RFV6YfgAZCO0F9LzLFNnKdbSR2fHo7WgdZRVvEJ" +
                "AHWQRD6gG8esor0kfpC0Ph1Pg97z+6SplJ58OpnCRhlChHRL5+66wPmH7//48c8X9NzNZLqi39CHPX9S" +
                "G9giryNzj5ba6Zl9p+V/0T0vW2e5+Ok/vpaLm4+/XFHKdZFD6ZjlYkUfM2rPUfWT2bpzhxO2rkGXXXSy" +
                "l46sZ5Cpfc2HQRJ4W9FtqydJEJqXCIc4aJNbs4PtfvQO8i499jcADUVVmAaOYHrsOCIgxNp5XW9dYvj6" +
                "l+SvUZSZ67dXWOWTVGN2SOoAjCpC8g4+dv0Wi0eQevlGIxB4ex8ulOQGfvDc9rmFgSNjeQD3SZPldKXb" +
                "fFPOuAE8SBJsVCc6s3d3eEznhH2QBWQD0z1D+h8OMNKiLavetoMnJqrAA2Bfa9Dr8zm0pn5Fnn2Y8Avk" +
                "8yan4PpnYD3WRYvidUpBGhvwiJVDDHtXY+32UJTfqbFQ57aR42G5MM+zTQHyTsnGMsRZbfAL+w2VQyVg" +
                "cy63y0XCEMAGk3tpx/xP6vxHx3pSGnoWRbMW5pnR7KLgPANXsjHFXFuFg4dC4Ig4OfT4FIrI2kXE6tQG" +
                "rESB2sVm8eQ5CtLzZzEvsqHEwwA0SD+yh6uZZ+M1Ys505K7pvsWUslXKVJG4dQX+SYiugeVbKLbCSD1G" +
                "Mx0PuKa8e1OmoGVddjPzXVEMZSacb44WidmDY5hXlna0mTllZnLJIayneVsymdP6IaAvQE1K3OgYTxlW" +
                "YD54tEF6eL6FXqbbx+XiCyxlBawqCQAA";
                
    }
}
