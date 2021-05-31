using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib.Utils;
using Iviz.XmlRpc;
using TopicTuple = System.Tuple<string, string>;
using TopicTuples = System.Tuple<string, string[]>;

namespace Iviz.Roslib.XmlRpc
{
    public static class StatusCode
    {
        public const int Error = -1;
        public const int Failure = 0;
        public const int Success = 1;
    }

    /// <summary>
    /// Implements communication to the ROS master API.
    /// </summary>
    public sealed class RosMasterClient : IDisposable
    {
        public Uri MasterUri { get; }
        public Uri CallerUri { get; }
        public string CallerId { get; }
        public int TimeoutInMs { get; set; } = 3000;

        readonly XmlRpcArg[] callerIdArgCache;

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
            callerIdArgCache = new XmlRpcArg[] {CallerId};

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
            XmlRpcArg[] args = callerIdArgCache;
            var response = MethodCall("getUri", args);
            return new GetUriResponse(response);
        }

        public async ValueTask<GetUriResponse> GetUriAsync(CancellationToken token = default)
        {
            XmlRpcArg[] args = callerIdArgCache;
            var response = await MethodCallAsync("getUri", args, token);
            return new GetUriResponse(response);
        }

        public LookupNodeResponse LookupNode(string nodeId)
        {
            if (nodeId == null)
            {
                throw new ArgumentNullException(nameof(nodeId));
            }

            XmlRpcArg[] args = {CallerId, nodeId};
            var response = MethodCall("lookupNode", args);
            return new LookupNodeResponse(response);
        }

        public async ValueTask<LookupNodeResponse> LookupNodeAsync(string nodeId, CancellationToken token = default)
        {
            if (nodeId == null)
            {
                throw new ArgumentNullException(nameof(nodeId));
            }

            XmlRpcArg[] args = {CallerId, nodeId};
            var response = await MethodCallAsync("lookupNode", args, token);
            return new LookupNodeResponse(response);
        }

        public GetPublishedTopicsResponse GetPublishedTopics(string subgraph = "")
        {
            XmlRpcArg[] args = {CallerId, subgraph};
            var response = MethodCall("getPublishedTopics", args);
            return new GetPublishedTopicsResponse(response);
        }

        public async ValueTask<GetPublishedTopicsResponse> GetPublishedTopicsAsync(string subgraph = "",
            CancellationToken token = default)
        {
            XmlRpcArg[] args = {CallerId, subgraph};
            var response = await MethodCallAsync("getPublishedTopics", args, token);
            return new GetPublishedTopicsResponse(response);
        }

        public GetPublishedTopicsResponse GetTopicTypes()
        {
            XmlRpcArg[] args = callerIdArgCache;
            var response = MethodCall("getTopicTypes", args);
            return new GetPublishedTopicsResponse(response);
        }

        public async ValueTask<GetPublishedTopicsResponse> GetTopicTypesAsync(CancellationToken token = default)
        {
            XmlRpcArg[] args = callerIdArgCache;
            var response = await MethodCallAsync("getTopicTypes", args, token);
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

            XmlRpcArg[] args = {CallerId, topic, topicType, CallerUri};
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

            XmlRpcArg[] args = {CallerId, topic, topicType, CallerUri};
            var response = await MethodCallAsync("registerSubscriber", args, token);
            return new RegisterSubscriberResponse(response);
        }

        public UnregisterSubscriberResponse UnregisterSubscriber(string topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            XmlRpcArg[] args = {CallerId, topic, CallerUri};
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

            XmlRpcArg[] args = {CallerId, topic, CallerUri};
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

            XmlRpcArg[] args = {CallerId, topic, topicType, CallerUri};
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

            XmlRpcArg[] args = {CallerId, topic, topicType, CallerUri};
            var response = await MethodCallAsync("registerPublisher", args, token);
            return new RegisterPublisherResponse(response);
        }

        public UnregisterPublisherResponse UnregisterPublisher(string topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            XmlRpcArg[] args = {CallerId, topic, CallerUri};
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

            XmlRpcArg[] args = {CallerId, topic, CallerUri};
            var response = await MethodCallAsync("unregisterPublisher", args, token);
            return new UnregisterPublisherResponse(response);
        }

        public GetSystemStateResponse GetSystemState()
        {
            XmlRpcArg[] args = callerIdArgCache;
            var response = MethodCall("getSystemState", args);
            return new GetSystemStateResponse(response);
        }

        public async ValueTask<GetSystemStateResponse> GetSystemStateAsync(CancellationToken token = default)
        {
            XmlRpcArg[] args = callerIdArgCache;
            var response = await MethodCallAsync("getSystemState", args, token);
            return new GetSystemStateResponse(response);
        }

