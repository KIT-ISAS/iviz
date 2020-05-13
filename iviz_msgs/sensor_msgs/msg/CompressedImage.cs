using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    [DataContract]
    public sealed class CompressedImage : IMessage
    {
        // This message contains a compressed image
        
        [DataMember] public std_msgs.Header header { get; set; } // Header timestamp should be acquisition time of image
        // Header frame_id should be optical frame of camera
        // origin of frame should be optical center of camera
        // +x should point to the right in the image
        // +y should point down in the image
        // +z should point into to plane of the image
        
        [DataMember] public string format { get; set; } // Specifies the format of the data
        //   Acceptable values:
        //     jpeg, png
        [DataMember] public byte[] data { get; set; } // Compressed image buffer
    
        /// <summary> Constructor for empty message. </summary>
        public CompressedImage()
        {
            header = new std_msgs.Header();
            format = "";
            data = System.Array.Empty<byte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public CompressedImage(std_msgs.Header header, string format, byte[] data)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.format = format ?? throw new System.ArgumentNullException(nameof(format));
            this.data = data ?? throw new System.ArgumentNullException(nameof(data));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal CompressedImage(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.format = b.DeserializeString();
            this.data = b.DeserializeStructArray<byte>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new CompressedImage(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.header);
            b.Serialize(this.format);
            b.SerializeStructArray(this.data, 0);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            header.Validate();
            if (format is null) throw new System.NullReferenceException();
            if (data is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(format);
                size += 1 * data.Length;
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/CompressedImage";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8f7a12909da2c9d3332d540a0977563f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
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
                
    }
}
