#nullable enable

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

namespace Iviz.Ros
{
    /// <inheritdoc cref="IListener"/>
    /// <typeparam name="T">The ROS message type</typeparam>
    public sealed class Listener<T> : IListener where T : IMessage, IDeserializable<T>, new()
    {
        static RoslibConnection Connection => RosManager.Connection;

        readonly ConcurrentQueue<T> messageQueue = new();
        readonly Action<T>? handlerOnGameThread;
        readonly Func<T, IRosReceiver, bool>? directHandler;
        readonly List<T> messageHelper = new(32);

        int droppedMsgCounter;
        long lastMsgBytes;
        int totalMsgCounter;
        int recentMsgCounter;
        RosTransportHint transportHint;

        bool CallbackInGameThread => handlerOnGameThread != null;
        (int active, int total) NumPublishers => Connection.GetNumPublishers(Topic);

        public string Topic { get; }
        public string Type { get; }
        public RosListenerStats Stats { get; private set; }
        public int MaxQueueSize { get; set; } = 1;
        public bool Subscribed { get; private set; }

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

        Listener(string topic, RosTransportHint transportHint)
        {
            ThrowHelper.ThrowIfNullOrEmpty(topic, nameof(topic));
            Topic = topic;
            Type = BuiltIns.GetMessageType<T>();
            this.transportHint = transportHint;
            GameThread.EverySecond += UpdateStats;
        }

        /// <summary>
        /// Creates a listener with an asynchronous handler.
        /// Messages will be put in a queue and the callback will only be executed on the game thread. 
        /// </summary>
        /// <param name="topic">The topic to subscribe to</param>
        /// <param name="handler">The callback to execute in the game thread if a message is available</param>
        /// <param name="transportHint">Tells the subscriber which protocol is preferred</param>
        public Listener(string topic, Action<T> handler,
            RosTransportHint transportHint = RosTransportHint.PreferTcp) : this(topic, transportHint)
        {
            ThrowHelper.ThrowIfNull(handler, nameof(handler));
            handlerOnGameThread = handler;
            GameThread.ListenersEveryFrame += CallHandlerOnGameThread;
            Connection.Subscribe(this);
            Subscribed = true;
        }

        /// <summary>
        /// Creates a listener with a synchronous handler.
        /// The callback will be executed as soon as a message arrives.
        /// </summary>
        /// <param name="topic">The topic to subscribe to</param>
        /// <param name="handler">
        /// The callback to execute as soon as a message is available.
        /// It accepts a message of type <see cref="T"/> and an <see cref="IRosReceiver"/> which contains information
        /// about the connection where the message was received.  
        /// It returns a boolean which indicates whether the message was processed.
        /// This is only used for logging purposes.
        /// </param>
        /// <param name="transportHint">Tells the subscriber which protocol is preferred</param>
        public Listener(string topic, Func<T, IRosReceiver, bool> handler,
            RosTransportHint transportHint = RosTransportHint.PreferTcp) : this(topic, transportHint)
        {
            ThrowHelper.ThrowIfNull(handler, nameof(handler));
            directHandler = handler;
            Connection.Subscribe(this);
            Subscribed = true;
        }

        /// <summary>
        /// Creates a listener with a synchronous handler.
        /// The callback will be executed as soon as a message arrives.
        /// </summary>
        /// <param name="topic">The topic to subscribe to</param>
        /// <param name="handler">
        /// The callback to execute as soon as a message is available.
        /// It returns a boolean which indicates whether the message was processed.
        /// This is only used for logging purposes.
        /// </param>
        /// <param name="transportHint">Tells the subscriber which protocol is preferred</param>
        public Listener(string topic, Func<T, bool> handler,
            RosTransportHint transportHint = RosTransportHint.PreferTcp) :
            this(topic, (t, _) => handler(t), transportHint)
        {
        }

