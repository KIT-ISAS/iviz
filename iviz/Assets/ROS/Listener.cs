#nullable enable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Tools;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting;

namespace Iviz.Ros
{
    /// <inheritdoc cref="Listener"/>
    /// <typeparam name="T">The ROS message type</typeparam>
    public sealed class Listener<T> : Listener where T : IMessage, new()
    {
        readonly ChannelWriter<T>? messageWriter;
        readonly ChannelReader<T>? messageReader;

        readonly Action<T>? handlerOnGameThread;
        readonly Func<T, IRosConnection, bool>? directHandler;
        readonly Serializer<T> serializer;

        int droppedMsgCounter;
        int lastMsgBytes;
        int totalMsgCounter;
        int recentMsgCounter;
        RosTransportHint transportHint;

        public override RosTransportHint TransportHint
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

        Listener(string topic, RosTransportHint transportHint, T generator) : base(topic, generator.RosMessageType)
        {
            serializer = generator.CreateSerializer();
            this.transportHint = transportHint;
            GameThread.EverySecond += UpdateStats;
        }
        
        Listener(string topic, RosTransportHint transportHint) :  this(topic, transportHint, new T())
        {
        }

        /// <summary>
        /// Creates a listener with an asynchronous handler.
        /// Messages will be put in a queue and the callback will only be executed on the game thread. 
        /// </summary>
        /// <param name="topic">The topic to subscribe to</param>
        /// <param name="handler">The callback to execute in the game thread if a message is available</param>
        /// <param name="maxQueueSize">Max size of the bounded queue</param>
        /// <param name="transportHint">Tells the subscriber which protocol is preferred</param>
        public Listener(string topic, Action<T> handler, int maxQueueSize = 1,
            RosTransportHint transportHint = RosTransportHint.PreferTcp) : this(topic, transportHint)
        {
            ThrowHelper.ThrowIfNull(handler, nameof(handler));
            handlerOnGameThread = handler;
            GameThread.ListenersEveryFrame += CallHandlerOnGameThread;
            Connection.Subscribe(this);
            Subscribed = true;

            var options = new BoundedChannelOptions(maxQueueSize)
                { SingleReader = true, FullMode = BoundedChannelFullMode.DropOldest };

            var messageQueue = Channel.CreateBounded<T>(options);
            messageReader = messageQueue.Reader;
            messageWriter = messageQueue.Writer;
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
            Subscribed = true;
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

        public override void Dispose()
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
                RosLogger.Error($"{ToString()}: Exception while disposing", e);
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

        public override void SetSuspend(bool value)
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

        public override void Reset()
        {
            Unsubscribe();
            Subscribe();
        }

        internal void EnqueueMessage(T msg, IRosConnection receiver)
        {
            if (!Subscribed)
            {
                return;
            }

            if (messageWriter is null)
            {
                CallHandlerDirect(msg, receiver);
                return;
            }

            if (!messageWriter.TryWrite(msg))
            {
                droppedMsgCounter++;
            }
        }

        void CallHandlerOnGameThread()
        {
            if (!Subscribed || handlerOnGameThread == null || messageReader == null)
            {
                return;
            }

            int messageCount = messageReader.Count;
            if (messageCount == 0)
            {
                return;
            }

            for (int i = 0; i < messageCount; i++)
            {
                if (!messageReader.TryRead(out var msg))
                {
                    break; // shouldn't happen, single reader
                }

                try
                {
                    lastMsgBytes += serializer.RosMessageLength(msg);
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{ToString()}: Error during {nameof(serializer.RosMessageLength)}", e);
                }

                try
                {
                    handlerOnGameThread(msg);
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{ToString()}: Error during callback handler", e);
                }
            }

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

            try
            {
                lastMsgBytes += serializer.RosMessageLength(msg);
            }
            catch (Exception e)
            {
                RosLogger.Error($"{ToString()}: Error during {nameof(serializer.RosMessageLength)}", e);
            }

            try
            {
                if (directHandler(msg, receiver)) return;
            }
            catch (Exception e)
            {
                RosLogger.Error($"{ToString()}: Error during callback handler", e); // happens with annoying frequency
            }

            droppedMsgCounter++;
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
                messageReader?.Count ?? 0,
                droppedMsgCounter
            );

            RosManager.ReportBandwidthDown(lastMsgBytes);

            lastMsgBytes = 0;
            droppedMsgCounter = 0;
            recentMsgCounter = 0;
        }
    }
}