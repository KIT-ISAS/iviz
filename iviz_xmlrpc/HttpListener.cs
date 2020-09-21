using System;
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

        readonly TcpListener listener;

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

            while (true)
            {
                try
                {
                    TcpClient client = await listener.AcceptTcpClientAsync().ConfigureAwait(false);
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
        }

        bool disposed;
        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            listener.Stop();
        }

        public override string ToString()
        {
            return "[HttpListener]";
        }
    }
}