/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosbridgeMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ConnectedClient : IDeserializable<ConnectedClient>, IMessage
    {
        [DataMember (Name = "ip_address")] public string IpAddress;
        [DataMember (Name = "connection_time")] public time ConnectionTime;
    
        /// Constructor for empty message.
        public ConnectedClient()
        {
            IpAddress = string.Empty;
        }
        
        /// Explicit constructor.
        public ConnectedClient(string IpAddress, time ConnectionTime)
        {
            this.IpAddress = IpAddress;
            this.ConnectionTime = ConnectionTime;
        }
        
        /// Constructor with buffer.
        internal ConnectedClient(ref Buffer b)
        {
            IpAddress = b.DeserializeString();
            ConnectionTime = b.Deserialize<time>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ConnectedClient(ref b);
        
        ConnectedClient IDeserializable<ConnectedClient>.RosDeserialize(ref Buffer b) => new ConnectedClient(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(IpAddress);
            b.Serialize(ConnectionTime);
        }
        
        public void RosValidate()
        {
            if (IpAddress is null) throw new System.NullReferenceException(nameof(IpAddress));
        }
    
        public int RosMessageLength => 12 + BuiltIns.GetStringSize(IpAddress);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "rosbridge_msgs/ConnectedClient";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "7f2187ce389b39b2b3bb2a3957e54c04";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACisuKcrMS1fILIhPTEkpSi0u5irJzE1VSM7Py0tNLsnMz4sH8bm4AFvLoaQoAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
