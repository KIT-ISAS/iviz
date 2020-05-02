using System;
using System.Linq;

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
        public readonly struct GetSystemStateResponse
        {
            public readonly StatusCode code;
            public readonly string statusMessage;

            public readonly Tuple<string, string[]>[] publishers;
            public readonly Tuple<string, string[]>[] subscribers;
            public readonly Tuple<string, string[]>[] services;

            public GetSystemStateResponse(object[] a)
            {
                code = (StatusCode)a[0];
                statusMessage = (string)a[1];


                object[] root = (object[])a[2];
                publishers = CreateTuple(root[0]);
                subscribers = CreateTuple(root[1]);
                services = CreateTuple(root[2]);
            }

            static Tuple<string, string[]>[] CreateTuple(object root)
            {
                object[] list = (object[])root;
                Tuple<string, string[]>[] result = new Tuple<string, string[]>[list.Length];
                for (int i = 0; i < list.Length; i++)
                {
                    object[] tuple = (object[])list[i];
                    string topic = (string)tuple[0];
                    string[] members = ((object[])tuple[1]).Cast<string>().ToArray();
                    result[i] = Tuple.Create(topic, members);
                }
                return result;
            }
        }

        public readonly struct GetUriResponse
        {
            public readonly StatusCode code;
            public readonly string statusMessage;
            public readonly string uri;

            public GetUriResponse(object[] a)
            {
                code = (StatusCode)a[0];
                statusMessage = (string)a[1];
                uri = (string)a[2];
            }
        }

        public readonly struct LookupNodeResponse
        {
            public readonly StatusCode code;
            public readonly string statusMessage;
            public readonly string uri;

            public LookupNodeResponse(object[] a)
            {
                code = (StatusCode)a[0];
                statusMessage = (string)a[1];
                uri = (string)a[2];
            }
        }

        public readonly struct GetPublishedTopicsResponse
        {
            public readonly StatusCode code;
            public readonly string statusMessage;
            public readonly Tuple<string, string>[] topics;

            public GetPublishedTopicsResponse(object[] a)
            {
                code = (StatusCode)a[0];
                statusMessage = (string)a[1];
                object[] tmp = (object[])a[2];
                topics = new Tuple<string, string>[tmp.Length];
                for (int i = 0; i < topics.Length; i++)
                {
                    object[] topic = (object[])tmp[i];
                    topics[i] = Tuple.Create((string)topic[0], (string)topic[1]);
                }
            }
        }

        public readonly struct RegisterSubscriberResponse
        {
            public readonly StatusCode code;
            public readonly string statusMessage;
            public readonly Uri[] publishers;

            public RegisterSubscriberResponse(object[] a)
            {
                code = (StatusCode)a[0];
                statusMessage = (string)a[1];
                object[] tmp = (object[])a[2];
                publishers = new Uri[tmp.Length];
                for (int i = 0; i < publishers.Length; i++)
                {
                    if (!Uri.TryCreate((string)tmp[i], UriKind.Absolute, out publishers[i]))
                    {
                        Logger.Log($"RcpMaster: Invalid uri '{tmp[i]}'");
                    }
                }
            }
        }

        public readonly struct UnregisterSubscriberResponse
        {
            public readonly StatusCode code;
            public readonly string statusMessage;
            public readonly int numUnsubscribed;

            public UnregisterSubscriberResponse(object[] a)
            {
                code = (StatusCode)a[0];
                statusMessage = (string)a[1];
                numUnsubscribed = (int)a[2];
            }
        }

        public readonly struct RegisterPublisherResponse
        {
            public readonly StatusCode code;
            public readonly string statusMessage;
            public readonly string[] subscribers;

            public RegisterPublisherResponse(object[] a)
            {
                code = (StatusCode)a[0];
                statusMessage = (string)a[1];
                object[] tmp = (object[])a[2];
                subscribers = new string[tmp.Length];
                for (int i = 0; i < subscribers.Length; i++)
                {
                    subscribers[i] = (string)tmp[i];
                }
            }
        }

        public readonly struct UnregisterPublisherResponse
        {
            public readonly StatusCode code;
            public readonly string statusMessage;
            public readonly int numUnregistered;

            public UnregisterPublisherResponse(object[] a)
            {
                code = (StatusCode)a[0];
                statusMessage = (string)a[1];
                numUnregistered = (int)a[2];
            }
        }

        public readonly struct LookupServiceResponse
        {
            public readonly StatusCode code;
            public readonly string statusMessage;
            public readonly string serviceUrl;

            public LookupServiceResponse(object[] a)
            {
                code = (StatusCode)a[0];
                statusMessage = (string)a[1];
                serviceUrl = (string)a[2];
            }
        }

        public readonly struct RegisterServiceResponse
        {
            public readonly StatusCode code;
            public readonly string statusMessage;

            public RegisterServiceResponse(object[] a)
            {
                code = (StatusCode)a[0];
                statusMessage = (string)a[1];
            }
        }

        public readonly struct UnregisterServiceResponse
        {
            public readonly StatusCode code;
            public readonly string statusMessage;
            public readonly int numUnregistered;

            public UnregisterServiceResponse(object[] a)
            {
                code = (StatusCode)a[0];
                statusMessage = (string)a[1];
                numUnregistered = (int)a[2];
            }
        }

        public readonly Uri MasterUri;
        public readonly Uri CallerUri;
        readonly string CallerId;

        public Master(Uri masterUri, string callerId, Uri callerUri)
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

        public RegisterServiceResponse RegisterService(string service, string rosRpcUri)
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

        public UnregisterServiceResponse UnregisterService(string service, string rosRpcUri)
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
}
