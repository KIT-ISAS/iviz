using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;
using HttpListenerContext = Iviz.XmlRpc.HttpListenerContext;

namespace Iviz.Roslib.XmlRpc
{
    internal sealed class NodeServer : IDisposable
    {
        readonly Dictionary<string, Func<object[], Arg[]>> methods;
        readonly Iviz.XmlRpc.HttpListener listener;
        readonly RosClient client;

        readonly SemaphoreSlim signal = new SemaphoreSlim(0, 1);
        Task task;

        public Uri ListenerUri => listener.LocalEndpoint;
        public Uri Uri => client.CallerUri;

        public NodeServer(RosClient client)
        {
            this.client = client;

            listener = new Iviz.XmlRpc.HttpListener(client.CallerUri);

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
            };
        }

        public void Start()
        {
            task = Task.Run(async () => await Run());
        }

        public override string ToString()
        {
            return "[RcpNodeServer]";
        }        
        
        async Task Run()
        {
            Logger.LogDebug($"{this}: Starting!");
            
            Task listenerTask = listener.Start(StartContext);

            await signal.WaitAsync();

            // tell the listener in every possible way to stop listening
            listener.Dispose();
            
            // and that is usually not enough. so we bail out
            if (!listenerTask.Wait(2000))
            {
                Logger.LogDebug($"{this}: Listener stuck. Abandoning.");
            }

            Logger.LogDebug($"{this}: Leaving thread");
        }

        async Task StartContext(HttpListenerContext context)
        {
            using (context)
            {
                try
                {
                    await Service.MethodResponseAsync(context, methods);
                }
                catch (Exception e)
                {
                    Logger.LogError(e);
                }
            }
        }

        bool disposed;
        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            try
            {
                signal.Release();
            }
            catch (SemaphoreFullException)
            {
            }

            task?.Wait();
        }

        Arg[] GetBusStats(object[] _)
        {
            Logger.Log("Was called: getBusStats");
            return new Arg[]
            {
                StatusCode.Error,
                "error=NYI",
                Array.Empty<Arg>()
            };
        }

        Arg[] GetBusInfo(object[] _)
        {
            var busInfo = client.GetBusInfoRcp();
            Arg[][] response = busInfo.Select(
                x => new Arg[]
                {
                    x.ConnectionId,
                    x.DestinationId,
                    x.Direction,
                    x.Transport,
                    x.Topic,
                    x.Connected,
                }).ToArray();

            return new Arg[]
            {
                StatusCode.Success,
                "ok",
                response
            };
        }

        Arg[] GetMasterUri(object[] _)
        {
            return new Arg[] {StatusCode.Success, "ok", client.MasterUri};
        }

        Arg[] Shutdown(object[] args)
        {
            if (client.ShutdownAction == null)
            {
                return new Arg[] {StatusCode.Success, "error=no shutdown handler set", 0};
            }

            string callerId = (string) args[0];
            string reason = args.Length > 1 ? (string) args[1] : "";
            client.ShutdownAction(callerId, reason, out int status, out string response);
            
            return new Arg[] {status, response, 0};
        }

        static Arg[] GetPid(object[] _)
        {
            int id = Process.GetCurrentProcess().Id;

            return new Arg[]
            {
                StatusCode.Success,
                "ok",
                id
            };
        }

        Arg[] GetSubscriptions(object[] _)
        {
            var subscriptions = client.GetSubscriptionsRcp();
            return new Arg[]
            {
                StatusCode.Success,
                "",
                new Arg(subscriptions.Select(info => (info.Topic, info.Type)))
            };
        }

        Arg[] GetPublications(object[] _)
        {
            var publications = client.GetPublicationsRcp();
            return new Arg[]
            {
                StatusCode.Success, "ok", new Arg(publications.Select(info => (info.Topic, info.Type)))
            };
        }

        Arg[] ParamUpdate(object[] args)
        {
            if (client.ParamUpdateAction == null)
            {
                return new Arg[] {StatusCode.Success, "ok", 0};
            }

            string callerId = (string) args[0];
            string parameterKey = (string) args[1];
            object parameterValue = args[2];
            client.ParamUpdateAction(callerId, parameterKey, parameterValue, out int status, out string response);
            
            return new Arg[] {status, response, 0};
        }

        Arg[] PublisherUpdate(object[] args)
        {
            if (args.Length < 3 ||
                !(args[0] is string callerId) ||
                !(args[1] is string topic) ||
                !(args[2] is object[] publishers))
            {
                return new Arg[] {StatusCode.Error, "error=failed to parse arguments", 0};
            }

            Uri[] publisherUris = new Uri[publishers.Length];
            for (int i = 0; i < publishers.Length; i++)
            {
                if (!Uri.TryCreate((string) publishers[i], UriKind.Absolute, out publisherUris[i]))
                {
                    Logger.Log($"RcpNodeServer: Invalid uri '{publishers[i]}'");
                }
            }

            try
            {
                client.PublisherUpdateRcp(topic, publisherUris);
                return new Arg[] {StatusCode.Success, "ok", 0};
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new Arg[] {StatusCode.Failure, "error=Unknown error: " + e.Message, 0};
            }
        }

        Arg[] RequestTopic(object[] args)
        {
            if (args.Length < 3 ||
                !(args[0] is string callerId) ||
                !(args[1] is string topic) ||
                !(args[2] is object[] protocols))
            {
                return new Arg[] {StatusCode.Error, "error=failed to parse arguments", 0};
            }

            if (protocols.Length == 0)
            {
                return new Arg[]
                {
                    StatusCode.Failure,
                    $"error=no compatible protocols found",
                    Array.Empty<string[]>()
                };
            }

            bool success = protocols.Any(entry =>
                entry is object[] protocol &&
                protocol.Length != 0 &&
                protocol[0] is string protocolName &&
                protocolName == "TCPROS"
            );

            if (!success)
            {
                return new Arg[]
                {
                    StatusCode.Failure,
                    "error=client only supports TCPROS",
                    Array.Empty<string[]>()
                };
            }

            try
            {
                if (!client.RequestTopicRpc(callerId, topic, out string hostname, out int port))
                {
                    return new Arg[]
                    {
                        StatusCode.Failure,
                        $"error=client is not publishing topic '{topic}'",
                        Array.Empty<string[]>()
                    };
                }

                return new Arg[]
                {
                    StatusCode.Success,
                    "ok",
                    new Arg[] {"TCPROS", hostname, port}
                };
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new Arg[]
                {
                    StatusCode.Error,
                    "error=Unknown error: " + e.Message,
                    Array.Empty<string[]>()
                };
            }
        }
    }
}