        /// <summary>
        /// Creates a listener with a synchronous handler. Used in reflection by EchoDialogData.
        /// Normal code should use the other constructors.
        /// </summary>
        [Preserve, UsedImplicitly]
        public Listener(string topic, Func<IMessage, IRosReceiver, bool> handler, RosTransportHint transportHint)
            : this(topic, (T msg, IRosReceiver receiver) => handler(msg, receiver), transportHint)
        {
        }

        public void Dispose()
        {
            GameThread.EverySecond -= UpdateStats;
            if (CallbackInGameThread)
            {
                GameThread.ListenersEveryFrame -= CallHandlerOnGameThread;
            }

            Unsubscribe();
        }

        /// <summary>
        /// Unsubscribes from the topic.
        /// </summary>
        public void Unsubscribe()
        {
            if (!Subscribed)
            {
                return;
            }

            Connection.Unsubscribe(this);
            Subscribed = false;
        }

        /// <summary>
        /// Subscribes to the topic.
        /// </summary>
        public void Subscribe()
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
                Unsubscribe();
            }
            else
            {
                Subscribe();
            }
        }

        public void SetPause(bool value)
        {
            Connection.SetPause(this, value);
        }

        public void Reset()
        {
            Unsubscribe();
            Subscribe();
        }

        internal void EnqueueMessage(in T msg, IRosReceiver receiver)
        {
            if (!Subscribed)
            {
                return;
            }

            if (!CallbackInGameThread)
            {
                CallHandlerDirect(msg, receiver);
                return;
            }

            if (messageQueue.Count >= MaxQueueSize)
            {
                messageQueue.TryDequeue(out _);
                Interlocked.Increment(ref droppedMsgCounter);
            }

            messageQueue.Enqueue(msg);
        }

        void CallHandlerOnGameThread()
        {
            if (!Subscribed || handlerOnGameThread == null)
            {
                return;
            }

            int messageCount = messageQueue.Count;
            if (messageCount == 0)
            {
                return;
            }
            
            messageHelper.Clear();

            foreach (int _ in ..messageCount) // copy a fixed amount, in case messages are still being added
            {
                if (messageQueue.TryDequeue(out T t))
                {
                    messageHelper.Add(t);
                }
            }

            int start = Mathf.Max(0, messageHelper.Count - MaxQueueSize); // should be 0 unless MaxQueueSize changed
            foreach (var msg in messageHelper.Skip(start))
            {
                try
                {
                    Interlocked.Add(ref lastMsgBytes, msg.RosMessageLength);
                    handlerOnGameThread(msg);
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Error during callback", e);
                }
            }

            Interlocked.Add(ref droppedMsgCounter, start);
            Interlocked.Add(ref totalMsgCounter, messageCount);
            Interlocked.Add(ref recentMsgCounter, messageCount);
        }

        void CallHandlerDirect(in T msg, IRosReceiver receiver)
        {
            if (directHandler == null) // shouldn't happen
            {
                return;
            }

            Interlocked.Increment(ref totalMsgCounter);
            Interlocked.Increment(ref recentMsgCounter);

            bool processed;
            try
            {
                Interlocked.Add(ref lastMsgBytes, msg.RosMessageLength);
                processed = directHandler(msg, receiver);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Error during callback", e); // happens with annoying frequency
                processed = false;
            }

            if (!processed)
            {
                Interlocked.Increment(ref droppedMsgCounter);
            }
        }

        void UpdateStats()
        {
            if (recentMsgCounter == 0)
            {
                Stats = default;
                return;
            }

            Stats = new RosListenerStats(
                totalMsgCounter,
                recentMsgCounter,
                lastMsgBytes,
                messageQueue.Count,
                droppedMsgCounter
            );

            RosManager.ReportBandwidthDown(lastMsgBytes);

            Interlocked.Exchange(ref lastMsgBytes, 0);
            Interlocked.Exchange(ref droppedMsgCounter, 0);
            Interlocked.Exchange(ref recentMsgCounter, 0);
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
                description.Append(numActivePublishers.ToString()).Append(" pub");
            }
        }

        public override string ToString() => $"[{nameof(Listener<T>)} {Topic} [{Type}]]";
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