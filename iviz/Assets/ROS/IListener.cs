using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Tools;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Ros
{
    /// <summary>
    /// Wrapper around a ROS subscriber.
    /// </summary>
    public interface IListener
    {
        [NotNull] string Topic { get; }
        [NotNull] string Type { get; }
        RosTransportHint TransportHint { get; set; }
        RosListenerStats Stats { get; }
        (int Active, int Total) NumPublishers { get; }
        int MaxQueueSize { set; }
        bool Subscribed { get; }
        void Stop();
        void SetSuspend(bool value);
        void Reset();
        void SetPause(bool value);
        void WriteDescriptionTo([NotNull] StringBuilder b);
    }

    public sealed class Listener<T> : IListener where T : IMessage, IDeserializable<T>, new()
    {
        [NotNull] static RoslibConnection Connection => ConnectionManager.Connection;

        readonly ConcurrentQueue<T> messageQueue = new ConcurrentQueue<T>();
        readonly Action<T> delayedHandler;
        readonly Func<T, bool> directHandler;
        readonly List<T> tmpMessageBag = new List<T>(32);
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


        Listener([NotNull] string topic, RosTransportHint transportHint)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentException("Invalid topic!", nameof(topic));
            }

            Topic = topic;
            Type = BuiltIns.GetMessageType(typeof(T));
            this.transportHint = transportHint;

            Logger.Info($"Subscribing to <b>{topic}</b> <i>[{Type}]</i>.");

            GameThread.EverySecond += UpdateStats;
        }

        public Listener([NotNull] string topic, [NotNull] Action<T> handler,
            RosTransportHint transportHint = RosTransportHint.PreferTcp) : this(topic, transportHint)
        {
            delayedHandler = handler ?? throw new ArgumentNullException(nameof(handler));
            callbackInGameThread = true;
            GameThread.ListenersEveryFrame += CallHandlerDelayed;
            Connection.Subscribe(this);
            Subscribed = true;
        }

        public Listener([NotNull] string topic, [NotNull] Func<T, bool> handler,
            RosTransportHint transportHint = RosTransportHint.PreferTcp) : this(topic, transportHint)
        {
            directHandler = handler ?? throw new ArgumentNullException(nameof(handler));
            callbackInGameThread = false;
            Connection.Subscribe(this);
            Subscribed = true;
        }

        // used by EchoDialogData
        [Preserve]
        public Listener([NotNull] string topic, [NotNull] Func<IMessage, bool> handler, RosTransportHint transportHint)
            : this(topic, (T msg) => handler(msg), transportHint)
        {
        }
        
        public string Topic { get; }
        public string Type { get; }
        public RosListenerStats Stats { get; private set; }
        public (int Active, int Total) NumPublishers => Connection.GetNumPublishers(Topic);
        public int MaxQueueSize { get; set; } = 1;
        public bool Subscribed { get; private set; }

        public void Stop()
        {
            GameThread.EverySecond -= UpdateStats;
            if (callbackInGameThread)
            {
                GameThread.ListenersEveryFrame -= CallHandlerDelayed;
            }

            Logger.Info($"Unsubscribing from {Topic}.");
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

        internal void EnqueueMessage([NotNull] in T msg)
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

            CallHandlerDirect(msg);
        }

        void CallHandlerDelayed()
        {
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
                    Logger.Error($"{this} Error during callback: ", e);
                }
            }

            droppedMsgs += start;
            totalMsgCounter += messageQueue.Count;
            recentMsgs += messageQueue.Count;
        }

        void CallHandlerDirect([NotNull] in T msg)
        {
            Interlocked.Increment(ref totalMsgCounter);
            Interlocked.Increment(ref recentMsgs);

            bool processed;
            try
            {
                Interlocked.Add(ref lastMsgBytes, msg.RosMessageLength);
                processed = directHandler(msg);
            }
            catch (Exception e)
            {
                Logger.Error($"{this} Error during callback: ", e);
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
                description.Append($"{numActivePublishers.ToString()}/{numPublishers.ToString()}↓");
            }
        }


        [NotNull]
        public override string ToString()
        {
            return $"[Listener {Topic} [{Type}]]";
        }
    }
}