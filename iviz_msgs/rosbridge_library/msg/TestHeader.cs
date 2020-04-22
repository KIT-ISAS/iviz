
namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestHeader : IMessage
    {
        public std_msgs.Header header;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rosbridge_library/TestHeader";
    
        public IMessage Create() => new TestHeader();
    
        public int GetLength()
        {
            int size = 0;
            size += header.GetLength();
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public TestHeader()
        {
            header = new std_msgs.Header();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "d7be0bb39af8fb9129d5a76e6b63a290";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACq2RTWvDMAyG74b+B0EP7QbtYLsVdhv7OAwG7b2otpYIHDuzlHb595NT9nXbYcFgHL/v" +
                "88qSaNh30sjVI2GgAu20zdztP38z97x92ID8jpu5OWwVU8ASoCPFgIrwmq0Obloqq0hHiubCrqcA062O" +
                "PcnajLuWBWw1lKhgjCMMYiLN4HPXDYk9KoFyR7/85uQECD0WZT9ELKbPJXCq8teCHVW6LaG3gZIneLrb" +
                "mCYJ+UHZChqN4AuhcGrsEtzASW+uq8HNd6e8siM11s2vcNAWtRZL730hqXWibCzj8vy4tbGtO2QpQWA5" +
                "/dvbUS7AQjwB9dm3sLTKX0ZtczIgwREL4yFSBXvrgFEX1bS4+EGuZW8gYcqf+DPxO+Mv2Eo5c+ubVq3N" +
                "LNbXy9BYA03Yl3zkYNLDOEF8ZEoKkQ8Fy+iq6xzp5ve1xyYy1zQR21Eke7YBBDixtk60VPo0jT0HN3Mf" +
                "KsIXf6kCAAA=";
                
    }
}
