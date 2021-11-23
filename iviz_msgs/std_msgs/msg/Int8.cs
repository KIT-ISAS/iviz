/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Int8 : IDeserializable<Int8>, IMessage
    {
        [DataMember (Name = "data")] public sbyte Data;
    
        /// Constructor for empty message.
        public Int8()
        {
        }
        
        /// Explicit constructor.
        public Int8(sbyte Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal Int8(ref Buffer b)
        {
            Data = b.Deserialize<sbyte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Int8(ref b);
        
        Int8 IDeserializable<Int8>.RosDeserialize(ref Buffer b) => new Int8(ref b);
    
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
        [Preserve] public const string RosMessageType = "std_msgs/Int8";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "27ffa0c9c4b8fb8492252bcad9e5c57b";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMK7FQSEksSeTiAgDmSq87CwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
