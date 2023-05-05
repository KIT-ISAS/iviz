#nullable enable

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.Ros
{
    internal sealed class SubscribedTopic<T> : RosCallback<T>, ISubscribedTopic
        where T : IMessage, new()
    {
        const int NumRetries = 3;
        const int WaitBetweenRetriesInMs = 500;

        readonly string topic;
        readonly RosTransportHint transportHint;
        readonly bool requiresDispose;

        Listener<T>[] listeners = Array.Empty<Listener<T>>();
        BagListener? bagListener;
        string? subscriberId;
        string? bagId;

        public IRosSubscriber? Subscriber { get; private set; }

        public SubscribedTopic(string topic, RosTransportHint transportHint)
        {
            ThrowHelper.ThrowIfNull(topic, nameof(topic));
            this.topic = topic;
            this.transportHint = transportHint;
            requiresDispose = typeof(IDisposable).IsAssignableFrom(typeof(T));
        }

        public void Add(Listener subscriber)
        {
            if (listeners.Contains(subscriber))
            {
                return;
            }

            listeners = listeners.Append((Listener<T>)subscriber).ToArray();
        }

        public void Remove(Listener subscriber)
        {
            if (!listeners.Contains(subscriber))
            {
                return;
            }

            listeners = listeners.Where(s => s != subscriber).ToArray();
        }

        public async ValueTask SubscribeAsync(IRosClient? client, Listener? listener, CancellationToken token)
        {
            if (listener != null)
            {
                Add(listener);
            }

            token.ThrowIfCancellationRequested();
            if (client == null)
            {
                Subscriber = null;
                return;
            }

            for (int i = 0; i < NumRetries; i++)
            {
                try
                {
                    IRosSubscriber subscriber;
                    (subscriberId, subscriber) = await client.SubscribeAsync(topic, this, transportHint, token);
                    if (bagListener != null)
                    {
                        bagId = subscriber.Subscribe(bagListener.EnqueueMessage);
                    }

                    Subscriber = subscriber;
                    break;
                }
                catch (RoslibException e)
                {
                    RosLogger.Error($"{ToString()}: Failed to subscribe to topic (try {i.ToString()}): ", e);
                    await Task.Delay(WaitBetweenRetriesInMs, token);
                }
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

        public int Count => listeners.Length;

        public void Invalidate()
        {
            Subscriber = null;
        }

        public override void Handle(T msg, IRosConnection receiver)
        {
            var cache = listeners;
            foreach (var listener in cache)
            {
                listener.EnqueueMessage(msg, receiver);
            }

            if (requiresDispose)
            {
                ((IDisposable)msg).Dispose();
            }
        }

        public override string ToString() => $"[{nameof(SubscribedTopic<T>)} '{topic}']";
    }
}