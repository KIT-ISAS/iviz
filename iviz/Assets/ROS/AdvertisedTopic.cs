#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.Ros
{
    internal sealed class AdvertisedTopic<T> : IAdvertisedTopic where T : IMessage
    {
        const int NumRetries = 3;
        const int WaitBetweenRetriesInMs = 500;

        readonly HashSet<Sender<T>> senders = new();
        readonly string topic;
        string? publisherId;
        
        int? id;

        public IRosPublisher? Publisher { get; private set; }

        public AdvertisedTopic(string topic)
        {
            this.topic = topic ?? throw new ArgumentNullException(nameof(topic));
        }

        public int? Id
        {
            get => id;
            set
            {
                id = value;
                foreach (var sender in senders)
                {
                    sender.Id = value;
                }
            }
        }

        public void Add(ISender publisher)
        {
            senders.Add((Sender<T>) publisher);
        }

        public void Remove(ISender publisher)
        {
            senders.Remove((Sender<T>) publisher);
        }

        public int Count => senders.Count;

        public async ValueTask AdvertiseAsync(RosClient? client, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            if (client != null)
            {
                foreach (int t in ..NumRetries)
                {
                    try
                    {
                        IRosPublisher publisher;
                        (publisherId, publisher) = await client.AdvertiseAsync<T>(topic, token);
                        Publisher = publisher;
                        return;
                    }
                    catch (RoslibException e)
                    {
                        RosLogger.Error($"{this}: Failed to advertise topic (try {t.ToString()}): ", e);
                        await Task.Delay(WaitBetweenRetriesInMs, token);
                    }
                }
            }

            publisherId = null;
            Publisher = null;
        }

        public async ValueTask UnadvertiseAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            if (Publisher != null && publisherId != null)
            {
                await Publisher.UnadvertiseAsync(publisherId, token);
            }
        }

        public void Invalidate()
        {
            Id = null;
            Publisher = null;
            publisherId = null;
        }

        public override string ToString()
        {
            return $"[AdvertisedTopic '{topic}']";
        }
    }
}