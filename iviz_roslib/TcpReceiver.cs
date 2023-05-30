using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;
using Iviz.Roslib.Utils;
using Iviz.Tools;

namespace Iviz.Roslib;

using static TcpReceiverUtils;

/// <summary>
/// Handles a ROS TCP connection.
/// </summary>
/// <typeparam name="TMessage"></typeparam>
internal sealed class TcpReceiver<TMessage> : LoopbackReceiver<TMessage>, IProtocolReceiver, ITcpReceiver
    where TMessage : IMessage
{
    const int DisposeTimeoutInMs = 2000;

    readonly ReceiverManager<TMessage> manager;
    readonly bool requestNoDelay;
    readonly CancellationTokenSource runningTs = new();
    readonly Task task;
    readonly int connectionTimeoutInMs;

    readonly MessageInfo cachedInfo;

    int receiveBufferSize = 8192;

    TopicInfo topicInfo;
    bool disposed;
    long numReceived;
    long bytesReceived;
    bool isPaused;

    public string Topic => topicInfo.Topic;
    public string Type => topicInfo.Type;
    public Endpoint RemoteEndpoint { get; }
    public Endpoint Endpoint { get; private set; }
    public IReadOnlyCollection<string> RosHeader { get; private set; }

    public bool IsPaused
    {
        set => isPaused = value;
    }

    public Uri RemoteUri { get; }
    public bool IsAlive => !task.IsCompleted;
    public bool IsConnected => TcpClient is { Connected: true };
    public ReceiverStatus Status { get; private set; }
    public ErrorMessage? ErrorDescription { get; private set; }
    public TcpClient? TcpClient { get; private set; }
    public string? RemoteId { get; private set; }

    public TcpReceiver(ReceiverManager<TMessage> manager,
        Uri remoteUri, Endpoint remoteEndpoint, TopicInfo topicInfo,
        bool requestNoDelay, int timeoutInMs)
    {
        RemoteUri = remoteUri;
        RemoteEndpoint = remoteEndpoint;
        RosHeader = Array.Empty<string>();
        this.topicInfo = topicInfo;
        this.manager = manager;
        this.requestNoDelay = requestNoDelay;
        connectionTimeoutInMs = timeoutInMs;
        cachedInfo = new MessageInfo(this);
        Status = ReceiverStatus.ConnectingTcp;
        task = TaskUtils.Run(async () => await StartSession().AwaitNoThrow(this), runningTs.Token);
    }

    public Ros1ReceiverState State => new TcpReceiverState(RemoteUri)
    {
        RemoteId = RemoteId,
        Status = Status,
        RequestNoDelay = requestNoDelay,
        EndPoint = Endpoint,
        RemoteEndpoint = RemoteEndpoint,
        NumReceived = numReceived,
        BytesReceived = bytesReceived,
        ErrorDescription = ErrorDescription,
        IsAlive = IsAlive,
    };

    async ValueTask StartSession()
    {
        var newTcpClient = await StartTcpConnection();
        
        // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        if (newTcpClient?.Client?.LocalEndPoint is not IPEndPoint ipEndpoint)
        {
            string errorMessage;
            if (newTcpClient == null)
            {
                errorMessage = "Could not connect to the TCP listener";
            }
            else if (newTcpClient.Client == null)
            {
                errorMessage = "Socket closed before handshake started";
            }
            else if (newTcpClient.Client.LocalEndPoint is not IPEndPoint)
            {
                errorMessage = "TcpClient did not provide a local IP address for connection";
            }
            else
            {
                errorMessage = "Connection failed! Unknown error";
            }

            ErrorDescription = new ErrorMessage(errorMessage);
            Logger.LogDebugFormat(BaseUtils.GenericExceptionFormat, this, errorMessage);

            await Cleanup(true);
            return;
        }

        bool shouldRetry;
        
        try
        {
            await ProcessSession(newTcpClient, ipEndpoint);
            shouldRetry = true;
        }
        catch (Exception e)
        {
            switch (e)
            {
                case ObjectDisposedException:
                case OperationCanceledException:
                    shouldRetry = false;
                    break;
                case IOException:
                case SocketException:
                case TimeoutException:
                    ErrorDescription = new ErrorMessage(e);
                    Logger.LogDebugFormat(BaseUtils.GenericExceptionFormat, this, e);
                    shouldRetry = true;
                    break;
                case RoslibException:
                    ErrorDescription = new ErrorMessage(e);
                    Logger.LogErrorFormat(BaseUtils.GenericExceptionFormat, this, e);
                    shouldRetry = false;
                    break;
                default:
                    Logger.LogFormat(BaseUtils.GenericExceptionFormat, this, e);
                    shouldRetry = false;
                    break;
            }
        }

        await Cleanup(shouldRetry);
    }
    
    async ValueTask<TcpClient?> StartTcpConnection()
    {
        Logger.LogDebugFormat("{0}: Trying to connect!", this);

        var newTcpClient = new TcpClient(AddressFamily.InterNetworkV6)
        {
            Client = { DualMode = true },
            NoDelay = requestNoDelay
        };
        (string hostname, int port) = RemoteEndpoint;

        try
        {
            await newTcpClient.TryConnectAsync(hostname, port, runningTs.Token, connectionTimeoutInMs);
        }
        catch (Exception e)
        {
            if (e is not (IOException or SocketException or OperationCanceledException))
            {
                Logger.LogFormat(BaseUtils.GenericExceptionFormat, this, e);
            }

            newTcpClient.Dispose();
            ErrorDescription = new ErrorMessage("Could not connect to the TCP listener");
            return null;
        }

        if (newTcpClient.Client is { RemoteEndPoint: not null, LocalEndPoint: not null })
        {
            return newTcpClient;
        }

        newTcpClient.Dispose();
        ErrorDescription = new ErrorMessage("Could not connect to the TCP listener");
        return null;
    }

    async ValueTask ProcessSession(TcpClient client, IPEndPoint ipEndpoint)
    {
        Logger.LogDebugFormat("{0}: Connected!", this);
        Endpoint = new Endpoint(ipEndpoint);
        TcpClient = client;
        
        await ProcessHandshake(client);
        Status = ReceiverStatus.Running;
        await ProcessMessages(client);
    }
    
    ValueTask SendHeader(TcpClient client)
    {
        string[] contents =
        {
            $"message_definition={topicInfo.MessageDependencies}",
            $"callerid={topicInfo.CallerId}",
            $"topic={topicInfo.Topic}",
            $"md5sum={topicInfo.Md5Sum}",
            $"type={topicInfo.Type}",
            requestNoDelay ? "tcp_nodelay=1" : "tcp_nodelay=0"
        };

        return client.WriteHeaderAsync(contents, runningTs.Token);
    }

    async ValueTask ProcessHandshake(TcpClient client)
    {
        await SendHeader(client);

        using var readBuffer = new ResizableRent();

        int rcvLength = await ReceivePacket(client, readBuffer, runningTs.Token);
        if (rcvLength < 0)
        {
            throw new IOException("Connection closed during handshake");
        }

        var responseHeader = RosUtils.ParseHeader(readBuffer, rcvLength);
        var dictionary = RosUtils.CreateHeaderDictionary(responseHeader);

        if (dictionary.TryGetValue("callerid", out string? remoteCallerId))
        {
            RemoteId = remoteCallerId;
        }

        if (dictionary.TryGetValue("error", out string? message)) // TODO: improve error handling here
        {
            throw new RosHandshakeException($"Partner sent error message: [{message}]");
        }

        string[] rosHeader = responseHeader.ToArray();
        RosHeader = rosHeader;

        if (DynamicMessage.IsGeneric(typeof(TMessage)))
        {
            //bool allowDirectLookup = DynamicMessage.IsDynamic(typeof(TMessage));
            topicInfo = RosUtils.GenerateDynamicTopicInfo(topicInfo.CallerId, topicInfo.Topic, rosHeader);
        }
    }

    async ValueTask ProcessMessages(TcpClient client)
    {
        var deserializer = ((TMessage)topicInfo.Generator).CreateDeserializer();

        var token = runningTs.Token;
        using var readBuffer = new ResizableRent();

        while (true)
        {
            bool cachedIsPaused = isPaused;

            int rcvLength = cachedIsPaused
                ? await ReceiveAndIgnore(client, readBuffer.Array, token)
                : await ReceivePacket(client, readBuffer, token);

            if (rcvLength < 0)
            {
                Logger.LogDebugFormat("{0}: Partner closed connection", this);
                return;
            }

            numReceived++;
            bytesReceived += rcvLength + 4;

            if (cachedIsPaused)
            {
                continue;
            }

            TMessage message;
            unsafe
            {
                fixed (byte* bufferPtr = readBuffer.Array)
                {
                    var b = new ReadBuffer(bufferPtr, rcvLength);
                    deserializer.RosDeserialize(ref b, out message);
                }
            }

            cachedInfo.MessageSize = rcvLength;

            var callbacks = manager.callbacks;
            foreach (var callback in callbacks)
            {
                try
                {
                    callback.Handle(message, cachedInfo);
                }
                catch (Exception e)
                {
                    Logger.LogErrorFormat("{0}: Exception from " + nameof(ProcessMessages) + ": {1}", this, e);
                }
            }

            CheckBufferSize(client, rcvLength, ref receiveBufferSize);
        }
    }

    internal override void Post(TMessage message, int rcvLength)
    {
        if (task.IsCompleted) /* IsAlive */
        {
            return;
        }

        numReceived++;
        bytesReceived += rcvLength + 4;

        if (isPaused) return;

        cachedInfo.MessageSize = rcvLength;

        var callbacks = manager.callbacks;
        foreach (var callback in callbacks)
        {
            try
            {
                callback.Handle(message, cachedInfo);
            }
            catch (Exception e)
            {
                Logger.LogErrorFormat("{0}: Exception from " + nameof(Post) + ": {1}", this, e);
            }
        }
    }
    
    async ValueTask Cleanup(bool shouldRetry)
    {
        Status = ReceiverStatus.Dead;
        TcpClient?.Dispose();
        TcpClient = null;

        Logger.LogDebugFormat("{0}: Stopped!", this);

        if (runningTs.IsCancellationRequested)
        {
            return;
        }

        if (!shouldRetry)
        {
            runningTs.Cancel();
            return;
        }

        try
        {
            await Task.Delay(2000, runningTs.Token);
            _ = manager.RetryConnectionAsync(RemoteUri);
        }
        finally
        {
            runningTs.Cancel();
        }        
    }

    public async ValueTask DisposeAsync(CancellationToken token)
    {
        if (disposed) return;
        disposed = true;
        runningTs.Cancel();
        TcpClient?.Dispose();

        await task.AwaitNoThrow(DisposeTimeoutInMs, this, token);
    }

    public override string ToString()
    {
        return $"[{nameof(TcpReceiver<TMessage>)} for '{Topic}' PartnerUri={RemoteUri} " +
               $"PartnerSocket={RemoteEndpoint.Hostname}:{RemoteEndpoint.Port.ToString()}]";
    }
}

