using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;

namespace Iviz.RosMaster
{
    class Server
    {
        static async Task Main(string[] args)
        {
            Server server = new Server(TryGetCallerUri());
            await server.Start();
        }

        const int DefaultPort = 11311;
        readonly XmlRpc.HttpListener listener;
        readonly Dictionary<string, Func<object[], Arg[]>> methods;

        Server(Uri uri)
        {
            listener = new XmlRpc.HttpListener(uri);

            methods = new Dictionary<string, Func<object[], Arg[]>>
            {
            };
        }

        async Task Start()
        {
            Logger.LogDebug("RcpNodeServer: Starting!");
            await listener.Start(async context =>
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
            });
            Logger.LogDebug("RcpNodeServer: Leaving thread.");
        }

        static Uri TryGetCallerUri(int usingPort = DefaultPort)
        {
            return EnvironmentCallerUri ??
                   GetUriFromInterface(NetworkInterfaceType.Wireless80211, usingPort) ??
                   GetUriFromInterface(NetworkInterfaceType.Ethernet, usingPort) ??
                   new Uri($"http://{Dns.GetHostName()}:{usingPort}/");
        }

        static Uri GetUriFromInterface(NetworkInterfaceType type, int usingPort)
        {
            UnicastIPAddressInformation ipInfo =
                NetworkInterface.GetAllNetworkInterfaces()
                    .Where(iface =>
                        iface.NetworkInterfaceType == type && iface.OperationalStatus == OperationalStatus.Up)
                    .SelectMany(iface => iface.GetIPProperties().UnicastAddresses)
                    .FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork);

            return ipInfo is null ? null : new Uri($"http://{ipInfo.Address}:{usingPort}/");
        }

        static Uri EnvironmentCallerUri
        {
            get
            {
                string envStr = Environment.GetEnvironmentVariable("ROS_HOSTNAME") ??
                                Environment.GetEnvironmentVariable("ROS_IP");
                if (envStr is null)
                {
                    return null;
                }

                if (Uri.TryCreate(envStr, UriKind.Absolute, out Uri uri))
                {
                    return uri;
                }

                Logger.Log("RosClient: Environment variable for caller uri is not a valid uri!");
                return null;
            }
        }
    }
}