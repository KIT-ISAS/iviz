/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [Preserve, DataContract (Name = RosMessageType)]
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
        internal TestUInt8FixedSizeArray16(ref Buffer b)
        {
            Data = b.DeserializeStructArray<byte>(16);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestUInt8FixedSizeArray16(ref b);
        
        TestUInt8FixedSizeArray16 IDeserializable<TestUInt8FixedSizeArray16>.RosDeserialize(ref Buffer b) => new TestUInt8FixedSizeArray16(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Data, 16);
        }
        
        public void RosValidate()
        {
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
            if (Data.Length != 16) throw new RosInvalidSizeForFixedArrayException(nameof(Data), Data.Length, 16);
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 16;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "rosbridge_library/TestUInt8FixedSizeArray16";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "a4e84d0a73514dfe9696b4796e8755e7";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACivNzCuxiDY0i1VISSxJ5OICANuquFIQAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
