/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Int64 : IDeserializable<Int64>, IMessage
    {
        [DataMember (Name = "data")] public long Data;
    
        /// Constructor for empty message.
        public Int64()
        {
        }
        
        /// Explicit constructor.
        public Int64(long Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal Int64(ref Buffer b)
        {
            Data = b.Deserialize<long>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Int64(ref b);
        
        Int64 IDeserializable<Int64>.RosDeserialize(ref Buffer b) => new Int64(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/Int64";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "34add168574510e6e17f5d23ecc077ef";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzEzUUhJLEnkAgBZU74aCwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
