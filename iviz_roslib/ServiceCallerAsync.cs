

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;
using Nito.AsyncEx.Synchronous;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.Roslib
{
    internal sealed class ServiceCallerAsync<T> : IServiceCaller, IDisposable where T : IService
    {
        const int BufferSizeIncrease = 512;
        const byte ErrorByte = 0;

        readonly bool persistent;
        readonly bool requestNoDelay;
        readonly ServiceInfo<T> serviceInfo;
        readonly NetworkStream stream;
        readonly TcpClient tcpClient = new TcpClient();

        bool disposed;

        byte[] readBuffer = new byte[16];
        byte[] writeBuffer = new byte[16];

        public bool IsAlive => tcpClient.Connected;
        public string ServiceType => serviceInfo.Service;

        public ServiceCallerAsync(ServiceInfo<T> serviceInfo, Uri remoteUri, bool requestNoDelay, bool persistent)
        {
            this.serviceInfo = serviceInfo;
            this.requestNoDelay = requestNoDelay;
            this.persistent = persistent;

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

        async Task SendHeaderAsync()
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

            await Utils.WriteHeaderAsync(stream, contents).Caf();
        }
        
        public async Task StartAsync()
        {
            await SendHeaderAsync().Caf();

            int totalLength = await ReceivePacketAsync().Caf();
            if (totalLength == -1)
            {
                throw new IOException("Partner closed the connection");
            }

            List<string> responses = Utils.ParseHeader(readBuffer, totalLength);

            if (responses.Count == 0 || !responses[0].HasPrefix("error"))
            {
                return;
            }

            tcpClient.Close();

            int index = responses[0].IndexOf('=');
            throw new RosRpcException(index != -1
                ? $"Error: {responses[0].Substring(index + 1)}"
                : $"Error: {responses[0]}");
        }

        public void Start()
        {
            // just call the async version from sync
            Task.Run(async () => { await StartAsync().Caf(); }).WaitAndUnwrapException();
        }

        async Task<int> ReceivePacketAsync()
        {
            if (!await stream.ReadChunkAsync(readBuffer, 4))
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

            if (!await stream.ReadChunkAsync(readBuffer, length))
            {
                return -1;
            }

            return length;
        }

        public async Task<bool> ExecuteAsync(T service)
        {
            bool success;
            try
            {
                success = await ExecuteImplAsync(service).Caf();
            }
            catch (Exception e)
            {
                Logger.LogFormat("ServiceReceiver: Error during service call:{0}", e);
                success = false;
            }

            if (!persistent)
            {
                Dispose();
            }

            return success;
        }

        public bool Execute(T service)
        {
            // just call the async version from sync
            return Task.Run(async () => await ExecuteAsync(service).Caf()).WaitAndUnwrapException();
        }

        async Task<bool> ExecuteImplAsync(T service)
        {
            IRequest requestMsg = service.Request;
            int msgLength = requestMsg.RosMessageLength;
            if (writeBuffer.Length < msgLength)
            {
                writeBuffer = new byte[msgLength + BufferSizeIncrease];
            }

            uint sendLength = Buffer.Serialize(requestMsg, writeBuffer);
            await stream.WriteAsync(BitConverter.GetBytes(sendLength), 0, 4).Caf();
            await stream.WriteAsync(writeBuffer, 0, (int) sendLength).Caf();

            int rcvLengthH = await stream.ReadAsync(readBuffer, 0, 1);
            if (rcvLengthH == 0)
            {
                throw new IOException("Partner closed the connection");
            }

            byte statusByte = readBuffer[0];

            int rcvLength = await ReceivePacketAsync();
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

        public void Stop()
        {
            tcpClient.Close();
        }
    }
}