using Iviz.Roslib.Utils;

namespace Iviz.Roslib;

public sealed class SystemState : JsonToString
{
    public TopicTuple[] Publishers { get; }
    public TopicTuple[] Subscribers { get; }
    public TopicTuple[] Services { get; }

    public SystemState(TopicTuple[] publishers, TopicTuple[] subscribers, TopicTuple[] services) =>
        (Publishers, Subscribers, Services) = (publishers, subscribers, services);
}