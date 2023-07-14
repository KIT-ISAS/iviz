using System;
using System.IO;
using System.Runtime.Serialization;

namespace iviz_bridge_client;

public abstract class SerializableMessage
{
    public abstract void SerializeTo(MemoryStream stream);
}

[DataContract]
public sealed class ResponseMessage
{
    [DataMember(Name = "op")] public string Op = "";
    [DataMember(Name = "topic")] public string Topic = "";
    [DataMember(Name = "service")] public string Service = "";
    [DataMember(Name = "id")] public string Id = "";
    [DataMember(Name = "result")] public bool Result;
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
public sealed class PublishResponseMessage<T>
{
    [DataMember(Name = "msg")] public T? Msg;
}

[DataContract]
public sealed class PublishMessage<T> : SerializableMessage
{
    [DataMember(Name = "op")] public readonly string Op = "publish";
    [DataMember(Name = "topic")] public string Topic = "";
    [DataMember(Name = "msg")] public T? Msg;
    
    public override void SerializeTo(MemoryStream stream) => 
        Utf8Json.JsonSerializer.Serialize(stream, this);    
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

[DataContract]
public sealed class ServiceResponseMessage<TResponse> 
{
    [DataMember(Name = "id")] public string Id = "";
    [DataMember(Name = "service")] public string Service = "";
    [DataMember(Name = "values")] public TResponse? Values;
}