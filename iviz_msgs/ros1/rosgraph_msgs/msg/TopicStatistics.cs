/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosgraphMsgs
{
    [DataContract]
    public sealed class TopicStatistics : IDeserializableRos1<TopicStatistics>, IDeserializableRos2<TopicStatistics>, IMessageRos1, IMessageRos2
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
        
        /// Constructor with buffer.
        public TopicStatistics(ref ReadBuffer b)
        {
            b.DeserializeString(out Topic);
            b.DeserializeString(out NodePub);
            b.DeserializeString(out NodeSub);
            b.Deserialize(out WindowStart);
            b.Deserialize(out WindowStop);
            b.Deserialize(out DeliveredMsgs);
            b.Deserialize(out DroppedMsgs);
            b.Deserialize(out Traffic);
            b.Deserialize(out PeriodMean);
            b.Deserialize(out PeriodStddev);
            b.Deserialize(out PeriodMax);
            b.Deserialize(out StampAgeMean);
            b.Deserialize(out StampAgeStddev);
            b.Deserialize(out StampAgeMax);
        }
        
        /// Constructor with buffer.
        public TopicStatistics(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Topic);
            b.DeserializeString(out NodePub);
            b.DeserializeString(out NodeSub);
            b.Deserialize(out WindowStart);
            b.Deserialize(out WindowStop);
            b.Deserialize(out DeliveredMsgs);
            b.Deserialize(out DroppedMsgs);
            b.Deserialize(out Traffic);
            b.Deserialize(out PeriodMean);
            b.Deserialize(out PeriodStddev);
            b.Deserialize(out PeriodMax);
            b.Deserialize(out StampAgeMean);
            b.Deserialize(out StampAgeStddev);
            b.Deserialize(out StampAgeMax);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new TopicStatistics(ref b);
        
        public TopicStatistics RosDeserialize(ref ReadBuffer b) => new TopicStatistics(ref b);
        
        public TopicStatistics RosDeserialize(ref ReadBuffer2 b) => new TopicStatistics(ref b);
    
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
        
        public void RosSerialize(ref WriteBuffer2 b)
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
            if (Topic is null) BuiltIns.ThrowNullReference();
            if (NodePub is null) BuiltIns.ThrowNullReference();
            if (NodeSub is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 88;
                size += WriteBuffer.GetStringSize(Topic);
                size += WriteBuffer.GetStringSize(NodePub);
                size += WriteBuffer.GetStringSize(NodeSub);
                return size;
            }
        }
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Topic);
            WriteBuffer2.AddLength(ref c, NodePub);
            WriteBuffer2.AddLength(ref c, NodeSub);
            WriteBuffer2.AddLength(ref c, WindowStart);
            WriteBuffer2.AddLength(ref c, WindowStop);
            WriteBuffer2.AddLength(ref c, DeliveredMsgs);
            WriteBuffer2.AddLength(ref c, DroppedMsgs);
            WriteBuffer2.AddLength(ref c, Traffic);
            WriteBuffer2.AddLength(ref c, PeriodMean);
            WriteBuffer2.AddLength(ref c, PeriodStddev);
            WriteBuffer2.AddLength(ref c, PeriodMax);
            WriteBuffer2.AddLength(ref c, StampAgeMean);
            WriteBuffer2.AddLength(ref c, StampAgeStddev);
            WriteBuffer2.AddLength(ref c, StampAgeMax);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rosgraph_msgs/TopicStatistics";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "10152ed868c5097a5e2e4a89d7daa710";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE32RXW6DMBCE3znFSnmtkqo9RU+BDF7CSvhH3gWS23cdTEIh6htefzPjWU7gjUMIHUiP" +
                "ICFSW7Ek8tdyqE7gg0Ugu0JxbAbiHtMK5vtap29YHhtuEzU7mBf4QYgRYqGWwcQ43DVW58QgpO+aydsw" +
                "V5vvWgVJdpMQH9mj06Ac7ZDZXJHB4kATJrRgx6VU/zQlL99fL6J2fOWnC/+1SSHG/0yW+8UiF0um66g9" +
                "8h9AHpq7IBdlIbPIofEXFmtxujhzg4iJgoUGZUb0IHN4PqhSY11b8AWqs/YwXLyOrLm9i1Pf9a+VGGgM" +
                "a2lV6jC30p3r9l3MJbZcj8ZiOsOPh1YlBV9vbdAN+iDQmwnBFFo3IbqUYdCC8Hl+vfKRUKtwV+o13/fa" +
                "KHK1XzP4/jHTAgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
