using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    [DataContract]
    public sealed class Temperature : IMessage
    {
        // Single temperature reading.
        
        [DataMember] public std_msgs.Header header { get; set; } // timestamp is the time the temperature was measured
        // frame_id is the location of the temperature reading
        
        [DataMember] public double temperature { get; set; } // Measurement of the Temperature in Degrees Celsius
        
        [DataMember] public double variance { get; set; } // 0 is interpreted as variance unknown
    
        /// <summary> Constructor for empty message. </summary>
        public Temperature()
        {
            header = new std_msgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Temperature(std_msgs.Header header, double temperature, double variance)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.temperature = temperature;
            this.variance = variance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Temperature(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.temperature = b.Deserialize<double>();
            this.variance = b.Deserialize<double>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new Temperature(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.header);
            b.Serialize(this.temperature);
            b.Serialize(this.variance);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            header.Validate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += header.RosMessageLength;
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/Temperature";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ff71b307acdbe7c871a5a6d7ed359100";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61TTYvbMBC961cM5LC7haSlLT0s9NSlH4eFwuYeJvbEFpUlVzNO6n/fJ7nZpJRCDxUG" +
                "y9KbNzPvjWlFTz52QchkGCWzTVkoC7c43ThHn7GVTP3yuqwVmR9EjYeRvJL1Ug+WzRXViZUGYcW+dfS3" +
                "taJD5kF2vj2ThdSw+RQpHf7g/FUeqjuExPbu7W+3C9/jknOQaGeK7RXIR3qQLosofZCgftIrtiNnz7GR" +
                "S3WvSlk+muQxi0lL6OoZNcVvMZ2ie/+fl3t8+nRPau1u0E5fLk44GGYcW84tdDVu2ZgOCQ75rpe8DnKU" +
                "QNUXlFlvbR5FNwjc9qULpU4idAhhpkkBskRNGoYpekguF1/P8YiEXEwjZ/PNFDgDnzIsKPBqXGHHo/J9" +
                "kiLJl4d7YKJKM5lHQTMYGtimsA2X5CaI+eZ1CXCr7Smti7YdBuwyVNazlWLlByRXrZrfI8eLpbkNuCGO" +
                "IEurdFvPdvjUO0ISlCBjanq6ReVfZ+sxSGUEqmd7TDuIGygA1psSdHN3xRwrdeSYzvQL4yXHv9DGZ97S" +
                "07qHZ6F0r1MHAQEcczr6FtD9XEma4MuwBr/PnGdX/6aa0q0+Fo0BQlR1BG9WTY3nMosnb71Ty4X9/Bs5" +
                "9xOlNCYm2gMAAA==";
                
    }
}
