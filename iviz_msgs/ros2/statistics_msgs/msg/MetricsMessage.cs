/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.StatisticsMsgs
{
    [DataContract]
    public sealed class MetricsMessage : IDeserializable<MetricsMessage>, IMessageRos2
    {
        //############################################
        // A generic metrics message providing statistics for measurements from different sources. For example,
        // measure a system's CPU % for a given window yields the following data points over a window of time:
        //
        //   - average cpu %
        //   - std deviation
        //   - min
        //   - max
        //   - sample count
        //
        // These are all represented as different 'StatisticDataPoint's.
        //############################################
        // Name metric measurement source, e.g., node, topic, or process name
        [DataMember (Name = "measurement_source_name")] public string MeasurementSourceName;
        // Name of the metric being measured, e.g. cpu_percentage, free_memory_mb, message_age, etc.
        [DataMember (Name = "metrics_source")] public string MetricsSource;
        // Unit of measure of the metric, e.g. percent, mb, seconds, etc.
        [DataMember (Name = "unit")] public string Unit;
        // Measurement window start time
        [DataMember (Name = "window_start")] public time WindowStart;
        // Measurement window end time
        [DataMember (Name = "window_stop")] public time WindowStop;
        // A list of statistics data point, defined in StatisticDataPoint.msg
        [DataMember (Name = "statistics")] public StatisticDataPoint[] Statistics;
    
        /// Constructor for empty message.
        public MetricsMessage()
        {
            MeasurementSourceName = "";
            MetricsSource = "";
            Unit = "";
            Statistics = System.Array.Empty<StatisticDataPoint>();
        }
        
        /// Constructor with buffer.
        public MetricsMessage(ref ReadBuffer2 b)
        {
            b.DeserializeString(out MeasurementSourceName);
            b.DeserializeString(out MetricsSource);
            b.DeserializeString(out Unit);
            b.Deserialize(out WindowStart);
            b.Deserialize(out WindowStop);
            b.DeserializeArray(out Statistics);
            for (int i = 0; i < Statistics.Length; i++)
            {
                Statistics[i] = new StatisticDataPoint(ref b);
            }
        }
        
        public MetricsMessage RosDeserialize(ref ReadBuffer2 b) => new MetricsMessage(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(MeasurementSourceName);
            b.Serialize(MetricsSource);
            b.Serialize(Unit);
            b.Serialize(WindowStart);
            b.Serialize(WindowStop);
            b.SerializeArray(Statistics);
        }
        
        public void RosValidate()
        {
            if (MeasurementSourceName is null) BuiltIns.ThrowNullReference();
            if (MetricsSource is null) BuiltIns.ThrowNullReference();
            if (Unit is null) BuiltIns.ThrowNullReference();
            if (Statistics is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Statistics.Length; i++)
            {
                if (Statistics[i] is null) BuiltIns.ThrowNullReference(nameof(Statistics), i);
                Statistics[i].RosValidate();
            }
        }
    
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, MeasurementSourceName);
            WriteBuffer2.Advance(ref c, MetricsSource);
            WriteBuffer2.Advance(ref c, Unit);
            WriteBuffer2.Advance(ref c, WindowStart);
            WriteBuffer2.Advance(ref c, WindowStop);
            WriteBuffer2.Advance(ref c, Statistics);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "statistics_msgs/MetricsMessage";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
