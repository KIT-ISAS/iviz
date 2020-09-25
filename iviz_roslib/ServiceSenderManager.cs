using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    internal sealed class ServiceSenderManager
    {
        public Uri Uri { get; }
        public string Service => serviceInfo.Service;
        public string ServiceType => serviceInfo.Type;
        readonly TcpListener listener;
        readonly ServiceInfo serviceInfo;
        readonly Action<IService> callback;
        bool keepGoing;

        readonly List<ServiceSender> connections = new List<ServiceSender>();

        public ServiceSenderManager(ServiceInfo serviceInfo, string host, Action<IService> callback)
        {
            this.serviceInfo = serviceInfo;
            this.callback = callback;

            keepGoing = true;

            listener = new TcpListener(IPAddress.Any, 0);
            listener.Start();

            IPEndPoint localEndpoint = (IPEndPoint) listener.LocalEndpoint;
            Uri = new Uri($"rosrpc://{host}:{localEndpoint.Port}/");
            Logger.LogDebug($"{this}: Starting {serviceInfo.Service} [{serviceInfo.Type}] at {Uri}");

            Run();
        }

        async void Run()
        {
            try
            {
                while (keepGoing)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync().ConfigureAwait(false);
                    if (!keepGoing)
                    {
                        break;
                    }
                    ServiceSender sender = new ServiceSender(serviceInfo, client, callback);
                    lock (connections)
                    {
                        connections.Add(sender);
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                Logger.LogDebug($"{this}: Leaving thread."); // expected
            }
            catch (ThreadAbortException e)
            {
                Logger.Log($"{this}: Thread aborted! " + e);
                Thread.ResetAbort();
            }
            catch (Exception e)
            {
                Logger.Log($"{this}: Stopped thread" + e);
            }

            Logger.LogDebug($"{this}: Leaving thread (normally)"); // also expected
        }

        public void Cleanup()
        {
            lock (connections)
            {
                List<int> toDelete = new List<int>();
                for (int i = connections.Count - 1; i >= 0; i--)
                {
                    if (!connections[i].IsAlive)
                    {
                        toDelete.Add(i);
                    }
                }

                foreach (int id in toDelete)
                {
                    Logger.LogDebug(
                        $"{this}: Removing service connection with '{connections[id].Hostname}' - dead x_x");
                    connections[id].Stop();
                    connections.RemoveAt(id);
                }
            }
        }


        public void Stop()
        {
            lock (connections)
            {
                connections.ForEach(x => x.Stop());
                connections.Clear();
            }

            keepGoing = false;
            try
            {
                listener.Stop();
            }
            catch (Exception)
            {
            }
        }

        public override string ToString()
        {
            return $"[ServiceSenderManager {Service} [{ServiceType}] at {Uri}]";
        }
    }
}