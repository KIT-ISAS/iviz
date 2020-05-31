using System;
using System.Net;
using System.Net.Sockets;

namespace Iviz.RoslibSharp.XmlRpc
{
    class HttpListener
    {
        readonly TcpListener listener;
        bool keepGoing = true;

        public HttpListener(Uri uri)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            listener = new TcpListener(IPAddress.Any, uri.Port);
        }

        public void Start(Action<HttpListenerContext> callback)
        {
            listener.Start();
            while (keepGoing)
            {
                TcpClient client;

                try
                {
                    client = listener.AcceptTcpClient();
                    callback(new HttpListenerContext(client));
                }
                catch (Exception e) when
                (e is ObjectDisposedException || e is SocketException)
                {
                    break;
                }
            }
        }

        public void Stop()
        {
            keepGoing = false;
            listener.Server.Shutdown(SocketShutdown.Both);
            listener.Stop();
        }
    }
}
