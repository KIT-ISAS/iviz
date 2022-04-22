/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TestDurationArray : IDeserializable<TestDurationArray>, IMessage
    {
        [DataMember (Name = "durations")] public duration[] Durations;
    
        /// Constructor for empty message.
        public TestDurationArray()
        {
            Durations = System.Array.Empty<duration>();
        }
        
        /// Explicit constructor.
        public TestDurationArray(duration[] Durations)
        {
            this.Durations = Durations;
        }
        
        /// Constructor with buffer.
        public TestDurationArray(ref ReadBuffer b)
        {
            Durations = b.DeserializeStructArray<duration>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestDurationArray(ref b);
        
        public TestDurationArray RosDeserialize(ref ReadBuffer b) => new TestDurationArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Durations);
        }
        
        public void RosValidate()
        {
            if (Durations is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + 8 * Durations.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/TestDurationArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8b3bcadc803a7fcbc857c6a1dab53bcd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0spLUosyczPi45VSIEyi7kADvrU2BUAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
