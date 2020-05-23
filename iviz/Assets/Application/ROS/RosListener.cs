using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Msgs;
using Iviz.RoslibSharp;
using UnityEngine;

namespace Iviz.App
{
    public class RosListenerStats : JsonToString
    {
        public int TotalMessages { get; }
        public float JitterMin { get; }
        public float JitterMax { get; }
        public float JitterMean { get; }
        public float MessagesPerSecond { get; }
        public int MessagesInQueue { get; }

        public RosListenerStats() { }

        public RosListenerStats(int totalMessages, float jitterMin, float jitterMax, float jitterMean, float messagesPerSecond, int messagesInQueue)
        {
            TotalMessages = totalMessages;
            JitterMin = jitterMin;
            JitterMax = jitterMax;
            JitterMean = jitterMean;
            MessagesPerSecond = messagesPerSecond;
            MessagesInQueue = messagesInQueue;
        }
    }

    public abstract class RosListener
    {
        public string Topic { get; }
        public string Type { get; }
        public RosListenerStats Stats { get; protected set; } = new RosListenerStats();

        public bool HasPublishers => ConnectionManager.Connection.HasPublishers(Topic);

        protected RosListener(string topic, string type)
        {
            Topic = topic ?? throw new ArgumentNullException(nameof(topic));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public abstract void UpdateStats();
        public abstract void Stop();
    }

    public class RosListener<T> : RosListener where T : IMessage, new()
    {
        readonly Action<T> subscriptionHandler;
        readonly List<float> timesOfArrival = new List<float>();
        int totalMsgCounter;
        int msgInQueue;

        public int MaxQueueSize { get; set; } = 5;

        public RosListener(string topic, Action<T> handler) :
            base(topic, BuiltIns.GetMessageType(typeof(T)))
        {
            subscriptionHandler = handler;

            //Debug.Log("RosListener: Requesting subscription to topic " + Topic);
            ConnectionManager.Subscribe(this);
        }

        public void EnqueueMessage(T t)
        {
            if (msgInQueue >= MaxQueueSize)
            {
                return;
            }
            msgInQueue++;
            GameThread.RunOnce(() =>
            {
                totalMsgCounter++;
                msgInQueue--;
                timesOfArrival.Add(Time.time);
                try
                {
                    subscriptionHandler(t);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            });
        }

        public override void Stop()
        {
            ConnectionManager.Unsubscribe(this);
        }


        public override void UpdateStats()
        {
            if (!timesOfArrival.Any())
            {
                return;
            }
            else
            {
                float jitterMin = float.MaxValue;
                float jitterMax = float.MinValue;

                for (int i = 0; i < timesOfArrival.Count() - 1; i++)
                {
                    float jitter = timesOfArrival[i + 1] - timesOfArrival[i];
                    if (jitter < jitterMin) jitterMin = jitter;
                    if (jitter > jitterMax) jitterMax = jitter;
                }

                Stats = new RosListenerStats(
                    totalMsgCounter,
                    jitterMin,
                    jitterMax,
                    timesOfArrival.Count == 0 ? 0 : (timesOfArrival.Last() - timesOfArrival.First()) / timesOfArrival.Count(),
                    timesOfArrival.Count,
                    msgInQueue
                );
                timesOfArrival.Clear();
            }
        }
    }
}