        public LookupServiceResponse LookupService(string service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            XmlRpcArg[] args = {CallerId, service};
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

            XmlRpcArg[] args = {CallerId, service};
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

            XmlRpcArg[] args = {CallerId, service, rosRpcUri, CallerUri};
            var response = MethodCall("registerService", args);
            return new DefaultResponse(response);
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

            XmlRpcArg[] args = {CallerId, service, rosRpcUri, CallerUri};
            var response = await MethodCallAsync("registerService", args, token);
            return new DefaultResponse(response);
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

            XmlRpcArg[] args = {CallerId, service, rosRpcUri};
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

            XmlRpcArg[] args = {CallerId, service, rosRpcUri};
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
                throw new ParseException($"Rpc Response: Expected type object[], got {tmp}");
            }

            return result;
        }

        internal async ValueTask<XmlRpcValue[]> MethodCallAsync(string function, XmlRpcArg[] args,
            CancellationToken token)
        {
            using var ts = CancellationTokenSource.CreateLinkedTokenSource(token);
            ts.CancelAfter(TimeoutInMs);

            XmlRpcValue tmp;
            try
            {
                XmlRpcConnection freeConnection = rpcConnections.Min()!;
                //XmlRpcConnection freeConnection = rpcConnection;
                //using XmlRpcConnection freeConnection = new XmlRpcConnection("Rpc#0", MasterUri);
                tmp = await freeConnection.MethodCallAsync(CallerUri, function, args, ts.Token);
            }
            catch (OperationCanceledException)
            {
                if (!token.IsCancellationRequested)
                {
                    throw new TimeoutException($"Call to '{function}' timed out");
                }

                throw;
            }

            if (!tmp.TryGetArray(out XmlRpcValue[] result))
            {
                throw new ParseException($"Rpc Response: Expected type object[], got {tmp}");
            }

            return result;
        }
    }

    public abstract class BaseResponse
    {
        bool hasParseError;

        public int Code { get; protected set; }
        public string StatusMessage { get; protected set; } = "";
        public bool IsValid => Code == StatusCode.Success && !hasParseError;

        protected void MarkError()
        {
            Logger.LogFormat("[{0}]: Failed to parse response", GetType().Name);
            Code = StatusCode.Error;
            hasParseError = true;
        }

        public override string ToString()
        {
            return $"[{GetType().Name}]";
        }
    }

    public sealed class TopicTuple
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
    }

    public sealed class GetSystemStateResponse : BaseResponse
    {
        static readonly ReadOnlyCollection<TopicTuple> Empty = Array.Empty<TopicTuple>().AsReadOnly();

        public ReadOnlyCollection<TopicTuple> Publishers { get; } = Empty;
        public ReadOnlyCollection<TopicTuple> Subscribers { get; } = Empty;
        public ReadOnlyCollection<TopicTuple> Services { get; } = Empty;

        internal GetSystemStateResponse(XmlRpcValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGetInteger(out int code) ||
                !a[1].TryGetString(out string statusMessage))
            {
                MarkError();
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!a[2].TryGetArray(out XmlRpcValue[] root) || root.Length != 3)
            {
                MarkError();
                return;
            }


            Publishers = CreateTuple(root[0]);
            Subscribers = CreateTuple(root[1]);
            Services = CreateTuple(root[2]);
        }

        ReadOnlyCollection<TopicTuple> CreateTuple(XmlRpcValue root)
        {
            if (!root.TryGetArray(out XmlRpcValue[] objTuples))
            {
                MarkError();
                return Empty;
            }

            List<TopicTuple> result = new();
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

                result.Add(new TopicTuple(topic, members));
            }

            return result.AsReadOnly();
        }
    }

    public sealed class DefaultResponse : BaseResponse
    {
        internal DefaultResponse(XmlRpcValue[]? _)
        {
            Code = StatusCode.Success;
        }
    }

    public sealed class GetUriResponse : BaseResponse
    {
        public Uri? Uri { get; }

        internal GetUriResponse(XmlRpcValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGetInteger(out int code) ||
                !a[1].TryGetString(out string statusMessage))
            {
                MarkError();
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!a[2].TryGetString(out string uriStr))
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
                Logger.Log("Rpc Response: Failed to parse GetUriResponse uri: " + a[2]);
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
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGetInteger(out int code) ||
                !a[1].TryGetString(out string statusMessage))
            {
                MarkError();
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!a[2].TryGetString(out string uriStr))
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
                Logger.Log($"{this}: Failed to parse LookupNodeResponse uri: {uriStr}");
                MarkError();
                Uri = null;
            }
        }
    }

    public sealed class GetPublishedTopicsResponse : BaseResponse
    {
        static readonly ReadOnlyCollection<(string, string)> Empty = Array.Empty<(string, string)>().AsReadOnly();

        public ReadOnlyCollection<(string name, string type)> Topics { get; } = Empty;

        internal GetPublishedTopicsResponse(XmlRpcValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGetInteger(out int code) ||
                !a[1].TryGetString(out string statusMessage))
            {
                MarkError();
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!a[2].TryGetArray(out XmlRpcValue[] objTopics))
            {
                MarkError();
                return;
            }

            List<(string, string)> topics = new();
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

                topics.Add((topicName, topicType));
            }

            Topics = topics.AsReadOnly();
        }
    }

    public sealed class RegisterSubscriberResponse : BaseResponse
    {
        static readonly ReadOnlyCollection<Uri> Empty = Array.Empty<Uri>().AsReadOnly();

        public ReadOnlyCollection<Uri> Publishers { get; } = Empty;

        internal RegisterSubscriberResponse(XmlRpcValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGetInteger(out int code) ||
                !a[1].TryGetString(out string statusMessage))
            {
                MarkError();
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!a[2].TryGetArray(out XmlRpcValue[] objUriStrings))
            {
                MarkError();
                return;
            }


            List<Uri> publishers = new();
            foreach (var objUriStr in objUriStrings)
            {
                if (!objUriStr.TryGetString(out string uriStr) ||
                    !Uri.TryCreate(uriStr, UriKind.Absolute, out Uri? publisher))
                {
                    Logger.Log($"RcpMaster: Invalid uri '{objUriStr}'");
                    MarkError();
                    return;
                }

                publishers.Add(publisher);
            }

            Publishers = publishers.AsReadOnly();
        }
    }

    public sealed class UnregisterSubscriberResponse : BaseResponse
    {
        public int NumUnsubscribed { get; }

        internal UnregisterSubscriberResponse(XmlRpcValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGetInteger(out int code) ||
                !a[1].TryGetString(out string statusMessage))
            {
                MarkError();
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!a[2].TryGetInteger(out int numUnsubscribed))
            {
                MarkError();
                return;
            }

            NumUnsubscribed = numUnsubscribed;
        }
    }

    public sealed class RegisterPublisherResponse : BaseResponse
    {
        static readonly ReadOnlyCollection<string> Empty = Array.Empty<string>().AsReadOnly();

        public ReadOnlyCollection<string> Subscribers { get; } = Empty;

        internal RegisterPublisherResponse(XmlRpcValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGetInteger(out int code) ||
                !a[1].TryGetString(out string statusMessage))
            {
                MarkError();
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!a[2].TryGetArray(out XmlRpcValue[] objSubscriberStrs))
            {
                MarkError();
                return;
            }

            List<string> subscribers = new();
            foreach (var objSubscriberStr in objSubscriberStrs)
            {
                if (!objSubscriberStr.TryGetString(out string subscriberStr))
                {
                    MarkError();
                    return;
                }

                subscribers.Add(subscriberStr);
            }

            Subscribers = subscribers.AsReadOnly();
        }
    }

    public sealed class UnregisterPublisherResponse : BaseResponse
    {
        public int NumUnregistered { get; }

        internal UnregisterPublisherResponse(XmlRpcValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGetInteger(out int code) ||
                !a[1].TryGetString(out string statusMessage))
            {
                MarkError();
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!a[2].TryGetInteger(out int numUnregistered))
            {
                MarkError();
                return;
            }

            NumUnregistered = numUnregistered;
        }
    }

    public sealed class LookupServiceResponse : BaseResponse
    {
        public Uri? ServiceUrl { get; }

        internal LookupServiceResponse(XmlRpcValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGetInteger(out int code) ||
                !a[1].TryGetString(out string statusMessage))
            {
                MarkError();
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!a[2].TryGetString(out string uriStr) ||
                !Uri.TryCreate(uriStr, UriKind.Absolute, out Uri? uri))
            {
                MarkError();
                return;
            }

            ServiceUrl = uri;
        }
    }

    public sealed class UnregisterServiceResponse : BaseResponse
    {
        public int NumUnregistered { get; }

        internal UnregisterServiceResponse(XmlRpcValue[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !a[0].TryGetInteger(out int code) ||
                !a[1].TryGetString(out string statusMessage))
            {
                MarkError();
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!a[2].TryGetInteger(out int numUnregistered))
            {
                MarkError();
                return;
            }

            NumUnregistered = numUnregistered;
        }
    }
}