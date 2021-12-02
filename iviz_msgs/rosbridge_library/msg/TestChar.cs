/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TestChar : IDeserializable<TestChar>, IMessage
    {
        [DataMember (Name = "data")] public sbyte[] Data;
    
        /// Constructor for empty message.
        public TestChar()
        {
            Data = System.Array.Empty<sbyte>();
        }
        
        /// Explicit constructor.
        public TestChar(sbyte[] Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal TestChar(ref Buffer b)
        {
            Data = b.DeserializeStructArray<sbyte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TestChar(ref b);
        
        TestChar IDeserializable<TestChar>.RosDeserialize(ref Buffer b) => new TestChar(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength => 4 + Data.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "rosbridge_library/TestChar";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "7b8d15902c8b049d5a32b4cb73fa86f5";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACkvOSCyKjlVISSxJ5AIAudt/QwwAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
