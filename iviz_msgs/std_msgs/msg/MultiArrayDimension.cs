using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    public sealed class MultiArrayDimension : IMessage
    {
        public string label { get; set; } // label of given dimension
        public uint size { get; set; } // size of given dimension (in type units)
        public uint stride { get; set; } // stride of given dimension
    
        /// <summary> Constructor for empty message. </summary>
        public MultiArrayDimension()
        {
            label = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public MultiArrayDimension(string label, uint size, uint stride)
        {
            this.label = label ?? throw new System.ArgumentNullException(nameof(label));
            this.size = size;
            this.stride = stride;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MultiArrayDimension(Buffer b)
        {
            this.label = b.DeserializeString();
            this.size = b.Deserialize<uint>();
            this.stride = b.Deserialize<uint>();
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new MultiArrayDimension(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.label);
            b.Serialize(this.size);
            b.Serialize(this.stride);
        }
        
        public void Validate()
        {
            if (label is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += BuiltIns.UTF8.GetByteCount(label);
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "std_msgs/MultiArrayDimension";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "4cd0c83a8683deae40ecdac60e53bfa8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE22NMQqAMBAEe1+xYKOtvkjJGRbiRbxE0NcrUWy0m2KGsbRSPcIwSgBQPxQneG6icJxF" +
                "jVGrTE19B+MhKGahr4iGirQvgqxM1r7hdXJSwpt+HicAFGWdjgAAAA==";
                
    }
}
