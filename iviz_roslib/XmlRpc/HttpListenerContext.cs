using System;
using System.IO;
using System.Net.Sockets;
using Iviz.Msgs;

namespace Iviz.RoslibSharp.XmlRpc
{
    public sealed class HttpListenerContext : IDisposable
    {
        readonly TcpClient client;

        public HttpListenerContext(TcpClient client)
        {
            this.client = client;
        }

        public string GetRequest(int timeoutInMs = 2000)
        {
            StreamReader stream = new StreamReader(client.GetStream(), BuiltIns.UTF8);
            stream.BaseStream.ReadTimeout = timeoutInMs;
            int length = -1;
            while (true)
            {
                string line = stream.ReadLine();
                if (CheckHeaderLine(line, "Content-Length", out string lengthStr))
                {
                    if (!int.TryParse(lengthStr, out length))
                    {
                        throw new ParseException("Cannot parse length '" + lengthStr + "'");
                    }
                }
                else if (line.Length == 0 || line == "\r")
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
                numRead += stream.Read(buffer, 0, length -  numRead);
            }
            return new string(buffer, 0, numRead);
        }

        static bool CheckHeaderLine(string line, string key, out string value)
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

        public void Respond(string msgOut)
        {
            if (msgOut is null)
            {
                throw new ArgumentNullException(nameof(msgOut));
            }

            StreamWriter writer = new StreamWriter(client.GetStream(), BuiltIns.UTF8)
            {
                NewLine = "\r\n"
            };

            writer.WriteLine($"HTTP/1.0 200 OK");
            writer.WriteLine($"Server: iviz XML-RPC");
            writer.WriteLine($"Connection: close");
            writer.WriteLine($"Content-Type: text/xml; charset=utf-8");
            writer.WriteLine($"Content-Length: {BuiltIns.UTF8.GetByteCount(msgOut)}");
            writer.WriteLine();
            writer.Write(msgOut);
            writer.Close();
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
