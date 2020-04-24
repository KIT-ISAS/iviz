namespace Iviz.Msgs.sensor_msgs
{
    public sealed class Image : IMessage
    {
        // This message contains an uncompressed image
        // (0, 0) is at top-left corner of image
        //
        
        public std_msgs.Header header; // Header timestamp should be acquisition time of image
        // Header frame_id should be optical frame of camera
        // origin of frame should be optical center of camera
        // +x should point to the right in the image
        // +y should point down in the image
        // +z should point into to plane of the image
        // If the frame_id here and the frame_id of the CameraInfo
        // message associated with the image conflict
        // the behavior is undefined
        
        public uint height; // image height, that is, number of rows
        public uint width; // image width, that is, number of columns
        
        // The legal values for encoding are in file src/image_encodings.cpp
        // If you want to standardize a new string format, join
        // ros-users@lists.sourceforge.net and send an email proposing a new encoding.
        
        public string encoding; // Encoding of pixels -- channel meaning, ordering, size
        // taken from the list of strings in include/sensor_msgs/image_encodings.h
        
        public byte is_bigendian; // is this data bigendian?
        public uint step; // Full row length in bytes
        public byte[] data; // actual matrix data, size is (step * rows)
    
        /// <summary> Constructor for empty message. </summary>
        public Image()
        {
            header = new std_msgs.Header();
            encoding = "";
            data = System.Array.Empty<byte>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out height, ref ptr, end);
            BuiltIns.Deserialize(out width, ref ptr, end);
            BuiltIns.Deserialize(out encoding, ref ptr, end);
            BuiltIns.Deserialize(out is_bigendian, ref ptr, end);
            BuiltIns.Deserialize(out step, ref ptr, end);
            BuiltIns.Deserialize(out data, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(height, ref ptr, end);
            BuiltIns.Serialize(width, ref ptr, end);
            BuiltIns.Serialize(encoding, ref ptr, end);
            BuiltIns.Serialize(is_bigendian, ref ptr, end);
            BuiltIns.Serialize(step, ref ptr, end);
            BuiltIns.Serialize(data, ref ptr, end, 0);
        }
    
        public int GetLength()
        {
            int size = 21;
            size += header.GetLength();
            size += encoding.Length;
            size += 1 * data.Length;
            return size;
        }
    
        public IMessage Create() => new Image();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/Image";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "060021388200f6f0f447d0fcd9c64743";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAAE61VTW8bNxC981cQ0CF2YilBewkMFCnQfNSHAgXiW1EII3K0y4RLrvkhefPr+0hqJQu2" +
                "0xyykCUvyXnz8d4MF/K2N1EOHCN1LJV3iYyLkpzMTvlhDNhhLc2AbbGQF2+u5JtLCRNKMvlxaXmbYBYc" +
                "B+m38zkh/mTSWOrbz+FZyMNyMvCYaBhl7H22Wm5YkrrLJppkvKv7Jzj51HPE2gYaeG30Ayg/JqPItq2C" +
                "o/Ab6DkgH0xnXDnXDB4DKXapJfh9pFf3s/HojSsVkqlnCfw+SbgoL9/N6dV0DqD93v2Y4bdzQ/z54n60" +
                "5GoJ/hfhpp05lrPnAFKcPl89IP1Ry3Djtv45uFlSFKNXhhJEtDepP8VRxLa1RqXnEMrJDfe0Mz4UxWWn" +
                "eWscayEy0vv1F4RYC3syacBt+QoAEKmJV9LlYdPoC34fZ+u90YjnkXVdftJYeZsHFwX64BaxWe4gjR3Z" +
                "zFFuESOjY7RxnSRUDqRtjYWYgnpdgdfzdlypcRS14JPPck9NKOgHpylo8w1Fk473WAkFDdADIZ0vIBZm" +
                "wcdljhzi79bEFFfR56AYhzpeOU6Vssj4Qg/zQMbKMfjRxxpYxZ0DWQlxcHGMfC7Fh3kBaY/mnm2Uy6VU" +
                "PTnHFtySw+YVOgcdWP+LCPtpIguT9JVRjuCHSmqJuwA357GUyjhls+bXCDz6sB5iFx9VrW+8vwUr643p" +
                "kKJBio25CGB8aUokj3vvZqZj4vEsoI/Z2qIFcOg6iAARbKbETRpv//m3AT0wIJUyyAYPwdzX3ZZy8XxR" +
                "4V9WbV0K8dtPfsRfnz9dIwXdqtKmHnTw+aAXsJGoxls02EP6HDCWd+Cpjlg0Xt1N08hxVbWLoPFBldDD" +
                "1k4ylxEPCWLgD9lh4iU+jejZHpYoE8mRAoZithRwHgIwrhyvA6Kg4xP5LoM2ljfvr0uXR1Y5GQQ0FZ4D" +
                "U9XizXt55IfvxOJ275d45e7sfji0oeT7+SqieA0fL1tyK2CjOAwvujKBtTVe4yXoKSHw6FUvLxD531Pq" +
                "fZukOwqGNrayh/FugfqiGL24fIDsKrQj52f4hnjy8SOw7ohbclqigbQt2cfcUZ1qaM6d0Ti6mSqIsgaX" +
                "DXpkEyhMot6E1aVYfKy300no5RY+n65zP8/jWoj/ACoI2BHhBwAA";
                
    }
}
