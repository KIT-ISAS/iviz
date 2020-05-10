using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_library
{
    public sealed class TestTimeArray : IMessage
    {
        public time[] times { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestTimeArray()
        {
            times = System.Array.Empty<time>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestTimeArray(time[] times)
        {
            this.times = times ?? throw new System.ArgumentNullException(nameof(times));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestTimeArray(Buffer b)
        {
            this.times = b.DeserializeStructArray<time>(0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TestTimeArray(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeStructArray(this.times, 0);
        }
        
        public void Validate()
        {
            if (times is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 8 * times.Length;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "rosbridge_library/TestTimeArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "237b97d24fd33588beee4cd8978b149d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvJzE2NjlUoAVLFXAD3rdP6DQAAAA==";
                
    }
}
