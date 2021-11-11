#nullable enable

using System.Text;
using Iviz.Roslib;

namespace Iviz.Ros
{
    /// <summary>
    /// Wrapper around a ROS subscriber.
    /// </summary>
    public interface IListener
    {
        string Topic { get; }
        string Type { get; }
        RosTransportHint TransportHint { get; set; }
        RosListenerStats Stats { get; }
        (int active, int total) NumPublishers { get; }
        int MaxQueueSize { set; }
        bool Subscribed { get; }
        void Stop();
        void SetSuspend(bool value);
        void Reset();
        void SetPause(bool value);
        void WriteDescriptionTo(StringBuilder b);
    }
}