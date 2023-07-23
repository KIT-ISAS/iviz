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
using Iviz.Roslib.XmlRpc;
using Iviz.Tools;

namespace Iviz.Roslib;

internal sealed class UdpSender<TMessage> : ProtocolSender<TMessage>, IUdpSender where TMessage : IMessage
{
    readonly CancellationTokenSource runningTs = new();
    readonly SenderQueue<TMessage> senderQueue;
    readonly Serializer<TMessage> serializer;
    readonly TopicInfo topicInfo;
    readonly Task task;

    public IReadOnlyList<string> RosHeader { get; }
    public string RemoteCallerId { get; }
    public Endpoint RemoteEndpoint { get; }
    public Endpoint Endpoint { get; }
    public string Topic => topicInfo.Topic;
    public string Type => topicInfo.Type;
    public TransportType TransportType => TransportType.Udp;

    long bytesDropped;
    long bytesSent;
    bool disposed;

    int numDropped;
    long numSent;

    byte msgId;

    bool KeepRunning => !runningTs.IsCancellationRequested;

    public override bool IsAlive => !task.IsCompleted;

    public override long MaxQueueSizeInBytes
    {
        get => senderQueue.MaxQueueSizeInBytes;
        set => senderQueue.MaxQueueSizeInBytes = value;
    }

    public UdpClient UdpClient { get; }
    public int MaxPacketSize { get; }
    public LoopbackReceiver<TMessage>? LoopbackReceiver { private get; set; }

    public UdpSender(RpcUdpTopicRequest request, TopicInfo topicInfo,
        LatchedMessageProvider<TMessage> provider,
        out byte[] responseHeader)
    {
        this.topicInfo = topicInfo;
        serializer = ((TMessage)topicInfo.Generator).CreateSerializer();
        senderQueue = new SenderQueue<TMessage>(this, serializer);

        RemoteEndpoint = new Endpoint(request.Hostname, request.Port);

        int ownMaxPacketSize = RosUtils.TryGetMaxPacketSizeForAddress(request.Hostname) ??
                               (UdpRosParams.DefaultMTU - UdpRosParams.Ip4UdpHeadersLength);
        MaxPacketSize = Math.Min(request.MaxPacketSize, ownMaxPacketSize);

        var rosHeader = RosUtils.ParseHeader(request.Header);
        var fields = RosUtils.CreateHeaderDictionary(rosHeader);
        RosHeader = rosHeader;

        if (fields.Count < 4)
        {
            throw new RosInvalidHeaderException("Expected at least 4 fields. Closing connection");
        }

        if (fields.TryGetValue("callerid", out string? receivedId))
        {
            RemoteCallerId = receivedId;
        }
        else
        {
            throw new RosInvalidHeaderException("Expected entry 'callerid'");
        }

        if (!fields.TryGetValue("topic", out string? receivedTopic) || receivedTopic != topicInfo.Topic)
        {
            throw new RosInvalidHeaderException(
                $"Expected topic '{topicInfo.Topic}' but received request for '{receivedTopic}'");
        }

        if (!fields.TryGetValue("type", out string? receivedType) || receivedType != topicInfo.Type)
        {
            if (receivedType != DynamicMessage.RosAny) // "*"
            {
                throw new RosInvalidHeaderException(
                    $"error=Expected message type [{topicInfo.Type}] but received [{receivedType}]");
            }
        }

        if (!fields.TryGetValue("md5sum", out string? receivedMd5Sum) || receivedMd5Sum != topicInfo.Md5Sum)
        {
            if (receivedMd5Sum != DynamicMessage.RosAny) // "*"
            {
                throw new RosInvalidHeaderException(
                    $"Expected md5 '{topicInfo.Md5Sum}' but received '{receivedMd5Sum}'");
            }
        }

        UdpClient = new UdpClient(AddressFamily.InterNetworkV6) { Client = { DualMode = true } };
        UdpClient.TryConnect(RemoteEndpoint.Hostname, RemoteEndpoint.Port);
        UdpClient.Client.SendBufferSize = 32768;

        var maybeLocalEndPoint = (IPEndPoint?)UdpClient.Client.LocalEndPoint;
        if (maybeLocalEndPoint is null)
        {
            throw new RosConnectionException("Failed to initialize socket");
        }

        Endpoint = new Endpoint(maybeLocalEndPoint);

        string[] responseHeaderContents =
        {
            $"md5sum={topicInfo.Md5Sum}",
            $"type={topicInfo.Type}",
            $"callerid={topicInfo.CallerId}",
            provider.HasLatchedMessage ? "latching=1" : "latching=0",
            $"message_definition={topicInfo.MessageDependencies}",
        };

        responseHeader = StreamUtils.WriteHeaderToArray(responseHeaderContents);

        task = TaskUtils.Run(() => StartSession(provider).AwaitNoThrow(this));
    }

