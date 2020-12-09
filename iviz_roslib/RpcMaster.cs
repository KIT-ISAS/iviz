using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
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
    public sealed class RosMasterApi
    {
        public Uri MasterUri { get; }
        public Uri CallerUri { get; }
        public string CallerId { get; }
        public int TimeoutInMs { get; set; } = 2000;

        public RosMasterApi(Uri masterUri, string callerId, Uri callerUri)
        {
            MasterUri = masterUri;
            CallerUri = callerUri;
            CallerId = callerId;
        }

        public override string ToString()
        {
            return $"[Master masterUri={MasterUri} callerUri={CallerUri} callerId={CallerId}]";
        }

        public GetUriResponse GetUri()
        {
            Arg[] args = {CallerId};
            object[] response = MethodCall("getUri", args);
            return new GetUriResponse(response);
        }

        public async Task<GetUriResponse> GetUriAsync()
        {
            Arg[] args = {CallerId};
            object[] response = await MethodCallAsync("getUri", args).Caf();
            return new GetUriResponse(response);
        }

        public LookupNodeResponse LookupNode(string nodeId)
        {
            if (nodeId == null)
            {
                throw new ArgumentNullException(nameof(nodeId));
            }

            Arg[] args = {CallerId, nodeId};
            object[] response = MethodCall("lookupNode", args);
            return new LookupNodeResponse(response);
        }

        public async Task<LookupNodeResponse> LookupNodeAsync(string nodeId)
        {
            if (nodeId == null)
            {
                throw new ArgumentNullException(nameof(nodeId));
            }

            Arg[] args = {CallerId, nodeId};
            object[] response = await MethodCallAsync("lookupNode", args).Caf();
            return new LookupNodeResponse(response);
        }

        public GetPublishedTopicsResponse GetPublishedTopics(string subgraph = "")
        {
            Arg[] args = {CallerId, subgraph};
            object[] response = MethodCall("getPublishedTopics", args);
            return new GetPublishedTopicsResponse(response);
        }

        public async Task<GetPublishedTopicsResponse> GetPublishedTopicsAsync(string subgraph = "", CancellationToken token = default)
        {
            Arg[] args = {CallerId, subgraph};
            object[] response = await MethodCallAsync("getPublishedTopics", args, token).Caf();
            return new GetPublishedTopicsResponse(response);
        }

        public GetPublishedTopicsResponse GetTopicTypes()
        {
            Arg[] args = {CallerId};
            object[] response = MethodCall("getTopicTypes", args);
            return new GetPublishedTopicsResponse(response);
        }

        public async Task<GetPublishedTopicsResponse> GetTopicTypesAsync(CancellationToken token = default)
        {
            Arg[] args = {CallerId};
            object[] response = await MethodCallAsync("getTopicTypes", args, token).Caf();
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

            Arg[] args = {CallerId, topic, topicType, CallerUri};
            object[] response = MethodCall("registerSubscriber", args);
            return new RegisterSubscriberResponse(response);
        }

        public async Task<RegisterSubscriberResponse> RegisterSubscriberAsync(string topic, string topicType)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            if (topicType == null)
            {
                throw new ArgumentNullException(nameof(topicType));
            }

            Arg[] args = {CallerId, topic, topicType, CallerUri};
            object[] response = await MethodCallAsync("registerSubscriber", args).Caf();
            return new RegisterSubscriberResponse(response);
        }

        public UnregisterSubscriberResponse UnregisterSubscriber(string topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            Arg[] args = {CallerId, topic, CallerUri};
            object[] response = MethodCall("unregisterSubscriber", args);
            return new UnregisterSubscriberResponse(response);
        }

        public async Task<UnregisterSubscriberResponse> UnregisterSubscriberAsync(string topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            Arg[] args = {CallerId, topic, CallerUri};
            object[] response = await MethodCallAsync("unregisterSubscriber", args).Caf();
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

            Arg[] args = {CallerId, topic, topicType, CallerUri};
            object[] response = MethodCall("registerPublisher", args);
            return new RegisterPublisherResponse(response);
        }

        public async Task<RegisterPublisherResponse> RegisterPublisherAsync(string topic, string topicType)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            if (topicType == null)
            {
                throw new ArgumentNullException(nameof(topicType));
            }

            Arg[] args = {CallerId, topic, topicType, CallerUri};
            object[] response = await MethodCallAsync("registerPublisher", args).Caf();
            return new RegisterPublisherResponse(response);
        }

        public UnregisterPublisherResponse UnregisterPublisher(string topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            Arg[] args = {CallerId, topic, CallerUri};
            object[] response = MethodCall("unregisterPublisher", args);
            return new UnregisterPublisherResponse(response);
        }

        public async Task<UnregisterPublisherResponse> UnregisterPublisherAsync(string topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            Arg[] args = {CallerId, topic, CallerUri};
            object[] response = await MethodCallAsync("unregisterPublisher", args).Caf();
            return new UnregisterPublisherResponse(response);
        }

        public GetSystemStateResponse GetSystemState()
        {
            Arg[] args = {CallerId};
            object[] response = MethodCall("getSystemState", args);
            return new GetSystemStateResponse(response);
        }

        public async Task<GetSystemStateResponse> GetSystemStateAsync(CancellationToken token = default)
        {
            Arg[] args = {CallerId};
            object[] response = await MethodCallAsync("getSystemState", args, token).Caf();
            return new GetSystemStateResponse(response);
        }

        public LookupServiceResponse LookupService(string service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            Arg[] args = {CallerId, service};
            object[] response = MethodCall("lookupService", args);
            return new LookupServiceResponse(response);
        }

        public async Task<LookupServiceResponse> LookupServiceAsync(string service, CancellationToken token = default)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            Arg[] args = {CallerId, service};
            object[] response = await MethodCallAsync("lookupService", args, token).Caf();
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

            Arg[] args = {CallerId, service, rosRpcUri, CallerUri};
            object[] response = MethodCall("registerService", args);
            return new DefaultResponse(response);
        }

        public async Task<DefaultResponse> RegisterServiceAsync(string service, Uri rosRpcUri)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (rosRpcUri == null)
            {
                throw new ArgumentNullException(nameof(rosRpcUri));
            }

            Arg[] args = {CallerId, service, rosRpcUri, CallerUri};
            object[] response = await MethodCallAsync("registerService", args).Caf();
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

            Arg[] args = {CallerId, service, rosRpcUri};
            object[] response = MethodCall("unregisterService", args);
            return new UnregisterServiceResponse(response);
        }

        public async Task<UnregisterServiceResponse> UnregisterServiceAsync(string service, Uri rosRpcUri)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (rosRpcUri == null)
            {
                throw new ArgumentNullException(nameof(rosRpcUri));
            }

            Arg[] args = {CallerId, service, rosRpcUri};
            object[] response = await MethodCallAsync("unregisterService", args).Caf();
            return new UnregisterServiceResponse(response);
        }

        object[] MethodCall(string function, IEnumerable<Arg> args)
        {
            object tmp = XmlRpcService.MethodCall(MasterUri, CallerUri, function, args, TimeoutInMs);
            if (tmp is object[] result)
            {
                return result;
            }

            throw new ParseException($"Rpc Response: Expected type object[], got {tmp.GetType().Name}");
        }

        async Task<object[]> MethodCallAsync(string function, IEnumerable<Arg> args, CancellationToken token = default)
        {
            object tmp = await XmlRpcService.MethodCallAsync(MasterUri, CallerUri, function, args, TimeoutInMs, token)
                .Caf();
            if (tmp is object[] result)
            {
                return result;
            }

            throw new ParseException($"Rpc Response: Expected type object[], got {tmp.GetType().Name}");
        }
    }

    public abstract class BaseResponse
    {
        private protected bool hasParseError;

        public int Code { get; protected set; }
        public string? StatusMessage { get; protected set; }
        public bool IsValid => Code == StatusCode.Success && !hasParseError;
    }

    public class TopicTuple
    {
        public string Topic { get; }
        public ReadOnlyCollection<string> Members { get; }

        internal TopicTuple(string topic, IList<string> members)
        {
            Topic = topic;
            Members = members.AsReadOnly();
        }

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

        internal GetSystemStateResponse(object[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !(a[0] is int code) ||
                !(a[1] is string statusMessage))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!(a[2] is object[] root) || root.Length != 3)
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }


            Publishers = CreateTuple(root[0]);
            Subscribers = CreateTuple(root[1]);
            Services = CreateTuple(root[2]);
        }

        ReadOnlyCollection<TopicTuple> CreateTuple(object root)
        {
            if (!(root is object[] objTuples))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return Empty;
            }

            List<TopicTuple> result = new List<TopicTuple>();
            foreach (var objTuple in objTuples)
            {
                if (!(objTuple is object[] tuple) ||
                    tuple.Length != 2 ||
                    !(tuple[0] is string topic) ||
                    !(tuple[1] is object[] tmp))
                {
                    Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                    Code = StatusCode.Error;
                    hasParseError = true;
                    return Empty;
                }

                string[] members = tmp.Cast<string>().ToArray();
                result.Add(new TopicTuple(topic, members));
            }

            return result.AsReadOnly();
        }
    }

    public sealed class DefaultResponse : BaseResponse
    {
        internal DefaultResponse(object[]? _)
        {
            Code = StatusCode.Success;
        }
    }

    public sealed class GetUriResponse : BaseResponse
    {
        public Uri? Uri { get; }

        internal GetUriResponse(object[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !(a[0] is int code) ||
                !(a[1] is string statusMessage))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!(a[2] is string uriStr))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }


            if (Uri.TryCreate(uriStr, UriKind.Absolute, out Uri? uri))
            {
                Uri = uri;
            }
            else
            {
                Logger.Log($"Rpc Response: Failed to parse GetUriResponse uri: " + a[2]);
                hasParseError = true;
                Uri = null;
            }
        }
    }

    public sealed class LookupNodeResponse : BaseResponse
    {
        public Uri? Uri { get; }

        internal LookupNodeResponse(object[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !(a[0] is int code) ||
                !(a[1] is string statusMessage))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!(a[2] is string uriStr))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }


            if (Uri.TryCreate(uriStr, UriKind.Absolute, out Uri? uri))
            {
                Uri = uri;
            }
            else
            {
                Logger.Log($"RcpMaster: Failed to parse LookupNodeResponse uri: " + a[2]);
                hasParseError = true;
                Uri = null;
            }
        }
    }

    public sealed class GetPublishedTopicsResponse : BaseResponse
    {
        static readonly ReadOnlyCollection<(string, string)> Empty = Array.Empty<(string, string)>().AsReadOnly();

        public ReadOnlyCollection<(string name, string type)> Topics { get; } = Empty;

        internal GetPublishedTopicsResponse(object[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !(a[0] is int code) ||
                !(a[1] is string statusMessage))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!(a[2] is object[] objTopics))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            List<(string, string)> topics = new List<(string, string)>();
            foreach (var objTopic in objTopics)
            {
                if (!(objTopic is object[] topic) ||
                    topic.Length != 2 ||
                    !(topic[0] is string topicName) ||
                    !(topic[1] is string topicType))
                {
                    Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                    Code = StatusCode.Error;
                    hasParseError = true;
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

        internal RegisterSubscriberResponse(object[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !(a[0] is int code) ||
                !(a[1] is string statusMessage))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!(a[2] is object[] objUriStrs))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }


            List<Uri> publishers = new List<Uri>();
            foreach (var objUriStr in objUriStrs)
            {
                if (!(objUriStr is string uriStr) ||
                    !Uri.TryCreate(uriStr, UriKind.Absolute, out Uri? publisher))
                {
                    Logger.Log($"RcpMaster: Invalid uri '{objUriStr}'");
                    Code = StatusCode.Error;
                    hasParseError = true;
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

        internal UnregisterSubscriberResponse(object[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !(a[0] is int code) ||
                !(a[1] is string statusMessage))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!(a[2] is int numUnsubscribed))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            NumUnsubscribed = numUnsubscribed;
        }
    }

    public sealed class RegisterPublisherResponse : BaseResponse
    {
        static readonly ReadOnlyCollection<string> Empty = Array.Empty<string>().AsReadOnly();

        public ReadOnlyCollection<string> Subscribers { get; } = Empty;

        internal RegisterPublisherResponse(object[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !(a[0] is int code) ||
                !(a[1] is string statusMessage))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!(a[2] is object[] objSubscriberStrs))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            List<string> subscribers = new List<string>();
            foreach (var objSubscriberStr in objSubscriberStrs)
            {
                if (!(objSubscriberStr is string subscriberStr))
                {
                    Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                    Code = StatusCode.Error;
                    hasParseError = true;
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

        internal UnregisterPublisherResponse(object[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !(a[0] is int code) ||
                !(a[1] is string statusMessage))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!(a[2] is int numUnregistered))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            NumUnregistered = numUnregistered;
        }
    }

    public sealed class LookupServiceResponse : BaseResponse
    {
        public Uri? ServiceUrl { get; }

        internal LookupServiceResponse(object[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !(a[0] is int code) ||
                !(a[1] is string statusMessage))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!(a[2] is string uriStr) ||
                !Uri.TryCreate(uriStr, UriKind.Absolute, out Uri? uri))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            ServiceUrl = uri;
        }
    }

    public sealed class UnregisterServiceResponse : BaseResponse
    {
        public int NumUnregistered { get; }

        internal UnregisterServiceResponse(object[]? a)
        {
            if (a is null ||
                a.Length != 3 ||
                !(a[0] is int code) ||
                !(a[1] is string statusMessage))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            Code = code;
            StatusMessage = statusMessage;

            if (Code == StatusCode.Error)
            {
                return;
            }

            if (!(a[2] is int numUnregistered))
            {
                Logger.Log($"{this}: Parse error in {MethodBase.GetCurrentMethod()?.Name}");
                Code = StatusCode.Error;
                hasParseError = true;
                return;
            }

            NumUnregistered = numUnregistered;
        }
    }
}