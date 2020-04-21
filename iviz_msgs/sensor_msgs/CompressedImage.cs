
namespace Iviz.Msgs.sensor_msgs
{
    public sealed class CompressedImage : IMessage 
    {
        // This message contains a compressed image
        
        public std_msgs.Header header; // Header timestamp should be acquisition time of image
        // Header frame_id should be optical frame of camera
        // origin of frame should be optical center of camera
        // +x should point to the right in the image
        // +y should point down in the image
        // +z should point into to plane of the image
        
        public string format; // Specifies the format of the data
        //   Acceptable values:
        //     jpeg, png
        public byte[] data; // Compressed image buffer

        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/CompressedImage";

        public IMessage Create() => new CompressedImage();

        public int GetLength()
        {
            int size = 8;
            size += header.GetLength();
            size += format.Length;
            size += 1 * data.Length;
            return size;
        }

        /// <summary> Constructor for empty message. </summary>
        public CompressedImage()
        {
            header = new std_msgs.Header();
            format = "";
            data = new byte[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out format, ref ptr, end);
            BuiltIns.Deserialize(out data, ref ptr, end, 0);
        }

        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(format, ref ptr, end);
            BuiltIns.Serialize(data, ref ptr, end, 0);
        }

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "8f7a12909da2c9d3332d540a0977563f";

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
