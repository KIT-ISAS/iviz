//#define DEBUG__

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    internal sealed class ServiceReceiverAsync : IDisposable
    {
        const int BufferSizeIncrease = 512;

        byte[] readBuffer = new byte[16];
        byte[] writeBuffer = new byte[16];

        readonly NetworkStream stream;
        readonly TcpClient tcpClient = new TcpClient();
        readonly IPEndPoint remoteEndPoint;
        readonly ServiceInfo serviceInfo;
        readonly bool requestNoDelay;
        readonly bool persistent;

        public string RemoteHostname { get; }
        public int RemotePort { get; }

        public int NumReceived { get; private set; }
        public int BytesReceived { get; private set; }
        public int NumSent { get; private set; }
        public int BytesSent { get; private set; }
        public bool IsAlive => tcpClient.Connected;

        public int Port => remoteEndPoint.Port;
        public string Hostname => remoteEndPoint.Address.ToString();

        public ServiceReceiverAsync(
            ServiceInfo serviceInfo,
            Uri remoteUri,
            bool requestNoDelay,
            bool persistent)
        {
            this.serviceInfo = serviceInfo;
            this.requestNoDelay = requestNoDelay;
            this.persistent = persistent;

            RemoteHostname = remoteUri.Host;
            RemotePort = remoteUri.Port;

            tcpClient.ReceiveTimeout = 5000;
            tcpClient.SendTimeout = 5000;

            tcpClient.Connect(RemoteHostname, RemotePort);
            stream = tcpClient.GetStream();
            remoteEndPoint = (IPEndPoint) tcpClient.Client.RemoteEndPoint;
        }

        async Task<int> SerializeHeader()
        {
            string[] contents =
            {
                $"callerid={serviceInfo.CallerId}",
                $"service={serviceInfo.Service}",
                $"md5sum={serviceInfo.Md5Sum}",
                $"type={serviceInfo.Type}",
                $"tcp_nodelay={(requestNoDelay ? "1" : "0")}",
                $"persistent={(persistent ? "1" : "0")}",
            };
            int totalLength = 4 * contents.Length;
            foreach (var entry in contents)
            {
                totalLength += entry.Length;
            }

            byte[] array = new byte[totalLength + 4];
            using (BinaryWriter writer = new BinaryWriter(new MemoryStream(array)))
            {
                writer.Write(totalLength);
                foreach (var t in contents)
                {
                    writer.Write(t.Length);
                    writer.Write(BuiltIns.UTF8.GetBytes(t));

#if DEBUG__
                Logger.Log(">>> " + contents[i]);
#endif
                }
            }

            await stream.WriteAsync(array, 0, array.Length);
            return totalLength;
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

        public async Task<bool> StartAsync()
        {
            await SerializeHeader();

            int totalLength = await ReceivePacket();
            List<string> responses = ParseHeader(totalLength);

            if (responses.Count == 0 || !responses[0].HasPrefix("error"))
            {
                return true;
            }

            int index = responses[0].IndexOf('=');
            if (index != -1)
            {
                Logger.Log($"{this}: Closing socket! Error:\n{responses[0].Substring(index + 1)}");
            }
            else
            {
                Logger.Log($"{this}: Closing socket! Error:\n{responses[0]}");
            }

            tcpClient.Close();
            return false;

        }

        async Task<int> ReceivePacket()
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

        public async Task<bool> ExecuteAsync(IService service)
        {
            bool success;
            try
            {
                success = await ExecuteImpl(service);
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

        const byte ErrorByte = 0;

        async Task<bool> ExecuteImpl(IService service)
        {
            IRequest requestMsg = service.Request;
            int msgLength = requestMsg.RosMessageLength;
            if (writeBuffer.Length < msgLength)
            {
                writeBuffer = new byte[msgLength + BufferSizeIncrease];
            }

            uint sendLength = Msgs.Buffer.Serialize(requestMsg, writeBuffer);
            await stream.WriteAsync(BitConverter.GetBytes(sendLength), 0, 4);
            await stream.WriteAsync(writeBuffer, 0, (int) sendLength);
            BytesSent += (int) sendLength + 5;

            int rcvLength = await stream.ReadAsync(readBuffer, 0, 1);
            if (rcvLength == 0)
            {
                service.ErrorMessage = $"Connection to {Hostname}:{Port} closed remotely.";
                return false;
            }

            BytesReceived++;

            byte statusByte = readBuffer[0];

            rcvLength = await ReceivePacket();
            if (rcvLength == 0)
            {
                service.ErrorMessage = $"Connection to {Hostname}:{Port} closed remotely.";
                return false;
            }

            BytesReceived += 4 + rcvLength;

            if (statusByte == ErrorByte)
            {
                service.ErrorMessage = BuiltIns.UTF8.GetString(readBuffer, 0, rcvLength);
                return false;
            }

            service.ErrorMessage = null;
            service.Response = Msgs.Buffer.Deserialize(service.Response, readBuffer, rcvLength);
            NumReceived++;
            return true;
        }

        bool disposed;
        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            tcpClient.Dispose();
        }
    }
}