    async ValueTask StartSession(LatchedMessageProvider<TMessage> provider)
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

        UdpClient.Dispose();
        runningTs.CancelNoThrow(this);
        senderQueue.FlushRemaining();
    }

    async ValueTask ProcessLoop(LatchedMessageProvider<TMessage> provider)
    {
        using var writeBuffer = new Rent(MaxPacketSize);
        writeBuffer[..UdpRosParams.HeaderLength].Fill(0);

        _ = TaskUtils.Run(KeepAliveMessages);

        if (provider.TryGetLatchedMessage(out var latchedMsg))
        {
            Publish(latchedMsg);
        }

        while (KeepRunning)
        {
            await senderQueue.WaitAsync(runningTs.Token);

            var queue = senderQueue.ReadAll(ref numDropped, ref bytesDropped);

            var loopbackReceiver = LoopbackReceiver;
            if (loopbackReceiver != null)
            {
                senderQueue.DirectSendToLoopback(queue, loopbackReceiver, ref numSent, ref bytesSent);
            }
            else
            {
                await SendWithSocketAsync(queue, writeBuffer);
            }
        }
    }

    async ValueTask SendWithSocketAsync(RangeEnumerable<SenderQueue<TMessage>.Entry?> queue, Rent writeBuffer)
    {
        const int udpPlusSizeHeaders = UdpRosParams.HeaderLength + 4;

        using var messageBuffer = new ResizableRent();

        try
        {
            foreach (var e in queue)
            {
                if (e is not var (msg, msgLength, msgSignal))
                {
                    WriteKeepAlive(writeBuffer);
                    await WriteChunkAsync(udpPlusSizeHeaders);
                    continue;
                }

                if (msgLength + udpPlusSizeHeaders <= MaxPacketSize)
                {
                    WriteUnfragmented(writeBuffer, msgLength);
                    Serialize(msg, writeBuffer[udpPlusSizeHeaders..]);
                    await WriteChunkAsync(msgLength + udpPlusSizeHeaders);
                }
                else
                {
                    messageBuffer.EnsureCapacity(msgLength);
                    Serialize(msg, messageBuffer);

                    int maxPayloadSize = MaxPacketSize - UdpRosParams.HeaderLength;
                    int totalBlocks = (4 + msgLength + maxPayloadSize - 1) / maxPayloadSize;
                    int offset = 0;

                    {
                        WriteFragmentedFirst(writeBuffer, totalBlocks, msgLength);
                        int firstPayloadSize = maxPayloadSize - 4; // the first packet includes the message length
                        messageBuffer.Slice(offset, firstPayloadSize)
                            .CopyTo(writeBuffer.Slice(udpPlusSizeHeaders, firstPayloadSize));
                        //Buffer.BlockCopy(resizableBuffer.Array, offset,
                        //    array, udpPlusSizeHeaders, firstPayloadSize);
                        await WriteChunkAsync(MaxPacketSize);
                        offset += firstPayloadSize;
                    }

                    int blockNr = 1;
                    while (offset != msgLength)
                    {
                        WriteFragmentedN(writeBuffer, blockNr++);
                        int currentPayloadSize = Math.Min(msgLength - offset, maxPayloadSize);
                        messageBuffer.Slice(offset, currentPayloadSize)
                            .CopyTo(writeBuffer.Slice(UdpRosParams.HeaderLength, currentPayloadSize));
                        //Buffer.BlockCopy(resizableBuffer.Array, offset,
                        //    array, UdpRosParams.HeaderLength,
                        //    currentPayloadSize);
                        await WriteChunkAsync(UdpRosParams.HeaderLength + currentPayloadSize);
                        offset += currentPayloadSize;
                    }
                }

                numSent++;
                bytesSent += msgLength + udpPlusSizeHeaders;
                msgSignal?.TrySetResult();
            }
        }
        catch (Exception e)
        {
            senderQueue.FlushFrom(queue, e);
            throw;
        }

        ValueTask<int> WriteChunkAsync(int toWrite) => UdpClient.WriteChunkAsync(writeBuffer, toWrite, runningTs.Token);
    }

    unsafe void Serialize(TMessage msg, Span<byte> buffer)
    {
        fixed (byte* bufferPtr = buffer)
        {
            var b = new WriteBuffer(bufferPtr, buffer.Length);
            serializer.RosSerialize(msg, ref b);
        }
    }

    void WriteKeepAlive(Span<byte> array)
    {
        // 4 bytes connection id (here always 0)
        // 1 byte op code (2 - keepalive)
        array[4] = UdpRosParams.OpCodePing;
        // 1 byte msgId
        array[5] = ++msgId;
        // 2 bytes block id (0, first datagram)
        // 4 bytes message length
        array[8] = 0;
        array[9] = 0;
        array[10] = 0;
        array[11] = 0;
        // total 12
    }

    void WriteUnfragmented(Span<byte> array, int msgLength)
    {
        // 4 bytes connection id (here always 0)
        // 1 byte op code (0 - first datagram)
        array[4] = UdpRosParams.OpCodeData0;
        // 1 byte msgId
        array[5] = ++msgId;
        // 2 bytes block id (0, unfragmented)
        // 4 bytes message length
        array[8] = (byte)msgLength;
        array[9] = (byte)(msgLength >> 8);
        array[10] = (byte)(msgLength >> 0x10);
        array[11] = (byte)(msgLength >> 0x18);
        // total 12
    }

    void WriteFragmentedFirst(Span<byte> array, int totalBlocks, int msgLength)
    {
        // 4 bytes connection id (here always 0)
        // 1 byte op code (0 - first datagram)
        array[4] = UdpRosParams.OpCodeData0;
        // 1 byte msgId
        array[5] = ++msgId;
        // 2 bytes block id (total blocks)
        array[6] = (byte)totalBlocks;
        array[7] = (byte)(totalBlocks >> 8);
        // 4 bytes message length
        array[8] = (byte)msgLength;
        array[9] = (byte)(msgLength >> 8);
        array[10] = (byte)(msgLength >> 0x10);
        array[11] = (byte)(msgLength >> 0x18);
        // total 12
    }

    void WriteFragmentedN(Span<byte> array, int blockNr)
    {
        // 4 bytes connection id (here always 0)
        // 1 byte op code (0 - first datagram)
        array[4] = UdpRosParams.OpCodeDataN;
        // 1 byte msgId
        array[5] = msgId;
        // 2 bytes block id (total blocks)
        array[6] = (byte)blockNr;
        array[7] = (byte)(blockNr >> 8);
        // message length ignored here! it was written in the first datagram
        // total 8
    }

    async Task KeepAliveMessages()
    {
        while (KeepRunning)
        {
            await Task.Delay(3000, runningTs.Token);
            senderQueue.EnqueueEmpty();
        }
    }

    public override async ValueTask DisposeAsync(CancellationToken token)
    {
        if (disposed) return;
        disposed = true;

        runningTs.CancelNoThrow(this);
        UdpClient.Dispose();

        await task.AwaitNoThrow(5000, this, token);
    }

    public override Ros1SenderState State =>
        new()
        {
            IsAlive = IsAlive,
            TransportType = TransportType.Udp,
            Endpoint = Endpoint,
            RemoteId = RemoteCallerId,
            RemoteEndpoint = RemoteEndpoint,
            CurrentQueueSize = senderQueue.Count,
            MaxQueueSizeInBytes = MaxQueueSizeInBytes,
            NumSent = numSent,
            BytesSent = bytesSent,
            NumDropped = numDropped,
            BytesDropped = bytesDropped
        };

    public override void Publish(in TMessage message)
    {
        if (!task.IsCompleted)
        {
            senderQueue.Enqueue(message, ref numDropped, ref bytesDropped);
        }
    }

    public override ValueTask PublishAndWaitAsync(in TMessage message, CancellationToken token)
    {
        return task.IsCompleted
            ? Task.FromException(new ObjectDisposedException(nameof(UdpSender<TMessage>))).AsValueTask()
            : senderQueue.EnqueueAsync(message, token, ref numDropped, ref bytesDropped);
    }

    public override string ToString() =>
        $"[{nameof(UdpSender<TMessage>)} '{topicInfo.Topic}' :{Endpoint.Port.ToString()} >>'{RemoteCallerId}']";
}