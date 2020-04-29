using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    public sealed class Temperature : IMessage
    {
        // Single temperature reading.
        
        public std_msgs.Header header; // timestamp is the time the temperature was measured
        // frame_id is the location of the temperature reading
        
        public double temperature; // Measurement of the Temperature in Degrees Celsius
        
        public double variance; // 0 is interpreted as variance unknown
    
        /// <summary> Constructor for empty message. </summary>
        public Temperature()
        {
            header = new std_msgs.Header();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out temperature, ref ptr, end);
            BuiltIns.Deserialize(out variance, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(temperature, ref ptr, end);
            BuiltIns.Serialize(variance, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += header.RosMessageLength;
                return size;
            }
        }
    
        public IMessage Create() => new Temperature();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "sensor_msgs/Temperature";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "ff71b307acdbe7c871a5a6d7ed359100";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
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
