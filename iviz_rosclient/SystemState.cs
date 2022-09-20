using Iviz.Roslib.Utils;

namespace Iviz.Roslib;

/// <summary>
/// A snapshot of the system state: publishers, subscribers, services, and the node ids associated with it.  
/// </summary>
public sealed class SystemState : JsonToString
{
    /// <summary>
    /// Publishers and their subscribers.
    /// </summary>
    public TopicTuple[] Publishers { get; }
    
    /// <summary>
    /// Subscribers and their publishers.
    /// </summary>
    public TopicTuple[] Subscribers { get; }
    
    /// <summary>
    /// Services and their provider. Each service contains a maximum of one member.
    /// </summary>
    public TopicTuple[] Services { get; }

    public SystemState(TopicTuple[] publishers, TopicTuple[] subscribers, TopicTuple[] services) =>
        (Publishers, Subscribers, Services) = (publishers, subscribers, services);
}