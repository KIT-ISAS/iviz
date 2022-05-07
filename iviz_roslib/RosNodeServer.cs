using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Roslib.Utils;
using Iviz.Tools;
using Iviz.XmlRpc;

namespace Iviz.Roslib.XmlRpc;

/// <summary>
/// Manages queries from other ROS nodes or the master through XMLRPC 
/// </summary>
internal sealed class RosNodeServer
{
    static readonly XmlRpcArg DefaultOkResponse = OkResponse(0);

    readonly RosClient client;
    readonly Dictionary<string, Func<XmlRpcValue[], CancellationToken, ValueTask>> lateCallbacks;
    readonly HttpListener listener;

    readonly Dictionary<string, Func<XmlRpcValue[], XmlRpcArg>> methods;
    readonly CancellationTokenSource runningTs = new();

    Task? task;
    bool disposed;

    public RosNodeServer(RosClient client)
    {
        this.client = client;

        listener = new HttpListener(client.CallerUri.Port);

        methods = new Dictionary<string, Func<XmlRpcValue[], XmlRpcArg>>
        {
            ["getBusStats"] = GetBusStats,
            ["getBusInfo"] = GetBusInfo,
            ["getMasterUri"] = GetMasterUri,
            ["shutdown"] = Shutdown,
            ["getSubscriptions"] = GetSubscriptions,
            ["getPublications"] = GetPublications,
            ["paramUpdate"] = ParamUpdate,
            ["publisherUpdate"] = PublisherUpdate,
            ["requestTopic"] = RequestTopic,
            ["getPid"] = GetPid,
            ["system.multicall"] = SystemMulticall,
        };

        lateCallbacks = new Dictionary<string, Func<XmlRpcValue[], CancellationToken, ValueTask>>
        {
            ["publisherUpdate"] = PublisherUpdateLateCallback
        };
    }

    public int ListenerPort => listener.LocalPort;
    Uri Uri => client.CallerUri;

    public void Dispose()
    {
        DisposeAsync(true).WaitNoThrow(this);
    }

    public ValueTask DisposeAsync()
    {
        return DisposeAsync(false);
    }

    async ValueTask DisposeAsync(bool sync)
    {
        if (disposed)
        {
            return;
        }

        disposed = true;

        runningTs.Cancel();
        if (sync)
        {
            listener.Dispose();
            task?.WaitNoThrow(2000, this);
        }
        else
        {
            await listener.DisposeAsync().AwaitNoThrow(this);
            await task.AwaitNoThrow(2000, this);
        }
    }

    public void Start()
    {
        task = TaskUtils.Run(async () => await Run().AwaitNoThrow(this));
    }

    public override string ToString()
    {
        return $"[RosNodeServer {Uri}]";
    }

    async ValueTask Run()
    {
        try
        {
            await listener.StartAsync(StartContext, true).AwaitNoThrow(this);
        }
        finally
        {
            await listener.AwaitRunningTasks();
            Logger.LogDebugFormat("{0}: Leaving task", this);
        }
    }

    async ValueTask StartContext(HttpListenerContext context, CancellationToken token)
    {
        using var linkedTs = CancellationTokenSource.CreateLinkedTokenSource(token, runningTs.Token);
        try
        {
            await XmlRpcService.MethodResponseAsync(context, methods, lateCallbacks, linkedTs.Token);
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception e)
        {
            Logger.LogErrorFormat(BaseUtils.GenericExceptionFormat, this, e);
        }
    }

    static (int code, string msg, XmlRpcArg arg) OkResponse(XmlRpcArg arg) => (StatusCode.Success, "ok", arg);

    static (int code, string msg, XmlRpcArg arg) ErrorResponse(string msg) => (StatusCode.Error, msg, 0);

    static (int code, string msg, XmlRpcArg arg) FailureResponse(string msg) => (StatusCode.Failure, msg, 0);

    static XmlRpcArg GetBusStats(XmlRpcValue[] _)
    {
        return ErrorResponse("Not implemented yet");
    }

    XmlRpcArg GetBusInfo(XmlRpcValue[] _)
    {
        var busInfo = client.GetBusInfoRpc();
        XmlRpcArg[][] response = busInfo.Select(BusInfoToArg).ToArray();
        return OkResponse(response);
    }

