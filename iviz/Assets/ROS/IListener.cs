using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
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
        RosListenerStats Stats { get; }
        (int Active, int Total) NumPublishers { get; }
        int MaxQueueSize { set; }
        bool Subscribed { get; }
        void Stop();
        void Pause();
        void Unpause();
        void Reset();
    }

    public sealed class Listener<T> : IListener where T : IMessage, IDeserializable<T>, new()
    {
        [NotNull] static RoslibConnection Connection => ConnectionManager.Connection;

        readonly ConcurrentQueue<SharedMessage<T>> messageQueue = new ConcurrentQueue<SharedMessage<T>>();
        readonly Action<T> delayedHandler;
        readonly Func<T, bool> directHandler;
        readonly List<float> timesOfArrival = new List<float>();
        readonly List<SharedMessage<T>> tmpMessageBag = new List<SharedMessage<T>>();
        readonly bool callbackInGameThread;

        int dropped;
        long lastMsgBytes;
        int msgsInQueue;
        int totalMsgCounter;

        Listener([NotNull] string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentException("Invalid topic!", nameof(topic));
            }

            Topic = topic;
            Type = BuiltIns.GetMessageType(typeof(T));

            Logger.Info($"Subscribing to <b>{topic}</b> <i>[{Type}]</i>.");

            GameThread.EverySecond += UpdateStats;
        }


        public Listener([NotNull] string topic, [NotNull] Action<T> handler) : this(topic)
        {
            delayedHandler = handler ?? throw new ArgumentNullException(nameof(handler));
            callbackInGameThread = true;
            GameThread.ListenersEveryFrame += CallHandlerDelayed;

            Connection.Subscribe(this);
            Subscribed = true;
        }

        public Listener([NotNull] string topic, [NotNull] Func<T, bool> handler) : this(topic)
        {
            directHandler = handler ?? throw new ArgumentNullException(nameof(handler));
            callbackInGameThread = false;

            Connection.Subscribe(this);
            Subscribed = true;
        }

        public Listener([NotNull] string topic, [NotNull] Action<IMessage> handler) :
            this(topic, (T t) => handler(t))
        {
        }

        public string Topic { get; }
        public string Type { get; }
        public RosListenerStats Stats { get; private set; }
        public (int Active, int Total) NumPublishers => Connection.GetNumPublishers(Topic);
        public int MaxQueueSize { get; set; } = 50;
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

        public void Pause()
        {
            if (!Subscribed)
            {
                return;
            }

            Connection.Unsubscribe(this);
            Subscribed = false;
        }

        public void Unpause()
        {
            if (Subscribed)
            {
                return;
            }

            Connection.Subscribe(this);
            Subscribed = true;
        }

        public void Reset()
        {
            Pause();
            Unpause();
        }

        internal void EnqueueMessage([NotNull] SharedMessage<T> msg)
        {
            if (!Subscribed)
            {
                return;
            }

            if (callbackInGameThread)
            {
                messageQueue.Enqueue(msg);
                msgsInQueue++;
                return;
            }

            using (msg)
            {
                CallHandlerDirect(msg);
            }
        }

        void CallHandlerDelayed()
        {
            tmpMessageBag.Clear();
            while (messageQueue.TryDequeue(out SharedMessage<T> t))
            {
                tmpMessageBag.Add(t);
            }

            int start = Math.Max(0, tmpMessageBag.Count - MaxQueueSize);
            for (int i = start; i < tmpMessageBag.Count; i++)
            {
                using (var msg = tmpMessageBag[i])
                {
                    lock (timesOfArrival)
                    {
                        timesOfArrival.Add(Time.time);
                    }

                    try
                    {
                        lastMsgBytes += msg.Message.RosMessageLength;
                        delayedHandler(msg.Message);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                }
            }

            dropped += start;
            totalMsgCounter += messageQueue.Count;
            msgsInQueue = 0;
        }

        void CallHandlerDirect([NotNull] SharedMessage<T> msg)
        {
            totalMsgCounter++;

            lock (timesOfArrival)
            {
                timesOfArrival.Add(GameThread.GameTime);
            }

            bool processed;
            try
            {
                lastMsgBytes += msg.Message.RosMessageLength;
                processed = directHandler(msg.Message);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                processed = false;
            }

            if (!processed)
            {
                dropped++;
            }
        }

        void UpdateStats()
        {
            lock (timesOfArrival)
            {
                if (timesOfArrival.Count == 0)
                {
                    Stats = new RosListenerStats();
                    return;
                }

                Stats = new RosListenerStats(
                    totalMsgCounter,
                    timesOfArrival.Count,
                    lastMsgBytes,
                    msgsInQueue,
                    dropped
                );

                ConnectionManager.ReportBandwidthDown(lastMsgBytes);

                lastMsgBytes = 0;
                dropped = 0;
                timesOfArrival.Clear();
            }
        }

        [NotNull]
        public override string ToString()
        {
            return $"[Listener Topic='{Topic}' Type={Type}]";
        }
    }
}