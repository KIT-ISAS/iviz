/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class ColorChannel : IDeserializableRos1<ColorChannel>, IDeserializableRos2<ColorChannel>, IMessageRos1, IMessageRos2
    {
        [DataMember (Name = "colors")] public Color32[] Colors;
    
        /// Constructor for empty message.
        public ColorChannel()
        {
            Colors = System.Array.Empty<Color32>();
        }
        
        /// Explicit constructor.
        public ColorChannel(Color32[] Colors)
        {
            this.Colors = Colors;
        }
        
        /// Constructor with buffer.
        public ColorChannel(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out Colors);
        }
        
        /// Constructor with buffer.
        public ColorChannel(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(out Colors);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new ColorChannel(ref b);
        
        public ColorChannel RosDeserialize(ref ReadBuffer b) => new ColorChannel(ref b);
        
        public ColorChannel RosDeserialize(ref ReadBuffer2 b) => new ColorChannel(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Colors);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Colors);
        }
        
        public void RosValidate()
        {
            if (Colors is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + 4 * Colors.Length;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Colors);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "iviz_msgs/ColorChannel";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "04d8fd1feb40362aeedd2ef19014ebfa";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE3POz8kvMjaKjlVIBrGKubhsqQy4fIPdrRQyyzKr4nOL04v1nSE2cpVm5pVYKBRB6XQo" +
                "nQSlE7m4AEmfKA6bAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
