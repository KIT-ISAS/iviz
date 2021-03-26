using System;
using System.Threading;
using System.Threading.Tasks;
using Iviz.XmlRpc;

namespace Iviz.Roslib.XmlRpc
{
    public sealed class NodeClient
    {
        static readonly string[][] SupportedProtocols = {new[] {"TCPROS"}};

        public string CallerId { get; }
        public Uri CallerUri { get; }
        public int TimeoutInMs { get; }
        public Uri Uri { get; }

        public NodeClient(string callerId, Uri callerUri, Uri partnerUri, int timeoutInMs = 2000) =>
            (CallerId, CallerUri, Uri, TimeoutInMs) = (callerId, callerUri, partnerUri, timeoutInMs);

        public RequestTopicResponse RequestTopic(string topic)
        {
            XmlRpcArg[] args = {CallerId, topic, SupportedProtocols};
            var response = MethodCall("requestTopic", args);
            return new RequestTopicResponse(response);
        }

        public async ValueTask<RequestTopicResponse> RequestTopicAsync(string topic, CancellationToken token)
        {
            XmlRpcArg[] args = {CallerId, topic, SupportedProtocols};
            var response = await MethodCallAsync("requestTopic", args, token);
            return new RequestTopicResponse(response);
        }

        public GetMasterUriResponse GetMasterUri()
        {
            XmlRpcArg[] args = {CallerId};
            var response = MethodCall("getMasterUri", args);
            return new GetMasterUriResponse(response);
        }

        public async ValueTask<GetMasterUriResponse> GetMasterUriAsync(CancellationToken token = default)
        {
            XmlRpcArg[] args = {CallerId};
            var response = await MethodCallAsync("getMasterUri", args, token);
            return new GetMasterUriResponse(response);
        }

        public GetPidResponse GetPid()
        {
            XmlRpcArg[] args = {CallerId};
            var response = MethodCall("getPid", args);
            return new GetPidResponse(response);
        }

        public async ValueTask<GetPidResponse> GetPidAsync(CancellationToken token = default)
        {
            XmlRpcArg[] args = {CallerId};
            var response = await MethodCallAsync("getPid", args, token);
            return new GetPidResponse(response);
        }

        XmlRpcValue[] MethodCall(string function, XmlRpcArg[] args)
        {
            XmlRpcValue tmp = XmlRpcService.MethodCall(Uri, CallerUri, function, args);
            if (tmp.TryGetArray(out XmlRpcValue[] result))
            {
                return result;
            }

            throw new RosRpcException($"Error while calling '{function}' on '{Uri}': " +
                                      $"Expected type object[], got {tmp}");
        }

        async ValueTask<XmlRpcValue[]> MethodCallAsync(string function, XmlRpcArg[] args, CancellationToken token)
        {
            using var ts = CancellationTokenSource.CreateLinkedTokenSource(token);
            ts.CancelAfter(TimeoutInMs);

            XmlRpcValue tmp;
            try
            {
                tmp = await XmlRpcService.MethodCallAsync(Uri, CallerUri, function, args, ts.Token);
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
                throw new RosRpcException($"Error while calling '{function}' on '{Uri}': " +
                                          $"Expected type object[], got {tmp}");
            }

            return result;
        }


        public sealed class ProtocolResponse
        {
            public string Type { get; }
            public string Hostname { get; }
            public int Port { get; }

            public ProtocolResponse(string type, string hostname, int port) =>
                (Type, Hostname, Port) = (type, hostname, port);
        }

        public sealed class RequestTopicResponse : BaseResponse
        {
            public ProtocolResponse? Protocol { get; }

            public RequestTopicResponse(XmlRpcValue[] a)
            {
                if (a.Length != 3 ||
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

                if (!a[2].TryGetArray(out XmlRpcValue[] protocols))
                {
                    MarkError();
                    return;
                }

                if (protocols.Length == 0)
                {
                    Code = StatusCode.Error;
                    return;
                }

                if (protocols[0].TryGetString(out string tmpType))
                {
                    if (protocols.Length < 3 ||
                        !protocols[1].TryGetString(out string hostname) ||
                        !protocols[2].TryGetInteger(out int port))
                    {
                        MarkError();
                        return;
                    }

                    Protocol = new ProtocolResponse(tmpType, hostname, port);
                }
                else if (protocols[0].TryGetArray(out XmlRpcValue[] innerProtocols))
                {
                    if (innerProtocols.Length < 3 ||
                        !innerProtocols[0].TryGetString(out string type) ||
                        !innerProtocols[1].TryGetString(out string hostname) ||
                        !innerProtocols[2].TryGetInteger(out int port))
                    {
                        MarkError();
                        return;
                    }

                    Protocol = new ProtocolResponse(type, hostname, port);
                }
                else
                {
                    MarkError();
                }
            }
        }

        public sealed class GetMasterUriResponse : BaseResponse
        {
            public Uri? Uri { get; }

            public GetMasterUriResponse(XmlRpcValue[] a)
            {
                if (a.Length != 3 ||
                    !a[0].TryGetInteger(out int code) ||
                    !a[1].TryGetString(out string statusMessage) ||
                    !a[2].TryGetString(out string uriStr) ||
                    !Uri.TryCreate(uriStr, UriKind.Absolute, out Uri? uri))
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

                Uri = uri;
            }
        }

        public sealed class GetPidResponse : BaseResponse
        {
            public int Pid { get; }

            public GetPidResponse(XmlRpcValue[] a)
            {
                if (a.Length != 3 ||
                    !a[0].TryGetInteger(out int code) ||
                    !a[1].TryGetString(out string statusMessage) ||
                    !a[2].TryGetInteger(out int pid))
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

                Pid = pid;
            }
        }
    }
}