﻿using System;
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

internal sealed class TcpSender<T> : IProtocolSender<T>, ITcpSender where T : IMessage
{
    readonly SenderQueue<T> senderQueue;
    readonly CancellationTokenSource runningTs = new();
    readonly TopicInfo topicInfo;
    readonly Task task;
    readonly byte[] lengthBuffer = new byte[4];

    long bytesDropped;
    long bytesSent;
    bool disposed;

    int numDropped;
    long numSent;

    public IReadOnlyList<string> RosHeader { get; private set; } = Array.Empty<string>();
    public string? RemoteCallerId { get; private set; }
    public Endpoint Endpoint { get; }
    public Endpoint RemoteEndpoint { get; }
    public TransportType TransportType => TransportType.Tcp;
    public TcpClient TcpClient { get; }
    public ILoopbackReceiver<T>? LoopbackReceiver { private get; set; }

    bool KeepRunning => !runningTs.IsCancellationRequested;

    public bool TcpNoDelay
    {
        get => TcpClient.NoDelay;
        set => TcpClient.NoDelay = value;
    }

    public string Topic => topicInfo.Topic;
    public string Type => topicInfo.Type;
    bool IsRunning => !task.IsCompleted;
    public bool IsAlive => IsRunning && TcpClient.Client.CheckIfAlive();

    public long MaxQueueSizeInBytes
    {
        get => senderQueue.MaxQueueSizeInBytes;
        set => senderQueue.MaxQueueSizeInBytes = value;
    }

    public Ros1SenderState State =>
        new()
        {
            IsAlive = IsAlive,
            Endpoint = Endpoint,
            RemoteId = RemoteCallerId ?? "",
            RemoteEndpoint = RemoteEndpoint,
            CurrentQueueSize = senderQueue.Count,
            MaxQueueSizeInBytes = MaxQueueSizeInBytes,
            NumSent = numSent,
            BytesSent = bytesSent,
            NumDropped = numDropped,
            BytesDropped = bytesDropped
        };

    public TcpSender(TcpClient client, TopicInfo topicInfo, ILatchedMessageProvider<T> provider)
    {
        this.topicInfo = topicInfo;
        senderQueue = new SenderQueue<T>(this);
        TcpClient = client;
        Endpoint = new Endpoint((IPEndPoint)TcpClient.Client.LocalEndPoint!);
        RemoteEndpoint = new Endpoint((IPEndPoint)TcpClient.Client.RemoteEndPoint!);
        task = TaskUtils.Run(() => StartSession(provider).AwaitNoThrow(this), runningTs.Token);
    }

    public async ValueTask DisposeAsync(CancellationToken token)
    {
        if (disposed)
        {
            return;
        }

        disposed = true;

        runningTs.Cancel();
        TcpClient.Dispose();

        await task.AwaitNoThrow(5000, this, token);
    }

    async ValueTask<Rent<byte>> ReceivePacket()
    {
        if (!await TcpClient.ReadChunkAsync(lengthBuffer, 4, runningTs.Token))
        {
            throw new IOException("Connection closed during handshake");
        }

        int length = BitConverter.ToInt32(lengthBuffer, 0);
        if (length == 0)
        {
            return Rent.Empty<byte>();
        }

        var readBuffer = new Rent<byte>(length);
        try
        {
            if (await TcpClient.ReadChunkAsync(readBuffer, runningTs.Token))
            {
                return readBuffer;
            }
        }
        catch
        {
            readBuffer.Dispose();
            throw;
        }

        readBuffer.Dispose();
        throw new IOException("Connection closed during handshake");
    }

    ValueTask SendHeader(bool latching)
    {
        string[] contents =
        {
            $"md5sum={topicInfo.Md5Sum}",
            $"type={topicInfo.Type}",
            $"callerid={topicInfo.CallerId}",
            latching ? "latching=1" : "latching=0",
            $"message_definition={topicInfo.MessageDependencies}",
        };

        RosHeader = contents;
        return TcpClient.WriteHeaderAsync(contents, runningTs.Token);
    }

    ValueTask SendErrorHeader(string errorMessage)
    {
        string[] contents =
        {
            errorMessage,
            $"md5sum={topicInfo.Md5Sum}",
            $"type={topicInfo.Type}",
            $"callerid={topicInfo.CallerId}"
        };
        return TcpClient.WriteHeaderAsync(contents, runningTs.Token);
    }

