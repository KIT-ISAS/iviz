using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Tools;

namespace Iviz.XmlRpc
{
    /// <summary>
    /// Handler for an HTTP request that was sent to our server by another node .
    /// </summary>
    public sealed class HttpListenerContext : IDisposable
    {
        readonly TcpClient client;
        bool disposed;

        internal HttpListenerContext(TcpClient client)
        {
            this.client = client;
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            client.Close();
            disposed = true;
        }

        /// <summary>
        /// Retrieves the HTTP request.
        /// </summary>
        /// <param name="token">An optional cancellation token</param>
        /// <returns>An awaitable task</returns>
        /// <exception cref="TimeoutException">Wait time exceeded</exception>
        /// <exception cref="ParseException">The HTTP request could not be understood</exception>
        /// <exception cref="TimeoutException">Thrown if the timeout wait expired</exception>
        /// <exception cref="OperationCanceledException">Thrown if the token expired</exception>
        public async ValueTask<string> GetRequestAsync(CancellationToken token = default)
        {
            return (await HttpRequest.ReadIncomingDataAsync(client, false, token)).inData;
        }

        /// <summary>
        /// Sends an HTTP response.
        /// </summary>
        /// <param name="msgOut">The response message</param>
        /// <param name="timeoutInMs">Maximal time to wait</param>
        /// <param name="token">An optional cancellation token</param>
        /// <returns>An awaitable task</returns>
        /// <exception cref="ArgumentNullException">Thrown if msgOut is null</exception>
        /// <exception cref="TimeoutException">Thrown if the timeout wait expired</exception>
        /// <exception cref="OperationCanceledException">Thrown if the token expired</exception>
        public ValueTask RespondAsync(string msgOut, int timeoutInMs = 2000, CancellationToken token = default)
        {
            if (msgOut is null)
            {
                throw new ArgumentNullException(nameof(msgOut));
            }

            int msgOutLength = Defaults.UTF8.GetByteCount(msgOut);
            string str = "HTTP/1.0 200 OK\r\n" +
                         "Server: iviz XML-RPC\r\n" +
                         "Connection: close\r\n" +
                         "Content-Type: text/xml; charset=utf-8\r\n" +
                         $"Content-Length: {msgOutLength.ToString()}\r\n\r\n" +
                         msgOut;

            return client.WriteChunkAsync(str, token, timeoutInMs);
        }

        /// <summary>
        /// Sends a generic HTTP error response.
        /// </summary>
        /// <param name="timeoutInMs">Maximal time to wait</param>
        /// <param name="token">An optional cancellation token</param>
        /// <returns>An awaitable task</returns>
        /// <exception cref="ArgumentNullException">Thrown if msgOut is null</exception>
        /// <exception cref="TimeoutException">Thrown if the timeout wait expired</exception>
        /// <exception cref="OperationCanceledException">Thrown if the token expired</exception>
        public ValueTask RespondWithUnexpectedErrorAsync(int timeoutInMs = 2000, CancellationToken token = default)
        {
            const string errorMsg =
                "HTTP/1.0 500 Internal Server Error\r\n" +
                "Server: iviz XML-RPC\r\n" +
                "Connection: close\r\n" +
                "Content-Type: text/xml; charset=utf-8\r\n" +
                "Content-Length: 0\r\n" +
                "\r\n";

            return client.WriteChunkAsync(errorMsg, token, timeoutInMs);
        }
    }
}