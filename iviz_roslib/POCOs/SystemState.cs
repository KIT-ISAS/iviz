using System.Collections.ObjectModel;
using Iviz.Roslib.Utils;
using Iviz.Roslib.XmlRpc;

namespace Iviz.Roslib;

public sealed class SystemState : JsonToString
{
    public TopicTuple[] Publishers { get; }
    public TopicTuple[] Subscribers { get; }
    public TopicTuple[] Services { get; }

    internal SystemState(TopicTuple[] publishers, TopicTuple[] subscribers, TopicTuple[] services) =>
        (Publishers, Subscribers, Services) = (publishers, subscribers, services);
}