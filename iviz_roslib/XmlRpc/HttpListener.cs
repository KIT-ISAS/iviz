using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Iviz.RoslibSharp.XmlRpc
{
    class HttpListener : IDisposable
    {
        readonly TcpListener listener;
        bool keepGoing = true;
        const int timeoutInMs = 2000;

        public Uri LocalEndpoint { get; }

        public HttpListener(Uri uri)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            int port = uri.Port;
            if (uri.IsDefaultPort)
            {
                port = 0;
            }
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            IPEndPoint endpoint = (IPEndPoint)listener.LocalEndpoint;
            LocalEndpoint = new Uri($"http://{endpoint.Address}:{endpoint.Port}/");
        }

        public void Start(Action<HttpListenerContext> callback)
        {
            while (keepGoing)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    callback(new HttpListenerContext(client));
                }
                catch (Exception e)
                {
                    Logger.LogError("HttpListener: Leaving thread! " + e);
                    break;
                }
            }
        }

        public void Stop()
        {
            Logger.LogError("HttpListener: Requesting stop.");
            keepGoing = false;
            try
            {
                listener.Server.Shutdown(SocketShutdown.Both);
            }
            catch (Exception) {}
            try
            {
                listener.Stop();
            }
            catch (Exception) {}
            Logger.LogError("HttpListener: Stopped.");
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
