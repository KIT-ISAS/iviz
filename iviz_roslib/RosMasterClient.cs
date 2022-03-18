using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        return $"[RosMasterClient masterUri={MasterUri} callerUri={CallerUri} callerId={CallerId}]";
    }

    public GetUriResponse GetUri()
    {
        var response = MethodCall("getUri", cachedCallerIdArg);
        return new GetUriResponse(response);
    }

    public async ValueTask<GetUriResponse> GetUriAsync(CancellationToken token = default)
    {
        var response = await MethodCallAsync("getUri", cachedCallerIdArg, token);
        return new GetUriResponse(response);
    }

    public LookupNodeResponse LookupNode(string nodeId)
    {
        if (nodeId == null)
        {
            throw new ArgumentNullException(nameof(nodeId));
        }

        XmlRpcArg[] args = { CallerId, nodeId };
        var response = MethodCall("lookupNode", args);
        return new LookupNodeResponse(response);
    }

    public async ValueTask<LookupNodeResponse> LookupNodeAsync(string nodeId, CancellationToken token = default)
    {
        if (nodeId == null)
        {
            throw new ArgumentNullException(nameof(nodeId));
        }

        XmlRpcArg[] args = { CallerId, nodeId };
        var response = await MethodCallAsync("lookupNode", args, token);
        return new LookupNodeResponse(response);
    }

    public GetPublishedTopicsResponse GetPublishedTopics(string subgraph = "")
    {
        XmlRpcArg[] args = { CallerId, subgraph };
        var response = MethodCall("getPublishedTopics", args);
        return new GetPublishedTopicsResponse(response);
    }

    public async ValueTask<GetPublishedTopicsResponse> GetPublishedTopicsAsync(string subgraph = "",
        CancellationToken token = default)
    {
        XmlRpcArg[] args = { CallerId, subgraph };
        var response = await MethodCallAsync("getPublishedTopics", args, token);
        return new GetPublishedTopicsResponse(response);
    }

    public GetPublishedTopicsResponse GetTopicTypes()
    {
        var response = MethodCall("getTopicTypes", cachedCallerIdArg);
        return new GetPublishedTopicsResponse(response);
    }

    public async ValueTask<GetPublishedTopicsResponse> GetTopicTypesAsync(CancellationToken token = default)
    {
        var response = await MethodCallAsync("getTopicTypes", cachedCallerIdArg, token);
        return new GetPublishedTopicsResponse(response);
    }

    public RegisterSubscriberResponse RegisterSubscriber(string topic, string topicType)
    {
        if (topic == null)
        {
            throw new ArgumentNullException(nameof(topic));
        }

        if (topicType == null)
        {
            throw new ArgumentNullException(nameof(topicType));
        }

        XmlRpcArg[] args = { CallerId, topic, topicType, CallerUri };
        var response = MethodCall("registerSubscriber", args);
        return new RegisterSubscriberResponse(response);
    }

    public async ValueTask<RegisterSubscriberResponse> RegisterSubscriberAsync(string topic, string topicType,
        CancellationToken token = default)
    {
        if (topic == null)
        {
            throw new ArgumentNullException(nameof(topic));
        }

        if (topicType == null)
        {
            throw new ArgumentNullException(nameof(topicType));
        }

        XmlRpcArg[] args = { CallerId, topic, topicType, CallerUri };
        var response = await MethodCallAsync("registerSubscriber", args, token);
        return new RegisterSubscriberResponse(response);
    }

    public UnregisterSubscriberResponse UnregisterSubscriber(string topic)
    {
        if (topic == null)
        {
            throw new ArgumentNullException(nameof(topic));
        }

        XmlRpcArg[] args = { CallerId, topic, CallerUri };
        var response = MethodCall("unregisterSubscriber", args);
        return new UnregisterSubscriberResponse(response);
    }

    public async ValueTask<UnregisterSubscriberResponse> UnregisterSubscriberAsync(string topic,
        CancellationToken token = default)
    {
        if (topic == null)
        {
            throw new ArgumentNullException(nameof(topic));
        }

        XmlRpcArg[] args = { CallerId, topic, CallerUri };
        var response = await MethodCallAsync("unregisterSubscriber", args, token);
        return new UnregisterSubscriberResponse(response);
    }

    public RegisterPublisherResponse RegisterPublisher(string topic, string topicType)
    {
        if (topic == null)
        {
            throw new ArgumentNullException(nameof(topic));
        }

        if (topicType == null)
        {
            throw new ArgumentNullException(nameof(topicType));
        }

        XmlRpcArg[] args = { CallerId, topic, topicType, CallerUri };
        var response = MethodCall("registerPublisher", args);
        return new RegisterPublisherResponse(response);
    }

    public async ValueTask<RegisterPublisherResponse> RegisterPublisherAsync(string topic, string topicType,
        CancellationToken token)
    {
        if (topic == null)
        {
            throw new ArgumentNullException(nameof(topic));
        }

        if (topicType == null)
        {
            throw new ArgumentNullException(nameof(topicType));
        }

        XmlRpcArg[] args = { CallerId, topic, topicType, CallerUri };
        var response = await MethodCallAsync("registerPublisher", args, token);
        return new RegisterPublisherResponse(response);
    }

    public UnregisterPublisherResponse UnregisterPublisher(string topic)
    {
        if (topic == null)
        {
            throw new ArgumentNullException(nameof(topic));
        }

        XmlRpcArg[] args = { CallerId, topic, CallerUri };
        var response = MethodCall("unregisterPublisher", args);
        return new UnregisterPublisherResponse(response);
    }

    public async ValueTask<UnregisterPublisherResponse> UnregisterPublisherAsync(string topic,
        CancellationToken token = default)
    {
        if (topic == null)
        {
            throw new ArgumentNullException(nameof(topic));
        }

        XmlRpcArg[] args = { CallerId, topic, CallerUri };
        var response = await MethodCallAsync("unregisterPublisher", args, token);
        return new UnregisterPublisherResponse(response);
    }

    public GetSystemStateResponse GetSystemState()
    {
        var response = MethodCall("getSystemState", cachedCallerIdArg);
        return new GetSystemStateResponse(response);
    }

    public async ValueTask<GetSystemStateResponse> GetSystemStateAsync(CancellationToken token = default)
    {
        var response = await MethodCallAsync("getSystemState", cachedCallerIdArg, token);
        return new GetSystemStateResponse(response);
    }

    public LookupServiceResponse LookupService(string service)
    {
        if (service == null)
        {
            throw new ArgumentNullException(nameof(service));
        }

        XmlRpcArg[] args = { CallerId, service };
        var response = MethodCall("lookupService", args);
        return new LookupServiceResponse(response);
    }

    public async ValueTask<LookupServiceResponse> LookupServiceAsync(string service,
        CancellationToken token = default)
    {
        if (service == null)
        {
            throw new ArgumentNullException(nameof(service));
        }

        XmlRpcArg[] args = { CallerId, service };
        var response = await MethodCallAsync("lookupService", args, token);
        return new LookupServiceResponse(response);
    }

    public DefaultResponse RegisterService(string service, Uri rosRpcUri)
    {
        if (service == null)
        {
            throw new ArgumentNullException(nameof(service));
        }

        if (rosRpcUri == null)
        {
            throw new ArgumentNullException(nameof(rosRpcUri));
        }

        XmlRpcArg[] args = { CallerId, service, rosRpcUri, CallerUri };
        var response = MethodCall("registerService", args);
        return DefaultResponse.Create(response);
    }

    public async ValueTask<DefaultResponse> RegisterServiceAsync(string service, Uri rosRpcUri,
        CancellationToken token = default)
    {
        if (service == null)
        {
            throw new ArgumentNullException(nameof(service));
        }

        if (rosRpcUri == null)
        {
            throw new ArgumentNullException(nameof(rosRpcUri));
        }

        XmlRpcArg[] args = { CallerId, service, rosRpcUri, CallerUri };
        var response = await MethodCallAsync("registerService", args, token);
        return DefaultResponse.Create(response);
    }

    public UnregisterServiceResponse UnregisterService(string service, Uri rosRpcUri)
    {
        if (service == null)
        {
            throw new ArgumentNullException(nameof(service));
        }

        if (rosRpcUri == null)
        {
            throw new ArgumentNullException(nameof(rosRpcUri));
        }

        XmlRpcArg[] args = { CallerId, service, rosRpcUri };
        var response = MethodCall("unregisterService", args);
        return new UnregisterServiceResponse(response);
    }

    public async ValueTask<UnregisterServiceResponse> UnregisterServiceAsync(string service, Uri rosRpcUri,
        CancellationToken token = default)
    {
        if (service == null)
        {
            throw new ArgumentNullException(nameof(service));
        }

        if (rosRpcUri == null)
        {
            throw new ArgumentNullException(nameof(rosRpcUri));
        }

        XmlRpcArg[] args = { CallerId, service, rosRpcUri };
        var response = await MethodCallAsync("unregisterService", args, token);
        return new UnregisterServiceResponse(response);
    }

    internal XmlRpcValue[] MethodCall(string function, XmlRpcArg[] args)
    {
        using CancellationTokenSource ts = new(TimeoutInMs);

        XmlRpcValue tmp;
        try
        {
            tmp = XmlRpcService.MethodCall(MasterUri, CallerUri, function, args, ts.Token);
        }
        catch (OperationCanceledException)
        {
            throw new TimeoutException($"Call to '{function}' timed out");
        }

        if (!tmp.TryGetArray(out XmlRpcValue[] result))
        {
            throw new ParseException($"Expected type object[], got {tmp}");
        }

        return result;
    }

    internal async ValueTask<XmlRpcValue[]> MethodCallAsync(string function, XmlRpcArg[] args,
        CancellationToken token)
    {
        using var ts = token.CanBeCanceled
            ? CancellationTokenSource.CreateLinkedTokenSource(token)
            : new CancellationTokenSource();
        ts.CancelAfter(TimeoutInMs);

        XmlRpcValue rpcValue;
        try
        {
            var freeConnection = rpcConnections.Min()!;
            rpcValue = await freeConnection.MethodCallAsync(CallerUri, function, args, ts.Token);
        }
        catch (OperationCanceledException) when (!token.IsCancellationRequested)
        {
            throw new TimeoutException($"Call to '{function}' timed out");
        }

        if (!rpcValue.TryGetArray(out XmlRpcValue[] result))
        {
            throw new ParseException($"Rpc Response: Expected type object[], got {rpcValue}");
        }

        return result;
    }
}

