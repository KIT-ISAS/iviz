

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;
using Nito.AsyncEx.Synchronous;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.Roslib
{
    internal sealed class ServiceCallerAsync<T> : IServiceCaller where T : IService
    {
        const int BufferSizeIncrease = 512;
        const byte ErrorByte = 0;

        readonly bool requestNoDelay;
        readonly ServiceInfo<T> serviceInfo;
        readonly NetworkStream stream;
        readonly TcpClient tcpClient = new TcpClient();

        bool disposed;

        byte[] readBuffer = new byte[16];
        byte[] writeBuffer = new byte[16];

        public bool IsAlive => tcpClient.Connected;
        public string ServiceType => serviceInfo.Service;

        public ServiceCallerAsync(ServiceInfo<T> serviceInfo, Uri remoteUri, bool requestNoDelay)
        {
            this.serviceInfo = serviceInfo;
            this.requestNoDelay = requestNoDelay;

            string remoteHostname = remoteUri.Host;
            int remotePort = remoteUri.Port;

            tcpClient.ReceiveTimeout = 5000;
            tcpClient.SendTimeout = 5000;

            tcpClient.Connect(remoteHostname, remotePort);
            stream = tcpClient.GetStream();
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            stream.Dispose();
            tcpClient.Dispose();
        }

        async Task SendHeaderAsync(bool persistent, CancellationToken token)
        {
            string[] contents =
            {
                $"callerid={serviceInfo.CallerId}",
                $"service={serviceInfo.Service}",
                $"md5sum={serviceInfo.Md5Sum}",
                $"type={serviceInfo.Type}",
                requestNoDelay ? "tcp_nodelay=1" : "tcp_nodelay=0",
                persistent ? "persistent=1" : "persistent=0",
            };

            await Utils.WriteHeaderAsync(stream, contents, token).Caf();
        }

        async Task ProcessHandshake(bool persistent, CancellationToken token)
        {
            await SendHeaderAsync(persistent, token).Caf();

            int receivedLength = await ReceivePacketAsync(token).Caf();
            if (receivedLength == -1)
            {
                throw new IOException("Connection closed during handshake");
            }

            List<string> responses = Utils.ParseHeader(readBuffer, receivedLength);
            if (responses.Count != 0 && responses[0].HasPrefix("error"))
            {
                tcpClient.Close();

                int index = responses[0].IndexOf('=');
                throw new RosRpcException(index != -1
                    ? $"Failed handshake: {responses[0].Substring(index + 1)}"
                    : $"Failed handshake: {responses[0]}");
            }
        }
        
        public async Task StartAsync(bool persistent, CancellationToken token = default)
        {
            await ProcessHandshake(persistent, token);
        }

        public void Start(bool persistent)
        {
            // just call the async version from sync
            Task.Run(async () => { await StartAsync(persistent).Caf(); }).Wait();
        }

        async Task<int> ReceivePacketAsync(CancellationToken token)
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
            // just call the async version from sync
            return Task.Run(async () => await ExecuteAsync(service, token).Caf(), token).WaitAndUnwrapException();
        }

        async Task<bool> ExecuteImplAsync(T service, CancellationToken token)
        {
            IRequest requestMsg = service.Request;
            int msgLength = requestMsg.RosMessageLength;
            if (writeBuffer.Length < msgLength)
            {
                writeBuffer = new byte[msgLength + BufferSizeIncrease];
            }

            uint sendLength = Buffer.Serialize(requestMsg, writeBuffer);
            await stream.WriteAsync(BitConverter.GetBytes(sendLength), 0, 4, token).Caf();
            await stream.WriteAsync(writeBuffer, 0, (int) sendLength, token).Caf();

            int rcvLengthH = await stream.ReadAsync(readBuffer, 0, 1, token);
            if (rcvLengthH == 0)
            {
                throw new IOException("Partner closed the connection");
            }

            byte statusByte = readBuffer[0];

            int rcvLength = await ReceivePacketAsync(token);
            if (rcvLength == -1)
            {
                throw new IOException("Partner closed the connection");
            }

            if (statusByte == ErrorByte)
            {
                Logger.LogFormat("{0}: {1}", this, BuiltIns.UTF8.GetString(readBuffer, 0, rcvLength));
                return false;
            }

            service.Response = Buffer.Deserialize(service.Response, readBuffer, rcvLength);
            return true;
        }
    }
}