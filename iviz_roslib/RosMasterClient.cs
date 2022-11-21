﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib.Utils;
using Iviz.Tools;
using Iviz.XmlRpc;
using TopicTuple = System.Tuple<string, string>;
using TopicTuples = System.Tuple<string, string[]>;

namespace Iviz.Roslib.XmlRpc;

public static class StatusCode
{
    public const int Error = -1;
    public const int Failure = 0;
    public const int Success = 1;
}

/// <summary>
/// Manages queries to the ROS master through XML-RPC.
/// </summary>
public sealed class RosMasterClient : IDisposable
{
    public Uri MasterUri { get; }
    public Uri CallerUri { get; }
    public string CallerId { get; }
    public int TimeoutInMs { get; set; } = 3000;

    readonly XmlRpcArg[] cachedCallerIdArg;

    readonly List<XmlRpcConnection> rpcConnections = new();

    public long BytesReceived => rpcConnections.Sum(connection => connection.BytesReceived);
    public long BytesSent => rpcConnections.Sum(connection => connection.BytesSent);
    public int RequestsInQueue => rpcConnections.Sum(connection => connection.QueueSize);
    public int TotalRequests => rpcConnections.Sum(connection => connection.TotalRequests);
    public int AvgTimeInQueueInMs => rpcConnections[0].AvgTimeInQueueInMs;

