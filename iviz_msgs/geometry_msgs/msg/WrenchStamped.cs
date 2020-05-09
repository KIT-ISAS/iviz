using System.Runtime.Serialization;

namespace Iviz.Msgs.geometry_msgs
{
    public sealed class WrenchStamped : IMessage
    {
        // A wrench with reference coordinate frame and timestamp
        public std_msgs.Header header { get; set; }
        public Wrench wrench { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public WrenchStamped()
        {
            header = new std_msgs.Header();
            wrench = new Wrench();
        }
        
        /// <summary> Explicit constructor. </summary>
        public WrenchStamped(std_msgs.Header header, Wrench wrench)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.wrench = wrench ?? throw new System.ArgumentNullException(nameof(wrench));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal WrenchStamped(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.wrench = new Wrench(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new WrenchStamped(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            this.wrench.Serialize(b);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            if (wrench is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 48;
                size += header.RosMessageLength;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "geometry_msgs/WrenchStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "d78d3cb249ce23087ade7e7d0c40cfa7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UwW7UMBC9+ytG2kNbtA1SizhU4oCEgB6QKrWCYzUbTxKLxA72ZLfh63l2tikVHDgA" +
                "q2gTJ35v5s288Ybe0iGKrzs6OO0oSiN5KVSHEK3zrEJN5EGIvSV1gyTlYTQfha1E6srNfDlSlJsxb/7y" +
                "z3y6/XBFSe39kNr0coltNnSrSIqjpUGULStTE5CTazuJ573spaeSrVgqX3UeJVUA3nUuEa5WvETu+5mm" +
                "hE0aIHsYJu/qrHtV+4gH0nliGjmqq6ee4y9lyuy4knybShmv311hj09ST+qQ0AyGOgon51t8JDM5r5cX" +
                "GWA2d4dwjqW0qOwanLRjzcnKwxgl5Tw5XSHGi0VcBW4URxDFJjot7+6xTGeEIEhBxoDenCLzm1m74EEo" +
                "tOfoeNdLJq5RAbCeZNDJ2U/MvlB79uGRfmF8ivEntH7lzZrOO/Ssz+rT1KKA2DjGsHcWW3dzIal7J16p" +
                "d7vIcTYZtYQ0m/fFiprbVzqCO6cUaocG2GJhkzRm9tKNe2f/lRtbCXBdnBdLLgPw6KwouVPQkLIhUTAU" +
                "qYkCFSPXskWz4KCSMbodsquwEzUR1COPGfu2mCv7DH79LLWGeEkL2dMS/3DZ/xF4DPobhUz78u25yCpP" +
                "wXXxbfBw/SCMlmLAViSA1kVAXfAVWHHuQB+q45RskEQ+KDgG/gpKgYkymscRZJjkyD71nLH5NSCnUrXV" +
                "lg6d+GVXNkEZ2TLkrqboWmcXJAINK5jpKG5L2lzARH2/5LwEgyNBEoMWwFlF1w3NYaJDFoSHeDxbAu1k" +
                "zavMgIawzQfLkeJ5QW8Ceo+ypMRtNkhSnGqVMU0fWF+/oof1aV6fvpsfHy6CgrAFAAA=";
                
    }
}
