using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/MultiArrayDimension")]
    public sealed class MultiArrayDimension : IMessage
    {
        [DataMember (Name = "label")] public string Label { get; set; } // label of given dimension
        [DataMember (Name = "size")] public uint Size { get; set; } // size of given dimension (in type units)
        [DataMember (Name = "stride")] public uint Stride { get; set; } // stride of given dimension
    
        /// <summary> Constructor for empty message. </summary>
        public MultiArrayDimension()
        {
            Label = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public MultiArrayDimension(string Label, uint Size, uint Stride)
        {
            this.Label = Label;
            this.Size = Size;
            this.Stride = Stride;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MultiArrayDimension(Buffer b)
        {
            Label = b.DeserializeString();
            Size = b.Deserialize<uint>();
            Stride = b.Deserialize<uint>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new MultiArrayDimension(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Label);
            b.Serialize(Size);
            b.Serialize(Stride);
        }
        
        public void RosValidate()
        {
            if (Label is null) throw new System.NullReferenceException(nameof(Label));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += BuiltIns.UTF8.GetByteCount(Label);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/MultiArrayDimension";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4cd0c83a8683deae40ecdac60e53bfa8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE22NMQqAMBAEe1+xYKOtvkjJGRbiRbxE0NcrUWy0m2KGsbRSPcIwSgBQPxQneG6icJxF" +
                "jVGrTE19B+MhKGahr4iGirQvgqxM1r7hdXJSwpt+HicAFGWdjgAAAA==";
                
    }
}
