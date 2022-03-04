using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Roslib;

/// <summary>
/// Generic interface for all ROS channel readers.
/// </summary>
public interface IRosChannelReader : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Waits until a message arrives.
    /// </summary>
    /// <param name="token">A cancellation token that makes the function stop blocking when cancelled. If not provided, waits indefinitely.</param>
    /// <returns>False if the channel has been disposed</returns>        
    ValueTask<bool> WaitToReadAsync(CancellationToken token = default);
        
    /// <summary>
    /// Waits until a message arrives, and pulls it from the queue.
    /// </summary>
    /// <param name="token">A cancellation token that makes the function stop blocking when cancelled. If not provided, waits indefinitely.</param>
    /// <returns>The message that arrived.</returns>
    /// <exception cref="OperationCanceledException">Thrown if the token is canceled</exception>
    /// <exception cref="InvalidOperationException">Thrown if the queue has been disposed</exception>
    IMessage Read(CancellationToken token);
        
    /// <summary>
    /// Awaits until a message arrives, and pulls it from the queue.
    /// </summary>
    /// <param name="token">A cancellation token that makes the function stop blocking when cancelled. If not provided, waits indefinitely.</param>
    /// <returns>The message that arrived.</returns>
    ValueTask<IMessage> ReadAsync(CancellationToken token = default);
        
    /// <summary>
    /// Starts the channel. Must be called after the constructor.
    /// </summary>
    /// <param name="client">A connected IRosClient</param>
    /// <param name="topic">The topic to listen to</param>
    /// <param name="token">An optional cancellation token.</param>        
    public ValueTask StartAsync(IRosClient client, string topic, CancellationToken token = default);
        
    /// <summary>
    /// Starts the channel. Must be called after the constructor.
    /// </summary>
    /// <param name="client">A connected RosClient</param>
    /// <param name="topic">The topic to listen to</param>
    public void Start(IRosClient client, string topic);
        
    /// <summary>
    /// Enumerates through the available messages, and blocks while waiting for the next.
    /// It will only return either when the token has been canceled, or the channel has been disposed.
    /// </summary>
    /// <param name="token">An optional cancellation token.</param>
    /// <returns>An enumerator that can be used in a foreach</returns>
    public IEnumerable<IMessage> ReadAll(CancellationToken token = default);
        
    /// <summary>
    /// Enumerates through the available messages, without blocking.
    /// </summary>
    /// <returns>An enumerator that can be used in a foreach</returns>
    public IEnumerable<IMessage> TryReadAll();

#if !NETSTANDARD2_0
    /// <summary>
    /// Enumerates through the available messages, and 'awaits' while waiting for the next.
    /// It will only return either when the token has been canceled, or the channel has been disposed.
    /// </summary>
    /// <param name="token">A cancellation token that makes the function stop blocking when cancelled.</param>
    /// <returns>An enumerator that can be used in a foreach</returns>        
    public IAsyncEnumerable<IMessage> ReadAllAsync(CancellationToken token = default);
#endif
}