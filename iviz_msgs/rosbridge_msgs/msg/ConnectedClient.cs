using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_msgs
{
    public sealed class ConnectedClient : IMessage
    {
        public string ip_address { get; set; }
        public time connection_time { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ConnectedClient()
        {
            ip_address = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public ConnectedClient(string ip_address, time connection_time)
        {
            this.ip_address = ip_address ?? throw new System.ArgumentNullException(nameof(ip_address));
            this.connection_time = connection_time;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ConnectedClient(Buffer b)
        {
            this.ip_address = b.DeserializeString();
            this.connection_time = b.Deserialize<time>();
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new ConnectedClient(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.ip_address);
            b.Serialize(this.connection_time);
        }
        
        public void Validate()
        {
            if (ip_address is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += BuiltIns.UTF8.GetByteCount(ip_address);
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "rosbridge_msgs/ConnectedClient";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "7f2187ce389b39b2b3bb2a3957e54c04";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEysuKcrMS1fILIhPTEkpSi0u5irJzE1VSM7Py0tNLsnMz4sH8bm4AFvLoaQoAAAA";
                
    }
}
