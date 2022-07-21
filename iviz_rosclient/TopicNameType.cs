using System;
using System.Runtime.Serialization;

namespace Iviz.Roslib;

/// <summary>
/// Topic name and message type.
/// </summary>
[DataContract]
public readonly struct TopicNameType : IComparable<TopicNameType>, IEquatable<TopicNameType>
{
    /// <summary>
    /// Topic name
    /// </summary>
    [DataMember] public string Topic { get; }

    /// <summary>
    /// Topic type
    /// </summary>
    [DataMember] public string Type { get; }

    public TopicNameType(string topic, string type) => (Topic, Type) = (topic, type);
        
    public void Deconstruct(out string topic, out string type) => (topic, type) = (Topic, Type);
        
    public override string ToString() => $"[Topic='{Topic}' Type='{Type}']";

    public int CompareTo(TopicNameType other) => (Topic, Type).CompareTo((other.Topic, other.Type));

    public bool Equals(TopicNameType other) => Topic == other.Topic && Type == other.Type;

    public override bool Equals(object? obj) => obj is TopicNameType other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(Topic, Type);

    public static bool operator ==(TopicNameType left, TopicNameType right) => left.Equals(right);

    public static bool operator !=(TopicNameType left, TopicNameType right) => !left.Equals(right);
}

