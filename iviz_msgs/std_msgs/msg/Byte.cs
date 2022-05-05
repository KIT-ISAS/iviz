/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Byte : IDeserializable<Byte>, IMessage
    {
        [DataMember (Name = "data")] public byte Data;
    
        /// Constructor for empty message.
        public Byte()
        {
        }
        
        /// Explicit constructor.
        public Byte(byte Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public Byte(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Byte(ref b);
        
        public Byte RosDeserialize(ref ReadBuffer b) => new Byte(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Byte";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "ad736a2e8818154c487bb80fe42ce43b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE0uqLElVSEksSeTiAgAksd8TCwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
