using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Roslib.Utils;
using JetBrains.Annotations;

namespace Iviz.Ros
{
    internal interface ISubscribedTopic
    {
        [CanBeNull] IRosSubscriber Subscriber { get; }
        int Count { get; }
        void Add([NotNull] IListener subscriber);
        void Remove([NotNull] IListener subscriber);

        [NotNull]
        Task SubscribeAsync([CanBeNull] RosClient client, [CanBeNull] IListener listener = null,
            CancellationToken token = default);

        [NotNull]
        Task UnsubscribeAsync(CancellationToken token);

        void Invalidate();

        [CanBeNull] BagListener BagListener { set; }
    }

    internal sealed class SubscribedTopic<T> : ISubscribedTopic where T : IMessage, IDeserializable<T>, new()
    {
        const int NumRetries = 3;
        const int WaitBetweenRetriesInMs = 500;

        readonly ConcurrentSet<Listener<T>> listeners = new ConcurrentSet<Listener<T>>();
        [NotNull] readonly string topic;
        [CanBeNull] string subscriberId;

        [CanBeNull] BagListener bagListener;
        [CanBeNull] string bagId;

        public RosTransportHint TransportHint { get; }
        
        public SubscribedTopic([NotNull] string topic, RosTransportHint transportHint)
        {
            this.topic = topic ?? throw new ArgumentNullException(nameof(topic));
            TransportHint = transportHint;
        }

        public IRosSubscriber Subscriber { get; private set; }

        public void Add(IListener subscriber)
        {
            listeners.Add((Listener<T>)subscriber);
        }

        public void Remove(IListener subscriber)
        {
            listeners.Remove((Listener<T>)subscriber);
        }

        public async Task SubscribeAsync(RosClient client, IListener listener, CancellationToken token)
        {
            if (listener != null)
            {
                listeners.Add((Listener<T>)listener);
            }

            token.ThrowIfCancellationRequested();
            if (client != null)
            {
                for (int t = 0; t < NumRetries; t++)
                {
                    try
                    {
                        IRosSubscriber subscriber;
                        (subscriberId, subscriber) = await client.SubscribeAsync<T>(topic, Callback, token: token,
                            transportHint: TransportHint);
                        if (bagListener != null)
                        {
                            bagId = subscriber.Subscribe(bagListener.EnqueueMessage);
                        }

                        Subscriber = subscriber;
                        break;
                    }
                    catch (RoslibException e)
                    {
                        Core.Logger.Error($"Failed to subscribe to service (try {t.ToString()}): ", e);
                        await Task.Delay(WaitBetweenRetriesInMs, token);
                    }
                }
            }
            else
            {
                Subscriber = null;
            }
        }

        public async Task UnsubscribeAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (Subscriber == null)
            {
                return;
            }

            BagListener = null;

            if (subscriberId != null)
            {
                await Subscriber.UnsubscribeAsync(subscriberId, token);
                subscriberId = null;
            }

            Subscriber = null;
        }

        public BagListener BagListener
        {
            set
            {
                bagListener = value;
                if (Subscriber == null)
                {
                    return;
                }

                if (bagListener != null)
                {
                    if (bagId != null)
                    {
                        Subscriber.Unsubscribe(bagId);
                    }

                    bagId = Subscriber.Subscribe(bagListener.EnqueueMessage);
                }
                else if (bagId != null)
                {
                    Subscriber.Unsubscribe(bagId);
                    bagId = null;
                }
            }
        }

        public int Count => listeners.Count;

        public void Invalidate()
        {
            Subscriber = null;
        }

        void Callback(T msg)
        {
            foreach (var listener in listeners)
            {
                try
                {
                    listener.EnqueueMessage(msg);
                }
                catch (Exception e)
                {
                    Core.Logger.Error($"{this}: Error in callback", e);
                }
            }
        }

        [NotNull]
        public override string ToString()
        {
            return $"[SubscribedTopic '{topic}']";
        }
    }
}