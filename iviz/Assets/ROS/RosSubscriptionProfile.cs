#nullable enable
using Iviz.Bridge.Client;
using Iviz.Roslib;
using Iviz.Roslib2;

namespace Iviz.Ros
{
    public sealed class RosSubscriptionProfile : IRos1SubscriptionProfile, IRos2SubscriptionProfile, IRosbridgeSubscriptionProfile
    {
        public const RosSubscriptionProfile? Default = null;

        public static readonly RosSubscriptionProfile SpammyCanDrop = new(
            RosTransportHint.PreferUdp,
            QosProfile.SensorData,
            60,
            5);

        public static readonly RosSubscriptionProfile SensorKeepLast = new(
            RosTransportHint.PreferTcp,
            QosProfile.SensorData,
            10,
            1);
        
        public static readonly RosSubscriptionProfile ImportantKeepAll = new(
            RosTransportHint.PreferTcp,
            QosProfile.SubscriberDefault,
            100, // ideally both infinite, but rosbridge may commit seppuku
            100);

        /// <summary> ROS1: TCP or UDP? </summary>
        public RosTransportHint TransportHint { get; } 
        
        /// <summary> ROS2 Profile </summary>
        public QosProfile Profile { get; }
        
        /// <summary> ROSBridge: Max messages to send per second </summary>
        public int ThrottleRate { get;  }

        /// <summary> ROSBridge: Max messages to store in queue </summary>
        public int QueueLength { get; } 
        
        RosSubscriptionProfile(RosTransportHint transportHint, QosProfile profile, int throttleRate, int queueLength)
        {
            TransportHint = transportHint;
            Profile = profile;
            ThrottleRate = throttleRate;
            QueueLength = queueLength;
        }        
    }
}