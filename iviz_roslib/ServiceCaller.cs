using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib.Utils;
using Iviz.Tools;

namespace Iviz.Roslib;

internal sealed class ServiceCaller : IDisposable
{
    const int DefaultTimeoutInMs = 5000;
    const byte ErrorByte = 0;

    readonly bool requestNoDelay;
    readonly ServiceInfo serviceInfo;
    readonly TcpClient tcpClient;

    bool disposed;

    public bool IsAlive => tcpClient.Client.CheckIfAlive();
    public string ServiceType => serviceInfo.Service;
    public Uri? RemoteUri { get; private set; }

    public ServiceCaller(ServiceInfo serviceInfo, bool requestNoDelay = true)
    {
        this.serviceInfo = serviceInfo;
        this.requestNoDelay = requestNoDelay;

        tcpClient = new TcpClient(AddressFamily.InterNetworkV6)
        {
            Client = { DualMode = true },
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

    ValueTask SendHeaderAsync(bool persistent, CancellationToken token)
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

        return tcpClient.WriteHeaderAsync(contents, token);
    }

    async ValueTask ProcessHandshakeAsync(bool persistent, CancellationToken token)
    {
        await SendHeaderAsync(persistent, token);

        List<string> responses;
        using (var readBuffer = await ReceivePacketAsync(token))
        {
            responses = RosUtils.ParseHeader(readBuffer);
        }

        var values = RosUtils.CreateHeaderDictionary(responses);
        if (values.TryGetValue("error", out string? message))
        {
            throw new RosHandshakeException($"Partner sent error message: [{message}]");
        }
    }

    public async ValueTask StartAsync(Uri remoteUri, bool persistent, CancellationToken token)
    {
        RemoteUri = remoteUri;
        string remoteHostname = remoteUri.Host;
        int remotePort = remoteUri.Port;

        await tcpClient.TryConnectAsync(remoteHostname, remotePort, token, DefaultTimeoutInMs);
        await ProcessHandshakeAsync(persistent, token);
    }

    async ValueTask<Rent<byte>> ReceivePacketAsync(CancellationToken token)
    {
        byte[] lengthBuffer = new byte[4];
        if (!await tcpClient.ReadChunkAsync(lengthBuffer, 4, token))
        {
            throw new IOException("Partner closed connection");
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
                throw new IOException("Partner closed connection");
            }
        }
        catch (Exception)
        {
            readBuffer.Dispose();
            throw;
        }

        return readBuffer;
    }

    public async ValueTask ExecuteAsync(IService service, CancellationToken token)
    {
        if (tcpClient == null)
        {
            throw new InvalidOperationException("Service caller has not been started!");
        }

        if (service?.Request == null)
        {
            throw new NullReferenceException("Request cannot be null");
        }

        service.Request.RosValidate();

        var requestMsg = service.Request;
        int msgLength = requestMsg.RosMessageLength;

        using var writeBuffer = new Rent<byte>(msgLength);

        uint sendLength = requestMsg.SerializeTo(writeBuffer);

        await tcpClient.WriteChunkAsync(BitConverter.GetBytes(sendLength), 4, token);
        await tcpClient.WriteChunkAsync(writeBuffer.Array, (int)sendLength, token);

        byte statusByte = await ReadOneByteAsync(token);

        using var readBuffer = await ReceivePacketAsync(token);

        if (statusByte == ErrorByte)
        {
            throw new RosServiceCallFailed(serviceInfo.Service, BuiltIns.UTF8.GetString(readBuffer));
        }

        service.Response = (IResponse)service.Response.DeserializeFrom(readBuffer);
    }

    async ValueTask<byte> ReadOneByteAsync(CancellationToken token)
    {
        byte[] statusBuffer = new byte[1];
        if (!await tcpClient.ReadChunkAsync(statusBuffer, 1, token))
        {
            throw new IOException("Partner closed the connection");
        }

        return statusBuffer[0];
    }
}