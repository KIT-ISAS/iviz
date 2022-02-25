/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosgraphMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TopicStatistics : IDeserializable<TopicStatistics>, IMessage
    {
        // name of the topic
        [DataMember (Name = "topic")] public string Topic;
        // node id of the publisher
        [DataMember (Name = "node_pub")] public string NodePub;
        // node id of the subscriber
        [DataMember (Name = "node_sub")] public string NodeSub;
        // the statistics apply to this time window
        [DataMember (Name = "window_start")] public time WindowStart;
        [DataMember (Name = "window_stop")] public time WindowStop;
        // number of messages delivered during the window
        [DataMember (Name = "delivered_msgs")] public int DeliveredMsgs;
        // numbers of messages dropped during the window
        [DataMember (Name = "dropped_msgs")] public int DroppedMsgs;
        // traffic during the window, in bytes
        [DataMember (Name = "traffic")] public int Traffic;
        // mean/stddev/max period between two messages
        [DataMember (Name = "period_mean")] public duration PeriodMean;
        [DataMember (Name = "period_stddev")] public duration PeriodStddev;
        [DataMember (Name = "period_max")] public duration PeriodMax;
        // mean/stddev/max age of the message based on the
        // timestamp in the message header. In case the
        // message does not have a header, it will be 0.
        [DataMember (Name = "stamp_age_mean")] public duration StampAgeMean;
        [DataMember (Name = "stamp_age_stddev")] public duration StampAgeStddev;
        [DataMember (Name = "stamp_age_max")] public duration StampAgeMax;
    
        /// Constructor for empty message.
        public TopicStatistics()
        {
            Topic = "";
            NodePub = "";
            NodeSub = "";
        }
        
        /// Explicit constructor.
        public TopicStatistics(string Topic, string NodePub, string NodeSub, time WindowStart, time WindowStop, int DeliveredMsgs, int DroppedMsgs, int Traffic, duration PeriodMean, duration PeriodStddev, duration PeriodMax, duration StampAgeMean, duration StampAgeStddev, duration StampAgeMax)
        {
            this.Topic = Topic;
            this.NodePub = NodePub;
            this.NodeSub = NodeSub;
            this.WindowStart = WindowStart;
            this.WindowStop = WindowStop;
            this.DeliveredMsgs = DeliveredMsgs;
            this.DroppedMsgs = DroppedMsgs;
            this.Traffic = Traffic;
            this.PeriodMean = PeriodMean;
            this.PeriodStddev = PeriodStddev;
            this.PeriodMax = PeriodMax;
            this.StampAgeMean = StampAgeMean;
            this.StampAgeStddev = StampAgeStddev;
            this.StampAgeMax = StampAgeMax;
        }
        
        /// Constructor with buffer.
        public TopicStatistics(ref ReadBuffer b)
        {
            Topic = b.DeserializeString();
            NodePub = b.DeserializeString();
            NodeSub = b.DeserializeString();
            WindowStart = b.Deserialize<time>();
            WindowStop = b.Deserialize<time>();
            DeliveredMsgs = b.Deserialize<int>();
            DroppedMsgs = b.Deserialize<int>();
            Traffic = b.Deserialize<int>();
            PeriodMean = b.Deserialize<duration>();
            PeriodStddev = b.Deserialize<duration>();
            PeriodMax = b.Deserialize<duration>();
            StampAgeMean = b.Deserialize<duration>();
            StampAgeStddev = b.Deserialize<duration>();
            StampAgeMax = b.Deserialize<duration>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TopicStatistics(ref b);
        
        public TopicStatistics RosDeserialize(ref ReadBuffer b) => new TopicStatistics(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Topic);
            b.Serialize(NodePub);
            b.Serialize(NodeSub);
            b.Serialize(WindowStart);
            b.Serialize(WindowStop);
            b.Serialize(DeliveredMsgs);
            b.Serialize(DroppedMsgs);
            b.Serialize(Traffic);
            b.Serialize(PeriodMean);
            b.Serialize(PeriodStddev);
            b.Serialize(PeriodMax);
            b.Serialize(StampAgeMean);
            b.Serialize(StampAgeStddev);
            b.Serialize(StampAgeMax);
        }
        
        public void RosValidate()
        {
            if (Topic is null) BuiltIns.ThrowNullReference(nameof(Topic));
            if (NodePub is null) BuiltIns.ThrowNullReference(nameof(NodePub));
            if (NodeSub is null) BuiltIns.ThrowNullReference(nameof(NodeSub));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 88;
                size += BuiltIns.GetStringSize(Topic);
                size += BuiltIns.GetStringSize(NodePub);
                size += BuiltIns.GetStringSize(NodeSub);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosgraph_msgs/TopicStatistics";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "10152ed868c5097a5e2e4a89d7daa710";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE32RXW6DMBCE3znFSnmtkqo9RU+BDF7CSvhH3gWS23cdTEIh6htefzPjWU7gjUMIHUiP" +
                "ICFSW7Ek8tdyqE7gg0Ugu0JxbAbiHtMK5vtap29YHhtuEzU7mBf4QYgRYqGWwcQ43DVW58QgpO+aydsw" +
                "V5vvWgVJdpMQH9mj06Ac7ZDZXJHB4kATJrRgx6VU/zQlL99fL6J2fOWnC/+1SSHG/0yW+8UiF0um66g9" +
                "8h9AHpq7IBdlIbPIofEXFmtxujhzg4iJgoUGZUb0IHN4PqhSY11b8AWqs/YwXLyOrLm9i1Pf9a+VGGgM" +
                "a2lV6jC30p3r9l3MJbZcj8ZiOsOPh1YlBV9vbdAN+iDQmwnBFFo3IbqUYdCC8Hl+vfKRUKtwV+o13/fa" +
                "KHK1XzP4/jHTAgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
