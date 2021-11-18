/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/Temperature")]
    public sealed class Temperature : IDeserializable<Temperature>, IMessage
    {
        // Single temperature reading.
        [DataMember (Name = "header")] public StdMsgs.Header Header; // timestamp is the time the temperature was measured
        // frame_id is the location of the temperature reading
        [DataMember (Name = "temperature")] public double Temperature_; // Measurement of the Temperature in Degrees Celsius
        [DataMember (Name = "variance")] public double Variance; // 0 is interpreted as variance unknown
    
        /// <summary> Constructor for empty message. </summary>
        public Temperature()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Temperature(in StdMsgs.Header Header, double Temperature_, double Variance)
        {
            this.Header = Header;
            this.Temperature_ = Temperature_;
            this.Variance = Variance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Temperature(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Temperature_ = b.Deserialize<double>();
            Variance = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Temperature(ref b);
        }
        
        Temperature IDeserializable<Temperature>.RosDeserialize(ref Buffer b)
        {
            return new Temperature(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Temperature_);
            b.Serialize(Variance);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 16 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/Temperature";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ff71b307acdbe7c871a5a6d7ed359100";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq2TT4vbMBDF74J8h4EcdreQtLSlh0BPXfrnsFDY3MPEntiisuRqxkn97ftkN03CXnqo" +
                "CFiRZ34zem9MS3r2sQlCJl0vmW3IQlm4xunaOfqKrWRq58dlLcl8J2rc9eSVrJXpYN5coU6s1Akr9rW7" +
                "yr9dSzpk7mTn6zMspIrNp0jp8IL5pz10dwiJ7cP7m7cz72mu2Um0M2J7FeQjPUqTRZQ+SVA/6BXtyNlz" +
                "rGbURHtT2vLRJPdZTGrCrf5GDfFHTKe4cB//81q4p+cvG1Krd502+nr2YuHgmXGsOdeQ1rhmYzokmOSb" +
                "VvIqyFECTdag0+mtjb3oGonbtlxEqZEIKUIYaVAEWaIqdd0QPVSfnbzJRyYUY+o5m6+GwBnxKcOFEj55" +
                "V+j4qfwcpKjy7XGDmKhSDebR0AhCBecUzuEluQF6vntbEtxye0qrIm+DGbvMlbVspVn5BdW19Mm6QY1X" +
                "8+XWYEMdQZVa6X462+GvPhCKVELSp6qle3T+fbQWs1SmYLJtj4EHuIICoN6VpLuHK3Jpe0ORYzrjZ+Kl" +
                "xr9gC2XmljutWngWyu11aCAgAvucjr5G6H6cIFXwZV6D32fOo5s+qKmkW34uGiMIWZMjeLJqqjwMqOnk" +
                "rXVqudDPX5JbuN/IQO/63gMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