internal static class TcpReceiverUtils
{
    public static async ValueTask<int> ReceivePacket(TcpClient client, ResizableRent readBuffer,
        CancellationToken token)
    {
        byte[] array = readBuffer.Array;
        if (!await client.ReadChunkAsync(array, 4, token))
        {
            return -1;
        }

        int length = array.ReadInt();
        if (length == 0)
        {
            return 0;
        }

        const int maxMessageLength = 64 * 1024 * 1024;
        if (length is < 0 or > maxMessageLength)
        {
            throw new RosInvalidPacketSizeException(length);
        }

        readBuffer.EnsureCapacity(length);
        if (!await client.ReadChunkAsync(readBuffer.Array, length, token))
        {
            return -1;
        }

        return length;
    }

    public static async ValueTask<int> ReceiveAndIgnore(TcpClient client, byte[] readBuffer, CancellationToken token)
    {
        if (!await client.ReadChunkAsync(readBuffer, 4, token))
        {
            return -1;
        }

        int length = readBuffer.ReadInt();
        if (length == 0)
        {
            return 0;
        }

        if (!await client.ReadAndIgnoreAsync(length, token))
        {
            return -1;
        }

        return length;
    }

    public static void CheckBufferSize(TcpClient client, int rcvLength, ref int receiveBufferSize)
    {
        if (receiveBufferSize >= rcvLength)
        {
            return;
        }

        int recommendedSize = RosUtils.GetRecommendedBufferSize(rcvLength, receiveBufferSize);
        if (recommendedSize == receiveBufferSize)
        {
            return;
        }

        receiveBufferSize = recommendedSize;
        client.Client.ReceiveBufferSize = recommendedSize;
    }
}