public abstract class BaseResponse : JsonToString
{
    bool hasParseError;
    protected int responseCode;

    public string StatusMessage { get; protected set; } = "";
    public bool IsValid => responseCode == StatusCode.Success && !hasParseError;
    public bool IsFailure => responseCode == StatusCode.Failure;

    protected void MarkError()
    {
        Logger.LogFormat("[{0}]: Failed to parse response", GetType().Name);
        responseCode = StatusCode.Error;
        StatusMessage = "Failed to parse response";
        hasParseError = true;
    }

    protected bool TryGetValueFromArgs(XmlRpcValue[]? a, out XmlRpcValue value)
    {
        if (a is null 
            || a.Length != 3 
            || !a[0].TryGetInteger(out int code) 
            || !a[1].TryGetString(out string statusMessage))
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

    internal GetSystemStateResponse(XmlRpcValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }
            
        if (!value.TryGetArray(out XmlRpcValue[] root) || root.Length != 3)
        {
            MarkError();
            return;
        }
            
        Publishers = CreateTuple(root[0]);
        Subscribers = CreateTuple(root[1]);
        Services = CreateTuple(root[2]);
    }

    TopicTuple[] CreateTuple(XmlRpcValue root)
    {
        if (!root.TryGetArray(out XmlRpcValue[] objTuples))
        {
            MarkError();
            return Empty;
        }

        var result = new TopicTuple[objTuples.Length];
        int r = 0;
        foreach (var objTuple in objTuples)
        {
            if (!objTuple.TryGetArray(out XmlRpcValue[] tuple) ||
                tuple.Length != 2 ||
                !tuple[0].TryGetString(out string topic) ||
                !tuple[1].TryGetArray(out XmlRpcValue[] tmpMembers))
            {
                MarkError();
                return Empty;
            }

            string[] members = new string[tmpMembers.Length];
            for (int i = 0; i < members.Length; i++)
            {
                if (!tmpMembers[i].TryGetString(out string member))
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

    public static DefaultResponse Create(XmlRpcValue[] args)
    {
        if (args.Length != 3)
        {
            return ParseError;
        }

        if (!args[0].TryGetInteger(out int code))
        {
            return CodeNotIntError;
        }

        if (code == StatusCode.Success)
        {
            return Success;
        }

        return !args[1].TryGetString(out string message)
            ? MessageNotStringError
            : new DefaultResponse(code, message);
    }
}

public sealed class GetUriResponse : BaseResponse
{
    public Uri? Uri { get; }

    internal GetUriResponse(XmlRpcValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }
            
        if (!value.TryGetString(out string uriStr))
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

    internal LookupNodeResponse(XmlRpcValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }

        if (!value.TryGetString(out string uriStr))
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

    internal GetPublishedTopicsResponse(XmlRpcValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }
            
        if (!value.TryGetArray(out XmlRpcValue[] objTopics))
        {
            MarkError();
            return;
        }

        var topics = new (string, string)[objTopics.Length];
        int r = 0;
        foreach (var objTopic in objTopics)
        {
            if (!objTopic.TryGetArray(out XmlRpcValue[] topic) ||
                topic.Length != 2 ||
                !topic[0].TryGetString(out string topicName) ||
                !topic[1].TryGetString(out string topicType))
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

    internal RegisterSubscriberResponse(XmlRpcValue[]? a)
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
            if (!objUriStr.TryGetString(out string uriStr) ||
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

    internal UnregisterSubscriberResponse(XmlRpcValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }
            
        if (!value.TryGetInteger(out int numUnsubscribed))
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

    internal RegisterPublisherResponse(XmlRpcValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }
            
        if (!value.TryGetArray(out XmlRpcValue[] objSubscriberStrs))
        {
            MarkError();
            return;
        }

        string[] subscribers = new string[objSubscriberStrs.Length];
        int r = 0;
        foreach (var objSubscriberStr in objSubscriberStrs)
        {
            if (!objSubscriberStr.TryGetString(out string subscriberStr))
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

    internal UnregisterPublisherResponse(XmlRpcValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }
            
        if (!value.TryGetInteger(out int numUnregistered))
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

    internal LookupServiceResponse(XmlRpcValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }
            
        if (!value.TryGetString(out string uriStr) ||
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

    internal UnregisterServiceResponse(XmlRpcValue[]? a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }
            
        if (!value.TryGetInteger(out int numUnregistered))
        {
            MarkError();
            return;
        }

        NumUnregistered = numUnregistered;
    }
}