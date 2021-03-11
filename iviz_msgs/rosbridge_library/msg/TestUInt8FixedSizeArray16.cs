/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [Preserve, DataContract (Name = "rosbridge_library/TestUInt8FixedSizeArray16")]
    public sealed class TestUInt8FixedSizeArray16 : IDeserializable<TestUInt8FixedSizeArray16>, IMessage
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
        public TestUInt8FixedSizeArray16(ref Buffer b)
        {
            Data = b.DeserializeStructArray<byte>(16);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestUInt8FixedSizeArray16(ref b);
        }
        
        TestUInt8FixedSizeArray16 IDeserializable<TestUInt8FixedSizeArray16>.RosDeserialize(ref Buffer b)
        {
            return new TestUInt8FixedSizeArray16(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Data, 16);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
            if (Data.Length != 16) throw new System.IndexOutOfRangeException();
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 16;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/TestUInt8FixedSizeArray16";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a4e84d0a73514dfe9696b4796e8755e7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACivNzCuxiDY0i1VISSxJ5OLlAgCiI8VsEQAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
