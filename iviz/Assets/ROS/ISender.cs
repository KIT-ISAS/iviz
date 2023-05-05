#nullable enable

using System.Text;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Roslib;

namespace Iviz.Ros
{
    /// <summary>
    /// A wrapper around a <see cref="RosPublisher{TMessage}"/> that persists even if the connection is interrupted.
    /// After a connection is reestablished, this sender is used to readvertise the topic transparently.
    /// There can be multiple Senders that refer to the same shared publisher.
    /// The original is stored in an <see cref="AdvertisedTopic{T}"/>.  
    /// </summary>
    public abstract class Sender
    {
        internal int? Id { get; set; }
        public string Topic { get; }
        public string Type { get; }
        public RosSenderStats Stats { get; protected set; }
        public int NumSubscribers { get; protected set; }

        protected Sender(string topic, string type)
        {
            ThrowHelper.ThrowIfNullOrEmpty(topic, nameof(topic));
            ThrowHelper.ThrowIfNullOrEmpty(type, nameof(type));

            Topic = topic;
            Type = type;
        }

        public void WriteDescriptionTo(StringBuilder description)
        {
            if (Connection.GetNumSubscribers(Id) is not { } numSubscribers)
            {
                description.Append("Off");
                return;
            }

            description.Append(numSubscribers).Append(" sub");
        }

        public override string ToString() => $"[{nameof(Sender)} {Topic} [{Type}]]";

        public abstract void Dispose();
        public abstract void Publish(IMessage msg);

        private protected static RosConnection Connection => RosManager.RosConnection;
    }
}