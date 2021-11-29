using System;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Roslib.Utils;
using Iviz.Tools;
using Iviz.XmlRpc;

namespace Iviz.Roslib.XmlRpc
{
    /// <summary>
    /// Manages queries to other ROS nodes through XMLRPC 
    /// </summary>
    public sealed class RosNodeClient
    {
        static readonly string[] TcpProtocolEntry = { RosUtils.ProtocolTcpRosName };
        static readonly XmlRpcArg[] SupportedProtocolsOnlyTcp = { TcpProtocolEntry };

        public string CallerId { get; }
        public Uri CallerUri { get; }
        public int TimeoutInMs { get; }
        public Uri Uri { get; }

        public RosNodeClient(string callerId, Uri callerUri, Uri partnerUri, int timeoutInMs = 2000) =>
            (CallerId, CallerUri, Uri, TimeoutInMs) = (callerId, callerUri, partnerUri, timeoutInMs);

        public RequestTopicResponse RequestTopic(string topic, RosTransportHint transportHint,
            RpcUdpTopicRequest? udpTopicRequest)
        {
            var args = CreateRequestTopicArgs(topic, transportHint, udpTopicRequest);
            var response = MethodCall("requestTopic", args);
            return new RequestTopicResponse(response);
        }

        public async ValueTask<RequestTopicResponse> RequestTopicAsync(string topic,
            RosTransportHint transportHint, RpcUdpTopicRequest? udpTopicRequest,
            CancellationToken token)
        {
            var args = CreateRequestTopicArgs(topic, transportHint, udpTopicRequest);
            var response = await MethodCallAsync("requestTopic", args, token);
            return new RequestTopicResponse(response);
        }

        XmlRpcArg[] CreateRequestTopicArgs(string topic, RosTransportHint transportHint,
            RpcUdpTopicRequest? udpTopicRequest)
        {
            if (transportHint != RosTransportHint.OnlyTcp && udpTopicRequest == null)
            {
                throw new ArgumentException("Udp hint selected but not requested");
            }

            XmlRpcArg[] udpProtocolEntry;
            if (udpTopicRequest != null)
            {
                var (header, hostname, remotePort, maxPacketSize) = udpTopicRequest;
                udpProtocolEntry = new XmlRpcArg[]
                    { RosUtils.ProtocolUdpRosName, header, hostname, remotePort, maxPacketSize };
            }
            else
            {
                udpProtocolEntry = Array.Empty<XmlRpcArg>();
            }

            var supportedProtocols = transportHint switch
            {
                RosTransportHint.OnlyTcp => SupportedProtocolsOnlyTcp,
                RosTransportHint.OnlyUdp => new XmlRpcArg[] { udpProtocolEntry },
                RosTransportHint.PreferTcp => new XmlRpcArg[] { TcpProtocolEntry, udpProtocolEntry },
                RosTransportHint.PreferUdp => new XmlRpcArg[] { udpProtocolEntry, TcpProtocolEntry },
                _ => throw new ArgumentOutOfRangeException(nameof(transportHint), transportHint, null)
            };

            return new XmlRpcArg[] { CallerId, topic, supportedProtocols };
        }

        public GetMasterUriResponse GetMasterUri()
        {
            XmlRpcArg[] args = { CallerId };
            var response = MethodCall("getMasterUri", args);
            return new GetMasterUriResponse(response);
        }

        public async ValueTask<GetMasterUriResponse> GetMasterUriAsync(CancellationToken token = default)
        {
            XmlRpcArg[] args = { CallerId };
            var response = await MethodCallAsync("getMasterUri", args, token);
            return new GetMasterUriResponse(response);
        }

        public GetPidResponse GetPid()
        {
            XmlRpcArg[] args = { CallerId };
            var response = MethodCall("getPid", args);
            return new GetPidResponse(response);
        }

        public async ValueTask<GetPidResponse> GetPidAsync(CancellationToken token = default)
        {
            XmlRpcArg[] args = { CallerId };
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

        public override string ToString()
        {
            return $"[RosNodeClient {Uri}]";
        }
    }

    public sealed class RequestTopicResponse : BaseResponse
    {
        public Endpoint? TcpResponse { get; }
        public RpcUdpTopicResponse? UdpResponse { get; }

        internal RequestTopicResponse(XmlRpcValue[] a)
        {
            if (!TryGetValueFromArgs(a, out var value))
            {
                return;
            }
            
            if (!value.TryGetArray(out XmlRpcValue[] protocolInfo))
            {
                MarkError();
                return;
            }

            if (protocolInfo.Length == 0)
            {
                Logger.LogDebugFormat("[{0}]: Request for topic yielded no valid protocols", GetType().Name);
                responseCode = StatusCode.Error;
                return;
            }

            if (!protocolInfo[0].TryGetString(out string type))
            {
                MarkError();
                return;
            }

            switch (type)
            {
                case RosUtils.ProtocolTcpRosName:
                    if (protocolInfo.Length < 3
                        || !protocolInfo[1].TryGetString(out string hostname)
                        || !protocolInfo[2].TryGetInteger(out int port))
                    {
                        MarkError();
                        return;
                    }

                    TcpResponse = new Endpoint(hostname, port);
                    break;
                case RosUtils.ProtocolUdpRosName:
                    if (protocolInfo.Length < 6
                        || !protocolInfo[1].TryGetString(out hostname)
                        || !protocolInfo[2].TryGetInteger(out port)
                        || !protocolInfo[3].TryGetInteger(out int connectionId)
                        || !protocolInfo[4].TryGetInteger(out int maxPacketSize)
                        || !protocolInfo[5].TryGetBase64(out byte[] header))
                    {
                        MarkError();
                        return;
                    }

                    UdpResponse = new RpcUdpTopicResponse(hostname, port, connectionId, maxPacketSize, header);
                    break;
            }
        }
    }

    public sealed class GetMasterUriResponse : BaseResponse
    {
        public Uri? Uri { get; }

        internal GetMasterUriResponse(XmlRpcValue[] a)
        {
            if (!TryGetValueFromArgs(a, out var value))
            {
                return;
            }
            
            if (!value.TryGetString(out string uriStr) 
                || !Uri.TryCreate(uriStr, UriKind.Absolute, out Uri? uri))
            {
                MarkError();
                return;
            }

            Uri = responseCode == StatusCode.Error ? null : uri;
        }
    }

    public sealed class GetPidResponse : BaseResponse
    {
        public int Pid { get; }

        internal GetPidResponse(XmlRpcValue[] a)
        {
            if (!TryGetValueFromArgs(a, out var value))
            {
                return;
            }
            
            if (!value.TryGetInteger(out int pid))
            {
                MarkError();
                return;
            }

            Pid = pid;
        }
    }
}