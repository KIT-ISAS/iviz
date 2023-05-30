#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Roslib;

namespace Iviz.Ros
{
    /// <summary>
    /// A wrapper around a <see cref="RosSubscriber{T}"/> that persists even if the connection is interrupted.
    /// After a connection is reestablished, this listener is used to resubscribe transparently.
    /// There can be multiple Listeners that refer to the same shared subscriber.
    /// The original is stored in a <see cref="SubscribedTopic{T}"/>.  
    /// </summary>
    public abstract class Listener
    {
        protected int droppedMsgCounter;
        protected int lastMsgBytes;
        protected int totalMsgCounter;
        protected int recentMsgCounter;
        RosTransportHint transportHint;

        public string Topic { get; }
        public string Type { get; }
        public RosListenerStats Stats { get; private set; }
        public bool Subscribed { get; protected set; }

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

        protected Listener(string topic, string type, RosTransportHint transportHint)
        {
            ThrowHelper.ThrowIfNullOrEmpty(topic, nameof(topic));
            ThrowHelper.ThrowIfNullOrEmpty(type, nameof(type));

            Topic = topic;
            Type = type;
            this.transportHint = transportHint;

            GameThread.EverySecond += UpdateStats;
        }

        /// <summary>
        /// Sets the suspended state of the listener.
        /// If suspended, the topic will be unsubscribed. If unsuspended, the topic will be resubscribed.  
        /// </summary>        
        public abstract void SetSuspend(bool value);

        /// <summary>
        /// Unsubscribes and resubscribes from the topic.
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// Sets the paused state of the subscriber.
        /// When paused, the subscriber will still receive data, but will not parse it or generate a message. 
        /// </summary>      
        public void SetPause(bool value)
        {
            Connection.SetPause(this, value);
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
                droppedMsgCounter
            );

            RosManager.ReportBandwidthDown(lastMsgBytes);

            lastMsgBytes = 0;
            droppedMsgCounter = 0;
            recentMsgCounter = 0;
        }

        

        /// <summary>
        /// Writes the number of publishers and message frequency to the <see cref="StringBuilder"/> argument.
        /// </summary>      
        public void WriteDescriptionTo(StringBuilder description)
        {
            if (Connection.GetNumPublishers(Topic) is not { } numPublishers)
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
        
        public virtual void Dispose()
        {
            GameThread.EverySecond -= UpdateStats;

            try
            {
                Unsubscribe();
            }
            catch (ObjectDisposedException)
            {
                // ROS already shut down, so ignore
            }
            catch (Exception e)
            {
                RosLogger.Error($"{ToString()}: Exception while disposing", e);
            }
            
            Subscribed = false;
        }


        public override string ToString() => $"[{nameof(Listener)} '{Topic}' [{Type}]]";

        
        // -------------------------
        
        private protected static RosConnection Connection => RosManager.RosConnection;

        public static Listener Create(string topicName, Func<IMessage, IRosConnection, bool> handler, Type csType)
        {
            Type listenerType = typeof(Listener<>).MakeGenericType(csType);
            return (Listener)Activator.CreateInstance(listenerType,
                topicName, handler, RosTransportHint.PreferTcp);
        }

        [DoesNotReturn]
        public static Listener ThrowUnsupportedMessageType(string message) =>
            throw new InvalidOperationException($"Type {message} is not supported!");
    }
}