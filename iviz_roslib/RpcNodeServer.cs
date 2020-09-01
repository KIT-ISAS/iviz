using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Iviz.Roslib.XmlRpc
{
    sealed class NodeServer : IDisposable
    {
        readonly Dictionary<string, Func<object[], Arg[]>> methods;
        readonly HttpListener listener;
        readonly RosClient client;

        public Uri ListenerUri => listener.LocalEndpoint;
        public Uri Uri => client.CallerUri;

        public NodeServer(RosClient client)
        {
            this.client = client;

            listener = new HttpListener(client.CallerUri);

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
            //task = Task.Run(() =>
            //{
            Logger.LogDebug("RcpNodeServer: Starting!");
            listener.Start(context =>
            {
                Task.Run(() =>
                {
                    using (context)
                    {
                        try
                        {
                            Service.MethodResponse(context, methods);
                        }
                        catch (Exception e)
                        {
                            Logger.LogError(e);
                        }
                    }
                });
            });

            Logger.LogDebug("RcpNodeServer: Leaving thread.");
            //});
        }

        public void Stop()
        {
            listener.Dispose();
            //task.Wait();
        }

        public void Dispose()
        {
            Stop();
        }

        Arg[] GetBusStats(object[] _)
        {
            Logger.Log("Was called: getBusStats");
            return new[]
            {
                new Arg((int) StatusCode.Error),
                new Arg("error=NYI"),
                new Arg(Array.Empty<Arg>())
            };
        }

        Arg[] GetBusInfo(object[] _)
        {
            var busInfo = client.GetBusInfoRcp();
            Arg[][] response = busInfo.Select(
                x => new Arg[]
                {
                    new Arg(x.ConnectionId),
                    new Arg(x.DestinationId),
                    new Arg(x.Direction),
                    new Arg(x.Transport),
                    new Arg(x.Topic),
                    new Arg(x.Connected),
                }).ToArray();

            return new[]
            {
                new Arg((int) StatusCode.Success),
                new Arg("ok"),
                new Arg(response)
            };
        }

        Arg[] GetMasterUri(object[] _)
        {
            return new[]
            {
                new Arg((int) StatusCode.Success),
                new Arg("ok"),
                new Arg(client.MasterUri)
            };
        }

        Arg[] Shutdown(object[] args)
        {
            if (client.ShutdownAction == null)
            {
                return new[]
                {
                    new Arg((int) StatusCode.Failure),
                    new Arg("error=no shutdown handler set"),
                    new Arg(0)
                };
            }

            string callerId = (string) args[0];
            string reason = args.Length > 1 ? (string) args[1] : "";
            client.ShutdownAction(callerId, reason, out StatusCode status, out string response);
            return new[]
            {
                new Arg((int) status),
                new Arg(response),
                new Arg(0)
            };
        }

        Arg[] GetPid(object[] _)
        {
            int id = Process.GetCurrentProcess().Id;

            return new[]
            {
                new Arg((int) StatusCode.Success),
                new Arg("ok"),
                new Arg(id)
            };
        }

        Arg[] GetSubscriptions(object[] _)
        {
            return new[]
            {
                new Arg((int) StatusCode.Success),
                new Arg(""),
                new Arg(client.GetSubscriptionsRcp())
            };
        }

        Arg[] GetPublications(object[] _)
        {
            return new[]
            {
                new Arg((int) StatusCode.Success),
                new Arg("ok"),
                new Arg(client.GetPublicationsRcp())
            };
        }

        Arg[] ParamUpdate(object[] args)
        {
            if (client.ParamUpdateAction == null)
            {
                return new[]
                {
                    new Arg((int) StatusCode.Failure),
                    new Arg("error=no paramUpdate handler set"),
                    new Arg(0)
                };
            }

            string callerId = (string) args[0];
            string parameterKey = (string) args[1];
            object parameterValue = args[2];
            client.ParamUpdateAction(callerId, parameterKey, parameterValue, out StatusCode status,
                out string response);
            return new[]
            {
                new Arg((int) status),
                new Arg(response),
                new Arg(0)
            };
        }

        Arg[] PublisherUpdate(object[] args)
        {
            string topic = (string) args[1];
            object[] publishers = (object[]) args[2];
            try
            {
                Uri[] publisherUris = new Uri[publishers.Length];
                for (int i = 0; i < publishers.Length; i++)
                {
                    if (!Uri.TryCreate((string) publishers[i], UriKind.Absolute, out publisherUris[i]))
                    {
                        Logger.Log($"RcpNodeServer: Invalid uri '{publishers[i]}'");
                    }
                }

                client.PublisherUpdateRcp(topic, publisherUris);
                return new[]
                {
                    new Arg((int) StatusCode.Success),
                    new Arg("ok"),
                    new Arg(0)
                };
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new[]
                {
                    new Arg((int) StatusCode.Failure),
                    new Arg("error=Unknown error: " + e.Message),
                    new Arg(0)
                };
            }
        }

        Arg[] RequestTopic(object[] args)
        {
            try
            {
                string caller_id = (string) args[0];
                string topic = (string) args[1];
                object[] protocols = (object[]) args[2];

                if (protocols.Length == 0)
                {
                    return new[]
                    {
                        new Arg((int) StatusCode.Failure),
                        new Arg($"error=no compatible protocols found"),
                        new Arg(Array.Empty<string[]>())
                    };
                }

                bool success = false;
                foreach (var entry in protocols)
                {
                    object[] protocol = (object[]) entry;
                    if (protocol.Length == 0)
                    {
                        continue;
                    }

                    string protocolName = protocol[0] as string;
                    if (protocolName == null)
                    {
                        continue;
                    }

                    if (protocolName == "TCPROS")
                    {
                        success = true;
                        break;
                    }
                }

                if (!success)
                {
                    return new[]
                    {
                        new Arg((int) StatusCode.Failure),
                        new Arg("error=client only supports TCPROS"),
                        new Arg(Array.Empty<string[]>())
                    };
                }


                if (!client.RequestTopicRpc(caller_id, topic, out string hostname, out int port))
                {
                    return new[]
                    {
                        new Arg((int) StatusCode.Failure),
                        new Arg($"error=client is not publishing topic '{topic}'"),
                        new Arg(Array.Empty<string[]>())
                    };
                }

                return new[]
                {
                    new Arg((int) StatusCode.Success),
                    new Arg(""),
                    new Arg(
                        new Arg[]
                        {
                            new Arg("TCPROS"),
                            new Arg(hostname),
                            new Arg(port),
                        }
                    ),
                };
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new[]
                {
                    new Arg((int) StatusCode.Error),
                    new Arg("error=Unknown error: " + e.Message),
                    new Arg(Array.Empty<string[]>())
                };
            }
        }
    }
}