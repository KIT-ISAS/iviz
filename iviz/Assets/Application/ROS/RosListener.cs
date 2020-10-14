using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Roslib;
using UnityEngine;
using Logger = Iviz.Displays.Logger;

namespace Iviz.Controllers
{
    [DataContract]
    public class RosListenerStats : JsonToString
    {
        [DataMember] public int TotalMessages { get; }
        [DataMember] public float JitterMin { get; }
        [DataMember] public float JitterMax { get; }
        [DataMember] public float JitterMean { get; }
        [DataMember] public int MessagesPerSecond { get; }
        [DataMember] public int BytesPerSecond { get; }
        [DataMember] public int MessagesInQueue { get; }
        [DataMember] public int Dropped { get; }

        public RosListenerStats()
        {
        }

        public RosListenerStats(int totalMessages, float jitterMin, float jitterMax,
            float jitterMean, int messagesPerSecond, int bytesPerSecond, int messagesInQueue, int dropped)
        {
            TotalMessages = totalMessages;
            JitterMin = jitterMin;
            JitterMax = jitterMax;
            JitterMean = jitterMean;
            MessagesPerSecond = messagesPerSecond;
            BytesPerSecond = bytesPerSecond;
            MessagesInQueue = messagesInQueue;
            Dropped = dropped;
        }
    }

    /// <summary>
    /// Wrapper around a ROS subscriber.
    /// </summary>
    public abstract class RosListener
    {
        public string Topic { get; }
        public string Type { get; }
        public RosListenerStats Stats { get; private set; } = new RosListenerStats();
        public int NumPublishers => ConnectionManager.Connection.GetNumPublishers(Topic);
        public int MaxQueueSize { get; set; } = 50;

        protected int totalMsgCounter;
        protected int msgsInQueue;
        protected int lastMsgBytes;
        protected int dropped;
        
        protected readonly List<float> timesOfArrival = new List<float>();

        protected RosListener(string topic, string type)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentException("Invalid topic!", nameof(topic));
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException("Invalid type!", nameof(type));
            }

            Logger.Internal($"Subscribing to <b>{topic}</b> <i>[{type}]</i>.");

            Topic = topic;
            Type = type;

            GameThread.EverySecond += UpdateStats;
        }

        public virtual void Stop()
        {
            GameThread.EverySecond -= UpdateStats;
        }

        void UpdateStats()
        {
            if (!timesOfArrival.Any())
            {
                Stats = new RosListenerStats();
                return;
            }

            float jitterMin = float.MaxValue;
            float jitterMax = float.MinValue;

            for (int i = 0; i < timesOfArrival.Count - 1; i++)
            {
                float jitter = timesOfArrival[i + 1] - timesOfArrival[i];
                if (jitter < jitterMin)
                {
                    jitterMin = jitter;
                }

                if (jitter > jitterMax)
                {
                    jitterMax = jitter;
                }
            }

            Stats = new RosListenerStats(
                totalMsgCounter,
                jitterMin,
                jitterMax,
                timesOfArrival.Count == 0
                    ? 0
                    : (timesOfArrival.Last() - timesOfArrival.First()) / timesOfArrival.Count,
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

        public override string ToString()
        {
            return $"[Listener Topic='{Topic}' Type={Type}]";
        }

        public bool Subscribed { get; protected set; }

        public abstract void Pause();

        public abstract void Unpause();

        public void Reset()
        {
            Pause();
            Unpause();
        }
    }

    public sealed class RosListener<T> : RosListener where T : IMessage, new()
    {
        readonly Action<T> subscriptionHandler;
        readonly Queue<T> queue = new Queue<T>();

        public RosListener(string topic, Action<T> handler) :
            base(topic, BuiltIns.GetMessageType(typeof(T)))
        {
            subscriptionHandler = handler;

            ConnectionManager.Subscribe(this);
            Subscribed = true;

            GameThread.EveryFrame += CallHandler;
        }

        public void EnqueueMessage(T t)
        {
            if (t == null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            lock (queue)
            {
                queue.Enqueue(t);
                if (queue.Count > MaxQueueSize)
                {
                    queue.Dequeue();
                    dropped++;
                }

                msgsInQueue = queue.Count;
            }
        }

        void CallHandler()
        {
            lock (queue)
            {
                if (queue.Count == 0)
                {
                    return;
                }

                foreach (T t in queue)
                {
                    lastMsgBytes += t.RosMessageLength;
                    timesOfArrival.Add(Time.time);

                    subscriptionHandler(t);
                }

                totalMsgCounter += queue.Count;
                msgsInQueue = 0;
                queue.Clear();
            }
        }

        public override void Stop()
        {
            GameThread.EveryFrame -= CallHandler;
            Logger.Internal($"Unsubscribing from {Topic}.");
            if (Subscribed)
            {
                ConnectionManager.Unsubscribe(this);
                Subscribed = false;
            }

            base.Stop();
        }

        public override void Pause()
        {
            if (!Subscribed)
            {
                return;
            }

            ConnectionManager.Unsubscribe(this);
            Subscribed = false;
        }

        public override void Unpause()
        {
            if (Subscribed)
            {
                return;
            }

            ConnectionManager.Subscribe(this);
            Subscribed = true;
        }
    }
}