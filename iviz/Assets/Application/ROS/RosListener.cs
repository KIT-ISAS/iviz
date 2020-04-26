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
        public readonly int TotalMessages;
        public readonly float JitterMin;
        public readonly float JitterMax;
        public readonly float JitterMean;
        public readonly float MessagesPerSecond;
        public readonly int MessagesInQueue;

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
        public bool Connected => true;
        public bool Subscribed => true;

        protected RosListener(string topic, string type)
        {
            Topic = topic;
            Type = type;
        }

        public abstract RosListenerStats CalculateStats();
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

            Debug.Log("RosListener: Requesting subscription to topic " + Topic);
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


        public override RosListenerStats CalculateStats()
        {
            if (!timesOfArrival.Any())
            {
                return new RosListenerStats();
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

                RosListenerStats stats = new RosListenerStats(
                    totalMsgCounter,
                    jitterMin,
                    jitterMax,
                    timesOfArrival.Count == 0 ? 0 : (timesOfArrival.Last() - timesOfArrival.First()) / timesOfArrival.Count(),
                    timesOfArrival.Count,
                    msgInQueue
                );
                timesOfArrival.Clear();
                return stats;
            }
        }
    }
}



