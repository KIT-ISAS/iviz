﻿#nullable enable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.Ros
{
    public sealed class Listener<T> : IListener where T : IMessage, IDeserializable<T>, new()
    {
        static RoslibConnection Connection => ConnectionManager.Connection;

        readonly ConcurrentQueue<T> messageQueue = new();
        readonly Action<T>? delayedHandler;
        readonly Func<T, IRosReceiver, bool>? directHandler;
        readonly List<T> tmpMessageBag = new(32);
        readonly bool callbackInGameThread;

        int droppedMsgs;
        long lastMsgBytes;
        int totalMsgCounter;
        int recentMsgs;
        RosTransportHint transportHint;

        public RosTransportHint TransportHint
        {
            get => transportHint;
            set
            {
                if (transportHint == value)
                {
                    return;
                }

                transportHint = value;
                Reset();
            }
        }

        public string Topic { get; }
        public string Type { get; }
        public RosListenerStats Stats { get; private set; }
        public (int active, int total) NumPublishers => Connection.GetNumPublishers(Topic);
        public int MaxQueueSize { get; set; } = 1;
        public bool Subscribed { get; private set; }

        Listener(string topic, RosTransportHint transportHint)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentException("Invalid topic!", nameof(topic));
            }

            Topic = topic;
            Type = BuiltIns.GetMessageType(typeof(T));
            this.transportHint = transportHint;

            RosLogger.Info($"Subscribing to <b>{topic}</b> <i>[{Type}]</i>.");

            GameThread.EverySecond += UpdateStats;
        }

        public Listener(string topic, Action<T> handler,
            RosTransportHint transportHint = RosTransportHint.PreferTcp) : this(topic, transportHint)
        {
            delayedHandler = handler ?? throw new ArgumentNullException(nameof(handler));
            callbackInGameThread = true;
            GameThread.ListenersEveryFrame += CallHandlerDelayed;
            Connection.Subscribe(this);
            Subscribed = true;
        }

        public Listener(string topic, Func<T, IRosReceiver, bool> handler,
            RosTransportHint transportHint = RosTransportHint.PreferTcp) : this(topic, transportHint)
        {
            directHandler = handler ?? throw new ArgumentNullException(nameof(handler));
            callbackInGameThread = false;
            Connection.Subscribe(this);
            Subscribed = true;
        }

        public Listener(string topic, Func<T, bool> handler,
            RosTransportHint transportHint = RosTransportHint.PreferTcp) :
            this(topic, (t, _) => handler(t), transportHint)
        {
        }

        // used by EchoDialogData
        [Preserve]
        public Listener(string topic, Func<IMessage, IRosReceiver, bool> handler, RosTransportHint transportHint)
            : this(topic, (T msg, IRosReceiver receiver) => handler(msg, receiver), transportHint)
        {
        }

        public void Stop()
        {
            GameThread.EverySecond -= UpdateStats;
            if (callbackInGameThread)
            {
                GameThread.ListenersEveryFrame -= CallHandlerDelayed;
            }

            RosLogger.Info($"Unsubscribing from {Topic}.");
            if (Subscribed)
            {
                Subscribed = false;
                Connection.Unsubscribe(this);
            }
        }

        public void Suspend()
        {
            if (!Subscribed)
            {
                return;
            }

            Connection.Unsubscribe(this);
            Subscribed = false;
        }

        public void Unsuspend()
        {
            if (Subscribed)
            {
                return;
            }

            Connection.Subscribe(this);
            Subscribed = true;
        }

        public void SetSuspend(bool value)
        {
            if (value)
            {
                Suspend();
            }
            else
            {
                Unsuspend();
            }
        }

        public void SetPause(bool value)
        {
            Connection.SetPause(this, value);
        }

        public void Reset()
        {
            Suspend();
            Unsuspend();
        }

        internal void EnqueueMessage(in T msg, IRosReceiver receiver)
        {
            if (!Subscribed)
            {
                return;
            }

            if (callbackInGameThread)
            {
                messageQueue.Enqueue(msg);
                return;
            }

            CallHandlerDirect(msg, receiver);
        }

        void CallHandlerDelayed()
        {
            if (delayedHandler == null)
            {
                return;
            }

            tmpMessageBag.Clear();
            while (messageQueue.TryDequeue(out T t))
            {
                tmpMessageBag.Add(t);
            }

            int start = Math.Max(0, tmpMessageBag.Count - MaxQueueSize);
            foreach (var msg in tmpMessageBag.Skip(start))
            {
                Interlocked.Increment(ref recentMsgs);
                try
                {
                    lastMsgBytes += msg.RosMessageLength;
                    delayedHandler(msg);
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this} Error during callback: ", e);
                }
            }

            droppedMsgs += start;
            totalMsgCounter += messageQueue.Count;
            recentMsgs += messageQueue.Count;
        }

        void CallHandlerDirect(in T msg, IRosReceiver receiver)
        {
            if (directHandler == null)
            {
                return;
            }

            Interlocked.Increment(ref totalMsgCounter);
            Interlocked.Increment(ref recentMsgs);

            bool processed;
            try
            {
                Interlocked.Add(ref lastMsgBytes, msg.RosMessageLength);
                processed = directHandler(msg, receiver);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this} Error during callback: ", e);
                processed = false;
            }

            if (!processed)
            {
                Interlocked.Increment(ref droppedMsgs);
            }
        }

        void UpdateStats()
        {
            if (recentMsgs == 0)
            {
                Stats = RosListenerStats.Empty;
                return;
            }

            Stats = new RosListenerStats(
                totalMsgCounter,
                recentMsgs,
                lastMsgBytes,
                messageQueue.Count,
                droppedMsgs
            );

            ConnectionManager.ReportBandwidthDown(lastMsgBytes);

            Interlocked.Exchange(ref lastMsgBytes, 0);
            Interlocked.Exchange(ref droppedMsgs, 0);
            Interlocked.Exchange(ref recentMsgs, 0);
        }

        public void WriteDescriptionTo(StringBuilder description)
        {
            (int numActivePublishers, int numPublishers) = NumPublishers;
            if (numPublishers == -1)
            {
                description.Append("Off");
            }
            else if (!Subscribed)
            {
                description.Append("PAUSED");
            }
            else
            {
                description.Append(numActivePublishers.ToString())
                    .Append("/")
                    .Append(numPublishers.ToString())
                    .Append(" pub");
            }
        }

        public override string ToString()
        {
            return $"[Listener {Topic} [{Type}]]";
        }
    }

    public static class Listener
    {
        public static IListener Create(string topicName, Func<IMessage, IRosReceiver, bool> handler, Type csType)
        {
            Type listenerType = typeof(Listener<>).MakeGenericType(csType);
            return (IListener)Activator.CreateInstance(listenerType,
                topicName, handler, RosTransportHint.PreferTcp);
        }
    }
}