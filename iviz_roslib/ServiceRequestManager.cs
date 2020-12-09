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
using Nito.AsyncEx.Synchronous;

namespace Iviz.Roslib
{
    internal sealed class ServiceRequestManager<T> : IServiceRequestManager where T : IService
    {
        readonly Func<T, Task> callback;
        readonly HashSet<ServiceRequestAsync<T>> connections = new HashSet<ServiceRequestAsync<T>>();

        readonly TcpListener listener;
        readonly ServiceInfo<T> serviceInfo;
        readonly SemaphoreSlim signal = new SemaphoreSlim(0, 1);
        readonly Task task;

        bool keepGoing;
        bool disposed;

        public ServiceRequestManager(ServiceInfo<T> serviceInfo, string host, Func<T, Task> callback)
        {
            this.serviceInfo = serviceInfo;
            this.callback = callback;

            keepGoing = true;

            listener = new TcpListener(IPAddress.Any, 0);
            listener.Start();

            IPEndPoint localEndpoint = (IPEndPoint) listener.LocalEndpoint;
            Uri = new Uri($"rosrpc://{host}:{localEndpoint.Port}/");

            Logger.LogDebugFormat("{0}: Starting {1} [{2}] at {3}",
                this, serviceInfo.Service, serviceInfo.Type, Uri);

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
                Logger.LogDebugFormat("{0}: Listener stuck. Abandoning.", this);
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

                    IPEndPoint? endPoint;
                    if (client == null
                        || (endPoint = (IPEndPoint?) client.Client.RemoteEndPoint) == null)
                    {
                        Logger.LogFormat("{0}: Received a request, but failed to initialize connection.", this);
                        continue;
                    }

                    var sender = new ServiceRequestAsync<T>(serviceInfo, client, new Endpoint(endPoint), callback);
                    connections.Add(sender);

                    Cleanup();
                }
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            catch (Exception e)
            {
                Logger.LogFormat("{0}: Stopped thread {1}", this, e);
                return;
            }

            Logger.LogDebugFormat("{0}: Leaving thread (normally)", this); // also expected
        }

        void Cleanup()
        {
            ServiceRequestAsync<T>[] toRemove = connections.Where(connection => !connection.IsAlive).ToArray();
            foreach (ServiceRequestAsync<T> connection in toRemove)
            {
                Logger.LogDebugFormat("{0}: Removing service connection with '{1}' - dead x_x",
                    this, connection.Hostname);
                connection.Stop();
                connections.Remove(connection);
            }
        }


        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;

            signal.Release();
            
            task.WaitNoThrow(this);

            foreach (ServiceRequestAsync<T> sender in connections)
            {
                sender.Stop();
            }

            connections.Clear();
        }

        public async Task DisposeAsync()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;

            signal.Release();
            
            await task.AwaitNoThrow(this).Caf();

            Task[] tasks = connections.Select(sender => sender.StopAsync()).ToArray();
            await Task.WhenAll(tasks).AwaitNoThrow(this).Caf();
            connections.Clear();
        }

        public override string ToString()
        {
            return $"[ServiceRequestManager {Service} [{ServiceType}] at {Uri}]";
        }
    }
}