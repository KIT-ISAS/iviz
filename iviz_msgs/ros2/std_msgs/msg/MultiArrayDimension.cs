/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.StdMsgs
{
    [DataContract]
    public sealed class MultiArrayDimension : IDeserializable<MultiArrayDimension>, IMessageRos2
    {
        // This was originally provided as an example message.
        // It is deprecated as of Foxy
        // It is recommended to create your own semantically meaningful message.
        // However if you would like to continue using this please use the equivalent in example_msgs.
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
        public MultiArrayDimension(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Label);
            b.Deserialize(out Size);
            b.Deserialize(out Stride);
        }
        
        public MultiArrayDimension RosDeserialize(ref ReadBuffer2 b) => new MultiArrayDimension(ref b);
    
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
    
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, Label);
            WriteBuffer2.Advance(ref c, Size);
            WriteBuffer2.Advance(ref c, Stride);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/MultiArrayDimension";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
