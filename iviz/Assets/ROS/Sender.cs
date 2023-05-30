#nullable enable

using System;
using System.Text;
using System.Threading;
using Iviz.Core;
using Iviz.Msgs;

namespace Iviz.Ros
{
    /// <inheritdoc cref="Sender"/>
    /// <typeparam name="T">The ROS message type</typeparam>
    public sealed class Sender<T> : Sender where T : IMessage, new()
    {
        Sender(string topic, T generator) : base(topic, generator.RosMessageType, generator.CreateSerializer())
        {
            Connection.Advertise(this);
        }

        public Sender(string topic) : this(topic, new T())
        {
        }

        public void Reset()
        {
            Connection.Unadvertise(this);
            Connection.Advertise(this);
        }
    }
}