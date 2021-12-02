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
            b.SerializeStructArray(Data);
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
                "H4sIAAAAAAAACq1VTW8bNxC9768goEPsxFKC9hIYKFqgaVofChSIb0UhjMjRLlsuueaQkpVf30dSK1lw" +
                "nObQhSx5Sc6bj/dmuFD3gxU1sgj1rHTwiawXRV5lr8M4ReywUXbEdrdQV+9u1LtrBRNKKoVp6XibYBY9" +
                "RxW287mu+43JYGloP8dnoY7LycJjonFSMoTsjNqwIv2Qrdhkg6/7Z7jZ/OI5YW0jjby25glUmJLV5NpW" +
                "wdH4jfQSUIi2t76cawbPgTT71BL8OtKbx9l4CtaXCqk0sAL+kBRclJev5vTmcAlgwt5/m+HnS0P8wXdQ" +
                "kyNfS/CfCHftzKmcA0eQ4s3l6hHp51qGO78NL8HNkiKRoC0liGhv03COo4ht66xOLyGUkxseaGdDLIrL" +
                "3vDWejZdl5He998hxFrYs0kDbss3AIBIrdwon8dNoy+GvczWe2sQzzPruvxFYx1cHr106IN7xOa4hzR2" +
                "5DKL2iJGRscY63tFqBxI21oHMUX9tgKv521Z6WnqasEPIas9NaGgH7yhaOxnFE153mMlFjRAj4R0/gax" +
                "MItBllk4yk/OSpKVhBw141DPK8+pUiaML/Qwj2SdmmKYgtTAKu4cyKrrji5Okc+l+GVeQNqTfWQnarlU" +
                "eiDv2YFb8ti8QeegA+t/grC/TGRhkv5hlCOGsZJa4i7AzbmUUlmvXTb8FoFLiOtRenlWtaHx/h6srDe2" +
                "R4oWKTbmBMD4MpRInfZ+nJmWxNNFQB+zc0UL4ND3EAEi2BwSN2m8//OvBvTEgHTKIBs8RPtYd1vKxfNV" +
                "hX9dtXXddT/8z0/3+6dfb5GCaVVpUw86+HTUC9hIVOMtGhwgfY4YyzvwVEcsGq/upsPEsqraRdD4oEro" +
                "YecOCnJCmwcIfByzx8RLfB7Rsz0sUSZSE0UMxewo4jwEYH05XgdEQcdH+CGDNlZ3H25LlwvrnCwCOhSe" +
                "I1PV4t0HdeKHH7rF/T4s8cr9xf1wbEPFj/NVRHILH69bcitgozgML6YygbU1XuUa9HiEwFPQg7pC5H8c" +
                "0lCuFghwR9HSBs0JYIx3B9RXxejV9RPkEvat8uTDDN8Qzz6+BbagNNyS0xINZFzJXnKPAuIgmnNnDY5u" +
                "DhVEO4vLBj2yiRQPXb0Jq8tu8bHeTmehl1v4crrO/TyP6677FyoI2BHhBwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
