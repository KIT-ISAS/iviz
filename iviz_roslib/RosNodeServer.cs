﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib.Utils;
using Iviz.XmlRpc;

namespace Iviz.Roslib.XmlRpc
{
    internal sealed class RosNodeServer
    {
        static readonly XmlRpcArg DefaultOkResponse = OkResponse(0);

        readonly RosClient client;
        readonly Dictionary<string, Func<XmlRpcValue[], CancellationToken, Task>> lateCallbacks;
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

            lateCallbacks = new Dictionary<string, Func<XmlRpcValue[], CancellationToken, Task>>
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

        public Task DisposeAsync()
        {
            return DisposeAsync(false);
        }

        async Task DisposeAsync(bool sync)
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

            runningTs.Dispose();
        }

        public void Start()
        {
            task = TaskUtils.StartLongTask(Run);
        }

        public override string ToString()
        {
            return $"[RosNodeServer {Uri}]";
        }

        async Task Run()
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

        async Task StartContext(HttpListenerContext context, CancellationToken token)
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

        static (int code, string msg, XmlRpcArg arg) OkResponse(XmlRpcArg arg)
        {
            return (StatusCode.Success, "ok", arg);
        }

        static (int code, string msg, XmlRpcArg arg) ErrorResponse(string msg)
        {
            return (StatusCode.Error, msg, 0);
        }

        static XmlRpcArg GetBusStats(XmlRpcValue[] _)
        {
            Logger.Log("Was called: getBusStats");
            return ErrorResponse("Not implemented yet");
        }

        XmlRpcArg GetBusInfo(XmlRpcValue[] _)
        {
            var busInfo = client.GetBusInfoRcp();
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
                info.Transport,
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
                return (StatusCode.Failure, "No shutdown handler set", 0);
            }

            if (args.Length < 2 ||
                !args[0].TryGetString(out string callerId) ||
                !args[1].TryGetString(out string reason))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            client.ShutdownAction(callerId, reason, out _, out _);

            return DefaultOkResponse;
        }

        static XmlRpcArg GetPid(XmlRpcValue[] _)
        {
#if NET5_0
            int id = Environment.ProcessId;
#else
            int id = Process.GetCurrentProcess().Id;
#endif
            return OkResponse(id);
        }

        XmlRpcArg GetSubscriptions(XmlRpcValue[] _)
        {
            var subscriptions = client.GetSubscriptionsRcp();
            return OkResponse(new XmlRpcArg(subscriptions.Select(info => (info.Topic, info.Type))));
        }

        XmlRpcArg GetPublications(XmlRpcValue[] _)
        {
            var publications = client.GetPublicationsRcp();
            return OkResponse(new XmlRpcArg(publications.Select(info => (info.Topic, info.Type))));
        }

        XmlRpcArg ParamUpdate(XmlRpcValue[] args)
        {
            if (client.ParamUpdateAction == null)
            {
                return DefaultOkResponse;
            }

            if (args.Length < 3 ||
                !args[0].TryGetString(out string callerId) ||
                !args[1].TryGetString(out string parameterKey))
            {
                return ErrorResponse("Failed to parse arguments");
            }


            client.ParamUpdateAction(callerId, parameterKey, args[2], out _, out _);

            return DefaultOkResponse;
        }

        static XmlRpcArg PublisherUpdate(XmlRpcValue[] args)
        {
            // processing happens in PublisherUpdateLateCallback
            return DefaultOkResponse;
        }

        async Task PublisherUpdateLateCallback(XmlRpcValue[] args, CancellationToken token)
        {
            if (args.Length < 3 ||
                !args[1].TryGetString(out string topic) ||
                !args[2].TryGetArray(out XmlRpcValue[] publishers))
            {
                return;
            }

            List<Uri> publisherUris = new();
            foreach (XmlRpcValue publisherObj in publishers)
            {
                if (!publisherObj.TryGetString(out string publisherStr) ||
                    !Uri.TryCreate(publisherStr, UriKind.Absolute, out Uri? publisherUri) ||
                    publisherUri == null)
                {
                    Logger.LogFormat("{0}: Invalid uri '{1}'", this, publisherObj);
                    continue;
                }

                publisherUris.Add(publisherUri);
            }

            try
            {
                await client.PublisherUpdateRcpAsync(topic, publisherUris, token);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                Logger.LogErrorFormat("{0}: Error while updating the publisher list. {1}", this, e);
            }
        }

        XmlRpcArg RequestTopic(XmlRpcValue[] args)
        {
            if (args.Length < 3 ||
                !args[0].TryGetString(out string callerId) ||
                !args[1].TryGetString(out string topic) ||
                !args[2].TryGetArray(out XmlRpcValue[] protocols))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            if (protocols.Length == 0)
            {
                return (StatusCode.Failure, "No compatible protocols found", 0);
            }

            bool success = protocols.Any(entry =>
                entry.TryGetArray(out XmlRpcValue[] protocol) &&
                protocol.Length != 0 &&
                protocol[0].TryGetString(out string protocolName) &&
                protocolName == "TCPROS"
            );

            if (!success)
            {
                return (StatusCode.Failure, "Client only supports TCPROS", 0);
            }

            Endpoint? endpoint;
            try
            {
                endpoint = client.RequestTopicRpc(callerId, topic);
            }
            catch (Exception e)
            {
                Logger.LogErrorFormat("{0}: Error in RequestTopic: {1}", this, e);
                return (StatusCode.Error, $"Unknown error: {e.Message}", 0);
            }

            return endpoint?.Hostname == null
                ? (StatusCode.Error, "Internal error [duplicate request]", 0)
                : OkResponse(new XmlRpcArg[] {"TCPROS", endpoint.Value.Hostname, endpoint.Value.Port});
        }

        XmlRpcArg SystemMulticall(XmlRpcValue[] args)
        {
            if (args.Length != 1 ||
                !args[0].TryGetArray(out XmlRpcValue[] calls))
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
}