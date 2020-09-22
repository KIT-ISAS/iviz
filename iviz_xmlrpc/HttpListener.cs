using System;
using System.Collections.Generic;
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
        bool keepGoing = true;

        public Uri LocalEndpoint { get; }

        public HttpListener(Uri uri)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            int port = uri.IsDefaultPort ? AnyPort : uri.Port;
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            IPEndPoint endpoint = (IPEndPoint) listener.LocalEndpoint;
            LocalEndpoint = new Uri($"http://{endpoint.Address}:{endpoint.Port}/");
        }

        public async Task Start(Func<HttpListenerContext, Task> callback)
        {
            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            while (keepGoing)
            {
                try
                {
                    Logger.LogDebug($"{this}: Accepting request...");
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    Logger.LogDebug($"{this}: Accept Out!");
                    if (!keepGoing)
                    {
                        client.Dispose();
                        break;
                    }
                    await callback(new HttpListenerContext(client));
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
            Logger.Log($"{this}: Leaving thread normally");
        }

        bool disposed;
        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
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
            return $"[HttpListener :{LocalEndpoint?.Port ?? -1}]";
        }
    }
}