    static XmlRpcArg[] BusInfoToArg(BusInfo info)
    {
        return new XmlRpcArg[]
        {
            info.ConnectionId,
            info.DestinationId,
            info.Direction switch
            {
                BusInfo.DirectionType.In => "i",
                BusInfo.DirectionType.Out => "o",
                _ => ""
            },
            info.TransportType switch
            {
                TransportType.Tcp => RosUtils.ProtocolTcpRosName,
                TransportType.Udp => RosUtils.ProtocolUdpRosName,
                _ => ""
            },
            info.Topic,
            info.Connected ? 1 : 0
        };
    }

    XmlRpcArg GetMasterUri(XmlRpcValue[] _)
    {
        return OkResponse(client.MasterUri);
    }

    XmlRpcArg Shutdown(XmlRpcValue[] args)
    {
        if (client.ShutdownAction == null)
        {
            return FailureResponse("Shutdown failed. No callback set by the client.");
        }

        if (args.Length < 2 ||
            !args[0].TryGetString(out string callerId) ||
            !args[1].TryGetString(out string reason))
        {
            return ErrorResponse("Failed to parse arguments");
        }

        client.ShutdownAction(callerId, reason);

        return DefaultOkResponse;
    }

    static XmlRpcArg GetPid(XmlRpcValue[] _)
    {
        return OkResponse(ConnectionUtils.GetProcessId());
    }

    XmlRpcArg GetSubscriptions(XmlRpcValue[] _)
    {
        var subscriptions = client.GetSubscriptionsRpc();
        return OkResponse(new XmlRpcArg(subscriptions.Select(info => (info.Topic, info.Type)).ToArray()));
    }

    XmlRpcArg GetPublications(XmlRpcValue[] _)
    {
        var publications = client.GetPublicationsRpc();
        return OkResponse(new XmlRpcArg(publications.Select(info => (info.Topic, info.Type)).ToArray()));
    }

    XmlRpcArg ParamUpdate(XmlRpcValue[] args)
    {
        if (client.ParamUpdateAction == null)
        {
            return DefaultOkResponse;
        }

        if (args.Length < 3 ||
            !args[0].TryGetString(out string callerId) ||
            !args[1].TryGetString(out string parameterKey) ||
            args[2].IsEmpty)
        {
            return ErrorResponse("Failed to parse arguments");
        }

        client.ParamUpdateAction(callerId, parameterKey, args[2]);

        return DefaultOkResponse;
    }

    static XmlRpcArg PublisherUpdate(XmlRpcValue[] args)
    {
        // processing happens in PublisherUpdateLateCallback
        return DefaultOkResponse;
    }

    async ValueTask PublisherUpdateLateCallback(XmlRpcValue[] args, CancellationToken token)
    {
        if (args.Length < 3 ||
            !args[1].TryGetString(out string topic) ||
            !args[2].TryGetArray(out var publishers))
        {
            return;
        }

        var publisherUris = new List<Uri>();
        foreach (var publisherObj in publishers)
        {
            if (!publisherObj.TryGetString(out string publisherStr) ||
                !Uri.TryCreate(publisherStr, UriKind.Absolute, out Uri? publisherUri))
            {
                Logger.LogFormat("{0}: Invalid uri '{1}'", this, publisherObj);
                continue;
            }

            publisherUris.Add(publisherUri);
        }

        try
        {
            await client.PublisherUpdateRpcAsync(topic, publisherUris, token);
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            Logger.LogErrorFormat("{0}: Error while updating the publisher list. {1}", this, e);
        }
    }

