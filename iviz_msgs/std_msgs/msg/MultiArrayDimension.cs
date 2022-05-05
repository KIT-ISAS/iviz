/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
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
            b.DeserializeString(out Label);
            b.Deserialize(out Size);
            b.Deserialize(out Stride);
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
            if (Label is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 12 + BuiltIns.GetStringSize(Label);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/MultiArrayDimension";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "4cd0c83a8683deae40ecdac60e53bfa8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE22NMQqAMBAEe1+xYKOtvkjJGRbiRbxE0NcrUWy0m2KGsbRSPcIwSgBQPxQneG6icJxF" +
                "jVGrTE19B+MhKGahr4iGirQvgqxM1r7hdXJSwpt+HicAFGWdjgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
