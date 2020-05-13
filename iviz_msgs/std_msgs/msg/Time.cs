using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    [DataContract]
    public sealed class Time : IMessage
    {
        [DataMember] public time data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Time()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Time(time data)
        {
            this.data = data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Time(Buffer b)
        {
            this.data = b.Deserialize<time>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new Time(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.data);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 8;
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Time";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "cd7166c74c552c311fbcc2fe5a7bc289";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvJzE1VSEksSeTiAgBuylFyCwAAAA==";
                
    }
}
