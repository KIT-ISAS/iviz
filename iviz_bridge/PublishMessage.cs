using System.Runtime.Serialization;
using Iviz.Msgs;
using JetBrains.Annotations;

namespace Iviz.Bridge;

[DataContract]
public sealed class PublishMessage<T>
{
    [DataMember(Name = "msg")] public T? Msg;
}

[DataContract]
public sealed class SubscriberMessage
{
    [DataMember(Name = "op")] public readonly string? Op = "publish";
    [DataMember(Name = "topic")] public string? Topic;
    [DataMember(Name = "msg")] public IMessage? Msg;
}