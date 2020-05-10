using System.Runtime.Serialization;

namespace Iviz.Msgs.mesh_msgs
{
    public sealed class Feature : IMessage
    {
        public geometry_msgs.Point location { get; set; }
        public std_msgs.Float32[] descriptor { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Feature()
        {
            descriptor = System.Array.Empty<std_msgs.Float32>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Feature(geometry_msgs.Point location, std_msgs.Float32[] descriptor)
        {
            this.location = location;
            this.descriptor = descriptor ?? throw new System.ArgumentNullException(nameof(descriptor));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Feature(Buffer b)
        {
            this.location = new geometry_msgs.Point(b);
            this.descriptor = b.DeserializeArray<std_msgs.Float32>(0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new Feature(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.location.Serialize(b);
            b.SerializeArray(this.descriptor, 0);
        }
        
        public void Validate()
        {
            if (descriptor is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 28;
                size += 4 * descriptor.Length;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "mesh_msgs/Feature";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "ac711cf3ef6eb8582240a7afe5b9a573";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7XQsQrCQAwG4D1PEfABhFYcBFedBMFuIhKuaRtoL8clg/XptRQ66KqZ/vxLPtKyDux5" +
                "vA/W2vqsEh17DeSiEczruT/0Sl4W1xvWbCFLcs0A+x8PnC7HHbbfIlhh1Ylh0Ogk0dA7xqQmkxK1QXpv" +
                "k1wiNpkZLVFgaCb1doOPJY1Lev6L//mz+WJZYE1O8AKMerHtbwEAAA==";
                
    }
}
