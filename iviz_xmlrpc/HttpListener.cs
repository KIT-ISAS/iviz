using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.XmlRpc
{
    public sealed class HttpListener : IDisposable
    {
        const int AnyPort = 0;

        TcpListener listener;
        bool keepGoing;
        readonly List<(DateTime start, Task task)> backgroundTasks = new List<(DateTime, Task)>();


        public Uri LocalEndpoint { get; }

        public HttpListener(Uri uri)
        {
            if (uri is null) { throw new ArgumentNullException(nameof(uri)); }

            int port = uri.IsDefaultPort ? AnyPort : uri.Port;
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            IPEndPoint endpoint = (IPEndPoint) listener.LocalEndpoint;
            LocalEndpoint = new Uri($"http://{endpoint.Address}:{endpoint.Port}/");
        }

        public async Task StartAsync(Func<HttpListenerContext, Task> callback)
        {
            if (callback is null) { throw new ArgumentNullException(nameof(callback)); }

            keepGoing = true;
            while (keepGoing)
            {
                try
                {
                    Logger.LogDebug($"{this}: Accepting request...");
                    TcpClient client = await listener.AcceptTcpClientAsync().Caf();
                    Logger.LogDebug($"{this}: Accept Out!");

                    if (!keepGoing)
                    {
                        client.Dispose();
                        break;
                    }

                    AddToBackgroundTask(callback(new HttpListenerContext(client)));
                }
                catch (ObjectDisposedException)
                {
                    Logger.LogDebug($"{this}: Leaving thread");
                    break;
                }
                catch (Exception e)
                {
                    Logger.Log($"{this}: Leaving thread " + e);
                    break;
                }
            }

            Logger.LogDebug($"{this}: Leaving thread normally");
        }

        void AddToBackgroundTask(Task task)
        {
            backgroundTasks.RemoveAll(tuple => tuple.task.IsCompleted);
            backgroundTasks.Add((DateTime.Now, task));
        }

        public async Task AwaitRunningTasks(int timeoutInMs = 2000)
        {
            backgroundTasks.RemoveAll(tuple => tuple.task.IsCompleted);

            DateTime now = DateTime.Now;
            int count = backgroundTasks.Count(tuple => (tuple.start - now).TotalSeconds > 5);
            if (count > 0)
            {
                Logger.Log($"{this}: There appear to be {count} tasks deadlocked!");
            }

            try
            {
                await Task.WhenAll(backgroundTasks.Select(tuple => tuple.task)).WaitFor(timeoutInMs);
            }
            catch (Exception) { }
        }

        bool disposed;

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;

            if (!keepGoing)
            {
                // not started, dispose directly
                listener.Stop();
                listener = null;
                return;
            }

            keepGoing = false;

            // now we throw everything at the listener to try to leave AcceptTcpClientAsync()

            // first we enqueue a connection
            using (TcpClient client = new TcpClient())
            {
                Logger.LogDebug($"{this}: Using fake client");
                client.Connect(IPAddress.Loopback, LocalEndpoint.Port);
            }

            // now we close the listener
            Logger.LogDebug($"{this}: Stopping listener");
            listener.Stop();

            // now we close the underlying socket
            listener.Server.Close();

            // set listener to null to maybe trigger a null reference exception
            listener = null;

            // and hope that this is enough to leave AcceptTcpClientAsync()
            Logger.LogDebug($"{this}: Dispose out");
        }

        public override string ToString()
        {
            return $"[HttpListener {LocalEndpoint?.ToString() ?? "(not initialized)"}]";
        }
    }
}