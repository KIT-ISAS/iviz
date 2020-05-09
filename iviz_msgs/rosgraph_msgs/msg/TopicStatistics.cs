using System.Runtime.Serialization;

namespace Iviz.Msgs.rosgraph_msgs
{
    public sealed class TopicStatistics : IMessage
    {
        // name of the topic
        public string topic { get; set; }
        
        // node id of the publisher
        public string node_pub { get; set; }
        
        // node id of the subscriber
        public string node_sub { get; set; }
        
        // the statistics apply to this time window
        public time window_start { get; set; }
        public time window_stop { get; set; }
        
        // number of messages delivered during the window
        public int delivered_msgs { get; set; }
        // numbers of messages dropped during the window
        public int dropped_msgs { get; set; }
        
        // traffic during the window, in bytes
        public int traffic { get; set; }
        
        // mean/stddev/max period between two messages
        public duration period_mean { get; set; }
        public duration period_stddev { get; set; }
        public duration period_max { get; set; }
        
        // mean/stddev/max age of the message based on the
        // timestamp in the message header. In case the
        // message does not have a header, it will be 0.
        public duration stamp_age_mean { get; set; }
        public duration stamp_age_stddev { get; set; }
        public duration stamp_age_max { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TopicStatistics()
        {
            topic = "";
            node_pub = "";
            node_sub = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public TopicStatistics(string topic, string node_pub, string node_sub, time window_start, time window_stop, int delivered_msgs, int dropped_msgs, int traffic, duration period_mean, duration period_stddev, duration period_max, duration stamp_age_mean, duration stamp_age_stddev, duration stamp_age_max)
        {
            this.topic = topic ?? throw new System.ArgumentNullException(nameof(topic));
            this.node_pub = node_pub ?? throw new System.ArgumentNullException(nameof(node_pub));
            this.node_sub = node_sub ?? throw new System.ArgumentNullException(nameof(node_sub));
            this.window_start = window_start;
            this.window_stop = window_stop;
            this.delivered_msgs = delivered_msgs;
            this.dropped_msgs = dropped_msgs;
            this.traffic = traffic;
            this.period_mean = period_mean;
            this.period_stddev = period_stddev;
            this.period_max = period_max;
            this.stamp_age_mean = stamp_age_mean;
            this.stamp_age_stddev = stamp_age_stddev;
            this.stamp_age_max = stamp_age_max;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TopicStatistics(Buffer b)
        {
            this.topic = BuiltIns.DeserializeString(b);
            this.node_pub = BuiltIns.DeserializeString(b);
            this.node_sub = BuiltIns.DeserializeString(b);
            this.window_start = BuiltIns.DeserializeStruct<time>(b);
            this.window_stop = BuiltIns.DeserializeStruct<time>(b);
            this.delivered_msgs = BuiltIns.DeserializeStruct<int>(b);
            this.dropped_msgs = BuiltIns.DeserializeStruct<int>(b);
            this.traffic = BuiltIns.DeserializeStruct<int>(b);
            this.period_mean = BuiltIns.DeserializeStruct<duration>(b);
            this.period_stddev = BuiltIns.DeserializeStruct<duration>(b);
            this.period_max = BuiltIns.DeserializeStruct<duration>(b);
            this.stamp_age_mean = BuiltIns.DeserializeStruct<duration>(b);
            this.stamp_age_stddev = BuiltIns.DeserializeStruct<duration>(b);
            this.stamp_age_max = BuiltIns.DeserializeStruct<duration>(b);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new TopicStatistics(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            BuiltIns.Serialize(this.topic, b);
            BuiltIns.Serialize(this.node_pub, b);
            BuiltIns.Serialize(this.node_sub, b);
            BuiltIns.Serialize(this.window_start, b);
            BuiltIns.Serialize(this.window_stop, b);
            BuiltIns.Serialize(this.delivered_msgs, b);
            BuiltIns.Serialize(this.dropped_msgs, b);
            BuiltIns.Serialize(this.traffic, b);
            BuiltIns.Serialize(this.period_mean, b);
            BuiltIns.Serialize(this.period_stddev, b);
            BuiltIns.Serialize(this.period_max, b);
            BuiltIns.Serialize(this.stamp_age_mean, b);
            BuiltIns.Serialize(this.stamp_age_stddev, b);
            BuiltIns.Serialize(this.stamp_age_max, b);
        }
        
        public void Validate()
        {
            if (topic is null) throw new System.NullReferenceException();
            if (node_pub is null) throw new System.NullReferenceException();
            if (node_sub is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 88;
                size += BuiltIns.UTF8.GetByteCount(topic);
                size += BuiltIns.UTF8.GetByteCount(node_pub);
                size += BuiltIns.UTF8.GetByteCount(node_sub);
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "rosgraph_msgs/TopicStatistics";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "10152ed868c5097a5e2e4a89d7daa710";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE32RXW6DMBCE3znFSnmtkqo9RU+BDF7CSvhH3gWS23cdTEIh6htefzPjWU7gjUMIHUiP" +
                "ICFSW7Ek8tdyqE7gg0Ugu0JxbAbiHtMK5vtap29YHhtuEzU7mBf4QYgRYqGWwcQ43DVW58QgpO+aydsw" +
                "V5vvWgVJdpMQH9mj06Ac7ZDZXJHB4kATJrRgx6VU/zQlL99fL6J2fOWnC/+1SSHG/0yW+8UiF0um66g9" +
                "8h9AHpq7IBdlIbPIofEXFmtxujhzg4iJgoUGZUb0IHN4PqhSY11b8AWqs/YwXLyOrLm9i1Pf9a+VGGgM" +
                "a2lV6jC30p3r9l3MJbZcj8ZiOsOPh1YlBV9vbdAN+iDQmwnBFFo3IbqUYdCC8Hl+vfKRUKtwV+o13/fa" +
                "KHK1XzP4/jHTAgAA";
                
    }
}
