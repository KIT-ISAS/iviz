using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;

namespace Iviz.Roslib
{
    internal interface IServiceSenderManager
    {
        string Service { get; }
        string ServiceType { get; }
        Uri Uri { get; }
        void Stop();
        Task StopAsync();
    }

    internal sealed class ServiceSenderManager<T> : IServiceSenderManager where T : IService
    {
        readonly Func<T, Task> callback;
        readonly HashSet<ServiceSenderAsync<T>> connections = new HashSet<ServiceSenderAsync<T>>();

        readonly TcpListener listener;
        readonly ServiceInfo<T> serviceInfo;
        readonly SemaphoreSlim signal = new SemaphoreSlim(0, 1);
        readonly Task task;

        bool keepGoing;

        public ServiceSenderManager(ServiceInfo<T> serviceInfo, string host, Func<T, Task> callback)
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
        public string ServiceType => serviceInfo.Type;

        async Task StartAsync()
        {
            Task loopTask = RunLoop();
            await signal.WaitAsync().Caf();
            keepGoing = false;
            listener.Stop();
            if (!await loopTask.WaitFor(2000).Caf())
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

                    var sender = new ServiceSenderAsync<T>(serviceInfo, client, callback);
                    connections.Add(sender);

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
            ServiceSenderAsync<T>[] toRemove = connections.Where(connection => !connection.IsAlive).ToArray();
            foreach (ServiceSenderAsync<T> connection in toRemove)
            {
                Logger.LogDebug(
                    $"{this}: Removing service connection with '{connection.Hostname}' - dead x_x");
                connection.Stop();
                connections.Remove(connection);
            }
        }


        public void Stop()
        {
            signal.Release();
            task.Wait();

            foreach (ServiceSenderAsync<T> sender in connections)
            {
                sender.Stop();
            }

            connections.Clear();
        }

        public async Task StopAsync()
        {
            signal.Release();
            await task.Caf();

            Task[] tasks = connections.Select(sender => sender.StopAsync()).ToArray();
            Task.WaitAll(tasks);
            connections.Clear();
        }

        public override string ToString()
        {
            return $"[ServiceSenderManager {Service} [{ServiceType}] at {Uri}]";
        }
    }
}