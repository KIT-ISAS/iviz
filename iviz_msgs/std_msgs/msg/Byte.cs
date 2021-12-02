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
        internal Byte(ref Buffer b)
        {
            Data = b.Deserialize<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Byte(ref b);
        
        Byte IDeserializable<Byte>.RosDeserialize(ref Buffer b) => new Byte(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/Byte";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "ad736a2e8818154c487bb80fe42ce43b";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACkuqLElVSEksSeTiAgAksd8TCwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