    public RosMasterClient(Uri masterUri, string callerId, Uri callerUri, int numParallelConnections = 1)
    {
        MasterUri = masterUri;
        CallerUri = callerUri;
        CallerId = callerId;
        cachedCallerIdArg = new XmlRpcArg[] { CallerId };

        if (numParallelConnections <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numParallelConnections));
        }

        for (int i = 0; i < numParallelConnections; i++)
        {
            rpcConnections.Add(new XmlRpcConnection("Rpc#" + i, masterUri));
        }
    }

    public void Dispose()
    {
        foreach (var connection in rpcConnections)
        {
            connection.Dispose();
        }
    }

    public override string ToString()
    {
        return $"{nameof(RosMasterClient)} masterUri={MasterUri} callerUri={CallerUri} callerId={CallerId}]";
    }

    public async ValueTask<GetUriResponse> GetUriAsync(CancellationToken token = default)
    {
        var response = await MethodCallAsync("getUri", cachedCallerIdArg, token);
        return new GetUriResponse(response);
    }

    public async ValueTask<LookupNodeResponse> LookupNodeAsync(string nodeId, CancellationToken token = default)
    {
        if (nodeId == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(nodeId));
        }

        XmlRpcArg[] args = { CallerId, nodeId };
        var response = await MethodCallAsync("lookupNode", args, token);
        return new LookupNodeResponse(response);
    }

    public async ValueTask<GetPublishedTopicsResponse> GetPublishedTopicsAsync(string subgraph = "",
        CancellationToken token = default)
    {
        XmlRpcArg[] args = { CallerId, subgraph };
        var response = await MethodCallAsync("getPublishedTopics", args, token);
        return new GetPublishedTopicsResponse(response);
    }

    public async ValueTask<GetPublishedTopicsResponse> GetTopicTypesAsync(CancellationToken token = default)
    {
        var response = await MethodCallAsync("getTopicTypes", cachedCallerIdArg, token);
        return new GetPublishedTopicsResponse(response);
    }

    public async ValueTask<RegisterSubscriberResponse> RegisterSubscriberAsync(string topic, string topicType,
        CancellationToken token = default)
    {
        if (topic == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(topic));
        }

        if (topicType == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(topicType));
        }

        XmlRpcArg[] args = { CallerId, topic, topicType, CallerUri };
        var response = await MethodCallAsync("registerSubscriber", args, token);
        return new RegisterSubscriberResponse(response);
    }

    public async ValueTask<UnregisterSubscriberResponse> UnregisterSubscriberAsync(string topic,
        CancellationToken token = default)
    {
        if (topic == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(topic));
        }

        XmlRpcArg[] args = { CallerId, topic, CallerUri };
        var response = await MethodCallAsync("unregisterSubscriber", args, token);
        return new UnregisterSubscriberResponse(response);
    }

    public async ValueTask<RegisterPublisherResponse> RegisterPublisherAsync(string topic, string topicType,
        CancellationToken token)
    {
        if (topic == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(topic));
        }

        if (topicType == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(topicType));
        }

        XmlRpcArg[] args = { CallerId, topic, topicType, CallerUri };
        var response = await MethodCallAsync("registerPublisher", args, token);
        return new RegisterPublisherResponse(response);
    }

    public async ValueTask<UnregisterPublisherResponse> UnregisterPublisherAsync(string topic,
        CancellationToken token = default)
    {
        if (topic == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(topic));
        }

        XmlRpcArg[] args = { CallerId, topic, CallerUri };
        var response = await MethodCallAsync("unregisterPublisher", args, token);
        return new UnregisterPublisherResponse(response);
    }

    public async ValueTask<GetSystemStateResponse> GetSystemStateAsync(CancellationToken token = default)
    {
        var response = await MethodCallAsync("getSystemState", cachedCallerIdArg, token);
        return new GetSystemStateResponse(response);
    }

    public async ValueTask<LookupServiceResponse> LookupServiceAsync(string service,
        CancellationToken token = default)
    {
        if (service == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(service));
        }

        XmlRpcArg[] args = { CallerId, service };
        var response = await MethodCallAsync("lookupService", args, token);
        return new LookupServiceResponse(response);
    }

    public async ValueTask<DefaultResponse> RegisterServiceAsync(string service, Uri rosRpcUri,
        CancellationToken token = default)
    {
        if (service == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(service));
        }

        if (rosRpcUri == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(rosRpcUri));
        }

        XmlRpcArg[] args = { CallerId, service, rosRpcUri, CallerUri };
        var response = await MethodCallAsync("registerService", args, token);
        return DefaultResponse.Create(response);
    }

    public async ValueTask<UnregisterServiceResponse> UnregisterServiceAsync(string service, Uri rosRpcUri,
        CancellationToken token = default)
    {
        if (service == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(service));
        }

        if (rosRpcUri == null)
        {
            BuiltIns.ThrowArgumentNull(nameof(rosRpcUri));
        }

        XmlRpcArg[] args = { CallerId, service, rosRpcUri };
        var response = await MethodCallAsync("unregisterService", args, token);
        return new UnregisterServiceResponse(response);
    }

    internal async ValueTask<RosValue[]> MethodCallAsync(string function, XmlRpcArg[] args,
        CancellationToken token)
    {
        using var ts = token.CanBeCanceled
            ? CancellationTokenSource.CreateLinkedTokenSource(token)
            : new CancellationTokenSource();
        ts.CancelAfter(TimeoutInMs);

        RosValue rpcValue;
        try
        {
            var freeConnection = rpcConnections.Min()!;
            rpcValue = await freeConnection.MethodCallAsync(CallerUri, function, args, ts.Token);
        }
        catch (OperationCanceledException) when (!token.IsCancellationRequested)
        {
            throw new TimeoutException($"Call to '{function}' timed out");
        }

        if (!rpcValue.TryGetArray(out var result))
        {
            throw new ParseException($"Rpc Response: Expected type object[], got {rpcValue}");
        }

        return result;
    }
}

public abstract class BaseResponse : JsonToString
{
    bool hasParseError;
    protected int? responseCode;

    public string StatusMessage { get; protected set; } = "";
    public bool IsValid => responseCode == StatusCode.Success && !hasParseError;
    public bool IsFailure => responseCode == StatusCode.Failure;
    public bool HasParseError => hasParseError;
    public bool IsSuccess => responseCode == StatusCode.Success;

    protected void MarkError()
    {
        Logger.LogFormat("[{0}]: Failed to parse response", GetType().Name);
        responseCode = null;
        StatusMessage = "Failed to parse response";
        hasParseError = true;
    }

