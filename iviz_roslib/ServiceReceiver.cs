//#define DEBUG__

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.Roslib
{
    internal interface IServiceReceiver
    {
        string ServiceType { get; }
        void Stop();
    }
    
    internal sealed class ServiceReceiver<T> : IServiceReceiver, IDisposable where T : IService
    {
        const int BufferSizeIncrease = 512;
        const byte ErrorByte = 0;

        readonly bool persistent;
        readonly IPEndPoint remoteEndPoint;
        readonly bool requestNoDelay;
        readonly ServiceInfo<T> serviceInfo;
        readonly NetworkStream stream;
        readonly TcpClient tcpClient = new TcpClient();

        bool disposed;

        byte[] readBuffer = new byte[16];
        byte[] writeBuffer = new byte[16];

        public bool IsAlive => tcpClient.Connected;
        public string ServiceType => serviceInfo.Service;

        public ServiceReceiver(
            ServiceInfo<T> serviceInfo,
            Uri remoteUri,
            bool requestNoDelay,
            bool persistent)
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
            remoteEndPoint = (IPEndPoint) tcpClient.Client.RemoteEndPoint;
        }

        int Port => remoteEndPoint.Port;
        string Hostname => remoteEndPoint.Address.ToString();

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

        async Task SerializeHeaderAsync()
        {
            string[] contents =
            {
                $"callerid={serviceInfo.CallerId}",
                $"service={serviceInfo.Service}",
                $"md5sum={serviceInfo.Md5Sum}",
                $"type={serviceInfo.Type}",
                $"tcp_nodelay={(requestNoDelay ? "1" : "0")}",
                $"persistent={(persistent ? "1" : "0")}"
            };
            
            int totalLength = 4 * contents.Length + contents.Sum(entry => entry.Length);

            byte[] array = new byte[totalLength + 4];
            using (BinaryWriter writer = new BinaryWriter(new MemoryStream(array)))
            {
                writer.Write(totalLength);
                foreach (string t in contents)
                {
                    writer.Write(t.Length);
                    writer.Write(BuiltIns.UTF8.GetBytes(t));

#if DEBUG__
                Logger.Log(">>> " + contents[i]);
#endif
                }
            }

            await stream.WriteAsync(array, 0, array.Length);
        }

        List<string> ParseHeader(int totalLength)
        {
            int numRead = 0;

            List<string> contents = new List<string>();
            while (numRead < totalLength)
            {
                int length = BitConverter.ToInt32(readBuffer, numRead);
                numRead += 4;
                string entry = BuiltIns.UTF8.GetString(readBuffer, numRead, length);
                numRead += length;
                contents.Add(entry);

#if DEBUG__
                Logger.Log("<<< " + contents.Last());
#endif
            }

            return contents;
        }

        public async Task StartAsync()
        {
            await SerializeHeaderAsync();

            int totalLength = await ReceivePacketAsync();
            List<string> responses = ParseHeader(totalLength);

            if (responses.Count == 0 || !responses[0].HasPrefix("error"))
            {
                return;
            }

            tcpClient.Close();

            int index = responses[0].IndexOf('=');
            throw new RosRpcException(index != -1
                ? $"Error: {responses[0].Substring(index + 1)}"
                : $"Error:{responses[0]}");
        }

        public void Start()
        {
            // we cheat here and just call the async version from sync
            Task.Run(async () => { await StartAsync().Caf(); }).Wait();
        }

        async Task<int> ReceivePacketAsync()
        {
            int numRead = 0;
            while (numRead < 4)
            {
                int readNow = await stream.ReadAsync(readBuffer, numRead, 4 - numRead);
                if (readNow == 0)
                {
                    return 0;
                }

                numRead += readNow;
            }

            int length = BitConverter.ToInt32(readBuffer, 0);
            if (readBuffer.Length < length)
            {
                readBuffer = new byte[length + BufferSizeIncrease];
            }

            numRead = 0;
            while (numRead < length)
            {
                int readNow = await stream.ReadAsync(readBuffer, numRead, length - numRead);
                if (readNow == 0)
                {
                    return 0;
                }

                numRead += readNow;
            }

            return length;
        }

        int ReceivePacket()
        {
            int numRead = 0;
            while (numRead < 4)
            {
                int readNow = stream.Read(readBuffer, numRead, 4 - numRead);
                if (readNow == 0)
                {
                    return 0;
                }

                numRead += readNow;
            }

            int length = BitConverter.ToInt32(readBuffer, 0);
            if (readBuffer.Length < length)
            {
                readBuffer = new byte[length + BufferSizeIncrease];
            }

            numRead = 0;
            while (numRead < length)
            {
                int readNow = stream.Read(readBuffer, numRead, length - numRead);
                if (readNow == 0)
                {
                    return 0;
                }

                numRead += readNow;
            }

            return length;
        }


        public async Task<bool> ExecuteAsync(T service)
        {
            bool success;
            try
            {
                success = await ExecuteImplAsync(service);
            }
            catch (Exception e)
            {
                service.ErrorMessage = e.Message;
                Logger.Log("ServiceReceiver: Error during service call:" + e);
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
            bool success;
            try
            {
                success = ExecuteImpl(service);
            }
            catch (Exception e)
            {
                service.ErrorMessage = e.Message;
                Logger.Log("ServiceReceiver: Error during service call:" + e);
                success = false;
            }

            if (!persistent)
            {
                Dispose();
            }

            return success;
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
            await stream.WriteAsync(BitConverter.GetBytes(sendLength), 0, 4);
            await stream.WriteAsync(writeBuffer, 0, (int) sendLength);

            int rcvLengthH = await stream.ReadAsync(readBuffer, 0, 1);
            if (rcvLengthH == 0)
            {
                service.ErrorMessage = $"Connection to {Hostname}:{Port} closed remotely.";
                return false;
            }

            byte statusByte = readBuffer[0];

            int rcvLength = await ReceivePacketAsync();
            if (rcvLength == 0)
            {
                service.ErrorMessage = $"Connection to {Hostname}:{Port} closed remotely.";
                return false;
            }

            if (statusByte == ErrorByte)
            {
                service.ErrorMessage = BuiltIns.UTF8.GetString(readBuffer, 0, rcvLength);
                return false;
            }

            service.ErrorMessage = null;
            service.Response = Buffer.Deserialize(service.Response, readBuffer, rcvLength);
            return true;
        }

        bool ExecuteImpl(T service)
        {
            IRequest requestMsg = service.Request;
            int msgLength = requestMsg.RosMessageLength;
            if (writeBuffer.Length < msgLength)
            {
                writeBuffer = new byte[msgLength + BufferSizeIncrease];
            }

            uint sendLength = Buffer.Serialize(requestMsg, writeBuffer);
            stream.Write(BitConverter.GetBytes(sendLength), 0, 4);
            stream.Write(writeBuffer, 0, (int) sendLength);

            int rcvLengthH = stream.Read(readBuffer, 0, 1);
            if (rcvLengthH == 0)
            {
                service.ErrorMessage = $"Connection to {Hostname}:{Port} closed remotely.";
                return false;
            }

            byte statusByte = readBuffer[0];

            int rcvLength = ReceivePacket();
            if (rcvLength == 0)
            {
                service.ErrorMessage = $"Connection to {Hostname}:{Port} closed remotely.";
                return false;
            }

            if (statusByte == ErrorByte)
            {
                service.ErrorMessage = BuiltIns.UTF8.GetString(readBuffer, 0, rcvLength);
                return false;
            }

            service.ErrorMessage = null;
            service.Response = Buffer.Deserialize(service.Response, readBuffer, rcvLength);
            return true;
        }

        public void Stop()
        {
            tcpClient.Close();
        }
    }
}