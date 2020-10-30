//#define DEBUG__

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.Roslib
{
    internal sealed class ServiceSenderAsync<T> where T : IService
    {
        const int BufferSizeIncrease = 1024;

        const byte ErrorByte = 0;
        const byte SuccessByte = 1;
        readonly Func<T, Task> callback;
        readonly IPEndPoint remoteEndPoint;
        readonly ServiceInfo<T> serviceInfo;
        readonly NetworkStream stream;
        readonly Task task;
        readonly TcpClient tcpClient;

        bool keepRunning;
        string remoteCallerId;
        byte[] readBuffer = new byte[1024];
        byte[] writeBuffer = new byte[1024];


        internal ServiceSenderAsync(ServiceInfo<T> serviceInfo, TcpClient tcpClient, Func<T, Task> callback)
        {
            stream = tcpClient.GetStream();
            this.tcpClient = tcpClient;
            this.callback = callback;
            remoteEndPoint = (IPEndPoint) tcpClient.Client.RemoteEndPoint;
            this.serviceInfo = serviceInfo;

            keepRunning = true;
            task = Task.Run(Run);
        }

        public bool IsAlive => task != null && !task.IsCompleted;
        string Service => serviceInfo.Service;
        int Port => remoteEndPoint.Port;
        public string Hostname => remoteEndPoint.Address.ToString();

        public void Stop()
        {
            keepRunning = false;
            tcpClient.Close();
            task.Wait();
        }

        public async Task StopAsync()
        {
            keepRunning = false;
            tcpClient.Close();
            await task.Caf();
        }

        async Task<int> ReceivePacket()
        {
            int numRead = 0;
            while (numRead < 4)
            {
                int readNow = await stream.ReadAsync(readBuffer, numRead, 4 - numRead).Caf();
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
                int readNow = await stream.ReadAsync(readBuffer, numRead, length - numRead).Caf();
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

        string ProcessRemoteHeader(IReadOnlyCollection<string> fields)
        {
            if (fields.Count < 3)
            {
                return "error=Expected at least 3 fields, closing connection";
            }

            Dictionary<string, string> values = new Dictionary<string, string>();
            foreach (string entry in fields)
            {
                int index = entry.IndexOf('=');
                if (index < 0)
                {
                    return $"error=Invalid field '{entry}'";
                }

                string key = entry.Substring(0, index);
                values[key] = entry.Substring(index + 1);
            }

            if (!values.TryGetValue("callerid", out string receivedId))
            {
                return
                    $"error=Expected callerid '{remoteCallerId}' but received instead '{receivedId}', closing connection";
            }

            remoteCallerId = receivedId;

            if (!values.TryGetValue("service", out string receivedService) || receivedService != serviceInfo.Service)
            {
                return
                    $"error=Expected service '{serviceInfo.Service}' but received instead '{receivedService}', closing connection";
            }

            if (!values.TryGetValue("md5sum", out string receivedMd5Sum) || receivedMd5Sum != serviceInfo.Md5Sum)
            {
                if (receivedMd5Sum == "*")
                {
                    Logger.LogDebug(
                        $"{this}: Expected md5 '{serviceInfo.Md5Sum}' but received instead '{receivedMd5Sum}'. Continuing...");
                }
                else
                {
                    return
                        $"error=Expected md5 '{serviceInfo.Md5Sum}' but received instead '{receivedMd5Sum}', closing connection";
                }
            }

            if (values.TryGetValue("type", out string receivedType) && receivedType != serviceInfo.Type)
            {
                if (receivedType == "*")
                {
                    Logger.LogDebug(
                        $"{this}: Expected type '{serviceInfo.Type}' but received instead '{receivedType}'. Continuing...");
                }
                else
                {
                    return
                        $"error=Expected type '{serviceInfo.Type}' but received instead '{receivedType}', closing connection";
                }
            }

            if (values.TryGetValue("tcp_nodelay", out string receivedNoDelay) && receivedNoDelay == "1")
            {
                tcpClient.NoDelay = true;
                Logger.LogDebug($"{this}: requested tcp_nodelay");
            }

            return null;
        }

        async Task SendResponseHeader(string errorMessage)
        {
            string[] contents;
            if (errorMessage != null)
            {
                contents = new[]
                {
                    errorMessage,
                    $"callerid={serviceInfo.CallerId}"
                };
            }
            else
            {
                contents = new[]
                {
                    $"callerid={serviceInfo.CallerId}"
                };
            }

            int totalLength = 4 * contents.Length;
            foreach (string entry in contents)
            {
                totalLength += entry.Length;
            }

            byte[] array = new byte[4 + totalLength];
            using (BinaryWriter writer = new BinaryWriter(new MemoryStream(array)))
            {
                writer.Write(totalLength);
                foreach (string entry in contents)
                {
                    writer.Write(entry.Length);
                    writer.Write(BuiltIns.UTF8.GetBytes(entry));
#if DEBUG__
                Logger.Log(">>> " + contents[i]);
#endif
                }
            }

            await stream.WriteAsync(array, 0, array.Length).Caf();
        }

        async Task<bool> DoHandshake()
        {
            int totalLength = await ReceivePacket().Caf();
            List<string> fields = ParseHeader(totalLength);
            string errorMessage = ProcessRemoteHeader(fields);

            if (errorMessage != null)
            {
                Logger.Log($"{this}: Failed handshake\n{errorMessage}");
            }

            await SendResponseHeader(errorMessage).Caf();

            return errorMessage == null;
        }

        async Task Run()
        {
            try
            {
                if (!await DoHandshake().Caf())
                {
                    keepRunning = false;
                }
            }
            catch (Exception e)
            {
                Logger.Log($"{this}: {e}");
            }

            byte[] lengthArray = new byte[4];

            byte[] ToLengthArray(uint i)
            {
                lengthArray[0] = (byte) i;
                lengthArray[1] = (byte) (i >> 8);
                lengthArray[2] = (byte) (i >> 0x10);
                lengthArray[3] = (byte) (i >> 0x18);
                return lengthArray;
            }

            byte[] statusByte = {0};
                
            while (keepRunning)
            {
                try
                {
                    int rcvLength = await ReceivePacket().Caf();
                    if (rcvLength == 0)
                    {
                        Logger.LogDebug($"{this}: closed remotely.");
                        break;
                    }

                    T serviceMsg = (T) serviceInfo.Generator.Create();
                    serviceMsg.Request = Buffer.Deserialize(serviceMsg.Request, readBuffer, rcvLength);

                    byte resultStatus;
                    string errorMessage;

                    try
                    {
                        await callback(serviceMsg).Caf();
                        serviceMsg.Response.RosValidate();
                    }
                    catch (Exception e)
                    {
                        Logger.LogError($"{this}: Inner exception in service callback: {e}");
                        serviceMsg.Response = null;
                    }

                    IResponse responseMsg = serviceMsg.Response;
                    if (responseMsg is null)
                    {
                        resultStatus = ErrorByte;
                        errorMessage = "Callback function returned null, an invalid value, or an exception happened.";
                    }
                    else
                    {
                        resultStatus = SuccessByte;
                        errorMessage = null;
                    }

                    statusByte[0] = resultStatus;
                    if (resultStatus == SuccessByte)
                    {
                        int msgLength = responseMsg.RosMessageLength;
                        if (writeBuffer.Length < msgLength)
                        {
                            writeBuffer = new byte[msgLength + BufferSizeIncrease];
                        }

                        uint sendLength = Buffer.Serialize(responseMsg, writeBuffer);

                        await stream.WriteAsync(statusByte, 0, 1).Caf();
                        await stream.WriteAsync(ToLengthArray(sendLength), 0, 4).Caf();
                        await stream.WriteAsync(writeBuffer, 0, (int) sendLength).Caf();
                    }
                    else
                    {
                        await stream.WriteAsync(statusByte, 0, 1).Caf();
                        await stream.WriteAsync(BitConverter.GetBytes(errorMessage.Length), 0, 4).Caf();

                        byte[] tmpBuffer = BuiltIns.UTF8.GetBytes(errorMessage);
                        await stream.WriteAsync(tmpBuffer, 0, tmpBuffer.Length).Caf();
                    }
                }
                catch (Exception e)
                {
                    Logger.Log($"{this}: {e}");
                }
            }

            tcpClient.Close();
        }

        public override string ToString()
        {
            return $"[TcpSender {Hostname}:{Port} '{Service}' >>'{remoteCallerId}']";
        }
    }
}