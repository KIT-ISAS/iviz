#nullable enable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Tools;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting;

namespace Iviz.Ros
{
    /// <inheritdoc cref="IListener"/>
    /// <typeparam name="T">The ROS message type</typeparam>
    public sealed class Listener<T> : IListener where T : IMessage, IDeserializable<T>, new()
    {
        static RoslibConnection Connection => RosManager.RosConnection;

        readonly ConcurrentQueue<T> messageQueue = new();
        int messageQueueCount;
        
        readonly Action<T>? handlerOnGameThread;
        readonly Func<T, IRosConnection, bool>? directHandler;
        readonly Serializer<T> serializer;

        T?[] messageHelper = new T[32];

        int droppedMsgCounter;
        int lastMsgBytes;
        int totalMsgCounter;
        int recentMsgCounter;
        RosTransportHint transportHint;
        bool subscribed;

        int NumPublishers => Connection.GetNumPublishers(Topic);

        public string Topic { get; }
        public string Type { get; }
        public RosListenerStats Stats { get; private set; }
        
        public int MaxQueueSize = 1;
        
        // ReSharper disable once ConvertToAutoPropertyWithPrivateSetter
        public bool Subscribed => subscribed;

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

            var generator = new T();
            Type = generator.RosMessageType;
            serializer = generator.CreateSerializer();

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
            subscribed = true;
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
        public Listener(string topic, Func<T, IRosConnection, bool> handler,
            RosTransportHint transportHint = RosTransportHint.PreferTcp) : this(topic, transportHint)
        {
            ThrowHelper.ThrowIfNull(handler, nameof(handler));
            directHandler = handler;
            Connection.Subscribe(this);
            subscribed = true;
        }

        /// <summary>
        /// Creates a listener with a synchronous handler. Used in reflection by EchoDialogData.
        /// Normal code should use the other constructors.
        /// </summary>
        [Preserve, UsedImplicitly]
        public Listener(string topic, Func<IMessage, IRosConnection, bool> handler, RosTransportHint transportHint)
            : this(topic, (T msg, IRosConnection receiver) => handler(msg, receiver), transportHint)
        {
        }

        public void Dispose()
        {
            GameThread.EverySecond -= UpdateStats;
            if (handlerOnGameThread != null)
            {
                GameThread.ListenersEveryFrame -= CallHandlerOnGameThread;
            }

            try
            {
                Unsubscribe();
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Exception while disposing", e);
            }
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
            subscribed = false;
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
            subscribed = true;
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

        internal void EnqueueMessage(T msg, IRosConnection receiver)
        {
            if (!subscribed)
            {
                return;
            }

            if (handlerOnGameThread == null)
            {
                CallHandlerDirect(msg, receiver);
                return;
            }

            if (messageQueueCount >= MaxQueueSize)
            {
                messageQueue.TryDequeue(out _);
                droppedMsgCounter++;
                Interlocked.Decrement(ref messageQueueCount);
            }

            messageQueue.Enqueue(msg);
            Interlocked.Increment(ref messageQueueCount);
        }

        void CallHandlerOnGameThread()
        {
            if (!subscribed || handlerOnGameThread == null)
            {
                return;
            }

            int messageCount = messageQueueCount;
            if (messageCount == 0)
            {
                return;
            }

            if (messageHelper.Length < messageCount)
            {
                messageHelper = new T[Mathf.NextPowerOfTwo(messageCount)];
            }

            for (int i = 0; i < messageCount; i++) // copy a fixed amount, in case messages are still being added
            {
                messageQueue.TryDequeue(out messageHelper[i]);
            }

            Interlocked.Add(ref messageQueueCount, -messageCount);

            int start = Mathf.Max(0, messageCount - MaxQueueSize); // should be 0 unless MaxQueueSize changed
            for (int i = start; i < messageCount; i++)
            {
                var msg = messageHelper[i]!;
                messageHelper[i] = default; // early mark for collect gc
                
                try
                {
                    lastMsgBytes += serializer.RosMessageLength(msg);
                    handlerOnGameThread(msg);
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{ToString()}: Error during callback", e);
                }
            }

            droppedMsgCounter += start;
            totalMsgCounter += messageCount;
            recentMsgCounter += messageCount;
        }

        void CallHandlerDirect(T msg, IRosConnection receiver)
        {
            if (directHandler == null) // shouldn't happen
            {
                return;
            }

            totalMsgCounter++;
            recentMsgCounter++;

            bool processed;
            try
            {
                lastMsgBytes += serializer.RosMessageLength(msg);
                processed = directHandler(msg, receiver);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{ToString()}: Error during callback", e); // happens with annoying frequency
                processed = false;
            }

            if (!processed)
            {
                droppedMsgCounter++;
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
                messageQueueCount,
                droppedMsgCounter
            );

            RosManager.ReportBandwidthDown(lastMsgBytes);

            lastMsgBytes = 0;
            droppedMsgCounter = 0;
            recentMsgCounter = 0;
        }

        public void WriteDescriptionTo(StringBuilder description)
        {
            int numPublishers = NumPublishers;
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
                description.Append(numPublishers.ToString()).Append(" pub");
            }
        }

        public override string ToString() => $"[{nameof(Listener<T>)} {Topic} [{Type}]]";
    }

    public static class Listener
    {
        public static IListener Create(string topicName, Func<IMessage, IRosConnection, bool> handler, Type csType)
        {
            Type listenerType = typeof(Listener<>).MakeGenericType(csType);
            return (IListener)Activator.CreateInstance(listenerType,
                topicName, handler, RosTransportHint.PreferTcp);
        }
        
        [DoesNotReturn]
        public static IListener ThrowUnsupportedMessageType(string message) =>
            throw new InvalidOperationException($"Type {message} is not supported!");
    }
}