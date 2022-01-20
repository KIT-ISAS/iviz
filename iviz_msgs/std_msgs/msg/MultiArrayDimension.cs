/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MultiArrayDimension : IDeserializable<MultiArrayDimension>, IMessage
    {
        /// Label of given dimension
        [DataMember (Name = "label")] public string Label;
        /// Size of given dimension (in type units)
        [DataMember (Name = "size")] public uint Size;
        /// Stride of given dimension
        [DataMember (Name = "stride")] public uint Stride;
    
        /// Constructor for empty message.
        public MultiArrayDimension()
        {
            Label = string.Empty;
        }
        
        /// Explicit constructor.
        public MultiArrayDimension(string Label, uint Size, uint Stride)
        {
            this.Label = Label;
            this.Size = Size;
            this.Stride = Stride;
        }
        
        /// Constructor with buffer.
        public MultiArrayDimension(ref ReadBuffer b)
        {
            Label = b.DeserializeString();
            Size = b.Deserialize<uint>();
            Stride = b.Deserialize<uint>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MultiArrayDimension(ref b);
        
        public MultiArrayDimension RosDeserialize(ref ReadBuffer b) => new MultiArrayDimension(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Label);
            b.Serialize(Size);
            b.Serialize(Stride);
        }
        
        public void RosValidate()
        {
            if (Label is null) throw new System.NullReferenceException(nameof(Label));
        }
    
        public int RosMessageLength => 12 + BuiltIns.GetStringSize(Label);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/MultiArrayDimension";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "4cd0c83a8683deae40ecdac60e53bfa8";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE22NMQqAMBAEe1+xYKOtvkjJGRbiRbxE0NcrUWy0m2KGsbRSPcIwSgBQPxQneG6icJxF" +
                "jVGrTE19B+MhKGahr4iGirQvgqxM1r7hdXJSwpt+HicAFGWdjgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
