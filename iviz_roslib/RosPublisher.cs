﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;

namespace Iviz.Roslib
{
    /// <summary>
    ///     Manager for a ROS publisher.
    /// </summary>
    /// <typeparam name="T">Topic type</typeparam>
    public class RosPublisher<T> : IRosPublisher<T> where T : IMessage
    {
        readonly RosClient client;
        readonly List<string> ids = new List<string>();
        readonly TcpSenderManager<T> manager;
        readonly CancellationTokenSource runningTs = new CancellationTokenSource();
        bool disposed;
        int totalPublishers;

        internal RosPublisher(RosClient client, TopicInfo<T> topicInfo)
        {
            this.client = client;
            manager = new TcpSenderManager<T>(this, topicInfo);
        }

        /// <summary>
        ///     Whether this publisher is valid.
        /// </summary>
        public bool IsAlive => !CancellationToken.IsCancellationRequested;

        /// <summary>
        ///     The number of ids generated by this publisher.
        /// </summary>
        public int NumIds => ids.Count;

        /// <summary>
        ///     The queue size in bytes.
        ///     If a message arrives that makes the queue larger than this, the oldest message will be discarded.
        /// </summary>
        public int MaxQueueSizeInBytes
        {
            get => manager.MaxQueueSizeInBytes;
            set => manager.MaxQueueSizeInBytes = value;
        }

        /// <summary>
        ///     A cancellation token that gets canceled when the publisher is disposed.
        /// </summary>
        public CancellationToken CancellationToken => runningTs.Token;

        public string Topic => manager.Topic;
        public string TopicType => manager.TopicType;
        public int NumSubscribers => manager.NumConnections;

        public bool LatchingEnabled
        {
            get => manager.Latching;
            set => manager.Latching = value;
        }


        public int TimeoutInMs
        {
            get => manager.TimeoutInMs;
            set => manager.TimeoutInMs = value;
        }

        public bool ForceTcpNoDelay
        {
            get => manager.ForceTcpNoDelay;
            set => manager.ForceTcpNoDelay = value;
        }

        public PublisherTopicState GetState()
        {
            AssertIsAlive();
            return new PublisherTopicState(Topic, TopicType, ids, manager.GetStates());
        }

        void IRosPublisher.Publish(IMessage message)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (!MessageTypeMatches(message.GetType()))
            {
                throw new InvalidMessageTypeException("Type does not match publisher.");
            }

            message.RosValidate();
            AssertIsAlive();
            manager.Publish((T) message);
        }

        Task IRosPublisher.PublishAsync(IMessage message, RosPublishPolicy policy, CancellationToken token)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (!MessageTypeMatches(message.GetType()))
            {
                throw new InvalidMessageTypeException("Type does not match publisher.");
            }

            return PublishAsync((T) message, policy, token);
        }


        /// <summary>
        ///     Publishes the given message into the topic.
        /// </summary>
        /// <param name="message">The message to be published.</param>
        /// <exception cref="ArgumentNullException">The message is null</exception>
        /// <exception cref="InvalidMessageTypeException">The message type does not match.</exception>
        public void Publish(T message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            AssertIsAlive();
            message.RosValidate();
            manager.Publish(message);
        }

        public Task PublishAsync(T message, RosPublishPolicy policy = RosPublishPolicy.DoNotWait,
            CancellationToken token = default)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            AssertIsAlive();
            message.RosValidate();

            CancellationToken linkedToken;
            linkedToken = token == default
                ? runningTs.Token
                : CancellationTokenSource.CreateLinkedTokenSource(runningTs.Token, token).Token;

            switch (policy)
            {
                case RosPublishPolicy.DoNotWait:
                    manager.Publish(message);
                    return Task.CompletedTask;
                case RosPublishPolicy.WaitUntilSent:
                    return manager.PublishAndWaitAsync(message, linkedToken);
                default:
                    return Task.CompletedTask;
            }
        }

        Endpoint? IRosPublisher.RequestTopicRpc(string remoteCallerId)
        {
            Endpoint? localEndpoint = manager.CreateConnectionRpc(remoteCallerId);
            return localEndpoint == null ? null : new Endpoint(client.CallerUri.Host, localEndpoint.Port);
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }

        public async Task DisposeAsync()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            runningTs.Cancel();
            ids.Clear();
            await manager.StopAsync().AwaitNoThrow(this);
            NumSubscribersChanged = null;
        }

        public string Advertise()
        {
            AssertIsAlive();

            string id = GenerateId();
            ids.Add(id);
            return id;
        }

        public bool Unadvertise(string id)
        {
            if (!IsAlive)
            {
                return true;
            }

            bool removed = RemoveId(id);

            if (ids.Count == 0)
            {
                Dispose();
                client.RemovePublisher(this);
            }

            return removed;
        }

        public async Task<bool> UnadvertiseAsync(string id, CancellationToken token = default)
        {
            if (!IsAlive)
            {
                return true;
            }

            bool removed = RemoveId(id);

            if (ids.Count == 0)
            {
                Task disposeTask = DisposeAsync().AwaitNoThrow(this);
                Task unadvertiseTask = client.RemovePublisherAsync(this, token).AwaitNoThrow(this);
                await Task.WhenAll(disposeTask, unadvertiseTask).Caf();
            }

            return removed;
        }

        public bool ContainsId(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return ids.Contains(id);
        }

        public bool MessageTypeMatches(Type type)
        {
            return type == typeof(T);
        }
        
        /// <summary>
        ///     Called when the number of subscribers has changed.
        /// </summary>
        public event Action<RosPublisher<T>>? NumSubscribersChanged;

        internal void RaiseNumConnectionsChanged()
        {
            NumSubscribersChanged?.Invoke(this);
        }

        void AssertIsAlive()
        {
            if (!IsAlive)
            {
                throw new ObjectDisposedException("This is not a valid publisher");
            }
        }

        string GenerateId()
        {
            string newId = totalPublishers == 0 ? Topic : $"{Topic}-{totalPublishers}";
            totalPublishers++;
            return newId;
        }

        void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            runningTs.Cancel();
            ids.Clear();
            manager.Stop();
            NumSubscribersChanged = null;
        }

        bool RemoveId(string topicId)
        {
            if (topicId is null)
            {
                throw new ArgumentNullException(nameof(topicId));
            }

            return ids.Remove(topicId);
        }

        public override string ToString()
        {
            return $"[Publisher {Topic} [{TopicType}] ]";
        }
    }
}