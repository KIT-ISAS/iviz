using System;

namespace Iviz.Roslib;

public readonly struct TopicTuple : IComparable<TopicTuple>
{
    public string Topic { get; }
    public string[] Members { get; }

    public TopicTuple(string topic, string[] members)
    {
        Topic = topic;
        Members = members;
    }

    public void Deconstruct(out string topic, out string[] members) =>
        (topic, members) = (Topic, Members);

    public override string ToString()
    {
        return $"[{Topic} [{string.Join(", ", Members)}]]";
    }

    public int CompareTo(TopicTuple other)
    {
        return string.Compare(Topic, other.Topic, StringComparison.Ordinal);
    }
}