/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Image : IDeserializable<Image>, IMessage
    {
        // This message contains an uncompressed image
        // (0, 0) is at top-left corner of image
        //
        [DataMember (Name = "header")] public StdMsgs.Header Header; // Header timestamp should be acquisition time of image
        // Header frame_id should be optical frame of camera
        // origin of frame should be optical center of camera
        // +x should point to the right in the image
        // +y should point down in the image
        // +z should point into to plane of the image
        // If the frame_id here and the frame_id of the CameraInfo
        // message associated with the image conflict
        // the behavior is undefined
        [DataMember (Name = "height")] public uint Height; // image height, that is, number of rows
        [DataMember (Name = "width")] public uint Width; // image width, that is, number of columns
        // The legal values for encoding are in file src/image_encodings.cpp
        // If you want to standardize a new string format, join
        // ros-users@lists.sourceforge.net and send an email proposing a new encoding.
        [DataMember (Name = "encoding")] public string Encoding; // Encoding of pixels -- channel meaning, ordering, size
        // taken from the list of strings in include/sensor_msgs/image_encodings.h
        [DataMember (Name = "is_bigendian")] public byte IsBigendian; // is this data bigendian?
        [DataMember (Name = "step")] public uint Step; // Full row length in bytes
        [DataMember (Name = "data")] public byte[] Data; // actual matrix data, size is (step * rows)
    
        /// Constructor for empty message.
        public Image()
        {
            Encoding = string.Empty;
            Data = System.Array.Empty<byte>();
        }
        
        /// Explicit constructor.
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
        
        /// Constructor with buffer.
        internal Image(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Height = b.Deserialize<uint>();
            Width = b.Deserialize<uint>();
            Encoding = b.DeserializeString();
            IsBigendian = b.Deserialize<byte>();
            Step = b.Deserialize<uint>();
            Data = b.DeserializeStructArray<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Image(ref b);
        
        Image IDeserializable<Image>.RosDeserialize(ref Buffer b) => new Image(ref b);
    
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
                size += BuiltIns.GetStringSize(Encoding);
                size += Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/Image";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "060021388200f6f0f447d0fcd9c64743";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
