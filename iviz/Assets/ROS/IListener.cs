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
        RosListenerStats Stats { get; }
        int NumPublishers { get; }
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
        
        readonly ConcurrentQueue<T> messageQueue = new ConcurrentQueue<T>();
        readonly Action<T> delayedHandler;
        readonly Func<T, bool> directHandler;
        readonly List<float> timesOfArrival = new List<float>();
        readonly List<T> tmpMessageBag = new List<T>();
        readonly bool callbackInGameThread = true;

        int dropped;
        int lastMsgBytes;
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

            Logger.Internal($"Subscribing to <b>{topic}</b> <i>[{Type}]</i>.");

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

        public string Topic { get; }
        string Type { get; }
        public RosListenerStats Stats { get; private set; }
        public int NumPublishers => Connection.GetNumPublishers(Topic);
        public int MaxQueueSize { get; set; } = 50;
        public bool Subscribed { get; private set; }

        public void Stop()
        {
            GameThread.EverySecond -= UpdateStats;
            if (callbackInGameThread)
            {
                GameThread.ListenersEveryFrame -= CallHandlerDelayed;
            }

            Logger.Internal($"Unsubscribing from {Topic}.");
            if (Subscribed)
            {
                Connection.Unsubscribe(this);
                Subscribed = false;
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

        internal void EnqueueMessage([NotNull] in T msg)
        {
            if (msg == null)
            {
                throw new ArgumentNullException(nameof(msg));
            }

            if (callbackInGameThread)
            {
                messageQueue.Enqueue(msg);
                msgsInQueue++;
            }
            else
            {
                CallHandlerDirect(msg);
            }
        }

        void CallHandlerDelayed()
        {
            tmpMessageBag.Clear();
            while (messageQueue.TryDequeue(out T t))
            {
                tmpMessageBag.Add(t);
            }

            int start = Math.Max(0, tmpMessageBag.Count - MaxQueueSize);
            for (int i = start; i < tmpMessageBag.Count; i++)
            {
                T msg = tmpMessageBag[i];
                lastMsgBytes += msg.RosMessageLength;
                timesOfArrival.Add(Time.time);

                try
                {
                    delayedHandler(msg);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }

            dropped += start;
            totalMsgCounter += messageQueue.Count;
            msgsInQueue = 0;
        }

        void CallHandlerDirect([NotNull] in T msg)
        {
            lastMsgBytes += msg.RosMessageLength;
            totalMsgCounter++;

            timesOfArrival.Add(GameThread.GameTime);

            bool processed;
            try
            {
                processed = directHandler(msg);
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

        [NotNull]
        public override string ToString()
        {
            return $"[Listener Topic='{Topic}' Type={Type}]";
        }
    }
}