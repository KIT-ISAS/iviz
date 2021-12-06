/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Duration : IDeserializable<Duration>, IMessage
    {
        [DataMember (Name = "data")] public duration Data;
    
        /// Constructor for empty message.
        public Duration()
        {
        }
        
        /// Explicit constructor.
        public Duration(duration Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal Duration(ref ReadBuffer b)
        {
            Data = b.Deserialize<duration>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Duration(ref b);
        
        public Duration RosDeserialize(ref ReadBuffer b) => new Duration(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
        [Preserve] public const string RosMessageType = "std_msgs/Duration";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "3e286caf4241d664e55f3ad380e2ae46";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0spLUosyczPU0hJLEnk4gIAtVhIcg8AAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