    bool ProcessRemoteHeader(List<string> fields, out string errorMessage)
    {
        if (fields.Count < 5)
        {
            errorMessage = "error=Expected at least 5 fields. Closing connection";
            return false;
        }

        var values = RosUtils.CreateHeaderDictionary(fields);
        if (values.TryGetValue("callerid", out string? receivedId))
        {
            RemoteCallerId = receivedId;
        }
        else
        {
            errorMessage = "error=Expected entry 'callerid'";
            return false;
        }

        if (!values.TryGetValue("topic", out string? receivedTopic) || receivedTopic != topicInfo.Topic)
        {
            errorMessage =
                $"error=Expected topic '{topicInfo.Topic}' but received request for '{receivedTopic}'";
            return false;
        }

        if (!values.TryGetValue("type", out string? receivedType) || receivedType != topicInfo.Type)
        {
            if (receivedType != DynamicMessage.RosAny) // "*"
            {
                errorMessage =
                    $"error=Expected message type [{topicInfo.Type}] but received [{receivedType}]";
                return false;
            }
        }

        if (!values.TryGetValue("md5sum", out string? receivedMd5Sum) || receivedMd5Sum != topicInfo.Md5Sum)
        {
            if (receivedMd5Sum != DynamicMessage.RosAny) // "*"
            {
                errorMessage =
                    $"error=Expected md5 '{topicInfo.Md5Sum}' but received '{receivedMd5Sum}'";
                return false;
            }
        }

        if (values.TryGetValue("tcp_nodelay", out string? receivedNoDelay) && receivedNoDelay == "1")
        {
            TcpNoDelay = true;
        }

        errorMessage = "";
        return true;
    }

    async ValueTask ProcessHandshake(bool latching)
    {
        List<string> fields;
        using (var readBuffer = await ReceivePacket())
        {
            fields = RosUtils.ParseHeader(readBuffer);
        }

        bool success = ProcessRemoteHeader(fields, out string errorMessage);
        if (success)
        {
            await SendHeader(latching);
        }
        else
        {
            await SendErrorHeader(errorMessage);
            throw new RosHandshakeException(
                $"Failed to parse header sent by partner. Error message: [{errorMessage}]");
        }
    }

    async ValueTask StartSession(ILatchedMessageProvider<T> provider)
    {
        Logger.LogDebugFormat("{0}: Started!", this);

        try
        {
            await ProcessLoop(provider);
        }
        catch (Exception e)
        {
            switch (e)
            {
                case OperationCanceledException:
                case ObjectDisposedException:
                    break;
                case IOException:
                case TimeoutException:
                case SocketException:
                    Logger.LogDebugFormat(BaseUtils.GenericExceptionFormat, this, e);
                    break;
                case RoslibException:
                    Logger.LogErrorFormat(BaseUtils.GenericExceptionFormat, this, e);
                    break;
                default:
                    Logger.LogFormat(BaseUtils.GenericExceptionFormat, this, e);
                    break;
            }
        }

        TcpClient.Dispose();
        runningTs.Cancel();
        senderQueue.FlushRemaining();
    }

    async ValueTask ProcessLoop(ILatchedMessageProvider<T> provider)
    {
        using var writeBuffer = new ResizableRent();

        await ProcessHandshake(provider.HasLatchedMessage());

        var latchedMsg = provider.GetLatchedMessage();
        if (latchedMsg.hasValue)
        {
            Publish(latchedMsg.value!);
        }

        while (KeepRunning)
        {
            await senderQueue.WaitAsync(runningTs.Token);

            var queue = senderQueue.ReadAll(ref numDropped, ref bytesDropped);

            if (LoopbackReceiver != null)
            {
                senderQueue.DirectSendToLoopback(queue, LoopbackReceiver, ref numSent, ref bytesSent);
            }
            else
            {
                await SendWithSocketAsync(queue, writeBuffer);
            }
        }
    }

    async ValueTask SendWithSocketAsync(RangeEnumerable<SenderQueue<T>.Entry?> queue, ResizableRent writeBuffer)
    {
        try
        {
            foreach (var entry in queue)
            {
                if (entry is not var (msg, msgLength, msgSignal))
                {
                    continue;
                }

                writeBuffer.EnsureCapacity(msgLength + 4);

                writeBuffer[..4].WriteInt(msgLength);

                WriteBuffer.Serialize(msg, writeBuffer[4..]);

                await TcpClient.WriteChunkAsync(writeBuffer.Array, msgLength + 4, runningTs.Token);

                numSent++;
                bytesSent += msgLength + 4;
                msgSignal?.TrySetResult();
            }
        }
        catch (Exception e)
        {
            senderQueue.FlushFrom(queue, e);
            throw;
        }
    }

    public void Publish(in T message)
    {
        if (IsRunning)
        {
            senderQueue.Enqueue(message, ref numDropped, ref bytesDropped);
        }
    }

    public ValueTask PublishAndWaitAsync(in T message, CancellationToken token)
    {
        return !IsRunning
            ? Task.FromException(new ObjectDisposedException("this")).AsValueTask()
            : senderQueue.EnqueueAsync(message, token, ref numDropped, ref bytesDropped);
    }

    public override string ToString() => $"[TcpSender '{Topic}' :{Endpoint.Port.ToString()} >>'{RemoteCallerId}']";
}