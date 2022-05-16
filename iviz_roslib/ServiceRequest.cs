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

namespace Iviz.Roslib;

/// <summary>
/// Class in charge of responding to service request received by a server
/// </summary>
internal sealed class ServiceRequest
{
    const byte ErrorByte = 0;
    const byte SuccessByte = 1;

    readonly Func<IService, ValueTask> callback;
    readonly Endpoint remoteEndPoint;
    readonly ServiceInfo serviceInfo;
    readonly Task task;
    readonly TcpClient tcpClient;

    bool KeepRunning => !runningTs.IsCancellationRequested;

    string? remoteCallerId;

    readonly CancellationTokenSource runningTs = new();

    internal ServiceRequest(ServiceInfo serviceInfo, TcpClient tcpClient, Endpoint remoteEndPoint,
        Func<IService, ValueTask> callback)
    {
        this.tcpClient = tcpClient;
        this.callback = callback;
        this.remoteEndPoint = remoteEndPoint;
        this.serviceInfo = serviceInfo;

        task = TaskUtils.Run(async () => await StartSession().AwaitNoThrow(this), runningTs.Token);
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
            if (!await tcpClient.ReadChunkAsync(readBuffer, token))
            {
                throw new IOException("Partner closed connection.");
            }
        }
        catch
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
            return $"error=Expected callerid '{remoteCallerId}' but partner provided " +
                   $"'{receivedId}', closing connection";
        }

        remoteCallerId = receivedId;

        if (!values.TryGetValue("service", out string? receivedService) || receivedService != serviceInfo.Service)
        {
            return $"error=Expected service '{serviceInfo.Service}' but partner provided " +
                   $"'{receivedService}', closing connection";
        }

        if (!values.TryGetValue("md5sum", out string? receivedMd5Sum) || receivedMd5Sum != serviceInfo.Md5Sum)
        {
            if (receivedMd5Sum == DynamicMessage.RosAny) // "*"
            {
                // OK?
            }
            else
            {
                return $"error=Expected md5 '{serviceInfo.Md5Sum}' but partner provided " +
                       $"'{receivedMd5Sum}', closing connection";
            }
        }

        if (values.TryGetValue("type", out string? receivedType) && receivedType != serviceInfo.Type)
        {
            if (receivedType == DynamicMessage.RosAny) // "*"
            {
                // OK?
            }
            else
            {
                return $"error=Expected type '{serviceInfo.Type}' but received instead " +
                       $"'{receivedType}', closing connection";
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
                    Logger.LogDebugFormat("{0}: Error in " + nameof(StartSession) + ": {1}", this, e);
                    break;
                default:
                    Logger.LogErrorFormat("{0}: Error in " + nameof(StartSession) + ": {1}", this, e);
                    break;
            }

            runningTs.Cancel();
            return;
        }


        while (KeepRunning)
        {
            try
            {
                IService serviceMsg;
                using (var readBuffer = await ReceivePacket(runningTs.Token))
                {
                    serviceMsg = serviceInfo.Create();
                    serviceMsg.Request = (IRequest)serviceMsg.Request.DeserializeFrom(readBuffer);
                }

                string? errorMessage = await ProcessCallback(serviceMsg);
                var responseMsg = serviceMsg.Response;

                if (errorMessage == null)
                {
                    int msgLength = responseMsg.RosMessageLength;
                    using var writeBuffer = new Rent<byte>(msgLength + 5);

                    WriteHeader(writeBuffer, SuccessByte, msgLength);
                    responseMsg.SerializeTo(writeBuffer[5..]);

                    await tcpClient.WriteChunkAsync(writeBuffer, runningTs.Token);
                }
                else
                {
                    using var errorMessageBytes = errorMessage.AsRent();

                    using (var headerBuffer = new Rent<byte>(5))
                    {
                        WriteHeader(headerBuffer, ErrorByte, errorMessageBytes.Length);
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
    
    async ValueTask<string?> ProcessCallback(IService serviceMsg)
    {
        try
        {
            await callback(serviceMsg);
        }
        catch (Exception e)
        {
            Logger.LogErrorFormat("{0}: Inner exception in service callback: {1}", this, e);
            return "Server callback function failed with an exception.";
        }
                    
        try
        {
            serviceMsg.Response.RosValidate();
        }
        catch (Exception e)
        {
            Logger.LogErrorFormat("{0}: Exception validating service callback response: {1}", this, e);
            return "Server callback returned an invalid response.";
        }

        return null;
    }    

    static void WriteHeader(Span<byte> array, byte status, int length)
    {
        array[0] = status;
        array[1..].WriteInt(length);
    }

    public override string ToString()
    {
        return $"[{nameof(ServiceRequest)} {Hostname}:{Port.ToString()} '{Service}' >>'{remoteCallerId}']";
    }
}