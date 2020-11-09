using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;

namespace Iviz.Roslib.XmlRpc
{
    internal sealed class NodeClient
    {
        static readonly string[][] SupportedProtocols = {new[] {"TCPROS"}};

        public string CallerId { get; }
        public Uri CallerUri { get; }
        public int TimeoutInMs { get; }
        public Uri Uri { get; }

        public NodeClient(string callerId, Uri callerUri, Uri otherUri, int timeoutInMs = 2000)
        {
            CallerId = callerId;
            CallerUri = callerUri;
            Uri = otherUri;
            TimeoutInMs = timeoutInMs;
        }

        public RequestTopicResponse RequestTopic(string topic)
        {
            Arg[] args = {CallerId, topic, SupportedProtocols};
            object[]? response = MethodCall("requestTopic", args);
            return new RequestTopicResponse(response);
        }

        public async Task<RequestTopicResponse> RequestTopicAsync(string topic)
        {
            Arg[] args = {CallerId, topic, SupportedProtocols};
            object[]? response = await MethodCallAsync("requestTopic", args).Caf();
            return new RequestTopicResponse(response);
        }

        public GetMasterUriResponse GetMasterUri()
        {
            Arg[] args = {CallerId};
            object[]? response = MethodCall("getMasterUri", args);
            return new GetMasterUriResponse(response);
        }

        public async Task<GetMasterUriResponse> GetMasterUriAsync()
        {
            Arg[] args = {CallerId};
            object[]? response = await MethodCallAsync("getMasterUri", args).Caf();
            return new GetMasterUriResponse(response);
        }

        public GetPidResponse GetPid()
        {
            Arg[] args = {CallerId};
            object[]? response = MethodCall("getPid", args);
            return new GetPidResponse(response);
        }

        public async Task<GetPidResponse> GetPidAsync()
        {
            Arg[] args = {CallerId};
            object[]? response = await MethodCallAsync("getPid", args).Caf();
            return new GetPidResponse(response);
        }

        object[]? MethodCall(string function, IEnumerable<Arg> args)
        {
            object tmp = XmlRpcService.MethodCall(Uri, CallerUri, function, args, TimeoutInMs);
            if (tmp is object[] result)
            {
                return result;
            }

            Logger.LogFormat("Rpc Response: Expected type object[], got {0}", tmp?.GetType().Name);
            return null;
        }

        async Task<object[]?> MethodCallAsync(string function, IEnumerable<Arg> args)
        {
            object tmp = await XmlRpcService.MethodCallAsync(Uri, CallerUri, function, args, TimeoutInMs).Caf();
            if (tmp is object[] result)
            {
                return result;
            }

            Logger.LogFormat("Rpc Response: Expected type object[], got {0}", tmp?.GetType().Name);
            return null;
        }


        public class ProtocolResponse
        {
            public string Type { get; }
            public string Hostname { get; }
            public int Port { get; }

            public ProtocolResponse(string type, string hostname, int port)
            {
                Type = type;
                Hostname = hostname;
                Port = port;
            }
        }

        public class RequestTopicResponse : BaseResponse
        {
            public ProtocolResponse? Protocol { get; }

            public RequestTopicResponse(object[]? a)
            {
                if (a is null ||
                    a.Length != 3 ||
                    !(a[0] is int code) ||
                    !(a[1] is string statusMessage) ||
                    !(a[2] is object[] protocols))
                {
                    Logger.LogFormat("{0}: Parse error in {1}", this, MethodBase.GetCurrentMethod()?.Name);                    
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

                if (protocols.Length == 0)
                {
                    Code = StatusCode.Error;
                    return;
                }

                switch (protocols[0])
                {
                    case string tmpType:
                    {
                        if (protocols.Length < 3 ||
                            !(protocols[1] is string hostname) ||
                            !(protocols[2] is int port))
                        {
                            Logger.LogFormat("{0}: Parse error in {1}", this, MethodBase.GetCurrentMethod()?.Name);                    
                            Code = StatusCode.Error;
                            hasParseError = true;
                            return;
                        }

                        Protocol = new ProtocolResponse(tmpType, hostname, port);
                        break;
                    }
                    case object[] innerProtocols:
                    {
                        if (innerProtocols.Length < 3 ||
                            !(innerProtocols[0] is string type) ||
                            !(innerProtocols[1] is string hostname) ||
                            !(innerProtocols[2] is int port))
                        {
                            Logger.LogFormat("{0}: Parse error in {1}", this, MethodBase.GetCurrentMethod()?.Name);                    
                            Code = StatusCode.Error;
                            hasParseError = true;
                            return;
                        }

                        Protocol = new ProtocolResponse(type, hostname, port);
                        break;
                    }
                    default:
                        Code = StatusCode.Error;
                        hasParseError = true;
                        return;
                }
            }
        }

        public class GetMasterUriResponse : BaseResponse
        {
            public Uri? Uri { get; }

            public GetMasterUriResponse(object[]? a)
            {
                if (a is null ||
                    a.Length != 3 ||
                    !(a[0] is int code) ||
                    !(a[1] is string statusMessage) ||
                    !(a[2] is string uriStr) ||
                    !Uri.TryCreate(uriStr, UriKind.Absolute, out Uri? uri))
                {
                    Logger.LogFormat("{0}: Parse error in {1}", this, MethodBase.GetCurrentMethod()?.Name);
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

                Uri = uri;
            }
        }

        public class GetPidResponse : BaseResponse
        {
            public int Pid { get; }

            public GetPidResponse(object[]? a)
            {
                if (a is null ||
                    a.Length != 3 ||
                    !(a[0] is int code) ||
                    !(a[1] is string statusMessage) ||
                    !(a[2] is int pid))
                {
                    Logger.LogFormat("{0}: Parse error in {1}", this, MethodBase.GetCurrentMethod()?.Name);                    
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
                
                Pid = pid;
            }
        }
    }
}