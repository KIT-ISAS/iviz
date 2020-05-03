using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using TopicTuple = System.Tuple<string, string>;
using TopicTuples = System.Tuple<string, string[]>;

namespace Iviz.RoslibSharp.XmlRpc
{
    public enum StatusCode
    {
        Error = -1,
        Failure = 0,
        Success = 1
    }

    public class Master
    {
        public Uri MasterUri { get; }
        public Uri CallerUri { get; }
        readonly string CallerId;

        internal Master(Uri masterUri, string callerId, Uri callerUri)
        {
            MasterUri = masterUri;
            CallerUri = callerUri;
            CallerId = callerId;
        }

        public GetUriResponse GetUri()
        {
            Arg[] args = { new Arg(CallerId) };
            object response = Service.MethodCall(MasterUri, CallerUri, "getUri", args);
            return new GetUriResponse((object[])response);
        }

        public LookupNodeResponse LookupNode(string nodeId)
        {
            Arg[] args = {
                new Arg(CallerId),
                new Arg(nodeId),
            };
            object response = Service.MethodCall(MasterUri, CallerUri, "lookupNode", args);
            return new LookupNodeResponse((object[])response);
        }

        public GetPublishedTopicsResponse GetPublishedTopics(string subgraph = "")
        {
            Arg[] args = {
                new Arg(CallerId),
                new Arg(subgraph),
            };
            object response = Service.MethodCall(MasterUri, CallerUri, "getPublishedTopics", args);
            return new GetPublishedTopicsResponse((object[])response);
        }

        public RegisterSubscriberResponse RegisterSubscriber(string topic, string topicType)
        {
            Arg[] args = {
                new Arg(CallerId),
                new Arg(topic),
                new Arg(topicType),
                new Arg(CallerUri.ToString()),
            };
            object response = Service.MethodCall(MasterUri, CallerUri, "registerSubscriber", args);
            return new RegisterSubscriberResponse((object[])response);
        }

        public UnregisterSubscriberResponse UnregisterSubscriber(string topic)
        {
            Arg[] args = {
                new Arg(CallerId),
                new Arg(topic),
                new Arg(CallerUri.ToString()),
            };
            object response = Service.MethodCall(MasterUri, CallerUri, "unregisterSubscriber", args);
            return new UnregisterSubscriberResponse((object[])response);
        }

        public RegisterPublisherResponse RegisterPublisher(string topic, string topicType)
        {
            Arg[] args = {
                new Arg(CallerId),
                new Arg(topic),
                new Arg(topicType),
                new Arg(CallerUri),
            };
            object response = Service.MethodCall(MasterUri, CallerUri, "registerPublisher", args);
            return new RegisterPublisherResponse((object[])response);
        }

        public UnregisterPublisherResponse UnregisterPublisher(string topic)
        {
            Arg[] args = {
                new Arg(CallerId),
                new Arg(topic),
                new Arg(CallerUri),
            };
            object response = Service.MethodCall(MasterUri, CallerUri, "unregisterPublisher", args);
            return new UnregisterPublisherResponse((object[])response);
        }

        public GetSystemStateResponse GetSystemState()
        {
            Arg[] args = {
                new Arg(CallerId),
            };
            object response = Service.MethodCall(MasterUri, CallerUri, "getSystemState", args);
            return new GetSystemStateResponse((object[])response);
        }

        public LookupServiceResponse LookupService(string service)
        {
            Arg[] args = {
                new Arg(CallerId),
                new Arg(service),
            };
            object response = Service.MethodCall(MasterUri, CallerUri, "lookupService", args);
            return new LookupServiceResponse((object[])response);
        }

        public RegisterServiceResponse RegisterService(string service, Uri rosRpcUri)
        {
            Arg[] args = {
                new Arg(CallerId),
                new Arg(service),
                new Arg(rosRpcUri),
                new Arg(CallerUri),
            };
            object response = Service.MethodCall(MasterUri, CallerUri, "registerService", args);
            return new RegisterServiceResponse((object[])response);
        }

        public UnregisterServiceResponse UnregisterService(string service, Uri rosRpcUri)
        {
            Arg[] args = {
                new Arg(CallerId),
                new Arg(service),
                new Arg(rosRpcUri),
            };
            object response = Service.MethodCall(MasterUri, CallerUri, "unregisterService", args);
            return new UnregisterServiceResponse((object[])response);
        }
    }

    public class GetSystemStateResponse
    {
        public StatusCode Code { get; }
        public string StatusMessage { get; }

        public ReadOnlyCollection<TopicTuples> Publishers { get; }
        public ReadOnlyCollection<TopicTuples> Subscribers { get; }
        public ReadOnlyCollection<TopicTuples> Services { get; }

        internal GetSystemStateResponse(object[] a)
        {
            Code = (StatusCode)a[0];
            StatusMessage = (string)a[1];


            object[] root = (object[])a[2];
            Publishers = CreateTuple(root[0]);
            Subscribers = CreateTuple(root[1]);
            Services = CreateTuple(root[2]);
        }

