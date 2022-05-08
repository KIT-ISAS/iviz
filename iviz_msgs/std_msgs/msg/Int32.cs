/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Int32 : IDeserializable<Int32>, IMessage
    {
        [DataMember (Name = "data")] public int Data;
    
        /// Constructor for empty message.
        public Int32()
        {
        }
        
        /// Explicit constructor.
        public Int32(int Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public Int32(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Int32(ref b);
        
        public Int32 RosDeserialize(ref ReadBuffer b) => new Int32(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Int32";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "da5909fbe378aeaf85e547e830cc1bb7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8vMKzE2UkhJLEnkAgAHaI4xCwAAAA==";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
