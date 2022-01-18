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
        /// Header timestamp should be acquisition time of image
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Header frame_id should be optical frame of camera
        // origin of frame should be optical center of camera
        // +x should point to the right in the image
        // +y should point down in the image
        // +z should point into to plane of the image
        // If the frame_id here and the frame_id of the CameraInfo
        // message associated with the image conflict
        // the behavior is undefined
        /// image height, that is, number of rows
        [DataMember (Name = "height")] public uint Height;
        /// image width, that is, number of columns
        [DataMember (Name = "width")] public uint Width;
        // The legal values for encoding are in file src/image_encodings.cpp
        // If you want to standardize a new string format, join
        // ros-users@lists.sourceforge.net and send an email proposing a new encoding.
        /// Encoding of pixels -- channel meaning, ordering, size
        [DataMember (Name = "encoding")] public string Encoding;
        // taken from the list of strings in include/sensor_msgs/image_encodings.h
        /// is this data bigendian?
        [DataMember (Name = "is_bigendian")] public byte IsBigendian;
        /// Full row length in bytes
        [DataMember (Name = "step")] public uint Step;
        //uint8[] data          # actual matrix data, size is (step * rows)
        /// [Rent]
        [DataMember (Name = "data")] public System.Memory<byte> Data;
    
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
        public Image(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Height = b.Deserialize<uint>();
            Width = b.Deserialize<uint>();
            Encoding = b.DeserializeString();
            IsBigendian = b.Deserialize<byte>();
            Step = b.Deserialize<uint>();
            Data = b.DeserializeStructRent<byte>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Image(ref b);
        
        public Image RosDeserialize(ref ReadBuffer b) => new Image(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Height);
            b.Serialize(Width);
            b.Serialize(Encoding);
            b.Serialize(IsBigendian);
            b.Serialize(Step);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Encoding is null) throw new System.NullReferenceException(nameof(Encoding));
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
                "H4sIAAAAAAAAE61VTW8bNxA9l7+CgA6xE0sJmkthIGiBpml9KFA0vgWBMCJHu2y55Jofkje/vo+kVrJg" +
                "O80hC1nykpw3H+/NcCFvexPlwDFSx1J5l8i4KMnJ7JQfxoAd1tIM2BYLefHmSr65lDChJJMfl5a3CWbB" +
                "cZB+O58T4g8mjaW+/RyehTwsJwOPiYZRxt5nq+WGJam7bKJJxru6f4KTTz1HrG2ggddGP4DyYzKKbNsq" +
                "OAq/gZ4D8sF0xpVzzeAxkGKXWoJfR3p1PxuP3rhSIZl6lsDvk4SL8vLVnF5N5wDa7923GX45N8SfL+5H" +
                "S66W4H8RbtqZYzl7DiDF6fPVA9KvtQw3buufg5slRTF6ZShBRHuT+lMcRWxba1R6DqGc3HBPO+NDUVx2" +
                "mrfGsRYiI723PyLEWtiTSQNuy1cAgEhNvJIuD5tGX/D7OFvvjUY8j6zr8pPGyts8uCjQB7eIzXIHaezI" +
                "Zo5yixgZHaON6yShciBtayzEFNTrCryet+NKjaOoBZ98lntqQkE/OE1Bmy8omnS8x0ooaIAeCOn8A2Jh" +
                "Fnxc5sgh/mJNTHEVfQ6KcajjleNUKYuML/QwD2SsHIMffayBVdw5kJUQBxfHyOdS/DYvIO3R3LONcrmU" +
                "qifn2IJbcti8QuegA+t/EWE/TWRhkv5llCP4oZJa4i7AzXkspTJO2az5NQKPPqyH2MVHVesb7z+BlfXG" +
                "dEjRIMXGXAQwvjQlkse9n2emY+LxLKAP2dqiBXDoOogAEWymxFEsqodPnxvSAwtSKYNtEBHMfd1tORfX" +
                "FxX/ZRXXpXiI8MNs/ulvTJDPQrz7zo/48+Pv18hPt5K1kQiRfDyICVQlqrkUgfboCw6Y2TuQWOcvurLu" +
                "pmnkuKrCRkL4oIRocGsnmcv8hz5xGwzZYRwmPs3v2R6WqCHJkQImZrYUcB7qMK4cr9OjoOMT+S6DU5Y3" +
                "76/LCIiscjIIaCoiCExVqDfv5ZE8vhOL271f4pW7s8vj0KOS7+d7iuI1fLxsya2AjeIwvOjKEtbWeI2X" +
                "oK6EwKNXvbxA5H9NqfdtzO4oGNrYyixmvwXqi2L04vIBsqvQjpyf4Rviyce3wLojbslpie7StmQfc0d1" +
                "5KFzd0bj6GaqIMoa6AgNtAkUJlGvyepSLD7Uq+vUBeWKPh+9c7PPs1yI/wBsocMc/gcAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
