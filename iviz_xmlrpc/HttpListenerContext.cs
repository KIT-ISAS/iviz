using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.XmlRpc
{
    /// <summary>
    /// Handler for an HTTP request.
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
        /// <param name="timeoutInMs">Maximal time to wait</param>
        /// <param name="token">An optional cancellation token</param>
        /// <returns>An awaitable task</returns>
        /// <exception cref="TimeoutException">Wait time exceeded</exception>
        /// <exception cref="ParseException">The HTTP request could not be understood</exception>
        /// <exception cref="TimeoutException">Thrown if the timeout wait expired</exception>
        /// <exception cref="OperationCanceledException">Thrown if the token expired</exception>
        public async Task<string> GetRequestAsync(int timeoutInMs = 2000, CancellationToken token = default)
        {
            StreamReader stream = new StreamReader(client.GetStream(), BuiltIns.UTF8);

            int length = -1;
            while (true)
            {
                Task<string?> readTask = stream.ReadLineAsync();
                if (!await readTask.WaitFor(timeoutInMs, token) || !readTask.RanToCompletion())
                {
                    token.ThrowIfCanceled();
                    throw new TimeoutException("Read line timed out!", readTask.Exception);
                }

                string? line = await readTask;
                if (line == null)
                {
                    if (!client.Connected)
                    {
                        throw new RpcConnectionException("Partner closed the connection.");
                    }

                    throw new ParseException("Read line returned empty value!");
                }

                if (CheckHeaderLine(line, "Content-Length", out string? lengthStr))
                {
                    if (!int.TryParse(lengthStr, out length))
                    {
                        throw new ParseException($"Cannot parse length '{lengthStr}'");
                    }
                }
                else if (string.IsNullOrEmpty(line) || line == "\r")
                {
                    break;
                }
            }

            if (length == -1)
            {
                throw new ParseException("Content-Length not found in HTTP header");
            }

            char[] buffer = new char[length];
            int numRead = 0;
            while (BuiltIns.UTF8.GetByteCount(buffer, 0, numRead) < length)
            {
                Task<int> readTask = stream.ReadAsync(buffer, numRead, length - numRead);
                if (!await readTask.WaitFor(timeoutInMs, token) || !readTask.RanToCompletion())
                {
                    token.ThrowIfCanceled();
                    throw new TimeoutException("Read line timed out!", readTask.Exception);
                }

                numRead += await readTask;
            }


            return new string(buffer, 0, numRead);
        }

        static bool CheckHeaderLine(string line, string key, out string? value)
        {
            if (line.Length < key.Length + 1 ||
                string.Compare(line, 0, key, 0, key.Length, true, BuiltIns.Culture) != 0)
            {
                value = null;
                return false;
            }

            int start = key.Length + 1;
            if (start == line.Length)
            {
                value = null;
                return false;
            }

            if (line[start] == ' ')
            {
                start++;
            }

            int end = line.Length - 1;
            if (line[end] == '\r')
            {
                end--;
            }

            value = line.Substring(start, end + 1 - start);
            return true;
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
        public async Task RespondAsync(string msgOut, int timeoutInMs = 2000, CancellationToken token = default)
        {
            if (msgOut is null)
            {
                throw new ArgumentNullException(nameof(msgOut));
            }

            int msgOutLength = BuiltIns.UTF8.GetByteCount(msgOut);
            string str = "HTTP/1.0 200 OK\r\n" + 
                         "Server: iviz XML-RPC\r\n" + 
                         "Connection: close\r\n" +
                         "Content-Type: text/xml; charset=utf-8\r\n" + 
                         $"Content-Length: {msgOutLength.ToString()}\r\n\r\n" +
                         msgOut;

            using StreamWriter writer = new StreamWriter(client.GetStream(), BuiltIns.UTF8);
            Task writeTask = writer.WriteAsync(str);
            if (!await writeTask.WaitFor(timeoutInMs, token) || !writeTask.RanToCompletion())
            {
                token.ThrowIfCanceled();
                throw new TimeoutException("Write response timed out!", writeTask.Exception);
            }
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
        public async Task RespondWithUnexpectedErrorAsync(int timeoutInMs = 2000, CancellationToken token = default)
        {
            const string errorMsg =
                "HTTP/1.0 500 Internal Server Error\r\n" +
                "Server: iviz XML-RPC\r\n" +
                "Connection: close\r\n" +
                "Content-Type: text/xml; charset=utf-8\r\n" +
                "Content-Length: 0\r\n" +
                "\r\n";

            using StreamWriter writer = new StreamWriter(client.GetStream(), BuiltIns.UTF8);
            Task writeTask = writer.WriteAsync(errorMsg);
            if (!await writeTask.WaitFor(timeoutInMs, token) || !writeTask.RanToCompletion())
            {
                token.ThrowIfCanceled();
                throw new TimeoutException("Write response timed out!", writeTask.Exception);
            }
        }
    }
}