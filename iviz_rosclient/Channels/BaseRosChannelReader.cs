using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib;

/// <summary>
/// Base class with common functionality for <see cref="RosChannelReader{T}"/>.
/// </summary>
/// <typeparam name="T">The ROS message type</typeparam>
public abstract class BaseRosChannelReader<T> : RosCallback<T>, IEnumerable<T>, IRosChannelReader, IAsyncEnumerable<T>
    where T : IMessage
{
    readonly ChannelReader<T> reader;
    protected readonly ChannelWriter<T> writer;

    protected bool disposed;
    protected string? subscriberId;
    protected IRosSubscriber<T>? subscriber;
    protected CancellationTokenRegistration subscriberToken;

    /// <summary>
    /// Tentative number of elements. This number may become outdated right after calling this property.
    /// Use this only as an estimate for the number of elements.
    /// </summary>
    public int Count => reader.Count;

    public IRosSubscriber<T> Subscriber =>
        subscriber ?? throw new InvalidOperationException("Channel has not been started!");

    public string Topic => Subscriber.Topic;

    public bool IsAlive => subscriber != null && !disposed;

    public BaseRosChannelReader()
    {
        var options = new UnboundedChannelOptions { SingleWriter = true };
        var channel = Channel.CreateUnbounded<T>(options);
        reader = channel.Reader;
        writer = channel.Writer;
        //messageQueue = new AsyncCollection<T>(backQueue);
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
        if (disposed) return;
        disposed = true;

        //messageQueue.CompleteAdding();
        writer.TryComplete();

        if (subscriber == null)
        {
            return; // not started
        }


        await subscriberToken.DisposeAsync();
        await subscriber.UnsubscribeAsync(subscriberId!).AwaitNoThrow(this);
    }

    public void Dispose()
    {
        TaskUtils.RunSync(DisposeAsync);
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
        writer.TryComplete();
    }

    /// <summary>
    /// Waits until a message arrives.
    /// </summary>
    /// <returns>False if the channel has been disposed</returns>
    public bool WaitToRead(int timeoutInMs)
    {
        using var ts = new CancellationTokenSource(timeoutInMs);
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
        return TaskUtils.RunSync(WaitToReadAsync, token);
    }

    /// <summary>
    /// Waits until a message arrives.
    /// </summary>
    /// <returns>False if the channel has been disposed</returns>
    public async ValueTask<bool> WaitToReadAsync(int timeoutInMs)
    {
        using var ts = new CancellationTokenSource(timeoutInMs);
        try
        {
            return await reader.WaitToReadAsync(ts.Token);
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
        reader.WaitToReadAsync(token);


    /// <summary>
    /// Waits a given time until a message arrives, and pulls it from the queue.
    /// </summary>
    /// <param name="timeoutInMs">The maximal time to wait</param>
    /// <returns>The message that arrived</returns>
    public T Read(int timeoutInMs)
    {
        using var ts = new CancellationTokenSource(timeoutInMs);
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
        return TaskUtils.RunSync(ReadAsync, token);
    }

    /// <summary>
    /// Awaits a given time until a message arrives, and pulls it from the queue.
    /// </summary>
    /// <param name="timeoutInMs">The maximal time to wait</param>
    /// <returns>The message that arrived</returns>
    public async ValueTask<T> ReadAsync(int timeoutInMs)
    {
        using var ts = new CancellationTokenSource(timeoutInMs);
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
        return reader.ReadAsync(token);
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
        return reader.TryRead(out t);
    }

    public void Clear()
    {
        while (TryRead(out _))
        {
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
        Func<CancellationToken,ValueTask<T>> readAsync = ReadAsync;

        while (true)
        {
            yield return TaskUtils.RunSync(readAsync, token);
        }
    }

    /// <summary>
    /// Enumerates through the available messages, without blocking.
    /// </summary>
    /// <returns>An enumerator that can be used in a foreach</returns>
    public IEnumerable<T> TryReadAll()
    {
        return reader.Count == 0
            ? Enumerable.Empty<T>() // most likely case, does not allocate
            : TryReadAllCore();
    }
    
    IEnumerable<T> TryReadAllCore()
    {
        while (true)
        {
            if (TryRead(out T? t))
            {
                yield return t;
            }
            else
            {
                yield break;
            }
        }
    }    

    /// <summary>
    /// Enumerates through the available messages, and 'awaits' while waiting for the next.
    /// It will only return either when the token has been canceled, or the channel has been disposed.
    /// </summary>
    /// <param name="token">A cancellation token that makes the function stop blocking when cancelled.</param>
    /// <returns>An enumerator that can be used in a foreach</returns>
    public IAsyncEnumerable<T> ReadAllAsync(CancellationToken token = default)
    {
        ThrowIfNotStarted();
        return reader.ReadAllAsync(token);
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
            yield return await reader.ReadAsync(token);
        }
    }
}