/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Int16 : IDeserializable<Int16>, IMessageRos1
    {
        [DataMember (Name = "data")] public short Data;
    
        /// Constructor for empty message.
        public Int16()
        {
        }
        
        /// Explicit constructor.
        public Int16(short Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public Int16(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Int16(ref b);
        
        public Int16 RosDeserialize(ref ReadBuffer b) => new Int16(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 2;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Int16";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "8524586e34fbd7cb1c08c5f5f1ca0e57";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMKzE0U0hJLEnk4gIAJDs+BgwAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
