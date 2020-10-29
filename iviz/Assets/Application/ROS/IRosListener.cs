using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Roslib;
using UnityEngine;

namespace Iviz.Controllers
{
    [DataContract]
    public class RosListenerStats : JsonToString
    {
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

        [DataMember] public int TotalMessages { get; }
        [DataMember] public float JitterMin { get; }
        [DataMember] public float JitterMax { get; }
        [DataMember] public float JitterMean { get; }
        [DataMember] public int MessagesPerSecond { get; }
        [DataMember] public int BytesPerSecond { get; }
        [DataMember] public int MessagesInQueue { get; }
        [DataMember] public int Dropped { get; }
    }

    /// <summary>
    /// Wrapper around a ROS subscriber.
    /// </summary>
    public interface IRosListener
    {
        string Topic { get; }
        string Type { get; }
        RosListenerStats Stats { get; }
        int NumPublishers { get; }
        int MaxQueueSize { set; }
        bool Subscribed { get; }
        void Stop();
        void Pause();
        void Unpause();
        void Reset();
    }

    public sealed class RosListener<T> : IRosListener where T : IMessage, IDeserializable<T>, new()
    {
        readonly ConcurrentQueue<T> messageQueue = new ConcurrentQueue<T>();
        readonly Action<T> subscriptionHandler;
        readonly List<float> timesOfArrival = new List<float>();
        readonly List<T> tmpMessageBag = new List<T>();

        int dropped;
        int lastMsgBytes;
        int msgsInQueue;
        int totalMsgCounter;

        public RosListener(string topic, Action<T> handler)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentException("Invalid topic!", nameof(topic));
            }

            Topic = topic;
            Type = BuiltIns.GetMessageType(typeof(T));

            Logger.Internal($"Subscribing to <b>{topic}</b> <i>[{Type}]</i>.");

            subscriptionHandler = handler;

            ConnectionManager.Subscribe(this);
            Subscribed = true;

            GameThread.EverySecond += UpdateStats;
            GameThread.EveryFrame += CallHandler;
        }

        public string Topic { get; }
        public string Type { get; }
        public RosListenerStats Stats { get; private set; } = new RosListenerStats();
        public int NumPublishers => ConnectionManager.Connection.GetNumPublishers(Topic);
        public int MaxQueueSize { get; set; } = 50;
        public bool Subscribed { get; private set; }

        public void Stop()
        {
            GameThread.EverySecond -= UpdateStats;
            GameThread.EveryFrame -= CallHandler;
            Logger.Internal($"Unsubscribing from {Topic}.");
            if (Subscribed)
            {
                ConnectionManager.Unsubscribe(this);
                Subscribed = false;
            }
        }

        public void Pause()
        {
            if (!Subscribed)
            {
                return;
            }

            ConnectionManager.Unsubscribe(this);
            Subscribed = false;
        }

        public void Unpause()
        {
            if (Subscribed)
            {
                return;
            }

            ConnectionManager.Subscribe(this);
            Subscribed = true;
        }

        public void Reset()
        {
            Pause();
            Unpause();
        }

        public void EnqueueMessage(in T t)
        {
            if (t == null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            messageQueue.Enqueue(t);
            msgsInQueue++;
        }

        void CallHandler()
        {
            tmpMessageBag.Clear();
            while (messageQueue.TryDequeue(out T t)) tmpMessageBag.Add(t);

            int start = Math.Max(0, tmpMessageBag.Count - MaxQueueSize);
            for (int i = start; i < tmpMessageBag.Count; i++)
            {
                T msg = tmpMessageBag[i];
                lastMsgBytes += msg.RosMessageLength;
                timesOfArrival.Add(Time.time);
                subscriptionHandler(msg);
            }

            dropped += start;
            totalMsgCounter += messageQueue.Count;
            msgsInQueue = 0;
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
    }
}