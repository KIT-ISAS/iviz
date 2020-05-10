using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestDurationArray : IMessage
    {
        public duration[] durations { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestDurationArray()
        {
            durations = System.Array.Empty<duration>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestDurationArray(duration[] durations)
        {
            this.durations = durations ?? throw new System.ArgumentNullException(nameof(durations));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestDurationArray(Buffer b)
        {
            this.durations = b.DeserializeStructArray<duration>(0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TestDurationArray(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeStructArray(this.durations, 0);
        }
        
        public void Validate()
        {
            if (durations is null) throw new System.NullReferenceException();
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
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "rosbridge_library/TestDurationArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "8b3bcadc803a7fcbc857c6a1dab53bcd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0spLUosyczPi45VSIEyi7kADvrU2BUAAAA=";
                
    }
}
