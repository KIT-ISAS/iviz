/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/Time")]
    public sealed class Time : IDeserializable<Time>, IMessage
    {
        [DataMember (Name = "data")] public time Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Time()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Time(time Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Time(ref Buffer b)
        {
            Data = b.Deserialize<time>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Time(ref b);
        }
        
        Time IDeserializable<Time>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Time";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "cd7166c74c552c311fbcc2fe5a7bc289";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvJzE1VSEksSeTiAgBuylFyCwAAAA==";
                
    }
}
