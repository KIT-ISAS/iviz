/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Int16 : IDeserializable<Int16>, IMessage
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
        internal Int16(ref Buffer b)
        {
            Data = b.Deserialize<short>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Int16(ref b);
        
        Int16 IDeserializable<Int16>.RosDeserialize(ref Buffer b) => new Int16(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 2;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/Int16";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "8524586e34fbd7cb1c08c5f5f1ca0e57";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsvMKzE0U0hJLEnk4gIAJDs+BgwAAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
