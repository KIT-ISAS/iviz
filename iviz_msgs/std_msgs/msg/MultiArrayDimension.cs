/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MultiArrayDimension : IDeserializable<MultiArrayDimension>, IMessage
    {
        /// <summary> Label of given dimension </summary>
        [DataMember (Name = "label")] public string Label;
        /// <summary> Size of given dimension (in type units) </summary>
        [DataMember (Name = "size")] public uint Size;
        /// <summary> Stride of given dimension </summary>
        [DataMember (Name = "stride")] public uint Stride;
    
        /// Constructor for empty message.
        public MultiArrayDimension()
        {
            Label = "";
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/MultiArrayDimension";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4cd0c83a8683deae40ecdac60e53bfa8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE22NMQqAMBAEe1+xYKOtvkjJGRbiRbxE0NcrUWy0m2KGsbRSPcIwSgBQPxQneG6icJxF" +
                "jVGrTE19B+MhKGahr4iGirQvgqxM1r7hdXJSwpt+HicAFGWdjgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
