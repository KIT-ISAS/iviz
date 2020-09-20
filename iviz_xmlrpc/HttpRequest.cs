using System;
using System.IO;
using System.Net.Sockets;
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

        public HttpRequest(Uri callerUri, Uri uri, int timeoutInMs = DefaultTimeoutInMs)
        {
            this.callerUri = callerUri ?? throw new ArgumentNullException(nameof(callerUri));
            this.uri = uri ?? throw new ArgumentNullException(nameof(uri));
            string hostname = uri.Host;
            int port = uri.Port;

            client = new TcpClient();
            Task task = client.ConnectAsync(hostname, port);
            if (!task.Wait(timeoutInMs) || task.IsCanceled)
            {
                throw new TimeoutException($"HttpRequest: Host '{hostname}' timed out");
            }
            if (task.IsFaulted)
            {
                throw new TimeoutException($"HttpRequest: Host '{hostname}' timed out", task.Exception);
            }
        }

        public async Task<string> Request(string msgIn, int timeoutInMs = DefaultTimeoutInMs)
        {
            if (msgIn is null)
            {
                throw new ArgumentNullException(nameof(msgIn));
            }

            StreamWriter writer = new StreamWriter(client.GetStream(), BuiltIns.UTF8)
            {
                NewLine = "\r\n"
            };

            string path = uri.AbsolutePath;
            await writer.WriteLineAsync($"POST {path} HTTP/1.0");
            await writer.WriteLineAsync($"User-Agent: iviz XML-RPC");
            await writer.WriteLineAsync($"Host: {callerUri.Host}");
            await writer.WriteLineAsync($"Content-Length: {BuiltIns.UTF8.GetByteCount(msgIn)}");
            await writer.WriteLineAsync($"Content-Type: text/xml; charset=utf-8");
            await writer.WriteLineAsync();
            await writer.WriteAsync(msgIn);
            await writer.FlushAsync();

            StreamReader reader = new StreamReader(client.GetStream(), BuiltIns.UTF8);
            reader.BaseStream.ReadTimeout = timeoutInMs;

            string response;

            //Logger.Log("Start: " + DateTime.Now);
            try
            {
                response = await reader.ReadToEndAsync();
            }
            catch (IOException e)
            {
                //Logger.Log("End: " + DateTime.Now);
                throw new TimeoutException("HttpRequest: Request response timed out!", e);
            }

            int index = response.IndexOf("\r\n\r\n", StringComparison.InvariantCulture);
            if (index == -1)
            {
                index = response.IndexOf("\n\n", StringComparison.InvariantCulture);
                if (index == -1)
                {
                    throw new ParseException($"Cannot find double line-end in HTTP header (received {response.Length} bytes)");
                }
                index += 2;
            }
            else
            {
                index += 4;
            }

            reader.Close();
            writer.Close();

            return response.Substring(index);
        }

        bool disposed;
        public void Dispose()
        {
            if (!disposed)
            {
                client.Close();
                disposed = true;
            }
        }
    }
}
