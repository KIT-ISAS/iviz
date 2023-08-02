#nullable enable
using Iviz.Bridge.Client;
using Iviz.Roslib;
using Iviz.Roslib2;

namespace Iviz.Ros
{
    public sealed class RosSubscriptionProfile : IRos1SubscriptionProfile, IRos2SubscriptionProfile,
        IRosbridgeSubscriptionProfile
    {
        public static RosSubscriptionProfile SpammyCanDrop { get; } = new(
            RosTransportHint.PreferUdp,
            QosProfile.SensorData,
            1000 / 50, // ms
            5);

        public static RosSubscriptionProfile SensorKeepLast { get; } = new(
            RosTransportHint.PreferTcp,
            QosProfile.SensorData,
            1000 / 20, // ms
            1);

        public static RosSubscriptionProfile ImportantKeepAll { get; } = new(
            RosTransportHint.PreferTcp,
            QosProfile.SubscriberDefault,
            0, // ms
            100); // ideally infinite, but rosbridge may commit seppuku

        public static RosSubscriptionProfile Default => ImportantKeepAll;

        /// <summary> ROS1: TCP or UDP? </summary>
        public RosTransportHint TransportHint { get; }

        /// <summary> ROS2 Profile </summary>
        public QosProfile Profile { get; }

        /// <summary> ROSBridge: Max messages to send per second </summary>
        public int ThrottleRate { get; }

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