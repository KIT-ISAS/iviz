using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Iviz.Roslib.Utils;

namespace Iviz.Roslib.XmlRpc
{
    public sealed class TopicTuple : JsonToString, IComparable<TopicTuple>
    {
        public string Topic { get; }
        public ReadOnlyCollection<string> Members { get; }

        internal TopicTuple(string topic, IList<string> members)
        {
            Topic = topic;
            Members = members.AsReadOnly();
        }

        public void Deconstruct(out string topic, out ReadOnlyCollection<string> members) =>
            (topic, members) = (Topic, Members);

        public override string ToString()
        {
            return $"[{Topic} [{string.Join(", ", Members)}]]";
        }

        public int CompareTo(TopicTuple? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return string.Compare(Topic, other.Topic, StringComparison.Ordinal);
        }
    }
}