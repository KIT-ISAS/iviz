/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TestUInt8 : IDeserializable<TestUInt8>, IMessage
    {
        [DataMember (Name = "data")] public byte[] Data;
    
        /// Constructor for empty message.
        public TestUInt8()
        {
            Data = System.Array.Empty<byte>();
        }
        
        /// Explicit constructor.
        public TestUInt8(byte[] Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public TestUInt8(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out Data);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestUInt8(ref b);
        
        public TestUInt8 RosDeserialize(ref ReadBuffer b) => new TestUInt8(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Data is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + Data.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/TestUInt8";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f43a8e1b362b75baa741461b46adc7e0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCuxiI5VSEksSeQCANR1vBgNAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
