using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;
using Nito.AsyncEx;
using Nito.AsyncEx.Synchronous;
#if !NETSTANDARD2_0
using System.Runtime.CompilerServices;

#endif

namespace Iviz.Roslib
{
    /// <summary>
    /// A helper class that wraps a subscriber. It employs a queue that stores messages
    /// in the background, and can be accessed without having to use a separate callback.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class RosChannelReader<T> : IDisposable, IEnumerable<T>, IRosChannelReader
#if !NETSTANDARD2_0
        , IAsyncDisposable
#endif
        where T : IMessage, IDeserializable<T>, new()
    {
        readonly AsyncProducerConsumerQueue<T> messageQueue = new AsyncProducerConsumerQueue<T>();
        CancellationTokenRegistration subscriberToken;
        bool disposed;
        string? subscriberId;
        IRosSubscriber<T>? subscriber;

        public RosChannelReader()
        {
        }

        /// <summary>
        /// Constructor for the channel. Also calls <see cref="Start(IRosClient, string, bool)"/>.
        /// </summary>
        /// <param name="client">A connected RosClient.</param>
        /// <param name="topic">The topic to listen to.</param>
        /// <param name="requestNoDelay">Whether NO_DELAY should be requested.</param>
        public RosChannelReader(IRosClient client, string topic, bool requestNoDelay = false)
        {
            Start(client, topic, requestNoDelay);
        }

#if !NETSTANDARD2_0
        public async ValueTask DisposeAsync()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            messageQueue.CompleteAdding();

            if (subscriber == null)
            {
                return; // not started
            }

            try
            {
                await subscriberToken.DisposeAsync();
                await subscriber.UnsubscribeAsync(subscriberId!);
            }
            catch (Exception e)
            {
                Logger.Log($"{this}: {e}");
            }
        }
#endif

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            messageQueue.CompleteAdding();

            if (subscriber == null)
            {
                return; // not started
            }

