/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class ColorChannel : IDeserializable<ColorChannel>, IMessage
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
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ColorChannel(ref b);
        
        public ColorChannel RosDeserialize(ref ReadBuffer b) => new ColorChannel(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Colors);
        }
        
        public void RosValidate()
        {
            if (Colors is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + 4 * Colors.Length;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "iviz_msgs/ColorChannel";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "04d8fd1feb40362aeedd2ef19014ebfa";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE3POz8kvMjaKjlVIBrGKubhsqQy4fIPdrRQyyzKr4nOL04v1nSE2cpVm5pVYKBRB6XQo" +
                "nQSlE7m4AEmfKA6bAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
