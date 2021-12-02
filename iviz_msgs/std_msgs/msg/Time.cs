/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Time : IDeserializable<Time>, IMessage
    {
        [DataMember (Name = "data")] public time Data;
    
        /// Constructor for empty message.
        public Time()
        {
        }
        
        /// Explicit constructor.
        public Time(time Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal Time(ref Buffer b)
        {
            Data = b.Deserialize<time>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Time(ref b);
        
        Time IDeserializable<Time>.RosDeserialize(ref Buffer b) => new Time(ref b);
    
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
        [Preserve] public const string RosMessageType = "std_msgs/Time";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "cd7166c74c552c311fbcc2fe5a7bc289";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACivJzE1VSEksSeTiAgBuylFyCwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
