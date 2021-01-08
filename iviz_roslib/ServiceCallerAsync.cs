using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.Rosapi;
using Iviz.XmlRpc;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.Roslib
{
    internal sealed class ServiceCallerAsync<T> : IServiceCaller where T : IService
    {
        const int DefaultTimeoutInMs = 5000;
        const int BufferSizeIncrease = 512;
        const byte ErrorByte = 0;

        readonly bool requestNoDelay;
        readonly ServiceInfo<T> serviceInfo;
        readonly TcpClient tcpClient;

        bool disposed;

        byte[] readBuffer = new byte[16];
        byte[] writeBuffer = new byte[16];

        public bool IsAlive => tcpClient.Connected;
        public string ServiceType => serviceInfo.Service;

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

        async Task SendHeaderAsync(NetworkStream stream, bool persistent, CancellationToken token)
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

            await Utils.WriteHeaderAsync(stream, contents, token).Caf();
        }

        async Task ProcessHandshake(NetworkStream stream, bool persistent, CancellationToken token)
        {
            await SendHeaderAsync(stream, persistent, token).Caf();

            int receivedLength = await ReceivePacketAsync(stream, token).Caf();
            if (receivedLength == -1)
            {
                throw new IOException("Connection closed during handshake");
            }

            List<string> responses = Utils.ParseHeader(readBuffer, receivedLength);
            if (responses.Count != 0 && responses[0].HasPrefix("error"))
            {
                int index = responses[0].IndexOf('=');
                throw new RosRpcException(index != -1
                    ? $"Failed handshake: {responses[0].Substring(index + 1)}"
                    : $"Failed handshake: {responses[0]}");
            }
        }

        public async Task StartAsync(Uri remoteUri, bool persistent, CancellationToken token = default)
        {
            string remoteHostname = remoteUri.Host;
            int remotePort = remoteUri.Port;
            Task task = tcpClient.ConnectAsync(remoteHostname, remotePort);
            if (!await task.WaitFor(DefaultTimeoutInMs, token) || !task.RanToCompletion())
            {
                if (task.IsFaulted)
                {
                    await task; // rethrow
                }

                throw new TimeoutException($"{this}: Host {remoteHostname}:{remotePort} timed out");
            }

            await ProcessHandshake(tcpClient.GetStream(), persistent, token);
        }

        public void Start(Uri remoteUri, bool persistent)
        {
            // just call the async version from sync
            Task.Run(async () => { await StartAsync(remoteUri, persistent).Caf(); }).WaitNoThrow(this);
        }

        async Task<int> ReceivePacketAsync(NetworkStream stream, CancellationToken token)
        {
            if (!await stream.ReadChunkAsync(readBuffer, 4, token))
            {
                return -1;
            }

            int length = BitConverter.ToInt32(readBuffer, 0);
            if (length == 0)
            {
                return 0;
            }

            if (readBuffer.Length < length)
            {
                readBuffer = new byte[length + BufferSizeIncrease];
            }

            if (!await stream.ReadChunkAsync(readBuffer, length, token))
            {
                return -1;
            }

            return length;
        }

        public async Task<bool> ExecuteAsync(T service, CancellationToken token)
        {
            bool success;
            try
            {
                success = await ExecuteImplAsync(service, token).Caf();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new RosRpcException("Error during service call", e);
            }

            return success;
        }

        public bool Execute(T service, CancellationToken token)
        {
            bool result = false;
            // just call the async version from sync
            Task.Run(async () => result = await ExecuteAsync(service, token).Caf(), token).Wait(token);
            return result;
        }

        async Task<bool> ExecuteImplAsync(T service, CancellationToken token)
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
            if (writeBuffer.Length < msgLength)
            {
                writeBuffer = new byte[msgLength + BufferSizeIncrease];
            }

            uint sendLength = Buffer.Serialize(requestMsg, writeBuffer);

            var stream = tcpClient.GetStream();
            await stream.WriteAsync(BitConverter.GetBytes(sendLength), 0, 4, token).Caf();
            await stream.WriteAsync(writeBuffer, 0, (int) sendLength, token).Caf();

            int rcvLengthH = await stream.ReadAsync(readBuffer, 0, 1, token);
            if (rcvLengthH == 0)
            {
                throw new IOException("Partner closed the connection");
            }

            byte statusByte = readBuffer[0];

            int rcvLength = await ReceivePacketAsync(stream, token);
            if (rcvLength == -1)
            {
                throw new IOException("Partner closed the connection");
            }

            if (statusByte == ErrorByte)
            {
                Logger.LogFormat(Utils.GenericExceptionFormat, this, BuiltIns.UTF8.GetString(readBuffer, 0, rcvLength));
                return false;
            }

            service.Response = Buffer.Deserialize(service.Response, readBuffer, rcvLength);
            return true;
        }
    }
}