    protected bool TryGetValueFromArgs(RosValue[]? a, out RosValue value)
    {
        if (a is null 
            || a.Length != 3 
            || !a[0].TryGet(out int code) 
            || !a[1].TryGet(out string statusMessage))
        {
            MarkError();
            value = default;
            return false;
        }

        responseCode = code;
        StatusMessage = statusMessage;
        value = a[2]; 
            
        return responseCode == StatusCode.Success;
    }
}

public sealed class GetSystemStateResponse : BaseResponse
{
    static readonly TopicTuple[] Empty = Array.Empty<TopicTuple>();

    public TopicTuple[] Publishers { get; } = Empty;
    public TopicTuple[] Subscribers { get; } = Empty;
    public TopicTuple[] Services { get; } = Empty;

    internal GetSystemStateResponse(RosValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }
            
        if (!value.TryGetArray(out RosValue[] root) || root.Length != 3)
        {
            MarkError();
            return;
        }
            
        Publishers = CreateTuple(root[0]);
        Subscribers = CreateTuple(root[1]);
        Services = CreateTuple(root[2]);
    }

    TopicTuple[] CreateTuple(RosValue root)
    {
        if (!root.TryGetArray(out RosValue[] objTuples))
        {
            MarkError();
            return Empty;
        }

        var result = new TopicTuple[objTuples.Length];
        int r = 0;
        foreach (var objTuple in objTuples)
        {
            if (!objTuple.TryGetArray(out RosValue[] tuple) ||
                tuple.Length != 2 ||
                !tuple[0].TryGet(out string topic) ||
                !tuple[1].TryGetArray(out RosValue[] tmpMembers))
            {
                MarkError();
                return Empty;
            }

            string[] members = new string[tmpMembers.Length];
            for (int i = 0; i < members.Length; i++)
            {
                if (!tmpMembers[i].TryGet(out string member))
                {
                    MarkError();
                    return Empty;
                }

                members[i] = member;
            }

            result[r++] = new TopicTuple(topic, members);
        }

        Array.Sort(result);
        return result;
    }
}

public sealed class DefaultResponse : BaseResponse
{
    DefaultResponse(int responseCode, string message = "")
    {
        this.responseCode = responseCode;
        StatusMessage = message;
    }

    static readonly DefaultResponse Success = new(StatusCode.Success);

    static readonly DefaultResponse ParseError = new(StatusCode.Error,
        "Failed to parse response, expected three arguments");

    static readonly DefaultResponse CodeNotIntError = new(StatusCode.Error,
        "Expected integer as first argument");

    static readonly DefaultResponse MessageNotStringError = new(StatusCode.Error,
        "Expected string as second argument");

    public static DefaultResponse Create(RosValue[] args)
    {
        if (args.Length != 3)
        {
            return ParseError;
        }

        if (!args[0].TryGet(out int code))
        {
            return CodeNotIntError;
        }

        if (code == StatusCode.Success)
        {
            return Success;
        }

        return !args[1].TryGet(out string message)
            ? MessageNotStringError
            : new DefaultResponse(code, message);
    }
}

public sealed class GetUriResponse : BaseResponse
{
    public Uri? Uri { get; }

    internal GetUriResponse(RosValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }
            
        if (!value.TryGet(out string uriStr))
        {
            MarkError();
            return;
        }

        if (Uri.TryCreate(uriStr, UriKind.Absolute, out Uri? uri))
        {
            Uri = uri;
        }
        else
        {
            Logger.LogFormat("{0}: Failed to parse GetUriResponse uri: {1}", this, value.ToString());
            MarkError();
            Uri = null;
        }
    }
}

public sealed class LookupNodeResponse : BaseResponse
{
    public Uri? Uri { get; }

    internal LookupNodeResponse(RosValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }

        if (!value.TryGet(out string uriStr))
        {
            MarkError();
            return;
        }


