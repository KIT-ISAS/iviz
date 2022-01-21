/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
            Data = b.Deserialize<byte>();
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
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Byte";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ad736a2e8818154c487bb80fe42ce43b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0uqLElVSEksSeTiAgAksd8TCwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