    XmlRpcArg RequestTopic(XmlRpcValue[] args)
    {
        if (args.Length < 3 ||
            !args[0].TryGetString(out string callerId) ||
            !args[1].TryGetString(out string topic) ||
            !args[2].TryGetArray(out var protocols))
        {
            return ErrorResponse("Failed to parse arguments");
        }

        if (protocols.Length == 0)
        {
            return FailureResponse("No compatible protocols found");
        }

        bool requestsTcp;
        RpcUdpTopicRequest? requestsUdp;

        // just take whatever is in place 0
        // we assume that the subscriber decides the priority

        var protocol = protocols[0];
        if (!protocol.TryGetArray(out var entries))
        {
            return ErrorResponse("Expected array of arrays");
        }

        if (entries.Length == 0)
        {
            return ErrorResponse("Array is empty");
        }

        if (!entries[0].TryGetString(out var protocolType))
        {
            return ErrorResponse("Expected string as the first protocol entry");
        }

        if (protocolType == RosUtils.ProtocolTcpRosName)
        {
            requestsTcp = true;
            requestsUdp = null;
        }
        else if (protocolType == RosUtils.ProtocolUdpRosName)
        {
            if (entries.Length < 5
                || !entries[1].TryGetBase64(out byte[] rosHeader)
                || !entries[2].TryGetString(out string hostname)
                || !entries[3].TryGetInteger(out int port)
                || !entries[4].TryGetInteger(out int maxPacketSize))
            {
                return ErrorResponse("Failed to parse UDP entries");
            }

            if (port is < 0 or >= ushort.MaxValue)
            {
                return ErrorResponse($"Invalid port: {port.ToString()}");
            }

            if (maxPacketSize <= UdpRosParams.HeaderLength)
            {
                return ErrorResponse($"Invalid UDP max packet size: {maxPacketSize.ToString()}");
            }

            requestsTcp = false;
            requestsUdp = new RpcUdpTopicRequest(rosHeader, hostname, port, maxPacketSize);
        }
        else
        {
            return ErrorResponse($"Unknown transport protocol name '{protocolType}'");
        }

        try
        {
            var result = client.TryRequestTopicRpc(callerId, topic, requestsTcp, requestsUdp,
                out Endpoint? tcpResponse, out RpcUdpTopicResponse? udpResponse);

            return result switch
            {
                TopicRequestRpcResult.Success when tcpResponse is var (hostname, port) =>
                    OkResponse(new XmlRpcArg[] { RosUtils.ProtocolTcpRosName, hostname, port }),

                TopicRequestRpcResult.Success when
                    udpResponse is var (hostname, port, connectionId, maxPacketSize, header) =>
                    OkResponse(new XmlRpcArg[]
                        { RosUtils.ProtocolUdpRosName, hostname, port, connectionId, maxPacketSize, header }),

                TopicRequestRpcResult.Disposing =>
                    ErrorResponse("Internal error [client shutting down]"),

                TopicRequestRpcResult.NoSuchTopic =>
                    FailureResponse($"Client does not publish topic '{topic}'"),

                TopicRequestRpcResult.NoSuitableProtocol =>
                    FailureResponse("Publisher does not support any of the proposed protocols"),

                _ =>
                    ErrorResponse("Unknown error")
            };
        }
        catch (RosInvalidHeaderException e)
        {
            Logger.LogErrorFormat("{0}: Header error in RequestTopic: {1}", this, e);
            return ErrorResponse($"Error while parsing ROS header: {e.Message}");
        }
        catch (Exception e)
        {
            Logger.LogErrorFormat("{0}: Error in RequestTopic: {1}", this, e);
            return ErrorResponse($"Internal error: {e.Message}");
        }
    }

    XmlRpcArg SystemMulticall(XmlRpcValue[] args)
    {
        if (args.Length != 1 ||
            !args[0].TryGetArray(out var calls))
        {
            return ErrorResponse("Failed to parse arguments");
        }

        List<XmlRpcArg> responses = new(calls.Length);
        foreach (var callObject in calls)
        {
            if (!callObject.TryGetStruct(out var call))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            string? methodName = null;
            XmlRpcValue[]? arguments = null;
            foreach ((string key, XmlRpcValue value) in call)
            {
                switch (key)
                {
                    case "methodName":
                    {
                        if (!value.TryGetString(out string elementStr))
                        {
                            return ErrorResponse("Failed to parse method name");
                        }

                        methodName = elementStr;
                        break;
                    }
                    case "params":
                    {
                        if (!value.TryGetArray(out XmlRpcValue[] elementObjs) ||
                            elementObjs.Length == 0)
                        {
                            return ErrorResponse("Failed to parse arguments");
                        }

                        arguments = elementObjs;
                        break;
                    }
                    default:
                        return ErrorResponse("Failed to parse struct array");
                }
            }

            if (methodName == null || arguments == null)
            {
                return ErrorResponse("'methodname' or 'params' missing");
            }

            if (!methods.TryGetValue(methodName, out var method))
            {
                return ErrorResponse("Method not found");
            }

            XmlRpcArg response = method(arguments);
            responses.Add(response);

            if (lateCallbacks != null &&
                lateCallbacks.TryGetValue(methodName, out var lateCallback))
            {
                lateCallback(args, default);
            }
        }

        return responses.ToArray();
    }
}

public enum TopicRequestRpcResult
{
    Success,
    NoSuchTopic,
    Disposing,
    NoSuitableProtocol,
}