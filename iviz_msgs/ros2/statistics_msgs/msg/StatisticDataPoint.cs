/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.StatisticsMsgs
{
    [DataContract]
    public sealed class StatisticDataPoint : IDeserializable<StatisticDataPoint>, IMessageRos2
    {
        //############################################
        // This holds the structure of a single data point of a StatisticDataType.
        //
        // This message is used in MetricsStatisticsMessage, defined in MetricsStatisticsMessage.msg.
        //
        // Examples of the value of data point are
        // - average size of messages received
        // - standard deviation of the period of messages published
        // - maximum age of messages published
        //
        // A value of nan represents no data is available.
        // One example is that standard deviation is only available when there are two or more data points but there is only one,
        // and in this case the value would be nan.
        // +inf and -inf are not allowed.
        //
        //############################################
        // The statistic type of this data point, defined in StatisticDataType.msg
        // Default value should be StatisticDataType.STATISTICS_DATA_TYPE_UNINITIALIZED (0).
        [DataMember (Name = "data_type")] public byte DataType;
        // The value of the data point
        [DataMember (Name = "data")] public double Data;
    
        /// Constructor for empty message.
        public StatisticDataPoint()
        {
        }
        
        /// Explicit constructor.
        public StatisticDataPoint(byte DataType, double Data)
        {
            this.DataType = DataType;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public StatisticDataPoint(ref ReadBuffer2 b)
        {
            b.Deserialize(out DataType);
            b.Deserialize(out Data);
        }
        
        public StatisticDataPoint RosDeserialize(ref ReadBuffer2 b) => new StatisticDataPoint(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(DataType);
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 9;
        
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, DataType);
            WriteBuffer2.Advance(ref c, Data);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "statistics_msgs/StatisticDataPoint";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
