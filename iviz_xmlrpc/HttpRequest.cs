using System;
using System.IO;
using System.IO.Compression;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Tools;

namespace Iviz.XmlRpc
{
    /// <summary>
    /// Handler for an HTTP request that was sent by us to another server.
    /// </summary>
    public sealed class HttpRequest : IDisposable
    {
        readonly Uri callerUri;
        readonly Uri remoteUri;
        readonly TcpClient client;
        bool disposed;

        public bool IsAlive => !disposed && client.Client.CheckIfAlive();

        public HttpRequest(Uri callerUri, Uri remoteUri)
        {
            this.callerUri = callerUri ?? throw new ArgumentNullException(nameof(callerUri));
            this.remoteUri = remoteUri ?? throw new ArgumentNullException(nameof(remoteUri));
            client = new TcpClient(AddressFamily.InterNetworkV6) { Client = { DualMode = true }, NoDelay = true };
        }

        public Task StartAsync(CancellationToken token)
        {
            return client.TryConnectAsync(remoteUri.Host, remoteUri.Port, token);
        }

        string CreateRequest(string msgIn, bool keepAlive)
        {
            return $"POST {Uri.UnescapeDataString(remoteUri.AbsolutePath)} " +
                   (keepAlive ? "HTTP/1.1\r\n" : "HTTP/1.0\r\n") +
                   "User-Agent: iviz XML-RPC\r\n" +
                   $"Host: {callerUri.Host}\r\n" +
                   $"Content-Length: {Defaults.UTF8.GetByteCount(msgIn).ToString()}\r\n" +
                   "Accept-Encoding: gzip\r\n" +
                   "Content-Type: text/xml; charset=utf-8\r\n" +
                   $"\r\n{msgIn}";
        }

        (string, Rent<byte>, int length) CreateRequestGzipped(string msgIn, bool keepAlive = false)
        {
            using var srcBytes = msgIn.AsRent();
            var dstBytes = new Rent<byte>(srcBytes.Length);

            using var outputStream = new MemoryStream(dstBytes.Array);
            using (var compressionStream = new GZipStream(outputStream, CompressionMode.Compress, true))
            {
                compressionStream.Write(srcBytes.Array, 0, srcBytes.Length);
            }

            string header =
                $"POST {Uri.UnescapeDataString(remoteUri.AbsolutePath)} " +
                (keepAlive ? "HTTP/1.1\r\n" : "HTTP/1.0\r\n") +
                "User-Agent: iviz XML-RPC\r\n" +
                $"Host: {callerUri.Host}\r\n" +
                "Accept-Encoding: gzip\r\n" +
                "Content-Encoding: gzip\r\n" +
                $"Content-Length: {outputStream.Position.ToString()}\r\n" +
                "Content-Type: text/xml; charset=utf-8\r\n" +
                "\r\n";

            return (header, dstBytes, (int)outputStream.Position);
        }

        internal async Task<int> SendRequestAsync(string msgIn, bool keepAlive, bool gzipped, CancellationToken token)
        {
            if (gzipped)
            {
                var (header, payloadBytes, length) = CreateRequestGzipped(msgIn, keepAlive);
                using (payloadBytes)
                {
                    using (var headerBytes = header.AsRent())
                    {
                        await client.WriteChunkAsync(headerBytes, token);
                    }

                    await client.WriteChunkAsync(payloadBytes.Array, length, token);
                }

                return length;
            }

            string content = CreateRequest(msgIn, keepAlive);
            
            using var contentBytes = content.AsRent();
            await client.WriteChunkAsync(contentBytes, token);

            return content.Length;
        }

        static async Task<int> ReadHeaderAsync(TcpClient client, byte[] buffer, CancellationToken token)
        {
            byte[] singleByte = new byte[1];
            int pos = 0;
            while (pos < buffer.Length)
            {
                buffer[pos] = await client.ReadChunkAsync(singleByte, 1, token)
                    ? singleByte[0]
                    : throw new IOException("Partner closed connection");

                if (pos > 4 &&
                    buffer[pos] == '\n' &&
                    buffer[pos - 3] == '\r' &&
                    buffer[pos - 2] == '\n' &&
                    buffer[pos - 1] == '\r')
                {
                    return pos + 1;
                }

                pos++;
            }

            throw new ParseException("End of header not found");
        }

