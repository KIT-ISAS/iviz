#nullable enable

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.Ros
{
    internal sealed class AdvertisedTopic<T> : AdvertisedTopic where T : IMessage, new()
    {
        const int NumRetries = 3;
        const int WaitBetweenRetriesInMs = 500;

        readonly string topic;
        string? publisherId;

        public AdvertisedTopic(string topic)
        {
            ThrowHelper.ThrowIfNull(topic, nameof(topic));
            this.topic = topic;
        }

        public override void Add(Sender publisher)
        {
            if (publisher is not Sender<T>)
            {
                BuiltIns.ThrowArgument(nameof(publisher), "Message type does not match");
            }

            senders.Add(publisher);
        }

        public override void Remove(Sender publisher)
        {
            if (publisher is not Sender<T>)
            {
                BuiltIns.ThrowArgument(nameof(publisher), "Message type does not match");
            }

            senders.Remove(publisher);
        }

        public override async ValueTask AdvertiseAsync(IRosClient? client, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            if (client == null)
            {
                publisherId = null;
                Publisher = null;
                return;
            }

            for (int t = 0; t < NumRetries; t++)
            {
                try
                {
                    (publisherId, Publisher) = await client.AdvertiseAsync<T>(topic, token: token);
                    return;
                }
                catch (RoslibException e)
                {
                    RosLogger.Error($"{ToString()}: Failed to advertise topic '{topic}' (try {t.ToString()}): ", e);
                    await Task.Delay(WaitBetweenRetriesInMs, token);
                }
            }

            publisherId = null;
            Publisher = null;
        }

        public override async ValueTask UnadvertiseAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            if (Publisher != null && publisherId != null)
            {
                await Publisher.UnadvertiseAsync(publisherId, token);
            }
        }

        public override void Invalidate()
        {
            Id = null;
            Publisher = null;
            publisherId = null;
        }

        public override string ToString()
        {
            return $"[{nameof(AdvertisedTopic<T>)} '{topic}']";
        }
    }
}