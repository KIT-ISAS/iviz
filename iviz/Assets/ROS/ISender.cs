#nullable enable

using System.Text;
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
    public interface ISender
    {
        string Topic { get; }
        string Type { get; }
        internal int? Id { get; set; }
        RosSenderStats Stats { get; }
        int NumSubscribers { get; }
        void Dispose();
        void Publish(IMessage msg);
        void WriteDescriptionTo(StringBuilder description);
    }
}