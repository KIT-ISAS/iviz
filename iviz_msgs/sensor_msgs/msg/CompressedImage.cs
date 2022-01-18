/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class CompressedImage : IDeserializable<CompressedImage>, IMessage
    {
        // This message contains a compressed image
        /// Header timestamp should be acquisition time of image
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Header frame_id should be optical frame of camera
        // origin of frame should be optical center of camera
        // +x should point to the right in the image
        // +y should point down in the image
        // +z should point into to plane of the image
        /// Specifies the format of the data
        [DataMember (Name = "format")] public string Format;
        //   Acceptable values:
        //     jpeg, png
        /// Compressed image buffer
        [DataMember (Name = "data")] public byte[] Data;
    
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
        public CompressedImage(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Format = b.DeserializeString();
            Data = b.DeserializeStructArray<byte>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new CompressedImage(ref b);
        
        public CompressedImage RosDeserialize(ref ReadBuffer b) => new CompressedImage(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
                "H4sIAAAAAAAAE62UTW/UMBCG7/4VI+2hLaVFgguqxAFRAT0gIbU3hKpZezYxcmzXdrYNv57Xzn60lEIP" +
                "RFl5bc88850FXfU20yA5cyekgy9sfSbG3yEmHIshO+BOqc/CRhL187J5FrQ5LhaQwkOk3IfRGVoKsb4Z" +
                "bbbFBt/uKaw2MPrTs2OtEg9ybc09VIjFanbzVeVorImfAoVkO+ur3KzwGKTFF5j6J+n4bqscg/WFSqDS" +
                "C4HfF4KJuvlrTMfTQ4AJt/55ij8fKuIXqvno2LcU7Akql2R9R6uQBi57xGUUbVdWcpPd3G40DZcngyZ6" +
                "r7XEwksntGY3Sj57WpboR5TuJUXfqRFevv32vdHvCX34rZtoOa5WkpR6958f9eXy0xnlYq6H3OVXc0Mp" +
                "ZKKwN5wMWr1wcw7ZoB5VlHTiZC2OWvfCwXZbpij5FIptPvB24tElzk001ihQB0zIMHo0U5F992/1oYka" +
                "M0VO6LfRcYJ8SMb6Kt6astLxZrkZxWuhi/OzOoBZ9FgsHJpA0Ek418penFPL7ZvXVUEtrm7DCbbSPRi9" +
                "0qO+cFbuttlmFG5BL+bgTsFGcgRWTKbDdnaNbT4iGIELEoPu6RCef51KH+YmXXOyrREAxuQ4UA+q0sHR" +
                "PbJvaM8+bPEzcW/jOVi/49aYTnrUzNXo89ghgRCMKaytgehyahDtLOaYnF0mTpNqH5lmUi0+tsEvtXyt" +
                "Ilg556AtCmDo1pZ+NzWbz41SvwDx0C3xDwUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
