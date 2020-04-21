using System;

namespace Iviz.RoslibSharp
{
    class RpcNodeClient
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
            public readonly RpcStatusCode code;
            public readonly string statusMessage;
            public readonly ProtocolResponse protocol;

            public RequestTopicResponse(object[] a)
            {
                code = (RpcStatusCode)a[0];
                statusMessage = (string)a[1];
                protocol = new ProtocolResponse((object[])a[2]);
            }
        }

        readonly string CallerId;
        readonly Uri CallerUri;
        public Uri Uri { get; set; }

        public RpcNodeClient(string callerId, Uri callerUri)
        {
            CallerId = callerId;
            CallerUri = callerUri;
        }

        public RequestTopicResponse RequestTopic(string topic, string[][] protocols)
        {
            XmlRpc.Arg[] args = {
                new XmlRpc.Arg(CallerId),
                new XmlRpc.Arg(topic),
                new XmlRpc.Arg(protocols),
            };
            object response = XmlRpc.MethodCall(Uri, CallerUri, "requestTopic", args);
            return new RequestTopicResponse((object[])response);
        }

    }
}
