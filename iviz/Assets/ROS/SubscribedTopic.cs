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
    internal sealed class SubscribedTopic<T> : ISubscribedTopic where T : IMessage, IDeserializable<T>, new()
    {
        const int NumRetries = 3;
        const int WaitBetweenRetriesInMs = 500;

        readonly string topic;
        readonly RosTransportHint transportHint;

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
        }

        public void Add(IListener subscriber)
        {
            if (listeners.Contains(subscriber))
            {
                return;
            }

            listeners = listeners.Append((Listener<T>)subscriber).ToArray();
        }

        public void Remove(IListener subscriber)
        {
            if (!listeners.Contains(subscriber))
            {
                return;
            }

            listeners = listeners.Where(s => s != subscriber).ToArray();
        }

        public async ValueTask SubscribeAsync(IRosClient? client, IListener? listener, CancellationToken token)
        {
            if (listener != null)
            {
                Add(listener);
            }

            token.ThrowIfCancellationRequested();
            if (client != null)
            {
                foreach (int t in ..NumRetries)
                {
                    try
                    {
                        IRosSubscriber subscriber;
                        (subscriberId, subscriber) = await client.SubscribeAsync<T>(topic, Callback, transportHint, token);
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

        public int Count => listeners.Length;

        public void Invalidate()
        {
            Subscriber = null;
        }

        void Callback(in T msg, IRosReceiver receiver)
        {
            var cache = listeners;
            try
            {
                foreach (var listener in cache)
                {
                    try
                    {
                        listener.EnqueueMessage(in msg, receiver);
                    }
                    catch (Exception e)
                    {
                        RosLogger.Error($"{this}: Error in {nameof(Callback)}", e);
                    }
                }
            }
            finally
            {
                msg.Dispose();
            }
        }

        public override string ToString() => $"[{nameof(SubscribedTopic<T>)} '{topic}']";
    }
}