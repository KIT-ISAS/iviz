#define DEBUG__

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.RoslibSharp
{
    class ServiceSender
    {
        const int BufferSizeIncrease = 1024;

        byte[] readBuffer = new byte[1024];
        byte[] writeBuffer = new byte[1024];

        readonly BinaryReader reader;
        readonly BinaryWriter writer;
        readonly TcpClient tcpClient;
        readonly IPEndPoint remoteEndPoint;
        readonly Action<IService> callback;
        readonly Task task;
        bool keepRunning;

        public int NumReceived { get; private set; }
        public int BytesReceived { get; private set; }
        public int NumSent { get; private set; }
        public int BytesSent { get; private set; }
        public bool IsAlive => task != null && !task.IsCompleted && !task.IsFaulted;

        public string Service => ServiceInfo.Service;
        public int Port => remoteEndPoint.Port;
        public string Hostname => remoteEndPoint.Address.ToString();

        internal ServiceInfo ServiceInfo { get; }
        public string RemoteCallerId { get; private set; }

        internal ServiceSender(ServiceInfo serviceInfo, TcpClient tcpClient, Action<IService> callback)
        {
            NetworkStream stream = tcpClient.GetStream();
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);
            this.tcpClient = tcpClient;
            this.callback = callback;
            remoteEndPoint = (IPEndPoint)tcpClient.Client.RemoteEndPoint;
            ServiceInfo = serviceInfo;

            keepRunning = true;
            task = Task.Run(Run);
        }

        public void Stop()
        {
            keepRunning = false;
            tcpClient?.Close();
            if (!task.IsCompleted)
            {
                task.Wait();
            }
        }

        int ReceivePacket()
        {
            int numRead = 0;
            while (numRead < 4)
            {
                int readNow = reader.Read(readBuffer, numRead, 4 - numRead);
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
                int readNow = reader.Read(readBuffer, numRead, length - numRead);
                if (readNow == 0)
                {
                    return 0;
                }
                numRead += readNow;
            }
            return length;
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
#if DEBUG__
                Logger.Log("<<< " + entry);
#endif
                contents.Add(entry);
            }
            return contents;
        }

        string ProcessRemoteHeader(List<string> fields)
        {
            if (fields.Count < 3)
            {
                return "error=Expected at least 3 fields, closing connection";
            }

            Dictionary<string, string> values = new Dictionary<string, string>();
            for (int i = 0; i < fields.Count; i++)
            {
                int index = fields[i].IndexOf('=');
                if (index < 0)
                {
                    return $"error=Invalid field '{fields[i]}'";
                }
                string key = fields[i].Substring(0, index);
                values[key] = fields[i].Substring(index + 1);
            }

            if (!values.TryGetValue("callerid", out string receivedId))
            {
                return $"error=Expected callerid '{RemoteCallerId}' but received instead '{receivedId}', closing connection";
            }
            RemoteCallerId = receivedId;

            if (!values.TryGetValue("service", out string receivedService) || receivedService != ServiceInfo.Service)
            {
                return $"error=Expected service '{ServiceInfo.Service}' but received instead '{receivedService}', closing connection";
            }
            if (!values.TryGetValue("md5sum", out string receivedMd5sum) || receivedMd5sum != ServiceInfo.Md5Sum)
            {
                if (receivedMd5sum == "*")
                {
                    Logger.LogDebug($"{this}: Expected md5 '{ServiceInfo.Md5Sum}' but received instead '{receivedMd5sum}'. Continuing...");
                }
                else
                {
                    return $"error=Expected md5 '{ServiceInfo.Md5Sum}' but received instead '{receivedMd5sum}', closing connection";
                }
            }
            if (values.TryGetValue("type", out string receivedType) && receivedType != ServiceInfo.Type)
            {
                if (receivedType == "*")
                {
                    Logger.LogDebug($"{this}: Expected type '{ServiceInfo.Type}' but received instead '{receivedType}'. Continuing...");
                }
                else
                {
                    return $"error=Expected type '{ServiceInfo.Type}' but received instead '{receivedType}', closing connection";
                }
            }
            if (values.TryGetValue("tcp_nodelay", out string receivedNoDelay) && receivedNoDelay == "1")
            {
                tcpClient.NoDelay = true;
                Logger.LogDebug($"{this}: requested tcp_nodelay");

            }
            return null;
        }

        int SendResponseHeader(string errorMessage)
        {
            string[] contents;
            if (errorMessage != null)
            {
                contents = new[] {
                    errorMessage,
                    $"callerid={ServiceInfo.CallerId}",
                };
            }
            else
            {
                contents = new[] {
                    $"callerid={ServiceInfo.CallerId}",
                };
            }
            int totalLength = 4 * contents.Length;
            for (int i = 0; i < contents.Length; i++)
            {
                totalLength += contents[i].Length;
            }
            writer.Write(totalLength);
            for (int i = 0; i < contents.Length; i++)
            {
                writer.Write(contents[i].Length);
                writer.Write(BuiltIns.UTF8.GetBytes(contents[i]));
#if DEBUG__
                Logger.Log(">>> " + contents[i]);
#endif
            }
            return totalLength;
        }

        bool DoHandshake()
        {
            int totalLength = ReceivePacket();
            List<string> fields = ParseHeader(totalLength);
            string errorMessage = ProcessRemoteHeader(fields);

            if (errorMessage != null)
            {
                Logger.Log($"{this}: Failed handshake\n{errorMessage}");
            }
            SendResponseHeader(errorMessage);

            return errorMessage == null;
        }

        void Run()
        {
            try
            {
                if (!DoHandshake())
                {
                    keepRunning = false;
                }
            }
            catch (Exception e)
            {
                Logger.Log($"{this}: {e}");
            }

            while (keepRunning)
            {
                try
                {
                    int rcvLength = ReceivePacket();
                    if (rcvLength == 0)
                    {
                        Logger.LogDebug($"{this}: closed remotely.");
                        break;
                    }

                    IService serviceMsg = ServiceInfo.Generator.Create();
                    BuiltIns.Deserialize(serviceMsg.Request, readBuffer, rcvLength);
                    NumReceived++;
                    BytesReceived += rcvLength + 4;

                    const byte ErrorByte = 0;
                    const byte SuccessByte = 1;

                    byte resultStatus;
                    string errorMessage;

                    callback(serviceMsg);

                    IResponse responseMsg = serviceMsg.Response;
                    if (responseMsg == null)
                    {
                        resultStatus = ErrorByte;
                        errorMessage = serviceMsg.ErrorMessage ?? "Callback function returned null";
                    }
                    else if (serviceMsg.ErrorMessage != null)
                    {
                        resultStatus = ErrorByte;
                        errorMessage = serviceMsg.ErrorMessage;
                    }
                    else
                    {
                        resultStatus = SuccessByte;
                        errorMessage = null;
                    }

                    if (resultStatus == SuccessByte)
                    {
                        int msgLength = responseMsg.RosMessageLength;
                        if (writeBuffer.Length < msgLength)
                        {
                            writeBuffer = new byte[msgLength + BufferSizeIncrease];
                        }
                        uint sendLength = BuiltIns.Serialize(responseMsg, writeBuffer);
                        writer.Write(SuccessByte);
                        writer.Write(sendLength);
                        writer.Write(writeBuffer, 0, (int)sendLength);
                        BytesSent += (int)sendLength + 5;
                    }
                    else
                    {
                        writer.Write(ErrorByte);
                        writer.Write(errorMessage.Length);
                        writer.Write(errorMessage);
                        BytesSent += errorMessage.Length + 5;
                        Logger.Log(errorMessage);
                    }
                    NumSent++;
                }
                catch (NullReferenceException e)
                {
                    Logger.LogError($"{this}: {e}");
                }
                catch (Exception e) when (e is ArgumentException || e is IndexOutOfRangeException)
                {
                    Logger.Log($"{this}: {e}");
                }
            }
            tcpClient.Close();
        }

        public override string ToString()
        {
            return $"[TcpSender {Hostname}:{Port} '{Service}' >>'{RemoteCallerId}']";
        }

    }
}
