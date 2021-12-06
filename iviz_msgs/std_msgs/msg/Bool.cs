/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Bool : IDeserializable<Bool>, IMessage
    {
        [DataMember (Name = "data")] public bool Data;
    
        /// Constructor for empty message.
        public Bool()
        {
        }
        
        /// Explicit constructor.
        public Bool(bool Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal Bool(ref ReadBuffer b)
        {
            Data = b.Deserialize<bool>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Bool(ref b);
        
        public Bool RosDeserialize(ref ReadBuffer b) => new Bool(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
        [Preserve] public const string RosMessageType = "std_msgs/Bool";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "8b94c1b53db61fb6aed406028ad6332a";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vKz89RSEksSeQCAGFR0NcKAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
