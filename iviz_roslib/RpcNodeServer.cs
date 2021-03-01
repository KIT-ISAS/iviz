using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;

namespace Iviz.Roslib.XmlRpc
{
    internal sealed class NodeServer
    {
        static readonly Arg[] DefaultOkResponse = OkResponse(0);

        readonly RosClient client;
        readonly Dictionary<string, Func<object[], CancellationToken, Task>> lateCallbacks;
        readonly HttpListener listener;

        readonly Dictionary<string, Func<object[], Arg[]>> methods;
        readonly CancellationTokenSource runningTs = new();

        Task? task;
        bool disposed;

        public NodeServer(RosClient client)
        {
            this.client = client;

            listener = new HttpListener(client.CallerUri.Port);

            methods = new Dictionary<string, Func<object[], Arg[]>>
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

            lateCallbacks = new Dictionary<string, Func<object[], CancellationToken, Task>>
            {
                ["publisherUpdate"] = PublisherUpdateLateCallback
            };
        }

        public int ListenerPort => listener.LocalPort;
        Uri Uri => client.CallerUri;

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;

            runningTs.Cancel();
            listener.Dispose();
            task?.WaitForWithTimeout(2000).WaitNoThrow(this);
            runningTs.Dispose();
        }

        public async Task DisposeAsync()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;

            runningTs.Cancel();
            listener.Dispose();
            await task.WaitForWithTimeout(2000).AwaitNoThrow(this);
            runningTs.Dispose();
        }

        public void Start()
        {
            task = Task.Run(Run);
        }

        public override string ToString()
        {
            return $"[NodeServer {Uri}]";
        }

        async Task Run()
        {
            try
            {
                await listener.StartAsync(StartContext, true).AwaitNoThrow(this);
            }
            finally
            {
                await listener.AwaitRunningTasks().Caf();
                Logger.LogDebugFormat("{0}: Leaving thread", this);
            }
        }

        async Task StartContext(HttpListenerContext context, CancellationToken token)
        {
            using CancellationTokenSource linkedTs =
                CancellationTokenSource.CreateLinkedTokenSource(token, runningTs.Token);
            try
            {
                await XmlRpcService.MethodResponseAsync(context, methods, lateCallbacks, linkedTs.Token).Caf();
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Logger.LogErrorFormat(Utils.GenericExceptionFormat, this, e);
            }
        }

        static Arg[] OkResponse(Arg arg)
        {
            return new Arg[] {StatusCode.Success, "ok", arg};
        }
        
        static Arg[] ErrorResponse(string msg)
        {
            return new Arg[] {StatusCode.Error, msg, 0};
        }        

        static Arg[] GetBusStats(object[] _)
        {
            Logger.Log("Was called: getBusStats");
            return ErrorResponse("Not implemented yet");
        }

        Arg[] GetBusInfo(object[] _)
        {
            var busInfo = client.GetBusInfoRcp();
            Arg[][] response = busInfo.Select(BusInfoToArg).ToArray();
            return OkResponse(response);
        }

        static Arg[] BusInfoToArg(BusInfo info)
        {
            return new Arg[]
            {
                info.ConnectionId,
                info.DestinationId,
                info.Direction switch
                {
                    BusInfo.DirectionType.In => 1,
                    BusInfo.DirectionType.Out => 0,
                    _ => -1
                },
                info.Transport,
                info.Topic,
                info.Connected ? 1 : 0
            };
        }

        Arg[] GetMasterUri(object[] _)
        {
            return OkResponse(client.MasterUri);
        }

        Arg[] Shutdown(object[] args)
        {
            if (client.ShutdownAction == null)
            {
                return new Arg[] {StatusCode.Failure, "No shutdown handler set", 0};
            }

            string callerId = (string) args[0];
            string reason = args.Length > 1 ? (string) args[1] : "";
            client.ShutdownAction(callerId, reason, out _, out _);

            return DefaultOkResponse;
        }

        static Arg[] GetPid(object[] _)
        {
#if NET5_0
            int id = Environment.ProcessId;
#else
            int id = Process.GetCurrentProcess().Id;
#endif
            return OkResponse(id);
        }

        Arg[] GetSubscriptions(object[] _)
        {
            var subscriptions = client.GetSubscriptionsRcp();
            return OkResponse(new Arg(subscriptions.Select(info => (info.Topic, info.Type))));
        }

        Arg[] GetPublications(object[] _)
        {
            var publications = client.GetPublicationsRcp();
            return OkResponse(new Arg(publications.Select(info => (info.Topic, info.Type))));
        }

        Arg[] ParamUpdate(object[] args)
        {
            if (client.ParamUpdateAction == null)
            {
                return DefaultOkResponse;
            }

            string callerId = (string) args[0];
            string parameterKey = (string) args[1];
            object parameterValue = args[2];
            client.ParamUpdateAction(callerId, parameterKey, parameterValue, out _, out _);

            return DefaultOkResponse;
        }

        static Arg[] PublisherUpdate(object[] args)
        {
            // processing happens in PublisherUpdateLateCallback
            return DefaultOkResponse;
        }

        async Task PublisherUpdateLateCallback(object[] args, CancellationToken token)
        {
            if (args.Length < 3 ||
                !(args[1] is string topic) ||
                !(args[2] is object[] publishers))
            {
                return;
            }

            List<Uri> publisherUris = new();
            foreach (object publisherObj in publishers)
            {
                if (!(publisherObj is string publisherStr) ||
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
                await client.PublisherUpdateRcpAsync(topic, publisherUris, token).Caf();
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

        Arg[] RequestTopic(object[] args)
        {
            if (args.Length < 3 ||
                !(args[0] is string callerId) ||
                !(args[1] is string topic) ||
                !(args[2] is object[] protocols))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            if (protocols.Length == 0)
            {
                return new Arg[] {StatusCode.Failure, "No compatible protocols found", 0};
            }

            bool success = protocols.Any(entry =>
                entry is object[] protocol &&
                protocol.Length != 0 &&
                protocol[0] is string protocolName &&
                protocolName == "TCPROS"
            );

            if (!success)
            {
                return new Arg[] {StatusCode.Failure, "Client only supports TCPROS", 0};
            }

            Endpoint? endpoint;
            try
            {
                endpoint = client.RequestTopicRpc(callerId, topic);
            }
            catch (Exception e)
            {
                Logger.LogErrorFormat("{0}: Error in RequestTopic: {1}", this, e);
                return new Arg[] {StatusCode.Error, $"Unknown error: {e.Message}", 0};
            }

            return endpoint?.Hostname == null
                ? new Arg[] {StatusCode.Error, "Internal error [duplicate request]", 0}
                : OkResponse(new Arg[] {"TCPROS", endpoint.Value.Hostname, endpoint.Value.Port});
        }
        
        Arg[] SystemMulticall(object[] args)
        {
            if (args.Length != 1 ||
                !(args[0] is object[] calls))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            List<Arg> responses = new(calls.Length);
            foreach (var callObject in calls)
            {
                if (!(callObject is List<(string ElementName, object Element)> call))
                {
                    return ErrorResponse("Failed to parse arguments");
                }

                string? methodName = null;
                object[]? arguments = null;
                foreach ((string elementName, object element) in call)
                {
                    switch (elementName)
                    {
                        case "methodName":
                        {
                            if (!(element is string elementStr))
                            {
                                return ErrorResponse("Failed to parse method name");
                            }

                            methodName = elementStr;
                            break;
                        }
                        case "params":
                        {
                            if (!(element is object[] elementObjs) ||
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
                    
                Arg response = (Arg) method(arguments);
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