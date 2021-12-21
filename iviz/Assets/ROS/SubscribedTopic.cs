#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.Ros
{
    internal sealed class SubscribedTopic<T> : ISubscribedTopic where T : IMessage, IDeserializable<T>, new()
    {
        const int NumRetries = 3;
        const int WaitBetweenRetriesInMs = 500;

        readonly ConcurrentSet<Listener<T>> listeners = new();
        readonly string topic;
        readonly RosTransportHint transportHint;

        Listener<T>[] listenerCache = Array.Empty<Listener<T>>();
        BagListener? bagListener;
        string? subscriberId;
        string? bagId;

        public IRosSubscriber? Subscriber { get; private set; }
        
        public SubscribedTopic(string topic, RosTransportHint transportHint)
        {
            this.topic = topic ?? throw new ArgumentNullException(nameof(topic));
            this.transportHint = transportHint;
        }

        public void Add(IListener subscriber)
        {
            listeners.Add((Listener<T>)subscriber);
            listenerCache = listeners.ToArray();
        }

        public void Remove(IListener subscriber)
        {
            listeners.Remove((Listener<T>)subscriber);
            listenerCache = listeners.ToArray();
        }

        public async ValueTask SubscribeAsync(RosClient? client, IListener? listener, CancellationToken token)
        {
            if (listener != null)
            {
                listeners.Add((Listener<T>)listener);
            }

            token.ThrowIfCancellationRequested();
            if (client != null)
            {
                foreach (int t in ..NumRetries)
                {
                    try
                    {
                        IRosSubscriber subscriber;
                        (subscriberId, subscriber) = await client.SubscribeAsync<T>(topic, Callback, token: token,
                            transportHint: transportHint);
                        if (bagListener != null)
                        {
                            bagId = subscriber.Subscribe(bagListener.EnqueueMessage);
                        }

                        Subscriber = subscriber;
                        break;
                    }
                    catch (RoslibException e)
                    {
                        RosLogger.Error($"{this}: Failed to subscribe to topic (try {t.ToString()}): ", e);
                        await Task.Delay(WaitBetweenRetriesInMs, token);
                    }
                }
            }
            else
            {
                Subscriber = null;
            }
        }

        public async ValueTask UnsubscribeAsync(CancellationToken token)
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

        public BagListener? BagListener
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

        void Callback(in T msg, IRosReceiver receiver)
        {
            var cache = listenerCache;
            foreach (var listener in cache)
            {
                try
                {
                    listener.EnqueueMessage(in msg, receiver);
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Error in callback", e);
                }
            }
        }

        public override string ToString() => $"[SubscribedTopic '{topic}']";
    }
}