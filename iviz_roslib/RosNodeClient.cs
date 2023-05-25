﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Roslib.Utils;
using Iviz.Tools;
using Iviz.XmlRpc;

namespace Iviz.Roslib.XmlRpc;

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

    internal async ValueTask<RequestTopicResponse> RequestTopicAsync(string topic,
        RosTransportHint transportHint, RpcUdpTopicRequest? udpTopicRequest,
        CancellationToken token)
    {
        var args = CreateRequestTopicArgs(CallerId, topic, transportHint, udpTopicRequest);
        var response = await MethodCallAsync("requestTopic", args, token);
        return new RequestTopicResponse(response);
    }

    static XmlRpcArg[] CreateRequestTopicArgs(string callerId, string topic, RosTransportHint transportHint,
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

        return new XmlRpcArg[] { callerId, topic, supportedProtocols };
    }

    public async ValueTask<GetMasterUriResponse> GetMasterUriAsync(CancellationToken token = default)
    {
        XmlRpcArg[] args = { CallerId };
        var response = await MethodCallAsync("getMasterUri", args, token);
        return new GetMasterUriResponse(response);
    }
    
    public async ValueTask<GetPidResponse> GetPidAsync(CancellationToken token = default)
    {
        XmlRpcArg[] args = { CallerId };
        var response = await MethodCallAsync("getPid", args, token);
        return new GetPidResponse(response);
    }
    
    async ValueTask<RosValue[]> MethodCallAsync(string function, XmlRpcArg[] args, CancellationToken token)
    {
        using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
        tokenSource.CancelAfter(TimeoutInMs);

        RosValue wrapper;
        try
        {
            wrapper = await XmlRpcService.MethodCallAsync(Uri, CallerUri, function, args, tokenSource.Token);
        }
        catch (OperationCanceledException)
        {
            if (!token.IsCancellationRequested)
            {
                throw new TimeoutException($"Call to '{function}' timed out");
            }

            throw;
        }

        if (wrapper.TryGetArray(out RosValue[] response))
        {
            return response;
        }

        throw new RosRpcException($"Error while calling '{function}' on '{Uri}': " +
                                  $"Expected type object[], got {wrapper}");
    }

    public override string ToString() => $"[{nameof(RosNodeClient)} {Uri}]";
}

public sealed class RequestTopicResponse : BaseResponse
{
    public Endpoint? TcpResponse { get; }
    public RpcUdpTopicResponse? UdpResponse { get; }

    internal RequestTopicResponse(RosValue[] a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }

        if (!value.TryGetArray(out RosValue[] protocolInfo))
        {
            MarkError();
            return;
        }

        if (protocolInfo.Length == 0)
        {
            Logger.LogDebug($"[{nameof(RequestTopicResponse)}]: Request for topic yielded no valid protocols");
            responseCode = StatusCode.Error;
            return;
        }

        if (!protocolInfo[0].TryGet(out string type))
        {
            MarkError();
            return;
        }

        switch (type)
        {
            case RosUtils.ProtocolTcpRosName:
                if (protocolInfo.Length < 3
                    || !protocolInfo[1].TryGet(out string hostname)
                    || !protocolInfo[2].TryGet(out int port))
                {
                    MarkError();
                    return;
                }

                TcpResponse = new Endpoint(hostname, port);
                break;
            case RosUtils.ProtocolUdpRosName:
                if (protocolInfo.Length < 6
                    || !protocolInfo[1].TryGet(out hostname)
                    || !protocolInfo[2].TryGet(out port)
                    || !protocolInfo[3].TryGet(out int connectionId)
                    || !protocolInfo[4].TryGet(out int maxPacketSize)
                    || !protocolInfo[5].TryGet(out byte[] header))
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

    internal GetMasterUriResponse(RosValue[] a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }

        if (!value.TryGet(out string uriStr)
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

    internal GetPidResponse(RosValue[] a)
    {
        if (!TryGetValueFromArgs(a, out var value))
        {
            return;
        }

        if (!value.TryGet(out int pid))
        {
            MarkError();
            return;
        }

        Pid = pid;
    }
}