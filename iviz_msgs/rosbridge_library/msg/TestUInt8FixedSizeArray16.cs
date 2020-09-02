/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/TestUInt8FixedSizeArray16")]
    public sealed class TestUInt8FixedSizeArray16 : IMessage
    {
        [DataMember (Name = "data")] public byte[/*16*/] Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestUInt8FixedSizeArray16()
        {
            Data = new byte[16];
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestUInt8FixedSizeArray16(byte[] Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestUInt8FixedSizeArray16(Buffer b)
        {
            Data = b.DeserializeStructArray<byte>(16);
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new TestUInt8FixedSizeArray16(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeStructArray(Data, 16);
        }
        
        public void RosValidate()
        {
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
            if (Data.Length != 16) throw new System.IndexOutOfRangeException();
        }
    
        public int RosMessageLength => 16;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/TestUInt8FixedSizeArray16";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a4e84d0a73514dfe9696b4796e8755e7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCuxiDY0i1VISSxJ5OICANuquFIQAAAA";
                
    }
}
