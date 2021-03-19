using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;
using Nito.AsyncEx;

namespace Iviz.Roslib
{
    internal sealed class ServiceRequestManager<T> : IServiceRequestManager where T : IService
    {
        readonly Func<T, Task> callback;
        readonly HashSet<ServiceRequestAsync<T>> requests = new();
        readonly TcpListener listener;
        readonly ServiceInfo<T> serviceInfo;
        readonly Task task;

        readonly CancellationTokenSource tokenSource = new();
        bool KeepRunning => !tokenSource.IsCancellationRequested;
        
        bool disposed;

        public ServiceRequestManager(ServiceInfo<T> serviceInfo, string host, Func<T, Task> callback)
        {
            this.serviceInfo = serviceInfo;
            this.callback = callback;

            listener = new TcpListener(IPAddress.IPv6Any, 0) {Server = {DualMode = true}};
            listener.Start();

            IPEndPoint localEndpoint = (IPEndPoint) listener.LocalEndpoint;
            Uri = new Uri($"rosrpc://{host}:{localEndpoint.Port.ToString()}/");

            Logger.LogDebugFormat("{0}: Starting!", this);

            task = TaskUtils.StartLongTask(RunLoop);
        }

        public Uri Uri { get; }
        public string Service => serviceInfo.Service;
        public string ServiceType => serviceInfo.Type;


        async Task RunLoop()
        {
            try
            {
                while (KeepRunning)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    if (!KeepRunning)
                    {
                        break;
                    }

                    IPEndPoint? endPoint;
                    if ((endPoint = (IPEndPoint?) client.Client.RemoteEndPoint) == null)
                    {
                        Logger.LogFormat("{0}: Received a request, but failed to initialize connection.", this);
                        continue;
                    }

                    var sender = new ServiceRequestAsync<T>(serviceInfo, client, new Endpoint(endPoint), callback);
                    requests.Add(sender);

                    await CleanupAsync(tokenSource.Token);
                }
            }
            catch (Exception e)
            {
                if (!(e is ObjectDisposedException || e is OperationCanceledException))
                {
                    Logger.LogFormat("{0}: Stopped thread {1}", this, e);
                }

                return;
            }

            Logger.LogDebugFormat("{0}: Leaving task", this); // also expected
        }

        async Task CleanupAsync(CancellationToken token)
        {
            ServiceRequestAsync<T>[] toRemove = requests.Where(request => !request.IsAlive).ToArray();
            var tasks = toRemove.Select(async request =>
            {
                Logger.LogDebugFormat("{0}: Removing service connection with '{1}' - dead x_x",
                    this, request.Hostname);
                await request.StopAsync(token);
                requests.Remove(request);
            });
            await tasks.WhenAll().AwaitNoThrow(this);
        }

        public async Task DisposeAsync(CancellationToken token)
        {
            if (disposed)
            {
                return;
            }

            disposed = true;

            tokenSource.Cancel();

            // this is a bad hack, but it's the only reliable way I've found to make AcceptTcpClient come out 
            using (TcpClient client = new(AddressFamily.InterNetworkV6) {Client = {DualMode = true}})
            {
                await client.ConnectAsync(IPAddress.Loopback, Uri.Port);
            }

            listener.Stop();
            if (!await task.AwaitFor(2000, token))
            {
                Logger.LogDebugFormat("{0}: Listener stuck. Abandoning.", this);
            }


            Task[] tasks = requests.Select(request => request.StopAsync(token)).ToArray();
            await tasks.WhenAll().AwaitNoThrow(this);
            requests.Clear();
            
            tokenSource.Dispose();
        }

        public override string ToString()
        {
            return $"[ServiceRequestManager {Service} [{ServiceType}] at {Uri}]";
        }
    }
}