        static (int, string?, bool) ParseHeader(TextReader reader, bool validateFirstLine)
        {
            int? length = null;
            string? encoding = null;
            bool? connectionClose = null;

            string? firstLine = reader.ReadLine();
            if (firstLine == null)
            {
                throw new ParseException("Failed to parse header lines");
            }

            if (validateFirstLine)
            {
                int firstSpace = firstLine.IndexOf(' ');
                if (firstSpace < 0
                    || firstSpace + 3 >= firstLine.Length
                    || firstLine[firstSpace + 1] != '2'
                    || firstLine[firstSpace + 2] != '0'
                    || firstLine[firstSpace + 3] != '0')
                {
                    throw new HttpConnectionException($"Request failed with header: {firstLine}");
                }
            }

            while (true)
            {
                string? line = reader.ReadLine();

                if (line == null)
                {
                    throw new ParseException("Failed to parse header lines");
                }

                if (length == null && CheckHeaderLineForKey(line, "Content-Length", out string? lengthStr) &&
                    int.TryParse(lengthStr, out int lengthVal))
                {
                    length = lengthVal;
                }
                else if (encoding == null &&
                         CheckHeaderLineForKey(line, "Content-Encoding", out string? tmpEncodingStr))
                {
                    encoding = tmpEncodingStr;
                }
                else if (connectionClose == null &&
                         CheckHeaderLineForKey(line, "Connection", out string? connectionStr) &&
                         connectionStr.Equals("close", StringComparison.InvariantCultureIgnoreCase))
                {
                    connectionClose = true;
                }
                else if (string.IsNullOrEmpty(line) || line == "\r")
                {
                    break;
                }
            }

            if (length is null or < 0)
            {
                throw new ParseException("Content-Length not found in HTTP header");
            }

            return (length.Value, encoding, connectionClose ?? false);
        }

        internal Task<(string, int, bool)> GetResponseAsync(CancellationToken token)
        {
            return ReadIncomingDataAsync(client, true, token);
        }

        internal static async Task<(string inData, int length, bool shouldClose)>
            ReadIncomingDataAsync(TcpClient client, bool isRequest, CancellationToken token)
        {
            const int maxHeaderSize = 8192;
            int headerLength;
            int contentLength;
            string? encodingStr;
            bool connectionClose;

            using (var headerBytes = new Rent<byte>(maxHeaderSize))
            {
                headerLength = await ReadHeaderAsync(client, headerBytes.Array, token);
                using var reader = new StreamReader(new MemoryStream(headerBytes.Array, 0, headerLength));
                (contentLength, encodingStr, connectionClose) = ParseHeader(reader, isRequest);
            }

            using var content = new Rent<byte>(contentLength);
            if (!await client.ReadChunkAsync(content.Array, content.Length, token))
            {
                throw new IOException("Partner closed connection");
            }

            string result;
            if (encodingStr != null &&
                (encodingStr.Equals("gzip", StringComparison.InvariantCultureIgnoreCase) ||
                 encodingStr.Equals("x-gzip", StringComparison.InvariantCultureIgnoreCase)))
            {
                result = Decompress(content);
            }
            else
            {
                result = Defaults.UTF8.GetString(content.Array, 0, content.Length);
            }

            return (result, headerLength + contentLength, connectionClose);
        }

        static string Decompress(in Rent<byte> content)
        {
            try
            {
                // use a rented matrix, this lets us keep reusing the same buffer for all requests
                const int maxPayloadSize = 65536;
                using var inputStream = new MemoryStream(content.Array, 0, content.Length, false);
                using var outputBytes = new Rent<byte>(maxPayloadSize);
                using var outputStream = new MemoryStream(outputBytes.Array);
                using var decompressionStream = new GZipStream(inputStream, CompressionMode.Decompress);

                decompressionStream.CopyTo(outputStream);
                return Defaults.UTF8.GetString(outputBytes.Array, 0, (int)outputStream.Position);
            }
            catch (NotSupportedException)
            {
                // bigger than maxPayloadSize! we create a new array instead of renting
                using var inputStream = new MemoryStream(content.Array, 0, content.Length, false);
                using var outputStream = new MemoryStream();
                using var decompressionStream = new GZipStream(inputStream, CompressionMode.Decompress);

                decompressionStream.CopyTo(outputStream);
                return Defaults.UTF8.GetString(outputStream.GetBuffer(), 0, (int)outputStream.Position);
            }
        }

        static bool CheckHeaderLineForKey(string line, string key, out string value)
        {
            if (line.Length < key.Length + 1 ||
                string.Compare(line, 0, key, 0, key.Length, true, Defaults.Culture) != 0)
            {
                value = "";
                return false;
            }

            int start = key.Length + 1;
            if (start == line.Length)
            {
                value = "";
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


        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            client.Close();
        }

        public override string ToString()
        {
            return $"[HttpRequest uri={remoteUri}]";
        }
    }
}