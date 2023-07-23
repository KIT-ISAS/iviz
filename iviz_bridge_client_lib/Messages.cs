using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Iviz.Msgs;
using JetBrains.Annotations;

namespace Iviz.Bridge.Client;

public abstract class SerializableMessage
{
    public abstract void SerializeTo(MemoryStream stream);
}

[DataContract, UsedImplicitly]
public sealed class ResponseMessage
{
    [DataMember(Name = "op")] public string Op = "";
    [DataMember(Name = "topic")] public string Topic = "";
    [DataMember(Name = "service")] public string Service = "";
    [DataMember(Name = "id")] public string Id = "";
    [DataMember(Name = "result")] public bool Result;
    
    [Preserve]
    public ResponseMessage()
    {
    }    
}

[DataContract]
public sealed class GenericMessage : SerializableMessage
{
    [DataMember(Name = "op")] public string Op = "";
    [DataMember(Name = "topic")] public string Topic = "";
    [DataMember(Name = "type")] public string Type = "";

    public override void SerializeTo(MemoryStream stream) =>
        Utf8Json.JsonSerializer.Serialize(stream, this, null);
}

[DataContract]
public sealed class SubscribeMessage : SerializableMessage
{
    [DataMember(Name = "op")] public readonly string Op = "subscribe";
    [DataMember(Name = "topic")] public string Topic = "";
    [DataMember(Name = "type")] public string Type = "";
    [DataMember(Name = "compression")] public readonly string Compression = "cbor-raw";

    public override void SerializeTo(MemoryStream stream) =>
        Utf8Json.JsonSerializer.Serialize(stream, this, null);
}

[DataContract]
public sealed class FullSubscribeMessage : SerializableMessage
{
    [DataMember(Name = "op")] public readonly string Op = "subscribe";
    [DataMember(Name = "topic")] public string Topic = "";
    [DataMember(Name = "type")] public string Type = "";
    [DataMember(Name = "throttle_rate")] public int ThrottleRate;
    [DataMember(Name = "queue_length")] public int QueueLength;
    [DataMember(Name = "compression")] public readonly string Compression = "cbor-raw";

    public override void SerializeTo(MemoryStream stream) =>
        Utf8Json.JsonSerializer.Serialize(stream, this, null);
}

[DataContract, UsedImplicitly]
public sealed class PublishResponseMessage<T>
{
    [DataMember(Name = "msg")] public T? Msg;
    
    [Preserve]
    public PublishResponseMessage()
    {
    }    
}

[DataContract]
public sealed class PublishMessage<T> : SerializableMessage
{
    [DataMember(Name = "op")] public readonly string Op = "publish";
    [DataMember(Name = "topic")] public string Topic = "";
    [DataMember(Name = "msg")] public T? Msg;

    public override void SerializeTo(MemoryStream stream)
    {
        Utf8Json.JsonSerializer.Serialize(stream, this, null);
    }
}

[DataContract]
public sealed class GenericServiceMessage : SerializableMessage
{
    [DataMember(Name = "op")] public string Op = "";
    [DataMember(Name = "service")] public string Service = "";
    [DataMember(Name = "type")] public string Type = "";

    public override void SerializeTo(MemoryStream stream) =>
        Utf8Json.JsonSerializer.Serialize(stream, this);
}

[DataContract]
public sealed class CallServiceMessage<TRequest> : SerializableMessage
{
    [DataMember(Name = "op")] public readonly string Op = "call_service";
    [DataMember(Name = "id")] public string Id = "";
    [DataMember(Name = "service")] public string Service = "";
    [DataMember(Name = "args")] public TRequest? Args;

    public override void SerializeTo(MemoryStream stream) =>
        Utf8Json.JsonSerializer.Serialize(stream, this);
}

[DataContract, UsedImplicitly]
public sealed class ServiceResponseMessage<TResponse>
{
    [DataMember(Name = "id")] public string Id = "";
    [DataMember(Name = "service")] public string Service = "";
    [DataMember(Name = "values")] public TResponse? Values;
    
    [Preserve]
    public ServiceResponseMessage()
    {
    }    
}

[DataContract, UsedImplicitly]
public sealed class ServiceResponseErrorMessage
{
    [DataMember(Name = "values")] public string Values = "";

    [Preserve]
    public ServiceResponseErrorMessage()
    {
    }
}

[DataContract, UsedImplicitly]
public sealed class StatusMessage
{
    [DataMember(Name = "id")] public string Id = "";
    [DataMember(Name = "level")] public string Level = "";
    [DataMember(Name = "msg")] public string Msg = "";
    
    [Preserve]
    public StatusMessage()
    {
    }    
}