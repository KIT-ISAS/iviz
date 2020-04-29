using System.Runtime.Serialization;

namespace Iviz.Msgs.rosbridge_msgs
{
    public sealed class ConnectedClient : IMessage
    {
        public string ip_address;
        public time connection_time;
    
        /// <summary> Constructor for empty message. </summary>
        public ConnectedClient()
        {
            ip_address = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out ip_address, ref ptr, end);
            BuiltIns.Deserialize(out connection_time, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(ip_address, ref ptr, end);
            BuiltIns.Serialize(connection_time, ref ptr, end);
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
    
        public IMessage Create() => new ConnectedClient();
    
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
