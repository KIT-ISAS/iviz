/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract]
    public sealed class TestUInt8FixedSizeArray16 : IDeserializable<TestUInt8FixedSizeArray16>, IMessage
    {
        [DataMember (Name = "data")] public byte[/*16*/] Data;
    
        /// Constructor for empty message.
        public TestUInt8FixedSizeArray16()
        {
            Data = new byte[16];
        }
        
        /// Explicit constructor.
        public TestUInt8FixedSizeArray16(byte[] Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public TestUInt8FixedSizeArray16(ref ReadBuffer b)
        {
            b.DeserializeStructArray(16, out Data);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestUInt8FixedSizeArray16(ref b);
        
        public TestUInt8FixedSizeArray16 RosDeserialize(ref ReadBuffer b) => new TestUInt8FixedSizeArray16(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Data, 16);
        }
        
        public void RosValidate()
        {
            if (Data is null) BuiltIns.ThrowNullReference();
            if (Data.Length != 16) BuiltIns.ThrowInvalidSizeForFixedArray(Data.Length, 16);
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 16;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rosbridge_library/TestUInt8FixedSizeArray16";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "a4e84d0a73514dfe9696b4796e8755e7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvNzCuxiDY0i1VISSxJ5OICANuquFIQAAAA";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
