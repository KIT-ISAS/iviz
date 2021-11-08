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
using Buffer = System.Buffer;
using TaskCompletionSource = System.Threading.Tasks.TaskCompletionSource<object?>;

namespace Iviz.Roslib
{
    internal sealed class UdpSender<T> : IProtocolSender<T>, IUdpSender where T : IMessage
    {
        readonly CancellationTokenSource runningTs = new();
        readonly SenderQueue<T> senderQueue;
        readonly TopicInfo<T> topicInfo;
        readonly Task task;

        public IReadOnlyCollection<string> RosHeader { get; }
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

        public bool IsAlive => !task.IsCompleted;

        public long MaxQueueSizeInBytes
        {
            get => senderQueue.MaxQueueSizeInBytes;
            set => senderQueue.MaxQueueSizeInBytes = value;
        }

        public UdpClient UdpClient { get; }
        public int MaxPacketSize { get; }
        public ILoopbackReceiver<T>? LoopbackReceiver { private get; set; }

        public UdpSender(RpcUdpTopicRequest request, TopicInfo<T> topicInfo, NullableMessage<T> latchedMsg,
            out byte[] responseHeader)
        {
            this.topicInfo = topicInfo;
            senderQueue = new SenderQueue<T>(this);

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
                if (receivedType != DynamicMessage.RosMessageType) // "*"
                {
                    throw new RosInvalidHeaderException(
                        $"error=Expected message type [{topicInfo.Type}] but received [{receivedType}]");
                }
            }

            if (!fields.TryGetValue("md5sum", out string? receivedMd5Sum) || receivedMd5Sum != topicInfo.Md5Sum)
            {
                if (receivedMd5Sum != DynamicMessage.RosMd5Sum) // "*"
                {
                    throw new RosInvalidHeaderException(
                        $"Expected md5 '{topicInfo.Md5Sum}' but received '{receivedMd5Sum}'");
                }
            }

            UdpClient = new UdpClient(AddressFamily.InterNetworkV6) { Client = { DualMode = true } };
            UdpClient.TryConnect(RemoteEndpoint.Hostname, RemoteEndpoint.Port);
            UdpClient.Client.SendBufferSize = 32768;

            var maybeLocalEndPoint = (IPEndPoint?)UdpClient.Client.LocalEndPoint;
            if (maybeLocalEndPoint is not { } localEndPoint)
            {
                throw new RosConnectionException("Failed to initialize socket");
            }

            Endpoint = new Endpoint(localEndPoint);

            string[] responseHeaderContents =
            {
                $"md5sum={topicInfo.Md5Sum}",
                $"type={topicInfo.Type}",
                $"callerid={topicInfo.CallerId}",
                latchedMsg.HasValue ? "latching=1" : "latching=0",
                $"message_definition={topicInfo.MessageDependencies}",
            };

            responseHeader = StreamUtils.WriteHeaderToArray(responseHeaderContents);

            task = TaskUtils.StartLongTask(async () => await StartSession(latchedMsg).AwaitNoThrow(this));
        }

        async Task StartSession(NullableMessage<T> latchedMsg)
        {
            Logger.LogDebugFormat("{0}: Started!", this);

            try
            {
                await ProcessLoop(latchedMsg);
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
                    default:
                        Logger.LogFormat(BaseUtils.GenericExceptionFormat, this, e);
                        break;
                }
            }

            UdpClient.Dispose();
            try
            {
                runningTs.Cancel();
            }
            catch (ObjectDisposedException)
            {
            }

