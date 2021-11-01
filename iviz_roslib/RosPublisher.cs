﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib.XmlRpc;
using Iviz.Tools;

namespace Iviz.Roslib
{
    /// <summary>
    ///     Manager for a ROS publisher.
    /// </summary>
    /// <typeparam name="T">Topic type</typeparam>
    public sealed class RosPublisher<T> : IRosPublisher<T> where T : IMessage
    {
        readonly RosClient client;
        readonly List<string> ids = new();
        readonly SenderManager<T> manager;
        readonly CancellationTokenSource runningTs = new();
        int totalPublishers;
        bool disposed;

        internal RosPublisher(RosClient client, TopicInfo<T> topicInfo)
        {
            this.client = client;
            manager = new SenderManager<T>(this, topicInfo) { ForceTcpNoDelay = true };
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
                throw new RosInvalidMessageTypeException("Type does not match publisher.");
            }

            message.RosValidate();
            AssertIsAlive();
            manager.Publish((T)message);
        }

        Task IRosPublisher.PublishAsync(IMessage message, RosPublishPolicy policy, CancellationToken token)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (!MessageTypeMatches(message.GetType()))
            {
                throw new RosInvalidMessageTypeException("Type does not match publisher.");
            }

            return PublishAsync((T)message, policy, token);
        }


        /// <summary>
        ///     Publishes the given message into the topic.
        /// </summary>
        /// <param name="message">The message to be published.</param>
        /// <exception cref="ArgumentNullException">The message is null</exception>
        /// <exception cref="RosInvalidMessageTypeException">The message type does not match.</exception>
        public void Publish(in T message)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            AssertIsAlive();
            message.RosValidate();
            manager.Publish(message);
        }

        public Task PublishAsync(in T message, RosPublishPolicy policy = RosPublishPolicy.DoNotWait,
            CancellationToken token = default)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            AssertIsAlive();
            message.RosValidate();

            switch (policy)
            {
                case RosPublishPolicy.DoNotWait:
                    manager.Publish(message);
                    return Task.CompletedTask;
                case RosPublishPolicy.WaitUntilSent:
                    return manager.PublishAndWaitAsync(message, token);
                default:
                    return Task.CompletedTask;
            }
        }

        TopicRequestRpcResult IRosPublisher.RequestTopicRpc(bool requestsTcp, RpcUdpTopicRequest? requestsUdp,
            out Endpoint? tcpResponse, out RpcUdpTopicResponse? udpResponse)
        {
            if (disposed)
            {
                tcpResponse = null;
                udpResponse = null;
                return TopicRequestRpcResult.Disposing;
            }

            if (requestsTcp)
            {
                tcpResponse = new Endpoint(client.CallerUri.Host, manager.Endpoint.Port);
                udpResponse = null;
                return TopicRequestRpcResult.Success;
            }

            if (requestsUdp == null)
            {
                throw new InvalidOperationException("Either UDP or TCP needs to be requested");
            }

            tcpResponse = null;
            udpResponse = manager.CreateUdpConnection(requestsUdp, client.CallerUri.Host);
            return TopicRequestRpcResult.Success;
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }

        public async Task DisposeAsync(CancellationToken token)
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            runningTs.Cancel();
            ids.Clear();

            await manager.DisposeAsync(token).AwaitNoThrow(this);

            NumSubscribersChanged = null;
            runningTs.Dispose();
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
            manager.Dispose();
            NumSubscribersChanged = null;
            runningTs.Dispose();
        }

        public string Advertise()
        {
            AssertIsAlive();

            string id = GenerateId();
            ids.Add(id);
            return id;
        }

        public bool Unadvertise(string id, CancellationToken token = default)
        {
            if (!IsAlive)
            {
                return true;
            }

            bool removed = RemoveId(id);

            if (ids.Count == 0)
            {
                Task.Run(() => RemovePublisherAsync(token), token).WaitAndRethrow();
            }

            return removed;
        }

        public async ValueTask<bool> UnadvertiseAsync(string id, CancellationToken token = default)
        {
            if (!IsAlive)
            {
                return true;
            }

            bool removed = RemoveId(id);

            if (ids.Count == 0)
            {
                await RemovePublisherAsync(token);
            }

            return removed;
        }

        async Task RemovePublisherAsync(CancellationToken token)
        {
            Task disposeTask = DisposeAsync(token).AwaitNoThrow(this);
            Task unadvertiseTask = client.RemovePublisherAsync(this, token).AwaitNoThrow(this);
            await (disposeTask, unadvertiseTask).WhenAll();
        }

        public bool ContainsId(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return ids.Contains(id);
        }

        /// <summary>
        /// Checks whether the given type matches the publisher message type <see cref="T"/>  
        /// </summary>
        public bool MessageTypeMatches(Type type)
        {
            return type == typeof(T);
        }

        /// <summary>
        ///     Called when the number of subscribers has changed.
        /// </summary>
        public event Action<RosPublisher<T>>? NumSubscribersChanged;

        internal void RaiseNumSubscribersChanged()
        {
            if (NumSubscribersChanged == null)
            {
                return;
            }
            
            Task.Run(() =>
            {
                try
                {
                    NumSubscribersChanged?.Invoke(this);
                }
                catch (Exception e)
                {
                    Logger.LogErrorFormat("{0}: Exception from NumSubscribersChanged : {1}", this, e);
                }
            }, default);            
        }

        void AssertIsAlive()
        {
            if (!IsAlive)
            {
                throw new ObjectDisposedException("this", "This is not a valid publisher");
            }
        }

        string GenerateId()
        {
            Interlocked.Increment(ref totalPublishers);
            int prevNumPublishers = totalPublishers - 1;
            return prevNumPublishers == 0 ? Topic : $"{Topic}-{prevNumPublishers.ToString()}";
        }

        bool RemoveId(string topicId)
        {
            if (topicId is null)
            {
                throw new ArgumentNullException(nameof(topicId));
            }

            return ids.Remove(topicId);
        }

        internal bool TryGetLoopbackReceiver(in Endpoint endpoint, out ILoopbackReceiver<T>? receiver)
        {
            return client.TryGetLoopbackReceiver(Topic, endpoint, out receiver);
        }

        public override string ToString()
        {
            return $"[Publisher {Topic} [{TopicType}] ]";
        }
    }
}