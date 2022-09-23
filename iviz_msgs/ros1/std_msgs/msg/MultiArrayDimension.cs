/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class MultiArrayDimension : IHasSerializer<MultiArrayDimension>, IMessage
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
            Label = b.DeserializeString();
            b.Deserialize(out Size);
            b.Deserialize(out Stride);
        }
        
        public MultiArrayDimension(ref ReadBuffer2 b)
        {
            b.Align4();
            Label = b.DeserializeString();
            b.Align4();
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
    
        public int RosMessageLength
        {
            get
            {
                int size = 12;
                size += WriteBuffer.GetStringSize(Label);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Label);
            size = WriteBuffer2.Align4(size);
            size += 4; // Size
            size += 4; // Stride
            return size;
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
    
        public Serializer<MultiArrayDimension> CreateSerializer() => new Serializer();
        public Deserializer<MultiArrayDimension> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MultiArrayDimension>
        {
            public override void RosSerialize(MultiArrayDimension msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MultiArrayDimension msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MultiArrayDimension msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MultiArrayDimension msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(MultiArrayDimension msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<MultiArrayDimension>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MultiArrayDimension msg) => msg = new MultiArrayDimension(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MultiArrayDimension msg) => msg = new MultiArrayDimension(ref b);
        }
    }
}
