using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.XmlRpc
{
    public sealed class HttpListenerContext : IDisposable
    {
        readonly TcpClient client;

        public HttpListenerContext(TcpClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<string> GetRequest(int timeoutInMs = 2000)
        {
            StreamReader stream = new StreamReader(client.GetStream(), BuiltIns.UTF8);
            stream.BaseStream.ReadTimeout = timeoutInMs;
            int length = -1;
            while (true)
            {
                string line = await stream.ReadLineAsync();
                if (CheckHeaderLine(line, "Content-Length", out string lengthStr))
                {
                    if (!int.TryParse(lengthStr, out length))
                    {
                        throw new ParseException("Cannot parse length '" + lengthStr + "'");
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
                numRead += await stream.ReadAsync(buffer, 0, length - numRead);
            }

            return new string(buffer, 0, numRead);
        }

        static bool CheckHeaderLine(string line, string key, out string value)
        {
            if (line is null ||
                line.Length < key.Length + 1 ||
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

        public async Task Respond(string msgOut)
        {
            if (msgOut is null)
            {
                throw new ArgumentNullException(nameof(msgOut));
            }

            StreamWriter writer = new StreamWriter(client.GetStream(), BuiltIns.UTF8)
            {
                NewLine = "\r\n"
            };

            await writer.WriteLineAsync("HTTP/1.0 200 OK");
            await writer.WriteLineAsync("Server: iviz XML-RPC");
            await writer.WriteLineAsync("Connection: close");
            await writer.WriteLineAsync("Content-Type: text/xml; charset=utf-8");
            await writer.WriteLineAsync($"Content-Length: {BuiltIns.UTF8.GetByteCount(msgOut)}");
            await writer.WriteLineAsync();
            await writer.WriteAsync(msgOut);
            writer.Close();
        }

        bool disposed;
        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            client.Close();
            disposed = true;
        }
    }
}