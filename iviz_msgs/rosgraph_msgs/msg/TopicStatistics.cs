using System.Text;
using System.Runtime.Serialization;

namespace Iviz.Msgs.rosgraph_msgs
{
    public sealed class TopicStatistics : IMessage
    {
        // name of the topic
        public string topic;
        
        // node id of the publisher
        public string node_pub;
        
        // node id of the subscriber
        public string node_sub;
        
        // the statistics apply to this time window
        public time window_start;
        public time window_stop;
        
        // number of messages delivered during the window
        public int delivered_msgs;
        // numbers of messages dropped during the window
        public int dropped_msgs;
        
        // traffic during the window, in bytes
        public int traffic;
        
        // mean/stddev/max period between two messages
        public duration period_mean;
        public duration period_stddev;
        public duration period_max;
        
        // mean/stddev/max age of the message based on the
        // timestamp in the message header. In case the
        // message does not have a header, it will be 0.
        public duration stamp_age_mean;
        public duration stamp_age_stddev;
        public duration stamp_age_max;
    
        /// <summary> Constructor for empty message. </summary>
        public TopicStatistics()
        {
            topic = "";
            node_pub = "";
            node_sub = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out topic, ref ptr, end);
            BuiltIns.Deserialize(out node_pub, ref ptr, end);
            BuiltIns.Deserialize(out node_sub, ref ptr, end);
            BuiltIns.Deserialize(out window_start, ref ptr, end);
            BuiltIns.Deserialize(out window_stop, ref ptr, end);
            BuiltIns.Deserialize(out delivered_msgs, ref ptr, end);
            BuiltIns.Deserialize(out dropped_msgs, ref ptr, end);
            BuiltIns.Deserialize(out traffic, ref ptr, end);
            BuiltIns.Deserialize(out period_mean, ref ptr, end);
            BuiltIns.Deserialize(out period_stddev, ref ptr, end);
            BuiltIns.Deserialize(out period_max, ref ptr, end);
            BuiltIns.Deserialize(out stamp_age_mean, ref ptr, end);
            BuiltIns.Deserialize(out stamp_age_stddev, ref ptr, end);
            BuiltIns.Deserialize(out stamp_age_max, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(topic, ref ptr, end);
            BuiltIns.Serialize(node_pub, ref ptr, end);
            BuiltIns.Serialize(node_sub, ref ptr, end);
            BuiltIns.Serialize(window_start, ref ptr, end);
            BuiltIns.Serialize(window_stop, ref ptr, end);
            BuiltIns.Serialize(delivered_msgs, ref ptr, end);
            BuiltIns.Serialize(dropped_msgs, ref ptr, end);
            BuiltIns.Serialize(traffic, ref ptr, end);
            BuiltIns.Serialize(period_mean, ref ptr, end);
            BuiltIns.Serialize(period_stddev, ref ptr, end);
            BuiltIns.Serialize(period_max, ref ptr, end);
            BuiltIns.Serialize(stamp_age_mean, ref ptr, end);
            BuiltIns.Serialize(stamp_age_stddev, ref ptr, end);
            BuiltIns.Serialize(stamp_age_max, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 88;
                size += Encoding.UTF8.GetByteCount(topic);
                size += Encoding.UTF8.GetByteCount(node_pub);
                size += Encoding.UTF8.GetByteCount(node_sub);
                return size;
            }
        }
    
        public IMessage Create() => new TopicStatistics();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string RosMessageType = "rosgraph_msgs/TopicStatistics";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string RosMd5Sum = "10152ed868c5097a5e2e4a89d7daa710";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE32RXW6DMBCE3znFSnmtkqo9RU+BDF7CSvhH3gWS23cdTEIh6htefzPjWU7gjUMIHUiP" +
                "ICFSW7Ek8tdyqE7gg0Ugu0JxbAbiHtMK5vtap29YHhtuEzU7mBf4QYgRYqGWwcQ43DVW58QgpO+aydsw" +
                "V5vvWgVJdpMQH9mj06Ac7ZDZXJHB4kATJrRgx6VU/zQlL99fL6J2fOWnC/+1SSHG/0yW+8UiF0um66g9" +
                "8h9AHpq7IBdlIbPIofEXFmtxujhzg4iJgoUGZUb0IHN4PqhSY11b8AWqs/YwXLyOrLm9i1Pf9a+VGGgM" +
                "a2lV6jC30p3r9l3MJbZcj8ZiOsOPh1YlBV9vbdAN+iDQmwnBFFo3IbqUYdCC8Hl+vfKRUKtwV+o13/fa" +
                "KHK1XzP4/jHTAgAA";
                
    }
}
