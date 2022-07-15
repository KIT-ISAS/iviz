/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.StatisticsMsgs
{
    [DataContract]
    public sealed class StatisticDataType : IDeserializable<StatisticDataType>, IMessageRos2
    {
        //############################################
        // This file contains the commonly used constants for the statistics data type.
        //
        // The value 0 is reserved for unitialized statistic message data type.
        // Range of values: [0, 255].
        // Unallowed values: any value that is not specified in this file.
        //
        //############################################
        // Constant for uninitialized
        public const byte STATISTICS_DATA_TYPE_UNINITIALIZED = 0;
        // Allowed values
        public const byte STATISTICS_DATA_TYPE_AVERAGE = 1;
        public const byte STATISTICS_DATA_TYPE_MINIMUM = 2;
        public const byte STATISTICS_DATA_TYPE_MAXIMUM = 3;
        public const byte STATISTICS_DATA_TYPE_STDDEV = 4;
        public const byte STATISTICS_DATA_TYPE_SAMPLE_COUNT = 5;
    
        /// Constructor for empty message.
        public StatisticDataType()
        {
        }
        
        /// Constructor with buffer.
        public StatisticDataType(ref ReadBuffer2 b)
        {
        }
        
        public StatisticDataType RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static StatisticDataType? singleton;
        public static StatisticDataType Singleton => singleton ??= new StatisticDataType();
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public void GetRosMessageLength(ref int c) { }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "statistics_msgs/StatisticDataType";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
