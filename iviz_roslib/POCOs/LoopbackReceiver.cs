using Iviz.Msgs;

namespace Iviz.Roslib;

/// <summary>
/// Implemented by subscriber connections. Used to send data from a publisher to a subscriber directly
/// if it is part of the same client, to avoid serialization and transmission through a socket. 
/// </summary>
/// <typeparam name="T">The message type</typeparam>
public abstract class LoopbackReceiver<T> where T : IMessageRos1
{
    /// <summary>
    /// Send a message from a publisher directly to a subscriber.
    /// </summary>
    /// <param name="message">The message being sent.</param>
    /// <param name="messageSize">The cached message size.</param>
    internal abstract void Post(in T message, int messageSize);
}