
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
            data = System.Array.Empty<0>();
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
                
    }
}
