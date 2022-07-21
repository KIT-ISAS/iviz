using System;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Roslib;

/// <summary>
/// Determines how to wait for a message being sent.
/// </summary>
public enum RosPublishPolicy
{
    /// <summary>
    /// Enqueue the message without waiting. 
    /// </summary>
    DoNotWait,

    /// <summary>
    /// Enqueue the message and wait until all connections finish sending it.
    /// </summary>
    WaitUntilSent,
}

/// <summary>
/// Interface for all ROS publishers.
/// </summary>
public interface IRosPublisher : IDisposable
{
    /// <summary>
    /// A cancellation token that gets canceled when the publisher is disposed.
    /// </summary>
    public CancellationToken CancellationToken { get; }

    /// <summary>
    /// The name of the topic.
    /// </summary>
    public string Topic { get; }

    /// <summary>
    /// The ROS message type of the topic.
    /// </summary>        
    public string TopicType { get; }

    /// <summary>
    /// The number of publishers in the topic.
    /// </summary>
    public int NumSubscribers { get; }

    /// <summary>
    /// Publishes the given message into the topic. 
    /// </summary>
    /// <param name="message">The message to be published.</param>
    /// <exception cref="ArgumentNullException">The message is null</exception>
    /// <exception cref="RosInvalidMessageTypeException">The message type does not match.</exception>          
    public void Publish(IMessage message);

    /// <summary>
    /// Publishes the given message into the topic. 
    /// </summary>
    /// <param name="message">The message to be published.</param>
    /// <param name="policy">
    /// The policy for sending.
    /// If the default <see cref="RosPublishPolicy.DoNotWait"/> is set, the method will fail silently if
    /// an error happens.
    /// If <see cref="RosPublishPolicy.WaitUntilSent"/> is set, the task will wait until
    /// all the connections have sent the message, and throws an exception if at least one connection failed.
    /// </param>
    /// <param name="token">An optional cancellation token.</param>
    /// <exception cref="ArgumentNullException">The message is null</exception>
    /// <exception cref="RosInvalidMessageTypeException">The message type does not match.</exception>          
    /// <exception cref="AggregateException">An exception happened in one or multiple connections while sending the message.</exception>          
    public ValueTask PublishAsync(IMessage message, RosPublishPolicy policy = RosPublishPolicy.DoNotWait,
        CancellationToken token = default);

    /// <summary>
    /// Unregisters the given id from the publisher. If the publisher has no ids left, the topic will be unadvertised from the master.
    /// </summary>
    /// <param name="id">The id to be unregistered.</param>
    /// <param name="token">An optional cancellation token.</param>
    /// <returns>Whether the id belonged to the publisher.</returns>        
    public bool Unadvertise(string id, CancellationToken token = default);

    /// <summary>
    /// Unregisters the given id from the publisher. If the publisher has no ids left, the topic will be unadvertised from the master.
    /// </summary>
    /// <param name="id">The id to be unregistered.</param>
    /// <param name="token">An optional cancellation token.</param>
    /// <returns>Whether the id belonged to the publisher.</returns>
    public ValueTask<bool> UnadvertiseAsync(string id, CancellationToken token = default);

    /// <summary>
    /// Generates a new advertisement id. Use this string for Unadvertise().
    /// </summary>
    /// <returns>The advertisement id.</returns>
    public string Advertise();

    /// <summary>
    /// Checks whether this publisher has provided the given id from an Advertise() call.
    /// </summary>
    /// <param name="id">Identifier to check.</param>
    /// <returns>Whether the id was provided by this publisher.</returns>
    public bool ContainsId(string id);

    /// <summary>
    /// Checks whether this publisher's message type corresponds to the given type
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>Whether the class type matches.</returns>        
    public bool MessageTypeMatches(Type type);

    /// <summary>
    /// Returns a structure that represents the internal state of the publisher. 
    /// </summary>        
    public PublisherState GetState();

    public ValueTask<PublisherState> GetStateAsync();

    /// <summary>
    /// Whether latching is enabled. When active, new subscribers will automatically receive a copy of the last message sent.
    /// </summary>
    public bool LatchingEnabled { get; set; }

    /// <summary>
    /// Async version of Dispose(), for NET Standard 2.0 where IAsyncDisposable is not available.
    /// </summary>
    /// <returns>The awaitable dispose task.</returns>
    public ValueTask DisposeAsync(CancellationToken token);
}

public interface IRosPublisher<T> : IRosPublisher where T : IMessage
{
    /// <summary>
    /// Publishes the given message into the topic. 
    /// </summary>
    /// <param name="message">The message to be published.</param>
    /// <exception cref="ArgumentNullException">The message is null</exception>
    /// <exception cref="RosInvalidMessageTypeException">The message type does not match.</exception>          
    public void Publish(in T message);

    /// <summary>
    /// Publishes the given message into the topic.  
    /// </summary>
    /// <param name="message">The message to be published.</param>
    /// <param name="policy">
    /// The policy for sending.
    /// If the default <see cref="RosPublishPolicy.DoNotWait"/> is set, the method will fail silently if
    /// an error happens.
    /// If <see cref="RosPublishPolicy.WaitUntilSent"/> is set, the task will wait until
    /// all the connections have sent the message, and throws an exception if at least one connection failed.
    /// </param>
    /// <param name="token">An optional cancellation token.</param>
    /// <exception cref="ArgumentNullException">The message is null</exception>
    /// <exception cref="RosInvalidMessageTypeException">The message type does not match.</exception>          
    /// <exception cref="AggregateException">An exception happened in one or multiple connections while sending the message.</exception>          
    public ValueTask PublishAsync(in T message, RosPublishPolicy policy = RosPublishPolicy.DoNotWait,
        CancellationToken token = default);
}