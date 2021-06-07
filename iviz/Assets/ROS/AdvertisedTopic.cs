using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib;
using JetBrains.Annotations;

namespace Iviz.Ros
{
    
    internal interface IAdvertisedTopic
    {
        [CanBeNull] IRosPublisher Publisher { get; }
        int Id { get; set; }
        int Count { get; }
        void Add([NotNull] ISender subscriber);
        void Remove([NotNull] ISender subscriber);
        [NotNull] Task AdvertiseAsync([CanBeNull] RosClient client, CancellationToken token);
        [NotNull] Task UnadvertiseAsync([NotNull] RosClient client, CancellationToken token);
        void Invalidate();
    }
    
    internal sealed class AdvertisedTopic<T> : IAdvertisedTopic where T : IMessage
    {
        readonly HashSet<Sender<T>> senders = new HashSet<Sender<T>>();
        [NotNull] readonly string topic;
        int id;

        public AdvertisedTopic([NotNull] string topic)
        {
            this.topic = topic ?? throw new ArgumentNullException(nameof(topic));
        }

        public IRosPublisher Publisher { get; private set; }

        public int Id
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

        public async Task AdvertiseAsync(RosClient client, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            string fullTopic = topic[0] == '/' ? topic : $"{client?.CallerId}/{topic}";
            IRosPublisher publisher;
            if (client != null)
            {
                (_, publisher) = await client.AdvertiseAsync<T>(fullTopic, token);
            }
            else
            {
                publisher = null;
            }

            Publisher = publisher;
        }

        public async Task UnadvertiseAsync(RosClient client, CancellationToken token)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            token.ThrowIfCancellationRequested();
            string fullTopic = topic[0] == '/' ? topic : $"{client.CallerId}/{topic}";
            if (Publisher != null)
            {
                await Publisher.UnadvertiseAsync(fullTopic, token);
            }
        }

        public void Invalidate()
        {
            Id = RoslibConnection.InvalidId;
            Publisher = null;
        }

        [NotNull]
        public override string ToString()
        {
            return $"[AdvertisedTopic '{topic}']";
        }
    }
}