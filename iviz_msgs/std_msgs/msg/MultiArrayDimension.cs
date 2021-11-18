/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/MultiArrayDimension")]
    public sealed class MultiArrayDimension : IDeserializable<MultiArrayDimension>, IMessage
    {
        [DataMember (Name = "label")] public string Label; // label of given dimension
        [DataMember (Name = "size")] public uint Size; // size of given dimension (in type units)
        [DataMember (Name = "stride")] public uint Stride; // stride of given dimension
    
        /// <summary> Constructor for empty message. </summary>
        public MultiArrayDimension()
        {
            Label = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public MultiArrayDimension(string Label, uint Size, uint Stride)
        {
            this.Label = Label;
            this.Size = Size;
            this.Stride = Stride;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MultiArrayDimension(ref Buffer b)
        {
            Label = b.DeserializeString();
            Size = b.Deserialize<uint>();
            Stride = b.Deserialize<uint>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MultiArrayDimension(ref b);
        }
        
        MultiArrayDimension IDeserializable<MultiArrayDimension>.RosDeserialize(ref Buffer b)
        {
            return new MultiArrayDimension(ref b);
        }
    
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/MultiArrayDimension";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4cd0c83a8683deae40ecdac60e53bfa8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACmWNsQqAMAwF94L/8MBFV/0ipbEEaiqmFfTrDSou3W644zTvLAFxmikCaD9KCwIfJPC8" +
                "kigncYUljwOULzLRzIdqER0L8rkRinDW/g/t5C218KU6bdwNQPULfY8AAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