            try
            {
                subscriberToken.Dispose();
                subscriber.Unsubscribe(subscriberId!);
            }
            catch (Exception e)
            {
                Logger.Log($"{this}: {e}");
            }
        }

        /// <summary>
        /// Enumerates through the available messages, without blocking.
        /// </summary>
        /// <returns>An enumerator that can be used in a foreach</returns>
        public IEnumerator<T> GetEnumerator()
        {
            while (TryRead(out T t))
            {
                yield return t;
            }
        }

        /// <summary>
        /// Enumerates through the available messages, without blocking.
        /// </summary>
        /// <returns>An enumerator that can be used in a foreach</returns>        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IMessage IRosChannelReader.Read(CancellationToken token)
        {
            return Read(token);
        }

        async Task<IMessage> IRosChannelReader.ReadAsync(CancellationToken token)
        {
            return await ReadAsync(token);
        }

        /// <summary>
        /// Starts the channel from an existing subscriber. Must be called after the constructor.
        /// </summary>
        /// <param name="newSubscriber">The existing subscriber.</param>
        /// <exception cref="ArgumentNullException">Thrown if the client or the topic are null</exception>
        public void Start(IRosSubscriber<T> newSubscriber)
        {
            subscriberId = newSubscriber.Subscribe(Callback);
            subscriber = newSubscriber;
            subscriberToken = subscriber.CancellationToken.Register(OnSubscriberDisposed);
        }

        /// <summary>
        /// Starts the channel. Must be called after the constructor.
        /// </summary>
        /// <param name="client">A connected RosClient</param>
        /// <param name="topic">The topic to listen to</param>
        /// <param name="requestNoDelay">Whether NO_DELAY should be requested</param>
        /// <exception cref="ArgumentNullException">Thrown if the client or the topic are null</exception>
        public async Task StartAsync(IRosClient client, string topic, bool requestNoDelay = false)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            var (newId, newSubscriber) = await client.SubscribeAsync<T>(topic, Callback, requestNoDelay);

            subscriberId = newId;
            subscriber = newSubscriber;
            subscriberToken = subscriber.CancellationToken.Register(OnSubscriberDisposed);
        }

        /// <summary>
        /// Starts the channel. Must be called after the constructor.
        /// </summary>
        /// <param name="client">A connected RosClient</param>
        /// <param name="topic">The topic to listen to</param>
        /// <param name="requestNoDelay">Whether NO_DELAY should be requested</param>
        /// <exception cref="ArgumentNullException">Thrown if the client or the topic are null</exception>
        public void Start(IRosClient client, string topic, bool requestNoDelay = false)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            subscriberId = client.Subscribe(topic, Callback, out subscriber, requestNoDelay);
            subscriberToken = subscriber.CancellationToken.Register(OnSubscriberDisposed);
        }

        void OnSubscriberDisposed()
        {
            messageQueue.CompleteAdding();
        }

        void Callback(T t)
        {
            if (disposed)
            {
                return;
            }

            messageQueue.Enqueue(t);
        }

        /// <summary>
        /// Waits until a message arrives.
        /// </summary>
        /// <returns>False if the channel has been disposed</returns>
        public bool WaitToRead(int timeoutInMs)
        {
            using CancellationTokenSource ts = new CancellationTokenSource(timeoutInMs);
            return WaitToRead(ts.Token);
        }

        /// <summary>
        /// Waits until a message arrives.
        /// </summary>
        /// <param name="token">A cancellation token that makes the function stop blocking when cancelled. If not provided, waits indefinitely.</param>
        /// <returns>False if the channel has been disposed</returns>
        public bool WaitToRead(CancellationToken token = default)
        {
            return messageQueue.OutputAvailable(token);
        }

        /// <summary>
        /// Waits until a message arrives.
        /// </summary>
        /// <returns>False if the channel has been disposed</returns>
        public async Task<bool> WaitToReadAsync(int timeoutInMs)
        {
            using CancellationTokenSource ts = new CancellationTokenSource(timeoutInMs);
            return await WaitToReadAsync(ts.Token);
        }

        /// <summary>
        /// Waits until a message arrives.
        /// </summary>
        /// <param name="token">A cancellation token that makes the function stop blocking when cancelled. If not provided, waits indefinitely.</param>
        /// <returns>False if the channel has been disposed</returns>
        public async Task<bool> WaitToReadAsync(CancellationToken token = default)
        {
            return await messageQueue.OutputAvailableAsync(token);
        }


        /// <summary>
        /// Waits a given time until a message arrives, and pulls it from the queue.
        /// </summary>
        /// <param name="timeoutInMs">The maximal time to wait</param>
        /// <returns>The message that arrived</returns>
        /// <exception cref="OperationCanceledException">Thrown if the waiting times out</exception>
        /// <exception cref="InvalidOperationException">Thrown if the queue has been disposed</exception>
        public T Read(int timeoutInMs)
        {
            using CancellationTokenSource ts = new CancellationTokenSource(timeoutInMs);
            return Read(ts.Token);
        }

        /// <summary>
        /// Waits until a message arrives, and pulls it from the queue.
        /// </summary>
        /// <param name="token">A cancellation token that makes the function stop blocking when cancelled. If not provided, waits indefinitely.</param>
        /// <returns>The message that arrived.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the token is canceled</exception>
        /// <exception cref="InvalidOperationException">Thrown if the queue has been disposed</exception>
        public T Read(CancellationToken token = default)
        {
            return messageQueue.Dequeue(token);
        }

        /// Awaits a given time until a message arrives, and pulls it from the queue.
        /// </summary>
        /// <param name="timeoutInMs">The maximal time to wait</param>
        /// <returns>The message that arrived</returns>
        /// <exception cref="OperationCanceledException">Thrown if the waiting times out</exception>
        /// <exception cref="InvalidOperationException">Thrown if the queue has been disposed</exception>
        public async Task<T> ReadAsync(int timeoutInMs)
        {
            using CancellationTokenSource ts = new CancellationTokenSource(timeoutInMs);
            return await ReadAsync(ts.Token);
        }

        /// <summary>
        /// Awaits until a message arrives, and pulls it from the queue.
        /// </summary>
        /// <param name="token">A cancellation token that makes the function stop blocking when cancelled. If not provided, waits indefinitely.</param>
        /// <returns>The message that arrived.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the token is canceled</exception>
        /// <exception cref="InvalidOperationException">Thrown if the queue has been disposed</exception>
        public async Task<T> ReadAsync(CancellationToken token = default)
        {
            return await messageQueue.DequeueAsync(token);
        }


        /// <summary>
        /// Checks if there is a message in the queue, and returns it without blocking.
        /// </summary>
        /// <param name="t">The received message, or default if no message was available.</param>
        /// <returns>True if there was a message available.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the queue has been disposed</exception>
        public bool TryRead(out T t)
        {
            CancellationToken cancelled = new CancellationToken(true);
            Task<T> task = messageQueue.DequeueAsync(cancelled);
            if (!task.RanToCompletion())
            {
                t = default!;
                return false;
            }

            t = task.WaitAndUnwrapException();
            return true;
        }

        /// <summary>
        /// Waits a given time until a message arrives. Does not throw an exception if it times out.
        /// </summary>
        /// <param name="t">The received message, or default if no message was available.</param>
        /// <param name="timeoutInMs">The maximal time to wait.</param>
        /// <returns>True if there was a message available.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the queue has been disposed</exception>
        public bool TryRead(out T t, int timeoutInMs)
        {
            using CancellationTokenSource ts = new CancellationTokenSource(timeoutInMs);
            return TryRead(out t, ts.Token);
        }

        /// <summary>
        /// Waits until a message arrives. Does not throw an exception if it times out.
        /// </summary>
        /// <param name="t">The received message, or default if no message was available.</param>
        /// <param name="token">A cancellation token that makes the function stop blocking when cancelled.</param>
        /// <returns>True if there was a message available.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the queue has been disposed</exception>
        public bool TryRead(out T t, CancellationToken token)
        {
            try
            {
                t = messageQueue.Dequeue(token);
                return true;
            }
            catch (OperationCanceledException)
            {
                t = default!;
                return false;
            }
        }

        /// <summary>
        /// Enumerates through the available messages, and blocks while waiting for the next.
        /// It will only return either when the token has been canceled, or the channel has been disposed.
        /// </summary>
        /// <param name="externalToken">A cancellation token that makes the function stop blocking when cancelled.</param>
        /// <returns>An enumerator that can be used in a foreach</returns>
        /// <exception cref="InvalidOperationException">Thrown if the queue has been disposed</exception>
        public IEnumerable<T> ReadAll(CancellationToken externalToken)
        {
            while (true)
            {
                T msg;
                try
                {
                    msg = Read(externalToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                yield return msg;
            }
        }

        IEnumerable<IMessage> IRosChannelReader.ReadAll(CancellationToken externalToken)
        {
            while (true)
            {
                T msg;
                try
                {
                    msg = Read(externalToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                yield return msg;
            }
        }

#if !NETSTANDARD2_0
        /// <summary>
        /// Enumerates through the available messages, and blocks while waiting for the next.
        /// It will only return either when the token has been canceled, or the channel has been disposed.
        /// </summary>
        /// <param name="externalToken">A cancellation token that makes the function stop blocking when cancelled.</param>
        /// <returns>An enumerator that can be used in a foreach</returns>
        /// <exception cref="InvalidOperationException">Thrown if the queue has been disposed</exception>
        public async IAsyncEnumerable<T> ReadAllAsync(
            [EnumeratorCancellation] CancellationToken externalToken)
        {
            while (true)
            {
                T msg;
                try
                {
                    msg = await ReadAsync(externalToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                yield return msg;
            }
        }

        async IAsyncEnumerable<IMessage> IRosChannelReader.ReadAllAsync(
            [EnumeratorCancellation] CancellationToken externalToken)
        {
            while (true)
            {
                T msg;
                try
                {
                    msg = await ReadAsync(externalToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                yield return msg;
            }
        }
#endif

        public override string ToString()
        {
            if (subscriber == null)
            {
                return "[RosSubscriberQueue (uninitialized)]";
            }

            return disposed
                ? "[RosSubscriberQueue (disposed)]"
                : $"[RosSubscriberQueue {subscriber.Topic} [{subscriber.TopicType}]]";
        }
    }

    public static class RosChannelReader
    {
        public static IRosChannelReader CreateInstance(Type msgType)
        {
            if (typeof(IMessage) == msgType || !typeof(IMessage).IsAssignableFrom(msgType))
            {
                throw new ArgumentException("msgType is not a message type", nameof(msgType));
            }

            Type readerType = typeof(RosChannelReader<>).MakeGenericType(msgType);
            return (IRosChannelReader) Activator.CreateInstance(readerType)!;
        }
    }
}