        if (Uri.TryCreate(uriStr, UriKind.Absolute, out Uri? uri))
        {
            Uri = uri;
        }
        else
        {
            Logger.LogFormat("{0}: Failed to parse LookupNodeResponse uri: {1}", this, uriStr);
            MarkError();
            Uri = null;
        }
    }
}

public sealed class GetPublishedTopicsResponse : BaseResponse
{
    static readonly (string name, string type)[] Empty = Array.Empty<(string, string)>();

    public (string name, string type)[] Topics { get; } = Empty;

    internal GetPublishedTopicsResponse(RosValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }
            
        if (!value.TryGetArray(out RosValue[] objTopics))
        {
            MarkError();
            return;
        }

        var topics = new (string, string)[objTopics.Length];
        int r = 0;
        foreach (var objTopic in objTopics)
        {
            if (!objTopic.TryGetArray(out RosValue[] topic) ||
                topic.Length != 2 ||
                !topic[0].TryGet(out string topicName) ||
                !topic[1].TryGet(out string topicType))
            {
                MarkError();
                return;
            }

            topics[r++] = (topicName, topicType);
        }

        Array.Sort(topics);
        Topics = topics;
    }
}

public sealed class RegisterSubscriberResponse : BaseResponse
{
    static readonly Uri[] Empty = Array.Empty<Uri>();

    public Uri[] Publishers { get; } = Empty;

    internal RegisterSubscriberResponse(RosValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }
            
        if (!value.TryGetArray(out var objUriStrings))
        {
            MarkError();
            return;
        }

        var publishers = new Uri[objUriStrings.Length];
        int r = 0;
        foreach (var objUriStr in objUriStrings)
        {
            if (!objUriStr.TryGet(out string uriStr) ||
                !Uri.TryCreate(uriStr, UriKind.Absolute, out Uri? publisher))
            {
                Logger.LogFormat("{0}: Invalid uri '{1}'", this, objUriStr.ToString());
                MarkError();
                return;
            }

            publishers[r++] = publisher;
        }

        Publishers = publishers;
    }
}

public sealed class UnregisterSubscriberResponse : BaseResponse
{
    public int NumUnsubscribed { get; }

    internal UnregisterSubscriberResponse(RosValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }
            
        if (!value.TryGet(out int numUnsubscribed))
        {
            MarkError();
            return;
        }

        NumUnsubscribed = numUnsubscribed;
    }
}

public sealed class RegisterPublisherResponse : BaseResponse
{
    static readonly string[] Empty = Array.Empty<string>();

    public string[] Subscribers { get; } = Empty;

    internal RegisterPublisherResponse(RosValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }
            
        if (!value.TryGetArray(out RosValue[] objSubscriberStrs))
        {
            MarkError();
            return;
        }

        string[] subscribers = new string[objSubscriberStrs.Length];
        int r = 0;
        foreach (var objSubscriberStr in objSubscriberStrs)
        {
            if (!objSubscriberStr.TryGet(out string subscriberStr))
            {
                MarkError();
                return;
            }

            subscribers[r++] = subscriberStr;
        }

        Subscribers = subscribers;
    }
}

public sealed class UnregisterPublisherResponse : BaseResponse
{
    public int NumUnregistered { get; }

    internal UnregisterPublisherResponse(RosValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }
            
        if (!value.TryGet(out int numUnregistered))
        {
            MarkError();
            return;
        }

        NumUnregistered = numUnregistered;
    }
}

public sealed class LookupServiceResponse : BaseResponse
{
    public Uri? ServiceUri { get; }

    internal LookupServiceResponse(RosValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }
            
        if (!value.TryGet(out string uriStr) ||
            !Uri.TryCreate(uriStr, UriKind.Absolute, out Uri? uri))
        {
            MarkError();
            return;
        }

        ServiceUri = uri;
    }
}

public sealed class UnregisterServiceResponse : BaseResponse
{
    public int NumUnregistered { get; }

    internal UnregisterServiceResponse(RosValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }
            
        if (!value.TryGet(out int numUnregistered))
        {
            MarkError();
            return;
        }

        NumUnregistered = numUnregistered;
    }
}