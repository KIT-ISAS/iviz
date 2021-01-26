using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;
using Nito.AsyncEx.Synchronous;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.Roslib
{
    /// <summary>
    /// Class in charge of responding to service request received by a server
    /// </summary>
    /// <typeparam name="TService">The service type</typeparam>
    internal sealed class ServiceRequestAsync<TService> where TService : IService
    {
        const int BufferSizeIncrease = 1024;

        const byte ErrorByte = 0;
        const byte SuccessByte = 1;

        readonly Func<TService, Task> callback;
        readonly Endpoint remoteEndPoint;
        readonly ServiceInfo<TService> serviceInfo;
        readonly NetworkStream stream;
        readonly Task task;
        readonly TcpClient tcpClient;

        bool keepRunning;
        string? remoteCallerId;
        byte[] readBuffer = new byte[1024];
        byte[] writeBuffer = new byte[1024];

        internal ServiceRequestAsync(ServiceInfo<TService> serviceInfo, TcpClient tcpClient, Endpoint remoteEndPoint,
            Func<TService, Task> callback)
        {
            stream = tcpClient.GetStream();
            this.tcpClient = tcpClient;
            this.callback = callback;
            this.remoteEndPoint = remoteEndPoint;
            this.serviceInfo = serviceInfo;

            keepRunning = true;
            task = Task.Run(Run);
        }

        public bool IsAlive => !task.IsCompleted;
        string Service => serviceInfo.Service;
        int Port => remoteEndPoint.Port;
        public string Hostname => remoteEndPoint.Hostname;

        public Task StopAsync()
        {
            keepRunning = false;
            tcpClient.Close();
            return task.WaitForWithTimeout(2000).AwaitNoThrow(this);
        }

        async Task<int> ReceivePacket()
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

        string? ProcessRemoteHeader(List<string> fields)
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

            if (!values.TryGetValue("callerid", out string? receivedId))
            {
                return
                    $"error=Expected callerid '{remoteCallerId}' but partner provided '{receivedId}', closing connection";
            }

            remoteCallerId = receivedId;

            if (!values.TryGetValue("service", out string? receivedService) || receivedService != serviceInfo.Service)
            {
                return
                    $"error=Expected service '{serviceInfo.Service}' but partner provided '{receivedService}', closing connection";
            }

            if (!values.TryGetValue("md5sum", out string? receivedMd5Sum) || receivedMd5Sum != serviceInfo.Md5Sum)
            {
                if (receivedMd5Sum == "*")
                {
                    // OK?
                }
                else
                {
                    return
                        $"error=Expected md5 '{serviceInfo.Md5Sum}' but partner provided '{receivedMd5Sum}', closing connection";
                }
            }

            if (values.TryGetValue("type", out string? receivedType) && receivedType != serviceInfo.Type)
            {
                if (receivedType == "*")
                {
                    // OK?
                }
                else
                {
                    return
                        $"error=Expected type '{serviceInfo.Type}' but received instead '{receivedType}', closing connection";
                }
            }

            if (values.TryGetValue("tcp_nodelay", out string? receivedNoDelay) && receivedNoDelay == "1")
            {
                tcpClient.NoDelay = true;
            }

            return null;
        }

        async Task SendHeader(string? errorMessage)
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

            await Utils.WriteHeaderAsync(stream!, contents).Caf();
        }

        async Task ProcessHandshake()
        {
            int totalLength = await ReceivePacket().Caf();
            if (totalLength == -1)
            {
                throw new IOException("Connection closed during handshake.");
            }

            List<string> fields = Utils.ParseHeader(readBuffer, totalLength);
            string? errorMessage = ProcessRemoteHeader(fields);

            await SendHeader(errorMessage).Caf();

            if (errorMessage != null)
            {
                throw new RosRpcException($"Failed handshake: {errorMessage}");
            }
        }

        async Task Run()
        {
            try
            {
                await ProcessHandshake().Caf();
            }
            catch (Exception e)
            {
                Logger.LogErrorFormat("{0}: Error in ServiceRequestAsync: {1}", this, e);
                keepRunning = false;
                return;
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
                    if (rcvLength == -1)
                    {
                        Logger.LogDebugFormat("{0}: closed remotely.", this);
                        break;
                    }

                    TService serviceMsg = (TService) serviceInfo.Generator.Create();
                    serviceMsg.Request = Buffer.Deserialize(serviceMsg.Request, readBuffer, rcvLength);

                    byte resultStatus;
                    string? errorMessage;
                    bool errorInResponse;

                    try
                    {
                        await callback(serviceMsg).Caf();
                        serviceMsg.Response.RosValidate();
                        errorInResponse = false;
                    }
                    catch (Exception e)
                    {
                        Logger.LogErrorFormat("{0}: Inner exception in service callback: {1}", this, e);
                        errorInResponse = true;
                    }

                    IResponse responseMsg = serviceMsg.Response;
                    if (errorInResponse)
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
                    if (errorMessage == null)
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
                    Logger.LogFormat(Utils.GenericExceptionFormat, this, e);
                }
            }

            tcpClient.Close();
        }

        public override string ToString()
        {
            return $"[ServiceRequest {Hostname}:{Port} '{Service}' >>'{remoteCallerId}']";
        }
    }
}