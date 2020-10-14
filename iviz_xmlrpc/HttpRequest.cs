using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.StdMsgs;

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
            client = new TcpClient();
        }

        public void Start(int timeoutInMs = DefaultTimeoutInMs)
        {
            string hostname = uri.Host;
            int port = uri.Port;

            Task task = client.ConnectAsync(hostname, port);
            if (!task.Wait(timeoutInMs) || !task.IsCompleted) // will deadlock if called from Unity thread!
            {
                throw new TimeoutException($"HttpRequest: Host '{hostname}' timed out", task.Exception);
            }

            if (client.Client?.LocalEndPoint == null)
            {
                throw new RpcSocketException($"HttpRequest: Connection with '{hostname}' failed.");
            }            
        }

        public async Task StartAsync(int timeoutInMs = DefaultTimeoutInMs)
        {
            string hostname = uri.Host;
            int port = uri.Port;

            Task task = client.ConnectAsync(hostname, port);
            if (!await task.WaitFor(timeoutInMs) || !task.IsCompleted)
            {
                throw new TimeoutException($"HttpRequest: Host '{hostname}' timed out", task.Exception);
            }
            
            if (client.Client?.LocalEndPoint == null)
            {
                throw new RpcSocketException($"HttpRequest: Connection with '{hostname}' failed.");
            }            
        }

        string CreateRequest(string msgIn)
        {
            StringBuilder str = new StringBuilder();
            string path = Uri.UnescapeDataString(uri.AbsolutePath);
            str.Append($"POST {path} HTTP/1.0").Append("\r\n");
            str.Append($"User-Agent: iviz XML-RPC").Append("\r\n");
            str.Append($"Host: {callerUri.Host}").Append("\r\n");
            str.Append($"Content-Length: {BuiltIns.UTF8.GetByteCount(msgIn)}").Append("\r\n");
            str.Append($"Content-Type: text/xml; charset=utf-8").Append("\r\n");
            str.Append("\r\n");
            str.Append(msgIn).Append("\r\n");
            return str.ToString();
        }

        static string ProcessResponse(string response)
        {
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
        
        internal async Task<string> RequestAsync(string msgIn, int timeoutInMs = DefaultTimeoutInMs)
        {
            string response;
            using (Stream stream = client.GetStream())
            {
                StreamWriter writer = new StreamWriter(stream, BuiltIns.UTF8);
                await writer.WriteAsync(CreateRequest(msgIn)).Caf();
                await writer.FlushAsync().Caf();

                StreamReader reader = new StreamReader(stream, BuiltIns.UTF8);
                Task<string> readTask = reader.ReadToEndAsync();
                if (!await readTask.WaitFor(timeoutInMs) || !readTask.IsCompleted)
                {
                    reader.Close();
                    throw new TimeoutException("HttpRequest: Request response timed out!", readTask.Exception);
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
            client?.Close();
        }
    }
}