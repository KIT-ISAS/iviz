/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
        internal Int32(ref Buffer b)
        {
            Data = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Int32(ref b);
        
        Int32 IDeserializable<Int32>.RosDeserialize(ref Buffer b) => new Int32(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/Int32";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "da5909fbe378aeaf85e547e830cc1bb7";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsvMKzE2UkhJLEnkAgAHaI4xCwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
