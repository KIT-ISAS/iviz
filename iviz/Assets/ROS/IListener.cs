using System;
using System.Text;
using Iviz.Msgs;
using Iviz.Roslib;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Ros
{
    /// <summary>
    /// Wrapper around a ROS subscriber.
    /// </summary>
    public interface IListener
    {
        [NotNull] string Topic { get; }
        [NotNull] string Type { get; }
        RosTransportHint TransportHint { get; set; }
        RosListenerStats Stats { get; }
        (int active, int total) NumPublishers { get; }
        int MaxQueueSize { set; }
        bool Subscribed { get; }
        void Stop();
        void SetSuspend(bool value);
        void Reset();
        void SetPause(bool value);
        void WriteDescriptionTo([NotNull] StringBuilder b);
    }
}