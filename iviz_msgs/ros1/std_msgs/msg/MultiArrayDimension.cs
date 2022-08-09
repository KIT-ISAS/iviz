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
    
        public MultiArrayDimension()
        {
            Label = "";
        }
        
        public MultiArrayDimension(string Label, uint Size, uint Stride)
        {
            this.Label = Label;
            this.Size = Size;
            this.Stride = Stride;
        }
        
        public MultiArrayDimension(ref ReadBuffer b)
        {
            b.DeserializeString(out Label);
            b.Deserialize(out Size);
            b.Deserialize(out Stride);
        }
        
        public MultiArrayDimension(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Label);
            b.Deserialize(out Size);
            b.Deserialize(out Stride);
        }
        
        public MultiArrayDimension RosDeserialize(ref ReadBuffer b) => new MultiArrayDimension(ref b);
        
        public MultiArrayDimension RosDeserialize(ref ReadBuffer2 b) => new MultiArrayDimension(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Label);
            b.Serialize(Size);
            b.Serialize(Stride);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Label);
            b.Serialize(Size);
            b.Serialize(Stride);
        }
        
        public void RosValidate()
        {
            if (Label is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 12 + WriteBuffer.GetStringSize(Label);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = WriteBuffer2.AddLength(c, Label);
            c = WriteBuffer2.Align4(c);
            c += 4;  // Size
            c += 4;  // Stride
            return c;
        }
    
        public const string MessageType = "std_msgs/MultiArrayDimension";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "4cd0c83a8683deae40ecdac60e53bfa8";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE22NMQqAMBAEe1+xYKOtvkjJGRbiRbxE0NcrUWy0m2KGsbRSPcIwSgBQPxQneG6icJxF" +
                "jVGrTE19B+MhKGahr4iGirQvgqxM1r7hdXJSwpt+HicAFGWdjgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