            senderQueue.FlushRemaining();
        }

        async Task ProcessLoop(NullableMessage<T> latchedMsg)
        {
            if (latchedMsg.HasValue)
            {
                Publish(latchedMsg.Value);
            }

            using var writeBuffer = new Rent<byte>(MaxPacketSize);
            for (int i = 0; i < UdpRosParams.HeaderLength; i++)
            {
                writeBuffer[i] = 0;
            }

            _ = Task.Run(KeepAliveMessages);

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

        async Task SendWithSocketAsync(RangeEnumerable<SenderQueue<T>.Entry?> queue, Rent<byte> writeBuffer)
        {
            const int udpPlusSizeHeaders = UdpRosParams.HeaderLength + 4;
            byte[] array = writeBuffer.Array;

            using var resizableBuffer = new ByteBufferRent();

            try
            {
                foreach (var e in queue)
                {
                    if (e is not var (msg, msgLength, msgSignal))
                    {
                        WriteKeepAlive();
                        await WriteChunkAsync(udpPlusSizeHeaders);
                        continue;
                    }

                    if (msgLength + udpPlusSizeHeaders <= MaxPacketSize)
                    {
                        WriteUnfragmented(msgLength);
                        msg.SerializeToArray(array, udpPlusSizeHeaders);
                        await WriteChunkAsync(msgLength + udpPlusSizeHeaders);
                    }
                    else
                    {
                        resizableBuffer.EnsureCapacity(msgLength);
                        msg.SerializeToArray(resizableBuffer.Array);

                        int maxPayloadSize = MaxPacketSize - UdpRosParams.HeaderLength;
                        int totalBlocks = (4 + msgLength + maxPayloadSize - 1) / maxPayloadSize;
                        int offset = 0;

                        {
                            WriteFragmentedFirst(totalBlocks, msgLength);
                            int firstPayloadSize = maxPayloadSize - 4; // the first packet includes the message length
                            Buffer.BlockCopy(resizableBuffer.Array, offset,
                                array, udpPlusSizeHeaders, firstPayloadSize);
                            await WriteChunkAsync(MaxPacketSize);
                            offset += firstPayloadSize;
                        }

                        int blockNr = 1;
                        while (offset != msgLength)
                        {
                            WriteFragmentedN(blockNr++);
                            int currentPayloadSize = Math.Min(msgLength - offset, maxPayloadSize);
                            Buffer.BlockCopy(resizableBuffer.Array, offset,
                                array, UdpRosParams.HeaderLength,
                                currentPayloadSize);
                            await WriteChunkAsync(UdpRosParams.HeaderLength + currentPayloadSize);
                            offset += currentPayloadSize;
                        }
                    }

                    numSent++;
                    bytesSent += msgLength + udpPlusSizeHeaders;
                    msgSignal?.TrySetResult(null);
                }
            }
            catch (Exception e)
            {
                senderQueue.FlushFrom(queue, e);
                throw;
            }


            void WriteKeepAlive()
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

            void WriteUnfragmented(int msgLength)
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

            void WriteFragmentedFirst(int totalBlocks, int msgLength)
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

            void WriteFragmentedN(int blockNr)
            {
                // 4 bytes connection id (here always 0)
                // 1 byte op code (0 - first datagram)
                array[4] = UdpRosParams.OpCodeDataN;
                // 1 byte msgId
                array[5] = msgId;
                // 2 bytes block id (total blocks)
                array[6] = (byte)blockNr;
                array[7] = (byte)(blockNr >> 8);
                // no message length here! it was written in the first datagram
                // total 8
            }

            ValueTask<int> WriteChunkAsync(int toWrite)
            {
                return UdpClient.WriteChunkAsync(array, 0, toWrite, runningTs.Token);
            }
        }
        
        async Task KeepAliveMessages()
        {
            while (KeepRunning)
            {
                await Task.Delay(3000, runningTs.Token);
                senderQueue.EnqueueEmpty();
            }
        }

        public async Task DisposeAsync(CancellationToken token)
        {
            if (disposed)
            {
                return;
            }

            disposed = true;

            runningTs.Cancel();
            UdpClient.Dispose();

            await task.AwaitNoThrow(5000, this, token);
            runningTs.Dispose();
        }

        public PublisherSenderState State =>
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

        public void Publish(in T message)
        {
            if (IsAlive)
            {
                senderQueue.Enqueue(message, ref numDropped, ref bytesDropped);
            }
        }

        public Task PublishAndWaitAsync(in T message, CancellationToken token)
        {
            return !IsAlive
                ? Task.FromException(new InvalidOperationException("Sender has been disposed."))
                : senderQueue.EnqueueAsync(message, token, ref numDropped, ref bytesDropped);
        }

        public override string ToString() =>
            $"[UdpSender '{topicInfo.Topic}' :{Endpoint.Port.ToString()} >>'{RemoteCallerId}']";
    }
}