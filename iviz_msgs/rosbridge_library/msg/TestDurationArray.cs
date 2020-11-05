/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeLibrary
{
    [DataContract (Name = "rosbridge_library/TestDurationArray")]
    public sealed class TestDurationArray : IDeserializable<TestDurationArray>, IMessage
    {
        [DataMember (Name = "durations")] public duration[] Durations { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestDurationArray()
        {
            Durations = System.Array.Empty<duration>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestDurationArray(duration[] Durations)
        {
            this.Durations = Durations;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestDurationArray(ref Buffer b)
        {
            Durations = b.DeserializeStructArray<duration>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestDurationArray(ref b);
        }
        
        TestDurationArray IDeserializable<TestDurationArray>.RosDeserialize(ref Buffer b)
        {
            return new TestDurationArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Durations, 0);
        }
        
        public void RosValidate()
        {
            if (Durations is null) throw new System.NullReferenceException(nameof(Durations));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 8 * Durations.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosbridge_library/TestDurationArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8b3bcadc803a7fcbc857c6a1dab53bcd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0spLUosyczPi45VSIEyi7kADvrU2BUAAAA=";
                
    }
}
