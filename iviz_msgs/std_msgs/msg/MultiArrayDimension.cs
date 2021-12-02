/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MultiArrayDimension : IDeserializable<MultiArrayDimension>, IMessage
    {
        [DataMember (Name = "label")] public string Label; // label of given dimension
        [DataMember (Name = "size")] public uint Size; // size of given dimension (in type units)
        [DataMember (Name = "stride")] public uint Stride; // stride of given dimension
    
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
        internal MultiArrayDimension(ref Buffer b)
        {
            Label = b.DeserializeString();
            Size = b.Deserialize<uint>();
            Stride = b.Deserialize<uint>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MultiArrayDimension(ref b);
        
        MultiArrayDimension IDeserializable<MultiArrayDimension>.RosDeserialize(ref Buffer b) => new MultiArrayDimension(ref b);
    
        public void RosSerialize(ref Buffer b)
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
                "H4sIAAAAAAAACm2NsQqAMAwFd7/igYuu+kVKYwnUVEwq6NcbVFx0u+GOU1tZItIwUgJQP5QnRN5IEHgm" +
                "Uc5SFRbrOygf5KKbF31FNCywfSEUYdP2Df0UPPXwpp/HCQAUZZ2OAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
