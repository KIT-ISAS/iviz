using System;
using System.Collections.Concurrent;
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
using Nito.AsyncEx;
using TaskCompletionSource = System.Threading.Tasks.TaskCompletionSource<object?>;

namespace Iviz.Roslib
{
    internal sealed class UdpSender<T> : IProtocolSender<T> where T : IMessage
    {
        readonly CancellationTokenSource runningTs = new();
        readonly AsyncCollection<(T msg, TaskCompletionSource? signal)?> messageQueue = new();
        readonly TopicInfo<T> topicInfo;
        readonly UdpClient udpClient;
        readonly List<string> rosHeader;
        readonly string remoteCallerId;
        readonly int maxPackageSize;
        readonly Task task;
        
        
        public Endpoint RemoteEndpoint { get; }
        public Endpoint Endpoint { get; }

        long bytesDropped;
        long bytesSent;
        bool disposed;

        long numDropped;
        long numSent;

        bool KeepRunning => !runningTs.IsCancellationRequested;

        public bool IsAlive => !task.IsCompleted;

        public int MaxQueueSizeInBytes
        {
            set { } // nyi
        }
        
        public ILoopbackReceiver<T>? LoopbackReceiver { private get; set; }

        public UdpSender(RpcUdpTopicRequest request, TopicInfo<T> topicInfo, NullableMessage<T> latchedMsg,
            out byte[] responseHeader)
        {
            const int ownMaxPackageSize = 1500 - 20 /* IP header */ - 8 /* UDP header */;

            this.topicInfo = topicInfo;
            maxPackageSize = Math.Min(request.MaxPacketSize, ownMaxPackageSize);
            RemoteEndpoint = new Endpoint(request.Hostname, request.Port);

            rosHeader = RosUtils.ParseHeader(request.Header);
            var fields = RosUtils.CreateHeaderDictionary(rosHeader);

            if (fields.Count < 4)
            {
                throw new RosInvalidHeaderException("Expected at least 3 fields. Closing connection");
            }

            if (fields.TryGetValue("callerid", out string? receivedId))
            {
                remoteCallerId = receivedId;
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

            udpClient = new UdpClient(AddressFamily.InterNetworkV6) { Client = { DualMode = true } };
            udpClient.TryConnect(RemoteEndpoint.Hostname, RemoteEndpoint.Port);

            var maybeLocalEndPoint = (IPEndPoint?)udpClient.Client.LocalEndPoint;
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

            task = TaskUtils.StartLongTask(() => StartSession(latchedMsg));
        }

        async Task StartSession(NullableMessage<T> latchedMsg)
        {
            Logger.LogDebugFormat("{0}: Started!", this);

            try
            {
                await ProcessLoop(latchedMsg);
            }
            catch (Exception e) when (e is OperationCanceledException or ObjectDisposedException)
            {
            }
            catch (Exception e) when (e is IOException or TimeoutException or SocketException)
            {
                Logger.LogDebugFormat(BaseUtils.GenericExceptionFormat, this, e);
            }
            catch (Exception e)
            {
                Logger.LogFormat(BaseUtils.GenericExceptionFormat, this, e);
            }

            udpClient.Dispose();
            try
            {
                runningTs.Cancel();
            }
            catch (ObjectDisposedException)
            {
            }

            messageQueue.CompleteAdding();
            foreach (var pair in messageQueue.GetConsumingEnumerable())
            {
                if (pair == null)
                {
                    continue;
                }

                var (_, msgSignal) = pair.Value;
                msgSignal?.TrySetException(new RosQueueException(
                    $"Connection for '{remoteCallerId}' is shutting down", CreateConnectionInfo()));
            }

            Logger.Log(this + ": Dead!!");
        }

        async Task ProcessLoop(NullableMessage<T> latchedMsg)
        {
            if (latchedMsg.HasValue)
            {
                Publish(latchedMsg.Value);
            }

            const int udpRosHeaderLength = 12;

            using var writeBuffer = new Rent<byte>(maxPackageSize);
            for (int i = 0; i < udpRosHeaderLength; i++)
            {
                writeBuffer[i] = 0;
            }

            byte msgId = 0;

            void WriteKeepAlive()
            {
                byte[] array = writeBuffer.Array;

                // 4 bytes connection id (here always 0)
                // 1 byte op code (2 - keepalive)
                array[4] = UdpOpCode.Ping;
                // 1 byte msgId
                array[5] = msgId++;
                // 2 bytes block id (0, unfragmented)
                // 4 bytes message length
                array[8] = 0;
                array[9] = 0;
                array[10] = 0;
                array[11] = 0;
                // total 12
            }

            void WriteLengthToBuffer(int msgLength)
            {
                byte[] array = writeBuffer.Array;

                // 4 bytes connection id (here always 0)
                // 1 byte op code (0 - unfragmented datagram)
                // 1 byte msgId
                array[4] = UdpOpCode.Data0;
                array[5] = msgId++;
                // 2 bytes block id (0, unfragmented)
                // 4 bytes message length
                array[8] = (byte)msgLength;
                array[9] = (byte)(msgLength >> 8);
                array[10] = (byte)(msgLength >> 0x10);
                array[11] = (byte)(msgLength >> 0x18);
                // total 12
            }

            async Task KeepAliveMessages()
            {
                while (KeepRunning)
                {
                    await Task.Delay(3000, runningTs.Token);
                    await messageQueue.AddAsync(null);
                }
            }

            var _ = Task.Run(KeepAliveMessages);

            while (KeepRunning)
            {
                (T msg, TaskCompletionSource? signal)? pair;

                try
                {
                    pair = await messageQueue.TakeAsync(runningTs.Token);
                }
                catch (InvalidOperationException)
                {
                    break;
                }

                if (pair is null)
                {
                    WriteKeepAlive();
                    await udpClient.WriteChunkAsync(writeBuffer.Array, 0, udpRosHeaderLength, runningTs.Token);
                    continue;
                }

                var (msg, msgSignal) = pair.Value;
                int msgLength = msg.RosMessageLength;

                if (LoopbackReceiver != null)
                {
                    LoopbackReceiver.Post(msg, msgLength);
                    numSent++;
                    bytesSent += msgLength + 4;
                    msgSignal?.TrySetResult(null);
                    continue;
                }

                if (msgLength > maxPackageSize - udpRosHeaderLength)
                {
                    msgSignal?.TrySetException(CreateQueueOverflowException());
                    numDropped++;
                    bytesDropped += msgLength + 4;
                    continue;
                }

                try
                {
                    msg.SerializeToArray(writeBuffer.Array, udpRosHeaderLength);
                    WriteLengthToBuffer(msgLength);

                    await udpClient.WriteChunkAsync(writeBuffer.Array, 0, msgLength + udpRosHeaderLength,
                        runningTs.Token);

                    numSent++;
                    bytesSent += msgLength + 4;
                    msgSignal?.TrySetResult(null);
                }
                catch (Exception e)
                {
                    msgSignal?.TrySetException(CreateQueueException(e));
                    numDropped++;
                    bytesDropped += msgLength + 4;
                    throw;
                }
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
            messageQueue.CompleteAdding();
            udpClient.Dispose();

            await task.AwaitNoThrow(5000, this, token);
            runningTs.Dispose();
        }

        RosQueueException CreateQueueException(Exception e) =>
            new($"An unexpected exception was thrown while sending to node '{remoteCallerId}'", e,
                CreateConnectionInfo());

        RosQueueOverflowException CreateQueueOverflowException() =>
            new($"Package size was larger than the maximum. Fragmentation not implemented yet", CreateConnectionInfo());

        public PublisherSenderState State =>
            new()
            {
                IsAlive = IsAlive,
                TransportType = TransportType.Udp,
                Endpoint = Endpoint,
                RemoteId = remoteCallerId,
                RemoteEndpoint = RemoteEndpoint,
                CurrentQueueSize = 0,
                MaxQueueSize = 0,
                NumSent = numSent,
                BytesSent = bytesSent,
                NumDropped = numDropped,
                BytesDropped = bytesDropped
            };

        IRosSenderInfo CreateConnectionInfo() => new RosSenderInfo(
            topicInfo.Topic,
            topicInfo.Type,
            TransportType.Udp,
            remoteCallerId,
            RemoteEndpoint,
            Endpoint,
            rosHeader,
            State,
            IsAlive
        );

        public void Publish(in T message)
        {
            if (!IsAlive)
            {
                numDropped++;
                return;
            }
            
            messageQueue.Add((message, null));
        }

        public async Task PublishAndWaitAsync(T message, CancellationToken token)
        {
            if (!IsAlive)
            {
                numDropped++;
                throw new InvalidOperationException("Sender has been disposed.");
            }

            var msgSignal = new TaskCompletionSource();
            await messageQueue.AddAsync((message, msgSignal), token);

            using var registration = token.Register(() => msgSignal.TrySetCanceled(token));
            await msgSignal.Task;
        }

        public override string ToString() =>
            $"[UdpSender '{topicInfo.Topic}' :{Endpoint.Port.ToString()} >>'{remoteCallerId}']";
    }
}