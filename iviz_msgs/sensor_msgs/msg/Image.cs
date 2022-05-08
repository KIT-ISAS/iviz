/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class Image : IDeserializable<Image>, IMessage
    {
        // This message contains an uncompressed image
        // (0, 0) is at top-left corner of image
        //
        /// <summary> Header timestamp should be acquisition time of image </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Header frame_id should be optical frame of camera
        // origin of frame should be optical center of camera
        // +x should point to the right in the image
        // +y should point down in the image
        // +z should point into to plane of the image
        // If the frame_id here and the frame_id of the CameraInfo
        // message associated with the image conflict
        // the behavior is undefined
        /// <summary> Image height, that is, number of rows </summary>
        [DataMember (Name = "height")] public uint Height;
        /// <summary> Image width, that is, number of columns </summary>
        [DataMember (Name = "width")] public uint Width;
        // The legal values for encoding are in file src/image_encodings.cpp
        // If you want to standardize a new string format, join
        // ros-users@lists.sourceforge.net and send an email proposing a new encoding.
        /// <summary> Encoding of pixels -- channel meaning, ordering, size </summary>
        [DataMember (Name = "encoding")] public string Encoding;
        // taken from the list of strings in include/sensor_msgs/image_encodings.h
        /// <summary> Is this data bigendian? </summary>
        [DataMember (Name = "is_bigendian")] public byte IsBigendian;
        /// <summary> Full row length in bytes </summary>
        [DataMember (Name = "step")] public uint Step;
        /// <summary> [Rent] actual matrix data, size is (step * rows) </summary>
        [DataMember (Name = "data")] public Tools.SharedRent<byte> Data;
    
        /// Constructor for empty message.
        public Image()
        {
            Encoding = "";
            Data = Tools.SharedRent<byte>.Empty;
        }
        
        /// Constructor with buffer.
        public Image(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Height);
            b.Deserialize(out Width);
            b.DeserializeString(out Encoding);
            b.Deserialize(out IsBigendian);
            b.Deserialize(out Step);
            b.DeserializeStructRent(out Data);
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
            if (Encoding is null) BuiltIns.ThrowNullReference();
            if (Data is null) BuiltIns.ThrowNullReference();
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/Image";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "060021388200f6f0f447d0fcd9c64743";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61VTW8bNxC9768goEPsxFKC9hIYCBKgaVofChSNb0EgjJajXbZccs0hJW9+fR9JrWTB" +
                "dppDF7LkJTlvPt6b4ULd9kbUwCLUsWq9i2ScKHIqudYPY8AOa2UGbDcLdfHmSr25VDChqKIfl5a3EWbB" +
                "cVB+O59rmt+ZNJb6+nN4FuqwHA08RhpGJb1PVqsNK2rvkhETjXdl/wSnnnqOWNtAA6+NfgDlx2hasnUr" +
                "47T4DfQckA+mMy6fqwaPgVp2sSb4faRX97Px6I3LFVKxZwX8Piq4yC/fzenVdA6g/d79mOG3c0P8+ex+" +
                "tORKCf4T4aaeOZaz5wBSnD5fPSD9Uspw47b+ObhZUiTiW0MRItqb2J/iyGLbWtPG5xDyyQ33tDM+ZMUl" +
                "p3lrHOumSUjv558QYinsyaQC1+UrAECkRq6US8Om0hf8XmbrvdGI55F1WX7SuPU2DU4a9MEtYrPcQRo7" +
                "solFbREjo2O0cZ0iVA6kbY2FmEL7ugCv521ZtePYlIJPPqk9VaGgH5ymoM03FE053mMlZDRAD4R0/gax" +
                "MAtelkk4yAdrJMpKfAot41DHK8exUCaML/QwD2SsGoMfvZTACu4cyKppDi6Okc+l+HVeQNqjuWcrarlU" +
                "bU/OsQW35LB5hc5BB5b/BGE/TWRmkv5hlCP4oZCa487A1bnkUhnX2qT5NQIXH9aDdPKoan3l/S1YWW9M" +
                "hxQNUqzMCYDxpSmSOu69n5mWyONZQJ+StVkL4NB1EAEi2EyRqzTefvlagR4YfPkLE+ArZlRM4Bx0BHNf" +
                "DtXMcwAXxcvLIrHLpnn3Pz/NH59/u0YmuhanDj/I4fNBNiAlUgk7S7FHB3DAdN6BrjJp0X9lN04jy6pI" +
                "GEHjg2Khla2dVMqTHkrE3B+Sw+CLfJrUsz0sUS1SIwXMxmQp4Dx0YFw+XuZERsdH+C6BPVY3H69zswu3" +
                "KRoENGW6A1OR5M1HdaSJ75rF7d4v8crd2TVx6EbF9/ONRHINHy9rcitgozgML7owgbU1XuUS9OQQePRt" +
                "ry4Q+Z9T7H0dqDsKhja2sIcpb4H6Ihu9uHyA7Aq0I+dn+Ip48vEjsO6Im3Naoo+0zdlL6qgMN/Tozmgc" +
                "3UwFpLUGikOrbAKFqSkXYnHZLD6VS+qk93wZnw/Zua3nqd00/wIFjZgk6AcAAA==";
                
    
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            Data.Dispose();
        }
    }
}
