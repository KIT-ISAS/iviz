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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability",
            "CA2000:Objekte verwerfen, bevor Bereich verloren geht",
            Justification = "Wird später in 'callback' verworfen")]
        public async void Start(Action<HttpListenerContext> callback)
        {
            while (keepGoing)
            {
                try
                {
                    //Logger.Log("Entering");
                    //TcpClient client = listener.AcceptTcpClient();

                    TcpClient client = await listener.AcceptTcpClientAsync().ConfigureAwait(false);
                    //Logger.Log("Out");
                    callback(new HttpListenerContext(client));
                }
                catch (ThreadAbortException e)
                {
                    Logger.Log("RosRcpServer: Thread aborted! " + e);
                    Thread.ResetAbort();
                    return;
                }
                catch (Exception e)
                {
                    Logger.Log("HttpListener: Leaving thread! " + e);
                    break;
                }
            }
        }

        public void Stop()
        {
            Logger.Log("HttpListener: Requesting stop 2.");
            keepGoing = false;
            /*
            try
            {
                Logger.Log("HttpListener: Shutdown.");
                //listener.Server.Shutdown(SocketShutdown.Both);
                listener.Server.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            */
            try
            {
                Logger.Log("HttpListener: Stop.");
                listener.Stop();
            }
            catch (Exception) { }
            Logger.Log("HttpListener: Stopped.");
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
