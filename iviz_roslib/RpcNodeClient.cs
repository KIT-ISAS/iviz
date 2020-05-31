using System;

namespace Iviz.RoslibSharp.XmlRpc
{
    class NodeClient
    {
        public class ProtocolResponse
        {
            public readonly string type;
            public readonly string hostname;
            public readonly int port;

            public ProtocolResponse(object[] a)
            {
                if (a.Length == 0)
                {
                    type = null;
                    hostname = null;
                    port = -1;
                }
                else
                {
                    if (a[0] is object[])
                    {
                        a = (object[])a[0];
                    }
                    type = (string)a[0];
                    hostname = (string)a[1];
                    port = (int)a[2];
                }
            }
        }


        public class RequestTopicResponse
        {
            public readonly StatusCode code;
            public readonly string statusMessage;
            public readonly ProtocolResponse protocol;

            public RequestTopicResponse(object[] a)
            {
                code = (StatusCode)a[0];
                statusMessage = (string)a[1];
                protocol = new ProtocolResponse((object[])a[2]);
            }
        }

        public class GetMasterUriResponse
        {
            public readonly StatusCode code;
            public readonly string statusMessage;
            public readonly Uri uri;

            public GetMasterUriResponse(object[] a)
            {
                code = (StatusCode)a[0];
                statusMessage = (string)a[1];
                uri = new Uri((string)a[2]);
            }
        }

        readonly string CallerId;
        readonly Uri CallerUri;
        readonly int timeoutInMs;
        public Uri Uri { get; }

        public NodeClient(string callerId, Uri callerUri, Uri otherUri, int timeoutInMs = 2000)
        {
            CallerId = callerId;
            CallerUri = callerUri;
            Uri = otherUri;
            this.timeoutInMs = timeoutInMs;
        }

        public RequestTopicResponse RequestTopic(string topic, string[][] protocols)
        {
            Arg[] args = {
                new Arg(CallerId),
                new Arg(topic),
                new Arg(protocols),
            };
            object response = Service.MethodCall(Uri, CallerUri, "requestTopic", args, timeoutInMs);
            return new RequestTopicResponse((object[])response);
        }

        public GetMasterUriResponse GetMasterUri()
        {
            Arg[] args = {
                new Arg(CallerId)
            };
            object response = Service.MethodCall(Uri, CallerUri, "getMasterUri", args, timeoutInMs);
            return new GetMasterUriResponse((object[])response);
        }
    }
}
