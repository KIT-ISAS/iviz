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

            IPEndPoint endpoint = (IPEndPoint)listener.LocalEndpoint;
            LocalEndpoint = new Uri($"http://{endpoint.Address}:{endpoint.Port}/");
        }

        public async Task Start(Action<HttpListenerContext> callback)
        {
            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            while (keepGoing)
            {
                try
                {
                    //Logger.Log("Entering");
                    TcpClient client = await listener.AcceptTcpClientAsync().ConfigureAwait(false);
                    //Logger.Log("Out");
                    callback(new HttpListenerContext(client));
                }
                catch (Exception e)
                {
                    Logger.Log("HttpListener: Leaving thread! " + e);
                    break;
                }
            }
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
                    TcpClient client = await listener.AcceptTcpClientAsync().ConfigureAwait(false);
                    await callback(new HttpListenerContext(client));
                }
                catch (Exception e)
                {
                    Logger.Log("HttpListener: Leaving thread! " + e);
                    break;
                }
            }
        }        

        void Stop()
        {
            keepGoing = false;
            try
            {
                listener.Stop();
            }
            catch (Exception) { }
        }

        public void Dispose()
        {
            if (keepGoing)
            {
                Stop();
            }
        }
    }
}
