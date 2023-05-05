using System.Runtime.Serialization;
using Iviz.Msgs;
using JetBrains.Annotations;

namespace Iviz.Bridge;

[DataContract]
public sealed class PublishMessage<T>
{
    [DataMember(Name = "msg")] public T? Msg { get; set; }
}

[DataContract]
public sealed class SubscriberMessage
{
    [DataMember(Name = "op")] public string? Op { get; } = "publish";
    [DataMember(Name = "topic")] public string? Topic { get; set; }
    [DataMember(Name = "msg")] public IMessage? Msg { get; set; }
}