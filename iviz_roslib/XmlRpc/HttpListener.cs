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

        public HttpListener(Uri uri)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            listener = new TcpListener(IPAddress.Any, uri.Port);
            listener.Start();
        }

        public void Start(Action<HttpListenerContext> callback)
        {
            while (keepGoing)
            {
                TcpClient client;

                try
                {
                    while (keepGoing && !listener.Pending())
                    {
                        Task.Delay(10);
                    }
                    if (!keepGoing)
                    {
                        break;
                    }
                    Task<TcpClient> task = listener.AcceptTcpClientAsync();
                    if (!task.Wait(timeoutInMs) || task.IsCanceled || task.IsFaulted || !keepGoing)
                    {
                        Logger.Log("HttpListener: Incoming connection timed out!");
                        continue;
                    }
                    callback(new HttpListenerContext(task.Result));
                }
                catch (Exception e) when
                (e is ObjectDisposedException || e is SocketException)
                {
                    Logger.Log("Break");
                    break;
                }
            }
        }

        public void Stop()
        {
            keepGoing = false;
            /*
            try
            {
                listener.Server.Shutdown(SocketShutdown.Both);
            }
            catch (SocketException) { }
            */
            listener.Stop();
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
