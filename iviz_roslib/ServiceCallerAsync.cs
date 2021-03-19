using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib.Utils;
using Iviz.XmlRpc;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.Roslib
{
    internal sealed class ServiceCallerAsync<T> : IServiceCaller where T : IService
    {
        const int DefaultTimeoutInMs = 5000;
        const byte ErrorByte = 0;

        readonly bool requestNoDelay;
        readonly ServiceInfo<T> serviceInfo;
        readonly TcpClient tcpClient;

        bool disposed;

        public bool IsAlive => tcpClient.Connected;
        public string ServiceType => serviceInfo.Service;
        public Uri? RemoteUri { get; private set; }

        public ServiceCallerAsync(ServiceInfo<T> serviceInfo, bool requestNoDelay = true)
        {
            this.serviceInfo = serviceInfo;
            this.requestNoDelay = requestNoDelay;

            tcpClient = new TcpClient(AddressFamily.InterNetworkV6)
            {
                Client = {DualMode = true},
                ReceiveTimeout = DefaultTimeoutInMs,
                SendTimeout = DefaultTimeoutInMs
            };
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            tcpClient.Dispose();
        }

        Task SendHeaderAsync(NetworkStream stream, bool persistent, CancellationToken token)
        {
            string[] contents =
            {
                $"callerid={serviceInfo.CallerId}",
                $"service={serviceInfo.Service}",
                $"type={serviceInfo.Type}",
                $"md5sum={serviceInfo.Md5Sum}",
                requestNoDelay ? "tcp_nodelay=1" : "tcp_nodelay=0",
                persistent ? "persistent=1" : "persistent=0",
            };

            return stream.WriteHeaderAsync(contents, token);
        }

        async Task ProcessHandshakeAsync(NetworkStream stream, bool persistent, CancellationToken token)
        {
            await SendHeaderAsync(stream, persistent, token);

            List<string> responses;
            using (var readBuffer = await ReceivePacketAsync(stream, token))
            {
                responses = BaseUtils.ParseHeader(readBuffer);
            }

            if (responses.Count != 0 && responses[0].HasPrefix("error"))
            {
                int index = responses[0].IndexOf('=');
                throw new RosHandshakeException(index != -1
                    ? $"Failed handshake: {responses[0].Substring(index + 1)}"
                    : $"Failed handshake: {responses[0]}");
            }
        }

        public async Task StartAsync(Uri remoteUri, bool persistent, CancellationToken token)
        {
            RemoteUri = remoteUri;
            string remoteHostname = remoteUri.Host;
            int remotePort = remoteUri.Port;

            await tcpClient.TryConnectAsync(remoteHostname, remotePort, token, DefaultTimeoutInMs);
            await ProcessHandshakeAsync(tcpClient.GetStream(), persistent, token);
        }

        static async ValueTask<Rent<byte>> ReceivePacketAsync(NetworkStream stream, CancellationToken token)
        {
            byte[] lengthBuffer = new byte[4];
            if (!await stream.ReadChunkAsync(lengthBuffer, 4, token))
            {
                throw new IOException("Partner closed connection");
            }

            int length = BitConverter.ToInt32(lengthBuffer, 0);

            if (length == 0)
            {
                return Rent.Empty<byte>();
            }

            var readBuffer = new Rent<byte>(length);
            try
            {
                if (!await stream.ReadChunkAsync(readBuffer.Array, length, token))
                {
                    throw new IOException("Partner closed connection");
                }
            }
            catch (Exception)
            {
                readBuffer.Dispose();
                throw;
            }

            return readBuffer;
        }

        public async Task ExecuteAsync(T service, CancellationToken token)
        {
            if (tcpClient == null)
            {
                throw new InvalidOperationException("Service caller has not been started!");
            }

            if (service.Request == null)
            {
                throw new NullReferenceException("Request cannot be null");
            }

            service.Request.RosValidate();

            IRequest requestMsg = service.Request;
            int msgLength = requestMsg.RosMessageLength;

            using var writeBuffer = new Rent<byte>(msgLength);

            uint sendLength = Buffer.Serialize(requestMsg, writeBuffer.Array);

            var stream = tcpClient.GetStream();
            await stream.WriteChunkAsync(BitConverter.GetBytes(sendLength), 4, token);
            await stream.WriteChunkAsync(writeBuffer.Array, (int) sendLength, token);

            byte statusByte = await ReadOneByteAsync(stream, token);

            using var readBuffer = await ReceivePacketAsync(stream, token);

            if (statusByte == ErrorByte)
            {
                throw new RosServiceCallFailed(serviceInfo.Service,
                    BuiltIns.UTF8.GetString(readBuffer.Array, 0, readBuffer.Length));
            }

            service.Response = Buffer.Deserialize(service.Response, readBuffer.Array, readBuffer.Length);
        }

        static async Task<byte> ReadOneByteAsync(NetworkStream stream, CancellationToken token)
        {
            if (stream.DataAvailable)
            {
                return (byte) stream.ReadByte();
            }

            byte[] statusBuffer = new byte[1];
            if (!await stream.ReadChunkAsync(statusBuffer, 1, token))
            {
                throw new IOException("Partner closed the connection");
            }

            return statusBuffer[0];
        }
    }
}