using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;
using Iviz.Roslib.Utils;
using Iviz.Tools;

namespace Iviz.Roslib
{
    /// <summary>
    /// Class in charge of responding to service request received by a server
    /// </summary>
    /// <typeparam name="TService">The service type</typeparam>
    internal sealed class ServiceRequest<TService> where TService : IService
    {
        const byte ErrorByte = 0;
        const byte SuccessByte = 1;

        readonly Func<TService, ValueTask> callback;
        readonly Endpoint remoteEndPoint;
        readonly ServiceInfo<TService> serviceInfo;
        readonly Task task;
        readonly TcpClient tcpClient;

        bool KeepRunning => !runningTs.IsCancellationRequested;

        string? remoteCallerId;

        readonly CancellationTokenSource runningTs = new();

        internal ServiceRequest(ServiceInfo<TService> serviceInfo, TcpClient tcpClient, Endpoint remoteEndPoint,
            Func<TService, ValueTask> callback)
        {
            this.tcpClient = tcpClient;
            this.callback = callback;
            this.remoteEndPoint = remoteEndPoint;
            this.serviceInfo = serviceInfo;

            task = TaskUtils.StartLongTask(async () => await StartSession().AwaitNoThrow(this), runningTs.Token);
        }

        public bool IsAlive => !task.IsCompleted;
        string Service => serviceInfo.Service;
        int Port => remoteEndPoint.Port;
        public string Hostname => remoteEndPoint.Hostname;

        public async ValueTask StopAsync(CancellationToken token)
        {
            tcpClient.Close();
            runningTs.Cancel();
            await task.AwaitNoThrow(2000, this, token);
            runningTs.Dispose();
        }

        async ValueTask<Rent<byte>> ReceivePacket(CancellationToken token)
        {
            byte[] lengthBuffer = new byte[4];
            if (!await tcpClient.ReadChunkAsync(lengthBuffer, 4, token))
            {
                throw new IOException("Partner closed connection.");
            }

            int length = BitConverter.ToInt32(lengthBuffer, 0);
            if (length == 0)
            {
                return Rent.Empty<byte>();
            }

            var readBuffer = new Rent<byte>(length);
            try
            {
                if (!await tcpClient.ReadChunkAsync(readBuffer.Array, length, token))
                {
                    throw new IOException("Partner closed connection.");
                }
            }
            catch (Exception)
            {
                readBuffer.Dispose();
                throw;
            }

            return readBuffer;
        }

        string? ProcessRemoteHeader(List<string> fields)
        {
            if (fields.Count < 3)
            {
                return "error=Expected at least 3 fields, closing connection";
            }

            var values = RosUtils.CreateHeaderDictionary(fields);

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
                if (receivedMd5Sum == DynamicMessage.RosMd5Sum) // "*"
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
                if (receivedType == DynamicMessage.RosMessageType) // "*"
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

        ValueTask SendHeader(string? errorMessage)
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
                    $"callerid={serviceInfo.CallerId}",
                    $"type={serviceInfo.Type}",
                    $"md5sum={serviceInfo.Md5Sum}",
                };
            }

            return tcpClient.WriteHeaderAsync(contents, runningTs.Token);
        }

        async ValueTask ProcessHandshake()
        {
            List<string> fields;
            using (var readBuffer = await ReceivePacket(runningTs.Token))
            {
                fields = RosUtils.ParseHeader(readBuffer);
            }

            string? errorMessage = ProcessRemoteHeader(fields);

            await SendHeader(errorMessage);

            if (errorMessage != null)
            {
                throw new RosHandshakeException($"Failed handshake: {errorMessage}");
            }
        }

        async ValueTask StartSession()
        {
            try
            {
                await ProcessHandshake();
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case OperationCanceledException:
                    case ObjectDisposedException:
                        break;
                    case IOException:
                    case SocketException:
                        Logger.LogDebugFormat("{0}: Error in ServiceRequestAsync: {1}", this, e);
                        break;
                    default:
                        Logger.LogErrorFormat("{0}: Error in ServiceRequestAsync: {1}", this, e);
                        break;
                }

                runningTs.Cancel();
                return;
            }


            while (KeepRunning)
            {
                try
                {
                    TService serviceMsg;
                    using (var readBuffer = await ReceivePacket(runningTs.Token))
                    {
                        serviceMsg = (TService)serviceInfo.Generator.Create();
                        serviceMsg.Request =
                            serviceMsg.Request.DeserializeFromArray(readBuffer.Array, readBuffer.Length);
                    }

                    byte resultStatus;
                    string? errorMessage;
                    bool errorInResponse;

                    try
                    {
                        await callback(serviceMsg);
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
                        errorMessage =
                            "Callback function failed: it returned null, an invalid value, or an exception happened.";
                    }
                    else
                    {
                        resultStatus = SuccessByte;
                        errorMessage = null;
                    }

                    if (errorMessage == null)
                    {
                        int msgLength = responseMsg.RosMessageLength;
                        using var writeBuffer = new Rent<byte>(msgLength + 5);

                        WriteHeader(writeBuffer, resultStatus, msgLength);
                        responseMsg.SerializeToArray(writeBuffer.Array, 5);

                        await tcpClient.WriteChunkAsync(writeBuffer, runningTs.Token);
                    }
                    else
                    {
                        using var errorMessageBytes = errorMessage.AsRent();

                        using (var headerBuffer = new Rent<byte>(5))
                        {
                            WriteHeader(headerBuffer, resultStatus, errorMessageBytes.Length);
                            await tcpClient.WriteChunkAsync(headerBuffer, runningTs.Token);
                        }

                        await tcpClient.WriteChunkAsync(errorMessageBytes, runningTs.Token);
                    }
                }
                catch (Exception e)
                {
                    if (e is IOException or SocketException)
                    {
                        Logger.LogDebugFormat(BaseUtils.GenericExceptionFormat, this, e);
                        break;
                    }

                    Logger.LogFormat(BaseUtils.GenericExceptionFormat, this, e);
                }
            }

            tcpClient.Close();
        }

        static void WriteHeader(in Rent<byte> rent, byte status, int i)
        {
            byte[] array = rent.Array;
            array[4] = (byte)(i >> 0x18);
            array[0] = status;
            array[1] = (byte)i;
            array[2] = (byte)(i >> 8);
            array[3] = (byte)(i >> 0x10);
        }

        public override string ToString()
        {
            return $"[ServiceRequest {Hostname}:{Port.ToString()} '{Service}' >>'{remoteCallerId}']";
        }
    }
}