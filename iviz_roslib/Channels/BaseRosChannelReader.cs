using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;
using Nito.AsyncEx;

namespace Iviz.Roslib;

/// <summary>
/// Base class with common functionality for <see cref="RosChannelReader"/> and <see cref="RosChannelReader{T}"/>.
/// </summary>
/// <typeparam name="T">The ROS message type</typeparam>
public abstract class BaseRosChannelReader<T> : IEnumerable<T>, IRosChannelReader, IAsyncEnumerable<T>
    where T : IMessage
{
    readonly ConcurrentQueue<T> backQueue = new();
    protected readonly AsyncCollection<T> messageQueue;
    protected bool disposed;
    protected string? subscriberId;
    protected IRosSubscriber<T>? subscriber;
    protected CancellationTokenRegistration subscriberToken;

    /// <summary>
    /// Tentative number of elements. This number may become outdated right after calling this property.
    /// Use this only as an estimate for the number of elements.
    /// </summary>
    public int Count => backQueue.Count;

    public IRosSubscriber<T> Subscriber =>
        subscriber ?? throw new InvalidOperationException("Channel has not been started!");

    public string Topic => Subscriber.Topic;

    public bool IsAlive => subscriber != null && !disposed;

    public BaseRosChannelReader()
    {
        messageQueue = new AsyncCollection<T>(backQueue);
    }

    /// <summary>
    /// Starts the channel. Must be called after the constructor.
    /// </summary>
    /// <param name="client">A connected RosClient</param>
    /// <param name="topic">The topic to listen to</param>
    /// <param name="token">An optional cancellation token.</param>
    public abstract ValueTask StartAsync(IRosClient client, string topic, CancellationToken token = default);

    /// <summary>
    /// Starts the channel. Must be called after the constructor.
    /// </summary>
    /// <param name="client">A connected RosClient</param>
    /// <param name="topic">The topic to listen to</param>
    public abstract void Start(IRosClient client, string topic);

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


#if !NETSTANDARD2_0
        await subscriberToken.DisposeAsync();
#else
            subscriberToken.Dispose();
#endif
        await subscriber.UnsubscribeAsync(subscriberId!).AsTask().AwaitNoThrow(this);
    }

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
            Logger.LogErrorFormat("{0}: Error in Dispose: {1}", this, e);
        }
    }

    /// <summary>
    /// Enumerates through the available messages, and blocks while waiting for the next.
    /// It will only return either when the token has been canceled, or the channel has been disposed.
    /// </summary>
    /// <returns>An enumerator that can be used in a foreach</returns>
    public IEnumerator<T> GetEnumerator()
    {
        return ReadAll().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    protected void OnSubscriberDisposed()
    {
        messageQueue.CompleteAdding();
    }

    /// <summary>
    /// Waits until a message arrives.
    /// </summary>
    /// <returns>False if the channel has been disposed</returns>
    public bool WaitToRead(int timeoutInMs)
    {
        using CancellationTokenSource ts = new(timeoutInMs);
        try
        {
            return WaitToRead(ts.Token);
        }
        catch (OperationCanceledException)
        {
            throw new TimeoutException("Wait to read timed out");
        }
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
    public async ValueTask<bool> WaitToReadAsync(int timeoutInMs)
    {
        using CancellationTokenSource ts = new(timeoutInMs);
        try
        {
            return await WaitToReadAsync(ts.Token);
        }
        catch (OperationCanceledException)
        {
            throw new TimeoutException("Wait to read timed out");
        }
    }

    /// <summary>
    /// Waits until a message arrives.
    /// </summary>
    /// <param name="token">A cancellation token that makes the function stop blocking when cancelled. If not provided, waits indefinitely.</param>
    /// <returns>False if the channel has been disposed</returns>
    public ValueTask<bool> WaitToReadAsync(CancellationToken token = default) =>
        new(messageQueue.OutputAvailableAsync(token));


    /// <summary>
    /// Waits a given time until a message arrives, and pulls it from the queue.
    /// </summary>
    /// <param name="timeoutInMs">The maximal time to wait</param>
    /// <returns>The message that arrived</returns>
    public T Read(int timeoutInMs)
    {
        using CancellationTokenSource ts = new(timeoutInMs);
        try
        {
            return Read(ts.Token);
        }
        catch (OperationCanceledException)
        {
            throw new TimeoutException("Wait for read timed out");
        }
    }

    void ThrowIfNotStarted()
    {
        if (subscriber == null)
        {
            throw new InvalidOperationException("Channel has not been started");
        }
    }

    /// <summary>
    /// Waits until a message arrives, and pulls it from the queue.
    /// </summary>
    /// <param name="token">A cancellation token that makes the function stop blocking when cancelled. If not provided, waits indefinitely.</param>
    /// <returns>The message that arrived.</returns>
    public T Read(CancellationToken token = default)
    {
        ThrowIfNotStarted();
        return messageQueue.Take(token);
    }

    /// <summary>
    /// Awaits a given time until a message arrives, and pulls it from the queue.
    /// </summary>
    /// <param name="timeoutInMs">The maximal time to wait</param>
    /// <returns>The message that arrived</returns>
    public async ValueTask<T> ReadAsync(int timeoutInMs)
    {
        using CancellationTokenSource ts = new(timeoutInMs);
        try
        {
            return await ReadAsync(ts.Token);
        }
        catch (OperationCanceledException)
        {
            throw new TimeoutException("Wait for read timed out");
        }
    }

    /// <summary>
    /// Awaits until a message arrives, and pulls it from the queue.
    /// </summary>
    /// <param name="token">A cancellation token that makes the function stop blocking when cancelled. If not provided, waits indefinitely.</param>
    /// <returns>The message that arrived.</returns>
    public ValueTask<T> ReadAsync(CancellationToken token = default)
    {
        ThrowIfNotStarted();
        return messageQueue.TakeAsync(token).AsValueTask();
    }


    /// <summary>
    /// Checks if there is a message in the queue, and returns it without blocking.
    /// </summary>
    /// <param name="t">The received message, or default if no message was available.</param>
    /// <returns>True if there was a message available.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the queue has been disposed</exception>
    public bool TryRead([NotNullWhen(true)] out T? t)
    {
        ThrowIfNotStarted();

        if (backQueue.Count == 0)
        {
            t = default;
            return false;
        }

        var cancelledToken = new CancellationToken(true);

        try
        {
            t = messageQueue.Take(cancelledToken);
            return true;
        }
        catch (OperationCanceledException)
        {
            // this shouldn't happen unless multiple reads get called at the same time
            t = default!;
            return false;
        }
    }


    /// <summary>
    /// Enumerates through the available messages, and blocks while waiting for the next.
    /// It will only return either when the token has been canceled, or the channel has been disposed.
    /// </summary>
    /// <param name="token">An optional cancellation token.</param>
    /// <returns>An enumerator that can be used in a foreach</returns>
    public IEnumerable<T> ReadAll(CancellationToken token = default)
    {
        while (true)
        {
            yield return Read(token);
        }
    }

    /// <summary>
    /// Enumerates through the available messages, without blocking.
    /// </summary>
    /// <returns>An enumerator that can be used in a foreach</returns>
    public IEnumerable<T> TryReadAll()
    {
        while (true)
        {
            if (backQueue.Count == 0)
            {
                yield break;
            }

            var cancelledToken = new CancellationToken(true);

            T element;
            try
            {
                element = messageQueue.Take(cancelledToken);
            }
            catch (OperationCanceledException)
            {
                // this shouldn't happen unless multiple reads get called at the same time
                yield break;
            }

            yield return element!;
        }
    }

    /// <summary>
    /// Enumerates through the available messages, and 'awaits' while waiting for the next.
    /// It will only return either when the token has been canceled, or the channel has been disposed.
    /// </summary>
    /// <param name="token">A cancellation token that makes the function stop blocking when cancelled.</param>
    /// <returns>An enumerator that can be used in a foreach</returns>
    public async IAsyncEnumerable<T> ReadAllAsync(
        [EnumeratorCancellation] CancellationToken token = default)
    {
        ThrowIfNotStarted();
        while (true)
        {
            yield return await messageQueue.TakeAsync(token);
        }
    }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken token = default)
    {
        return ReadAllAsync(token).GetAsyncEnumerator(token);
    }

    IMessage IRosChannelReader.Read(CancellationToken token)
    {
        return Read(token);
    }

    async ValueTask<IMessage> IRosChannelReader.ReadAsync(CancellationToken token)
    {
        return await ReadAsync(token);
    }

    IEnumerable<IMessage> IRosChannelReader.TryReadAll()
    {
        return (IEnumerable<IMessage>)TryReadAll();
    }

    IEnumerable<IMessage> IRosChannelReader.ReadAll(CancellationToken token)
    {
        return (IEnumerable<IMessage>)ReadAll(token);
    }

    async IAsyncEnumerable<IMessage> IRosChannelReader.ReadAllAsync([EnumeratorCancellation] CancellationToken token)
    {
        ThrowIfNotStarted();
        while (true)
        {
            yield return await messageQueue.TakeAsync(token);
        }
    }

    public override string ToString()
    {
        if (subscriber == null)
        {
            return "[RosChannelReader (uninitialized)]";
        }

        return disposed
            ? "[RosChannelReader (disposed)]"
            : $"[RosChannelReader {subscriber.Topic} [{subscriber.TopicType}]]";
    }
}