/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [Preserve, DataContract (Name = "rosbridge_library/TestHeader")]
    public sealed class TestHeader : IDeserializable<TestHeader>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestHeader()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestHeader(in StdMsgs.Header Header)
        {
            this.Header = Header;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestHeader(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestHeader(ref b);
        }
        
        TestHeader IDeserializable<TestHeader>.RosDeserialize(ref Buffer b)
        {
            return new TestHeader(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/TestHeader";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d7be0bb39af8fb9129d5a76e6b63a290";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq2RTWvDMAyG74b+B0EP7QbtYLsVdhv7OAwG7b2otpYIHDuzlHb595NT9nXbYcFgHL/v" +
                "88qSaNh30sjVI2GgAu20zdztP38z97x92ID8jpu5OWwVU8ASoCPFgIrwmq0Obloqq0hHiubCrqcA062O" +
                "PcnajLuWBWw1lKhgjCMMYiLN4HPXDYk9KoFyR7/85uQECD0WZT9ELKbPJXCq8teCHVW6LaG3gZIneLrb" +
                "mCYJ+UHZChqN4AuhcGrsEtzASW+uq8HNd6e8siM11s2vcNAWtRZL730hqXWibCzj8vy4tbGtO2QpQWA5" +
                "/dvbUS7AQjwB9dm3sLTKX0ZtczIgwREL4yFSBXvrgFEX1bS4+EGuZW8gYcqf+DPxO+Mv2Eo5c+ubVq3N" +
                "LNbXy9BYA03Yl3zkYNLDOEF8ZEoKkQ8Fy+iq6xzp5ve1xyYy1zQR21Eke7YBBDixtk60VPo0jT0HN3Mf" +
                "KsIXf6kCAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
