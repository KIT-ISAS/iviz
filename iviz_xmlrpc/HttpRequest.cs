using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.XmlRpc
{
    internal sealed class HttpRequest : IDisposable
    {
        const int DefaultTimeoutInMs = 2000;

        readonly Uri callerUri;
        readonly Uri uri;
        readonly TcpClient client;

        public HttpRequest(Uri callerUri, Uri uri)
        {
            this.callerUri = callerUri ?? throw new ArgumentNullException(nameof(callerUri));
            this.uri = uri ?? throw new ArgumentNullException(nameof(uri));
            client = new TcpClient(AddressFamily.InterNetworkV6) {Client = {DualMode = true}};
        }

        public void Start(int timeoutInMs = DefaultTimeoutInMs, CancellationToken token = default)
        {
            Task.Run(() => StartAsync(timeoutInMs, token), token).WaitAndRethrow();
        }

        public async Task StartAsync(int timeoutInMs = DefaultTimeoutInMs, CancellationToken token = default)
        {
            await client.TryConnectAsync(uri.Host, uri.Port, token, timeoutInMs);
        }

        string CreateRequest(string msgIn)
        {
            return $"POST {Uri.UnescapeDataString(uri.AbsolutePath)} HTTP/1.0\r\n" + 
                   "User-Agent: iviz XML-RPC\r\n" +
                   $"Host: {callerUri.Host}\r\n" +
                   $"Content-Length: {BuiltIns.UTF8.GetByteCount(msgIn).ToString()}\r\n" +
                   "Content-Type: text/xml; charset=utf-8\r\n" + 
                   $"\r\n{msgIn}" +
                   "\r\n";
        }

        static string ProcessResponse(string response)
        {
            if (response.Length == 0)
            {
                throw new IOException("Partner closed connection or returned empty response");
            }

            int index = response.IndexOf("\r\n\r\n", StringComparison.InvariantCulture);
            if (index == -1)
            {
                index = response.IndexOf("\n\n", StringComparison.InvariantCulture);
                if (index == -1)
                {
                    throw new ParseException(
                        $"Cannot find double line-end in HTTP header (received {response.Length} bytes)");
                }

                index += 2;
            }
            else
            {
                index += 4;
            }

            return response.Substring(index);
        }

        internal string Request(string msgIn, int timeoutInMs = DefaultTimeoutInMs)
        {
            string response;
            using (Stream stream = client.GetStream())
            {
                stream.ReadTimeout = timeoutInMs;
                stream.WriteTimeout = timeoutInMs;

                StreamWriter writer = new StreamWriter(stream, BuiltIns.UTF8);
                writer.Write(CreateRequest(msgIn));
                writer.Flush();

                StreamReader reader = new StreamReader(stream, BuiltIns.UTF8);
                response = reader.ReadToEnd();
            }

            return ProcessResponse(response);
        }

        internal async Task<string> RequestAsync(string msgIn, int timeoutInMs = DefaultTimeoutInMs,
            CancellationToken token = default)
        {
            string response;
            using (Stream stream = client.GetStream())
            {
                StreamWriter writer = new StreamWriter(stream, BuiltIns.UTF8);
                Task writeTask = writer.WriteAsync(CreateRequest(msgIn));
                if (!await writeTask.WaitFor(timeoutInMs, token) || !writeTask.RanToCompletion())
                {
                    writer.Close();
                    token.ThrowIfCanceled();
                    if (writeTask.IsFaulted)
                    {
                        ExceptionDispatchInfo.Capture(writeTask.Exception!.InnerException!).Throw();
                    }

                    throw new TimeoutException("HttpRequest: Request writing timed out!", writeTask.Exception);
                }

                await writer.FlushAsync().Caf();

                StreamReader reader = new StreamReader(stream, BuiltIns.UTF8);
                Task<string> readTask = reader.ReadToEndAsync();
                if (!await readTask.WaitFor(timeoutInMs, token) || !readTask.RanToCompletion())
                {
                    reader.Close();
                    token.ThrowIfCanceled();
                    if (readTask.IsFaulted)
                    {
                        ExceptionDispatchInfo.Capture(writeTask.Exception!.InnerException!).Throw();
                    }

                    throw new TimeoutException("HttpRequest: Request response timed out!", writeTask.Exception);
                }

                response = readTask.Result;
            }

            return ProcessResponse(response);
        }

        bool disposed;

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            client.Close();
        }
    }
}