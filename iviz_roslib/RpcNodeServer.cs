using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Iviz.RoslibSharp.XmlRpc
{
    class NodeServer
    {
        readonly Dictionary<string, Func<object[], Arg[]>> Methods;
        readonly HttpListener listener = new HttpListener();
        readonly RosClient client;
        volatile bool keepRunning;
        Task task;

        public Uri Uri => client.CallerUri;

        public NodeServer(RosClient client)
        {
            this.client = client;

            Methods = new Dictionary<string, Func<object[], Arg[]>>()
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
            keepRunning = true;

            string maskUri = "http://+:" + Uri.Port + Uri.AbsolutePath;
            Logger.Log("RcpNodeServer: Starting RPC server on " + maskUri);
            listener.Prefixes.Add(maskUri);
            listener.Start();
            Logger.Log("RcpNodeServer: Started!");

            task = Task.Run(() =>
            {
                try
                {
                    while (keepRunning)
                    {
                        HttpListenerContext context = listener.GetContext();
                        Task.Run(() =>
                       {
                           try
                           {
                               Service.MethodResponse(context, Methods);
                           }
                           catch (Exception e)
                           {
                               Logger.LogError(e.Message);
                               Logger.LogError(e.StackTrace);
                           }
                       });
                    }
                }
                catch (Exception e)
                {
                    Logger.LogDebug(e.Message);
                    Logger.LogDebug(e.StackTrace);
                }
                Logger.LogDebug("RcpNodeServer: Leaving thread.");
            });
        }

        public void Close()
        {
            keepRunning = false;
            listener.Close();
            task.Wait();
        }

        public Arg[] GetBusStats(object[] _)
        {
            Logger.Log("Was called: getBusStats");
            return new[] {
                    new Arg((int)StatusCode.Error),
                    new Arg("error=NYI"),
                    new Arg(Array.Empty<Arg>())
                };
        }

        public Arg[] GetBusInfo(object[] _)
        {
            var busInfo = client.GetBusInfoRcp();
            Arg[][] response = busInfo.Select(
                x => new Arg[] {
                    new Arg(x.ConnectionId),
                    new Arg(x.DestinationId),
                    new Arg(x.Direction),
                    new Arg(x.Transport),
                    new Arg(x.Topic),
                    new Arg(x.Connected),
                }).ToArray();

            return new[] {
                    new Arg((int)StatusCode.Success),
                    new Arg("ok"),
                    new Arg(response)
                };
        }

        public Arg[] GetMasterUri(object[] _)
        {
            return new[] {
                    new Arg((int)StatusCode.Success),
                    new Arg("ok"),
                    new Arg(client.MasterUri.ToString())
                };
        }

        public Arg[] Shutdown(object[] args)
        {
            if (client.ShutdownAction == null)
            {
                return new[] {
                    new Arg((int)StatusCode.Failure),
                    new Arg("error=no shutdown handler set"),
                    new Arg(0)
                };
            }
            else
            {
                string callerId = (string)args[0];
                string reason = args.Length > 1 ? (string)args[1] : "";
                client.ShutdownAction(callerId, reason, out StatusCode status, out string response);
                return new[] {
                    new Arg((int)status),
                    new Arg(response),
                    new Arg(0)
                };
            }
        }

        public Arg[] GetPid(object[] _)
        {
            return new[] {
                    new Arg((int)StatusCode.Success),
                    new Arg("ok"),
                    new Arg(Process.GetCurrentProcess().Id)
                };
        }

        public Arg[] GetSubscriptions(object[] _)
        {
            return new[] {
                    new Arg((int)StatusCode.Success),
                    new Arg(""),
                    new Arg(client.GetSubscriptionsRcp())
                };
        }

        public Arg[] GetPublications(object[] _)
        {
            return new[] {
                    new Arg((int)StatusCode.Success),
                    new Arg("ok"),
                    new Arg(client.GetPublicationsRcp())
                };
        }

        public Arg[] ParamUpdate(object[] args)
        {
            if (client.ParamUpdateAction == null)
            {
                return new[] {
                    new Arg((int)StatusCode.Failure),
                    new Arg("error=no paramUpdate handler set"),
                    new Arg(0)
                };
            }
            else
            {
                string callerId = (string)args[0];
                string parameterKey = (string)args[1];
                object parameterValue = args[2];
                client.ParamUpdateAction(callerId, parameterKey, parameterValue, out StatusCode status, out string response);
                return new[] {
                    new Arg((int)status),
                    new Arg(response),
                    new Arg(0)
                };
            }
        }

        public Arg[] PublisherUpdate(object[] args)
        {
            string topic = (string)args[1];
            object[] publishers = (object[])args[2];
            try
            {
                Uri[] publisherUris = new Uri[publishers.Length];
                for (int i = 0; i < publishers.Length; i++)
                {
                    if (!Uri.TryCreate((string)publishers[i], UriKind.Absolute, out publisherUris[i]))
                    {
                        Logger.Log($"RcpNodeServer: Invalid uri '{publishers[i]}'");
                    }
                }
                client.PublisherUpdateRcp(topic, publisherUris);
                return new[] {
                        new Arg((int)StatusCode.Success),
                        new Arg("ok"),
                        new Arg(0)
                    };
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new[] {
                        new Arg((int)StatusCode.Failure),
                        new Arg("error=Unknown error: " + e.Message),
                        new Arg(0)
                    };
            }
        }

        public Arg[] RequestTopic(object[] args)
        {
            try
            {
                string caller_id = (string)args[0];
                string topic = (string)args[1];
                object[] protocols = (object[])args[2];

                if (protocols.Length == 0)
                {
                    return new[] {
                            new Arg((int)StatusCode.Failure),
                            new Arg($"error=no compatible protocols found"),
                            new Arg(Array.Empty<string[]>())
                        };
                }

                bool success = false;
                for (int i = 0; i < protocols.Length; i++)
                {
                    object[] protocol = (object[])protocols[i];
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
                    return new[] {
                            new Arg((int)StatusCode.Failure),
                            new Arg("error=client only supports TCPROS"),
                            new Arg(Array.Empty<string[]>())
                        };
                }


                if (!client.RequestTopicRpc(caller_id, topic, out string hostname, out int port))
                {
                    return new[] {
                            new Arg((int)StatusCode.Failure),
                            new Arg($"error=client is not publishing topic '{topic}'"),
                            new Arg(Array.Empty<string[]>())
                        };
                }

                return new[] {
                        new Arg((int)StatusCode.Success),
                        new Arg(""),
                        new Arg(
                                new Arg[] {
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
                return new[] {
                        new Arg((int)StatusCode.Error),
                        new Arg("error=Unknown error: " + e.Message),
                        new Arg(Array.Empty<string[]>())
                    };
            }
        }
    }
}
