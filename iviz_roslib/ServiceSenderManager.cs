using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;

namespace Iviz.Roslib
{
    internal sealed class ServiceSenderManager
    {
        readonly Func<IService, Task> callback;

        readonly ConcurrentDictionary<ServiceSenderAsync, object> connections =
            new ConcurrentDictionary<ServiceSenderAsync, object>();

        readonly TcpListener listener;
        readonly ServiceInfo serviceInfo;
        readonly SemaphoreSlim signal = new SemaphoreSlim(0, 1);

        readonly Task task;

        bool keepGoing;

        public ServiceSenderManager(ServiceInfo serviceInfo, string host, Func<IService, Task> callback)
        {
            this.serviceInfo = serviceInfo;
            this.callback = callback;

            keepGoing = true;

            listener = new TcpListener(IPAddress.Any, 0);
            listener.Start();

            IPEndPoint localEndpoint = (IPEndPoint) listener.LocalEndpoint;
            Uri = new Uri($"rosrpc://{host}:{localEndpoint.Port}/");
            Logger.LogDebug($"{this}: Starting {serviceInfo.Service} [{serviceInfo.Type}] at {Uri}");

            task = Task.Run(StartAsync);
        }

        public Uri Uri { get; }
        public string Service => serviceInfo.Service;
        string ServiceType => serviceInfo.Type;

        async Task StartAsync()
        {
            Task loopTask = RunLoop();
            await signal.WaitAsync();
            keepGoing = false;
            listener.Stop();
            if (!await loopTask.WaitFor(2000))
            {
                Logger.LogDebug($"{this}: Listener stuck. Abandoning.");
            }
        }

        async Task RunLoop()
        {
            try
            {
                while (keepGoing)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync().Caf();
                    if (!keepGoing)
                    {
                        break;
                    }

                    ServiceSenderAsync sender = new ServiceSenderAsync(serviceInfo, client, callback);
                    connections[sender] = null;

                    Cleanup();
                }
            }
            catch (ObjectDisposedException)
            {
                Logger.LogDebug($"{this}: Leaving thread."); // expected
                return;
            }
            catch (Exception e)
            {
                Logger.Log($"{this}: Stopped thread{e}");
                return;
            }

            Logger.LogDebug($"{this}: Leaving thread (normally)"); // also expected
        }

        void Cleanup()
        {
            ServiceSenderAsync[] toRemove = connections.Keys.Where(connection => !connection.IsAlive).ToArray();
            foreach (ServiceSenderAsync connection in toRemove)
            {
                Logger.LogDebug(
                    $"{this}: Removing service connection with '{connection.Hostname}' - dead x_x");
                connection.Stop();
                connections.TryRemove(connection, out _);
            }
        }


        public void Stop()
        {
            foreach (ServiceSenderAsync sender in connections.Keys)
            {
                sender.Stop();
            }

            connections.Clear();
            signal.Release();
            task?.Wait();
        }

        public async Task StopAsync()
        {
            Task[] tasks = connections.Keys.Select(sender => sender.StopAsync()).ToArray();
            Task.WaitAll(tasks);
            connections.Clear();
            signal.Release();
            if (task != null)
            {
                await task;
            }
        }

        public override string ToString()
        {
            return $"[ServiceSenderManager {Service} [{ServiceType}] at {Uri}]";
        }
    }
}