        static ReadOnlyCollection<TopicTuples> CreateTuple(object root)
        {
            object[] list = (object[])root;
            TopicTuples[] result = new TopicTuples[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                object[] tuple = (object[])list[i];
                string topic = (string)tuple[0];
                string[] members = ((object[])tuple[1]).Cast<string>().ToArray();
                result[i] = Tuple.Create(topic, members);
            }
            return new ReadOnlyCollection<TopicTuples>(result);
        }
    }

    public class GetUriResponse
    {
        public StatusCode Code { get; }
        public string StatusMessage { get; }
        public Uri Uri { get; }

        internal GetUriResponse(object[] a)
        {
            Code = (StatusCode)a[0];
            StatusMessage = (string)a[1];
            Uri = new Uri((string)a[2]);
        }
    }

    public class LookupNodeResponse
    {
        public StatusCode Code { get; }
        public string StatusMessage { get; }
        public Uri Uri { get; }

        internal LookupNodeResponse(object[] a)
        {
            Code = (StatusCode)a[0];
            StatusMessage = (string)a[1];
            Uri = new Uri((string)a[2]);
        }
    }

    public class GetPublishedTopicsResponse
    {
        public StatusCode Code { get; }
        public string StatusMessage { get; }
        public ReadOnlyCollection<TopicTuple> Topics { get; }

        internal GetPublishedTopicsResponse(object[] a)
        {
            Code = (StatusCode)a[0];
            StatusMessage = (string)a[1];
            object[] tmp = (object[])a[2];

            TopicTuple[] topics = new TopicTuple[tmp.Length];
            for (int i = 0; i < topics.Length; i++)
            {
                object[] topic = (object[])tmp[i];
                topics[i] = Tuple.Create((string)topic[0], (string)topic[1]);
            }
            Topics = new ReadOnlyCollection<TopicTuple>(topics);
        }
    }

    public class RegisterSubscriberResponse
    {
        public StatusCode Code { get; }
        public string StatusMessage { get; }
        public ReadOnlyCollection<Uri> Publishers { get; }

        internal RegisterSubscriberResponse(object[] a)
        {
            Code = (StatusCode)a[0];
            StatusMessage = (string)a[1];
            object[] tmp = (object[])a[2];
            Uri[] publishers = new Uri[tmp.Length];
            for (int i = 0; i < publishers.Length; i++)
            {
                if (!Uri.TryCreate((string)tmp[i], UriKind.Absolute, out publishers[i]))
                {
                    Logger.Log($"RcpMaster: Invalid uri '{tmp[i]}'");
                }
            }
            Publishers = new ReadOnlyCollection<Uri>(publishers);
        }
    }

    public class UnregisterSubscriberResponse
    {
        public StatusCode Code { get; }
        public string StatusMessage { get; }
        public int NumUnsubscribed { get; }

        internal UnregisterSubscriberResponse(object[] a)
        {
            Code = (StatusCode)a[0];
            StatusMessage = (string)a[1];
            NumUnsubscribed = (int)a[2];
        }
    }

    public class RegisterPublisherResponse
    {
        public StatusCode Code { get; }
        public string StatusMessage { get; }
        public ReadOnlyCollection<string> Subscribers { get; }

        internal RegisterPublisherResponse(object[] a)
        {
            Code = (StatusCode)a[0];
            StatusMessage = (string)a[1];
            object[] tmp = (object[])a[2];
            string[] subscribers = new string[tmp.Length];
            for (int i = 0; i < subscribers.Length; i++)
            {
                subscribers[i] = (string)tmp[i];
            }
            Subscribers = new ReadOnlyCollection<string>(subscribers);
        }
    }

    public class UnregisterPublisherResponse
    {
        public StatusCode Code { get; }
        public string StatusMessage { get; }
        public int NumUnregistered { get; }

        internal UnregisterPublisherResponse(object[] a)
        {
            Code = (StatusCode)a[0];
            StatusMessage = (string)a[1];
            NumUnregistered = (int)a[2];
        }
    }

    public class LookupServiceResponse
    {
        public StatusCode Code { get; }
        public string StatusMessage { get; }
        public Uri ServiceUrl { get; }

        internal LookupServiceResponse(object[] a)
        {
            Code = (StatusCode)a[0];
            StatusMessage = (string)a[1];
            ServiceUrl = new Uri((string)a[2]);
        }
    }

    public class RegisterServiceResponse
    {
        public StatusCode Code { get; }
        public string StatusMessage { get; }

        internal RegisterServiceResponse(object[] a)
        {
            Code = (StatusCode)a[0];
            StatusMessage = (string)a[1];
        }
    }

    public class UnregisterServiceResponse
    {
        public StatusCode Code { get; }
        public string StatusMessage { get; }
        public int NumUnregistered { get; }

        internal UnregisterServiceResponse(object[] a)
        {
            Code = (StatusCode)a[0];
            StatusMessage = (string)a[1];
            NumUnregistered = (int)a[2];
        }
    }


}
