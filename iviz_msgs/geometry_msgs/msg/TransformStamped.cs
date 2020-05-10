using System.Runtime.Serialization;

namespace Iviz.Msgs.geometry_msgs
{
    public sealed class TransformStamped : IMessage
    {
        // This expresses a transform from coordinate frame header.frame_id
        // to the coordinate frame child_frame_id
        //
        // This message is mostly used by the 
        // <a href="http://wiki.ros.org/tf">tf</a> package. 
        // See its documentation for more information.
        
        public std_msgs.Header header { get; set; }
        public string child_frame_id { get; set; } // the frame id of the child frame
        public Transform transform { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TransformStamped()
        {
            header = new std_msgs.Header();
            child_frame_id = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public TransformStamped(std_msgs.Header header, string child_frame_id, Transform transform)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.child_frame_id = child_frame_id ?? throw new System.ArgumentNullException(nameof(child_frame_id));
            this.transform = transform;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TransformStamped(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.child_frame_id = b.DeserializeString();
            this.transform = new Transform(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TransformStamped(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            b.Serialize(this.child_frame_id);
            this.transform.Serialize(b);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            if (child_frame_id is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 60;
                size += header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(child_frame_id);
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "geometry_msgs/TransformStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "b5764a33bfeb3588febc2682852579b0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71VTU/cMBC951eMugeggqwEVQ8IOKG2HCpRgXpFQzJJLBI72BOW9Nf32dnN8iW1h5ZV" +
                "pLUdz5uP92ayoOvGBJLH3ksIEohJPdtQOd9R5V1HhXO+NJZVsOdOqBEuxedpc2PKbEHqSBt5fbNoTFve" +
                "bC/iavLWwRXXQnHpgrYjDUFKuh0TDG6dMDVeqtMPjWp/vFyuzJ3JvQu58/VSqw9nWp0s+Yx6Lu4AlEeb" +
                "KwGgBipdMXRildU4S8gDPjxe2ZhSOsyz7FvKYZ1KFtQbW78IlxYpmikTbF01JRkvTafZ9VypuWZZdvqP" +
                "f9n3q6/HFLS86UIdllPkMV9lW7IvUU3lkpVTro2pG/EHrTxICyPuehQ2vdWxl5BvKMBTixXP7ab6ILFw" +
                "XTdYU0QG1YClp/awNBby6NmrKYaW/SvCIzqeIPeD2ELo4vwYd2yQYlCDgEYgFF44xGpfnFM2GKtHh9Eg" +
                "W1yv3AG2UoOX2TlKzkpPBFoSh2P4+DgllwMbxRF4KQPtprMbbMMewQlCkN4VDe0i8stRGwgicvjA3vBt" +
                "mwRYoAJA3YlGO3tPkG2CtmzdBn5C3Pr4G1g748acDhpw1sbsw1CjgLjYe/dgyq36i9ZAvNSaW89+zKLV" +
                "5DJbfElS1EhfYgT/HIIrDAgoaWW02Sh5brn/pMZaHFTnx0mScxtsxOUlkoU0QkppO1BuRVciqNbKvRJP" +
                "iPKqPLo4oK2hpeynFOr80WTfptbNfgww8Da2tndTj79Pkutg3kiR6SG9exF/7ISLpF1nofxOGLSiyWZL" +
                "GJbGwzSOJKAKJh4m1T6mGIYY6mGdAqPjO0AKhBStue8Bxk9rEo9hsit5ne/TqkF9060ohNS2qdFNQd7U" +
                "mGMzG7Mx0zq5fdLqEEJq2ynmyRkoBMim2ns5XVQ0uoFWMSEs/Hq+ONA7x5X6QJ3bj8NlDfG8oJcO3b79" +
                "FNigmGxgvWod6+dP9Divxnn1612o3mrsLbYtOW/m78szzuPufivQWOQ/JrRZrbLsN7aLdoyMBwAA";
                
    }
}
