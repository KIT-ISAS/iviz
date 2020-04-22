
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/Image";
    
        public IMessage Create() => new Image();
    
        public int GetLength()
        {
            int size = 21;
            size += header.GetLength();
            size += encoding.Length;
            size += 1 * data.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public Image()
        {
            header = new std_msgs.Header();
            encoding = "";
            data = System.Array.Empty<0>();
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
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "060021388200f6f0f447d0fcd9c64743";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACq1VTW8bNxC9L+D/MIAOsRNLCdpLYKBogaRpfQgQIL4VhTDijnaZcsk1PyRvfn0fSa1k" +
                "wXGaQxey5CU5bz7em+GC7nodaJAQuBNSzkbWNhBbSla5YfTYkZb0gO1mQZdvrunNFcGEI0U3Lo1sI8y8" +
                "FU9uO59rmj+FWyz19efwLOiwHDU8Rh5GCr1LpqWNEKv7pIOO2tmyf4Kbzc+eI9bW8yBr3T6CcmPUik3d" +
                "yjgKv56fA3Jed9rmc9XgKZASG2uC30d69TAbj07bXCGKvRDw+0hwkV++m9Or6RygdXv7Y4Zfzw3xB9+O" +
                "RsO2lOA/EW7rmWM5e/Egxbbnqwekd6UMt3brnoObJcUhOKU5QkR7HftTHFlsW6NVfA4hn9xIzzvtfFZc" +
                "sq1stZW2aRLS+/knhFgKezKpwHX5GgAQqQ7XZNOwqfR5tw+z9V63iOeJdVn+prFyJg02NOiDO8RmpIM0" +
                "dmySBNoiRkHHtNp2xKgcSNtqAzF59boAr+ftsFLj2JSCTy7RnqtQ0A+2Zd/qrygaWdljxWc0QA+MdL6A" +
                "WJh5F5YpiA+/GR1iWAWXvBIc6mRlJRbKguALPSwDa0Ojd6MLJbCCOweyapqDi2Pkcyl+nxeQ9qgfxARa" +
                "Lkn1bK0YcMsWm9foHHRg+S8g7G8TmZnkfwTl8G4opOa4M3B1HnKptFUmtfIagQfn10PowpOq9ZX3t2Bl" +
                "vdEdUtRIsTIXAIyvliPTce/XmekQZTwL6EMyJmsBHNoOIkAEmylKlcbbv/6uQI8MWMUEssGD1w9lt6ac" +
                "PV8W+JdFW1fNRfPL//xcNB8//3GDJNpalzr3LiCFzwfJgJDIJeQswx7qF4/JvANVZcqi98punEYJqyJf" +
                "xI0PCoU2NmYiKAqd7qDxYUgWQy/KaUrP9rBEpZhG9piLybDHeWhA23y8zIiMjk+Q+wTmhG7f3+RGD6JS" +
                "1AhoylR74SLH2/d0pEjum8Xd3i3xKt3ZFXHoRJKH+TbicAMfL2tyK2CjOgIvbSEDa2u8hiswZBGCjE71" +
                "dInIP02xz7cLNLhjr3mD/gQwJrwB6ots9OLqEXIO+4YsWzfDV8STjx+BzSgVN+e0RA+1JmcfUocC4iD6" +
                "c6dbHN1MBUQZjfsGbbLx7KemXIbFZbP4UC6ok9bzRXw+YOeWnic2BPkvbZMmUOUHAAA=";
                
    }
}
