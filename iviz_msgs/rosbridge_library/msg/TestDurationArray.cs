using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestDurationArray : IMessage
    {
        public duration[] durations;
    
        /// <summary> Constructor for empty message. </summary>
        public TestDurationArray()
        {
            durations = System.Array.Empty<duration>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out durations, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(durations, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 8 * durations.Length;
                return size;
            }
        }
    
        public IMessage Create() => new TestDurationArray();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string RosMessageType = "rosbridge_library/TestDurationArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string RosMd5Sum = "8b3bcadc803a7fcbc857c6a1dab53bcd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0spLUosyczPi45VSIEyi7kADvrU2BUAAAA=";
                
    }
}
