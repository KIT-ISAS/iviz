/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/Image")]
    public sealed class Image : IDeserializable<Image>, IMessage
    {
        // This message contains an uncompressed image
        // (0, 0) is at top-left corner of image
        //
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; } // Header timestamp should be acquisition time of image
        // Header frame_id should be optical frame of camera
        // origin of frame should be optical center of camera
        // +x should point to the right in the image
        // +y should point down in the image
        // +z should point into to plane of the image
        // If the frame_id here and the frame_id of the CameraInfo
        // message associated with the image conflict
        // the behavior is undefined
        [DataMember (Name = "height")] public uint Height { get; set; } // image height, that is, number of rows
        [DataMember (Name = "width")] public uint Width { get; set; } // image width, that is, number of columns
        // The legal values for encoding are in file src/image_encodings.cpp
        // If you want to standardize a new string format, join
        // ros-users@lists.sourceforge.net and send an email proposing a new encoding.
        [DataMember (Name = "encoding")] public string Encoding { get; set; } // Encoding of pixels -- channel meaning, ordering, size
        // taken from the list of strings in include/sensor_msgs/image_encodings.h
        [DataMember (Name = "is_bigendian")] public byte IsBigendian { get; set; } // is this data bigendian?
        [DataMember (Name = "step")] public uint Step { get; set; } // Full row length in bytes
        [DataMember (Name = "data")] public byte[] Data { get; set; } // actual matrix data, size is (step * rows)
    
        /// <summary> Constructor for empty message. </summary>
        public Image()
        {
            Encoding = string.Empty;
            Data = System.Array.Empty<byte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Image(in StdMsgs.Header Header, uint Height, uint Width, string Encoding, byte IsBigendian, uint Step, byte[] Data)
        {
            this.Header = Header;
            this.Height = Height;
            this.Width = Width;
            this.Encoding = Encoding;
            this.IsBigendian = IsBigendian;
            this.Step = Step;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Image(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Height = b.Deserialize<uint>();
            Width = b.Deserialize<uint>();
            Encoding = b.DeserializeString();
            IsBigendian = b.Deserialize<byte>();
            Step = b.Deserialize<uint>();
            Data = b.DeserializeStructArray<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Image(ref b);
        }
        
        Image IDeserializable<Image>.RosDeserialize(ref Buffer b)
        {
            return new Image(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Height);
            b.Serialize(Width);
            b.Serialize(Encoding);
            b.Serialize(IsBigendian);
            b.Serialize(Step);
            b.SerializeStructArray(Data, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Encoding is null) throw new System.NullReferenceException(nameof(Encoding));
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 21;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Encoding);
                size += 1 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/Image";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "060021388200f6f0f447d0fcd9c64743";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
