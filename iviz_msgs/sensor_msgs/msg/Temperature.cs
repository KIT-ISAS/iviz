/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Temperature : IDeserializable<Temperature>, IMessage
    {
        // Single temperature reading.
        [DataMember (Name = "header")] public StdMsgs.Header Header; // timestamp is the time the temperature was measured
        // frame_id is the location of the temperature reading
        [DataMember (Name = "temperature")] public double Temperature_; // Measurement of the Temperature in Degrees Celsius
        [DataMember (Name = "variance")] public double Variance; // 0 is interpreted as variance unknown
    
        /// Constructor for empty message.
        public Temperature()
        {
        }
        
        /// Explicit constructor.
        public Temperature(in StdMsgs.Header Header, double Temperature_, double Variance)
        {
            this.Header = Header;
            this.Temperature_ = Temperature_;
            this.Variance = Variance;
        }
        
        /// Constructor with buffer.
        internal Temperature(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Temperature_ = b.Deserialize<double>();
            Variance = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Temperature(ref b);
        
        Temperature IDeserializable<Temperature>.RosDeserialize(ref Buffer b) => new Temperature(ref b);
    
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/Temperature";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "ff71b307acdbe7c871a5a6d7ed359100";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq2TT4/TMBDF7/4UI+Wwu0gtCBCHSpxY8eewEtL2Xk2TaWLh2MEzacm35zmhtBWXPWBV" +
                "iuvM/Gb83oQqevaxDUIm/SCZbcxCWbjB6do5+oqtZOqWx2VVZL4XNe4H8krWyXywbK5QJ1bqhRX7xl3l" +
                "366KDpl72fnmDAupZvMpUjr8w/zTHro7hMT24f3N24X3tNTsJdoZsb0K8pEepc0iSp8kqB/1inbk7DnW" +
                "C2qmvSlt+WiShywmDeFWf6PG+COmU3Qf//NyT89fNqTW7Hpt9fXihINhxrHh3EBX44aN6ZDgkG87yasg" +
                "Rwk0+4I257c2DaJrJG67cgulViJ0CGGiURFkierU92P0kHyx8SYfmZCLaeBsvh4DZ8SnDAtK+GxcoeOn" +
                "8nOUIsm3xw1ioko9mkdDEwg1bFPYhpfkRoj57m1JcNX2lFZF2xYDdhkq69hKs/ILkmvpk3WDGq+Wy63B" +
                "hjiCKo3S/Xy2w199IBRBCzKkuqN7dP59sg6DVEZg9myPaQe4hgKg3pWku4crcml7Q5FjOuMX4qXGS7CF" +
                "snDLnVYdPAvl9jq2EBCBQ05H3yB0P82QOvgyrMHvM+fJzV/TXNJVn4vGCELW7AierJpqDwMaOnnrnFou" +
                "9PNn5NxvpTQmJtoDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
