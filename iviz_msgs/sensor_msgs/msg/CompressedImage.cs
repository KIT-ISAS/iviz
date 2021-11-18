/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/CompressedImage")]
    public sealed class CompressedImage : IDeserializable<CompressedImage>, IMessage
    {
        // This message contains a compressed image
        [DataMember (Name = "header")] public StdMsgs.Header Header; // Header timestamp should be acquisition time of image
        // Header frame_id should be optical frame of camera
        // origin of frame should be optical center of camera
        // +x should point to the right in the image
        // +y should point down in the image
        // +z should point into to plane of the image
        [DataMember (Name = "format")] public string Format; // Specifies the format of the data
        //   Acceptable values:
        //     jpeg, png
        [DataMember (Name = "data")] public byte[] Data; // Compressed image buffer
    
        /// <summary> Constructor for empty message. </summary>
        public CompressedImage()
        {
            Format = string.Empty;
            Data = System.Array.Empty<byte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public CompressedImage(in StdMsgs.Header Header, string Format, byte[] Data)
        {
            this.Header = Header;
            this.Format = Format;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal CompressedImage(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Format = b.DeserializeString();
            Data = b.DeserializeStructArray<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new CompressedImage(ref b);
        }
        
        CompressedImage IDeserializable<CompressedImage>.RosDeserialize(ref Buffer b)
        {
            return new CompressedImage(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Format);
            b.SerializeStructArray(Data, 0);
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/CompressedImage";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8f7a12909da2c9d3332d540a0977563f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq2UTW/UMBCG75H6H0baQ1tKiwQXtBIHRAX0gITU3hBazdqziZFju7azbfj1vHay3S6o" +
                "0ANRVl7bM898Z0E3nUnUS0rcCinvMhuXiPG3DxHHosn0uGuaz8JaInXTMj8Lmo+zASRzHyh1frCa1kKs" +
                "bgeTTDbe1Xvymxm2Uz94HlibyL2sjH6E8iEbxXa6KhyFNfJTIB9Na1yRmxT+BClxGab+STq73ykHb1ym" +
                "7Cl3QuB3mWCibP4a09l4CND+zj1P8eehIn6w7SlYdjUFe0KTcjSupY2PPec94jqIMhsjqcrOt7Om5vxk" +
                "0ETvlZKQeW2FtmwHScunZYl+BGlfUnBtM8DLt9++V/ojoQ+/dROth81GYnPUvPvPz1Hz5frTklLWqz61" +
                "6dXUUkcNkpHZaY4a3Z65+oeEUIdCSjy3shVLtYHhY73NY5B00cwjgrcVh0axdqShBIJSYEj6waGfsuwH" +
                "YKcPTZSZKXBEyw2WI+R91MYV8dqXhY43ye0gTgldXS7LDCZRQzZwaARBReFUint1STW9b14XhWZxc+fP" +
                "sZX2YPpyhxLDWbnfJZxRuwW9mIK7ABvZEVjRiU7q2QrbdEowooQkeNXRCTz/OuauDC56ZcvR1F4AGMNj" +
                "QT0uSsenj8jF7SU5dn6Hn4h7G8/BFsrELTGdd6iZLdGnoUUCIRii3xoN0fVYIcoajDJZs44cx6Z+Z6rJ" +
                "ZvGxzn4u5asVwcopeWVQAE13JncPgzN/cdCQvwD9PijrEwUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
