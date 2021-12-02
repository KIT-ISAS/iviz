/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class CompressedImage : IDeserializable<CompressedImage>, IMessage
    {
        // This message contains a compressed image
        [DataMember (Name = "header")] public StdMsgs.Header Header; // Header timestamp should be acquisition time of image
        // Header frame_id should be optical frame of camera
        // origin of frame should be optical center of camera
        // +x should point to the right in the image
        // +y should point down in the image
        // +z should point into to plane of the image
        [DataMember (Name = "format")] public string Format; // Specifies the format of the data
        //   Acceptable values:
        //     jpeg, png
        [DataMember (Name = "data")] public byte[] Data; // Compressed image buffer
    
        /// Constructor for empty message.
        public CompressedImage()
        {
            Format = string.Empty;
            Data = System.Array.Empty<byte>();
        }
        
        /// Explicit constructor.
        public CompressedImage(in StdMsgs.Header Header, string Format, byte[] Data)
        {
            this.Header = Header;
            this.Format = Format;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal CompressedImage(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Format = b.DeserializeString();
            Data = b.DeserializeStructArray<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new CompressedImage(ref b);
        
        CompressedImage IDeserializable<CompressedImage>.RosDeserialize(ref Buffer b) => new CompressedImage(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Format);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Format is null) throw new System.NullReferenceException(nameof(Format));
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Format);
                size += Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/CompressedImage";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "8f7a12909da2c9d3332d540a0977563f";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq2UTW8TMRCG7/4VI+XQltIiwQVF4oCogB6QkNobQtXEnuwaee2t7U26/HpeezdNAwr0" +
                "wGojx/bMM9+7oNvWJuokJW6EdPCZrU/E+Nv1EcdiyHa4U+qzsJFI7bTMz4Lm42wBydz1lNowOEMrIdb3" +
                "g0022+DrPYX1DNupHzyPrHXkTu6seYIKfbaa3XRVOBpr5GOgEG1jfZGbFP4EafEZpv5JOn/YKffB+kw5" +
                "UG6FwG8zwUTZ/DWm8/EQYMLWP0/x56EifrAdqHfsawr2BJVytL6hdYgd5z3iphdt11ZSlZ1vZ03D+WjQ" +
                "RO+1lj7zyglt2A2SlsdliX700ryk3jdqgJdvv32v9CdCH37rJloN67VEpd7950d9ufm0pJTNXZea9Gpq" +
                "KIVMZPaGo0GrZ67OIRvUoooSL5xsxFHtXjhYb/PYS7pU83zgbcSjS5wbaShRoA6YkG7waKYs++7f6UMT" +
                "NWbqOaLfBscR8iEa64t4bcpCx5vkfhCvha6vlmUAk+ghWzg0gqCjcCqVvb6imts3r4uCWtxuwwW20hyM" +
                "Xm5RXzgrD7tsMwq3oBdTcJdgIzkCKybRaT27wzadEYzABemDbukUnn8dc1umFo2y4WhrIwCMyXGgnhSl" +
                "k7Mn5OL2kjz7sMNPxL2N52ALZeKWmC5a1MyV6NPQIIEQ7GPYWAPR1Vgh2lnMMTm7ihxHVT8y1aRafKyD" +
                "n0v5akWwckpBWxTA0Nbm9nFq5s+NUr8A8dAt8Q8FAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
