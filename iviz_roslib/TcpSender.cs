using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;
using Iviz.Roslib.Utils;
using Iviz.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TaskCompletionSource = System.Threading.Tasks.TaskCompletionSource<object?>;

namespace Iviz.Roslib
{
    internal sealed class TcpSender<T> : IProtocolSender<T>, IRosSenderInfo where T : IMessage
    {
        const int MaxPacketsWithoutConstraint = 2;

        readonly SemaphoreSlim signal = new(0);
        readonly ConcurrentQueue<(T msg, TaskCompletionSource? signal)> messageQueue = new();
        readonly CancellationTokenSource runningTs = new();
        readonly TopicInfo<T> topicInfo;
        readonly Task task;
        readonly byte[] lengthBuffer = new byte[4];
        readonly TcpClient tcpClient;

        long bytesDropped;
        long bytesSent;
        bool disposed;

        long numDropped;
        long numSent;

        public IReadOnlyCollection<string> RosHeader { get; private set; } = Array.Empty<string>();
        public string? RemoteCallerId { get; private set; }
        public Endpoint Endpoint { get; }
        public Endpoint RemoteEndpoint { get; }
        public TransportType TransportType => TransportType.Tcp;
        public ILoopbackReceiver<T>? LoopbackReceiver { private get; set; }

        bool KeepRunning => !runningTs.IsCancellationRequested;

        public bool TcpNoDelay
        {
            get => tcpClient.NoDelay;
            set => tcpClient.NoDelay = value;
        }

        public string Topic => topicInfo.Topic;
        public string Type => topicInfo.Type;
        bool IsRunning => !task.IsCompleted;
        public bool IsAlive => IsRunning && tcpClient.Client.CheckIfAlive();
        public int MaxQueueSizeInBytes { get; set; } = 50000;

        public PublisherSenderState State =>
            new()
            {
                IsAlive = IsAlive,
                Endpoint = Endpoint,
                RemoteId = RemoteCallerId ?? "",
                RemoteEndpoint = RemoteEndpoint,
                CurrentQueueSize = messageQueue.Count,
                MaxQueueSize = MaxQueueSizeInBytes,
                NumSent = numSent,
                BytesSent = bytesSent,
                NumDropped = numDropped,
                BytesDropped = bytesDropped
            };

        public TcpSender(TcpClient client, TopicInfo<T> topicInfo, NullableMessage<T> latchedMsg)
        {
            this.topicInfo = topicInfo;

            tcpClient = client;
            Endpoint = new Endpoint((IPEndPoint)tcpClient.Client.LocalEndPoint!);
            RemoteEndpoint = new Endpoint((IPEndPoint)tcpClient.Client.RemoteEndPoint!);
            task = TaskUtils.StartLongTask(() => StartSession(latchedMsg), runningTs.Token);
        }

        public async Task DisposeAsync(CancellationToken token)
        {
            if (disposed)
            {
                return;
            }

            disposed = true;

            runningTs.Cancel();
            tcpClient.Dispose();

            await task.AwaitNoThrow(5000, this, token);
            runningTs.Dispose();
        }

        async ValueTask<Rent<byte>> ReceivePacket()
        {
            if (!await tcpClient.ReadChunkAsync(lengthBuffer, 4, runningTs.Token))
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
                if (await tcpClient.ReadChunkAsync(readBuffer.Array, length, runningTs.Token))
                {
                    return readBuffer;
                }
            }
            catch (Exception)
            {
                readBuffer.Dispose();
                throw;
            }

            readBuffer.Dispose();
            throw new IOException("Connection closed during handshake");
        }

        Task SendHeader(bool latching)
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
            return tcpClient.WriteHeaderAsync(contents, runningTs.Token);
        }

        Task SendErrorHeader(string errorMessage)
        {
            string[] contents =
            {
                errorMessage,
                $"md5sum={topicInfo.Md5Sum}",
                $"type={topicInfo.Type}",
                $"callerid={topicInfo.CallerId}"
            };
            return tcpClient.WriteHeaderAsync(contents, runningTs.Token);
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
                if (receivedType != DynamicMessage.RosMessageType) // "*"
                {
                    errorMessage =
                        $"error=Expected message type [{topicInfo.Type}] but received [{receivedType}]";
                    return false;
                }
            }

            if (!values.TryGetValue("md5sum", out string? receivedMd5Sum) || receivedMd5Sum != topicInfo.Md5Sum)
            {
                if (receivedMd5Sum != DynamicMessage.RosMd5Sum) // "*"
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

        async Task ProcessHandshake(bool latching)
        {
            List<string> fields;
            using (Rent<byte> readBuffer = await ReceivePacket())
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

            tcpClient.Dispose();
            try
            {
                runningTs.Cancel();
            }
            catch (ObjectDisposedException)
            {
            }

            foreach (var (_, msgSignal) in messageQueue)
            {
                msgSignal?.TrySetException(new RosQueueException(
                    $"Connection for '{RemoteCallerId}' is shutting down", this));
            }
        }

        RosQueueException CreateQueueException(Exception e) =>
            new($"An unexpected exception was thrown while sending to node '{RemoteCallerId}'", e, this);

        RosQueueOverflowException CreateOverflowException() =>
            new($"Message could not be sent to node '{RemoteCallerId}'", this);

        async Task ProcessLoop(NullableMessage<T> latchedMsg)
        {
            using ByteBufferRent writeBuffer = new(4);

            await ProcessHandshake(latchedMsg.HasValue);

            if (latchedMsg.HasValue)
            {
                Publish(latchedMsg.Value);
            }

            List<Element> queue = new();

            while (KeepRunning)
            {
                await signal.WaitAsync(runningTs.Token);

                int totalQueueSizeInBytes = ReadFromQueue(queue);

                int startIndex;
                if (queue.Count <= MaxPacketsWithoutConstraint ||
                    totalQueueSizeInBytes < MaxQueueSizeInBytes)
                {
                    startIndex = 0;
                }
                else
                {
                    DiscardOldMessages(queue, totalQueueSizeInBytes, MaxQueueSizeInBytes,
                        out startIndex, out int newBytesDropped);

                    numDropped += startIndex;
                    bytesDropped += newBytesDropped;

                    var discarded = queue.Take(startIndex);
                    foreach (var e in discarded)
                    {
                        e.signal?.TrySetException(CreateOverflowException());
                    }
                }


                if (LoopbackReceiver != null)
                {
                    SendWithLoopback(queue.Skip(startIndex), LoopbackReceiver);
                }
                else
                {
                    await SendWithSocketAsync(queue.Skip(startIndex), writeBuffer);
                }
            }
        }

        void SendWithLoopback(in RangeEnumerable<Element> queue, ILoopbackReceiver<T> loopbackReceiver)
        {
            try
            {
                foreach (var (msg, msgLength, msgSignal) in queue)
                {
                    loopbackReceiver.Post(msg, msgLength);

                    numSent++;
                    bytesSent += msgLength + 4;
                    msgSignal?.TrySetResult(null);
                }
            }
            catch (Exception e)
            {
                SetExceptionsInMessages(queue, CreateQueueException(e));
                throw;
            }
        }

        async Task SendWithSocketAsync(RangeEnumerable<Element> queue, ByteBufferRent writeBuffer)
        {
            void WriteLengthToBuffer(int i)
            {
                byte[] array = writeBuffer.Array;
                array[3] = (byte)(i >> 0x18);
                array[0] = (byte)i;
                array[1] = (byte)(i >> 8);
                array[2] = (byte)(i >> 0x10);
            }

            try
            {
                foreach (var (msg, msgLength, msgSignal) in queue)
                {
                    writeBuffer.EnsureCapability(msgLength + 4);

                    msg.SerializeToArray(writeBuffer.Array, 4);
                    WriteLengthToBuffer(msgLength);
                    await tcpClient.WriteChunkAsync(writeBuffer.Array, msgLength + 4, runningTs.Token);

                    numSent++;
                    bytesSent += msgLength + 4;
                    msgSignal?.TrySetResult(null);
                }
            }
            catch (Exception e)
            {
                SetExceptionsInMessages(queue, CreateQueueException(e));
                throw;
            }
        }

        void SetExceptionsInMessages(IEnumerable<Element> queue, Exception e)
        {
            var entries = queue
                .Select(element => element.signal)
                .Where(msgSignal => msgSignal != null && !msgSignal.Task.IsCompleted);
            foreach (var msgSignal in entries)
            {
                msgSignal?.TrySetException(CreateQueueException(e));
            }
        }


        public void Publish(in T message)
        {
            if (!IsRunning)
            {
                numDropped++;
                return;
            }

            messageQueue.Enqueue((message, null));
            signal.Release();
        }

        public async Task PublishAndWaitAsync(T message, CancellationToken token)
        {
            if (!IsRunning)
            {
                numDropped++;
                throw new InvalidOperationException("Sender has been disposed.");
            }

            var msgSignal = new TaskCompletionSource();
            messageQueue.Enqueue((message, msgSignal));
            signal.Release();

            using var registration = token.Register(() => msgSignal.TrySetCanceled(token));
            await msgSignal.Task;
        }

        int ReadFromQueue(List<Element> result)
        {
            int totalQueueSizeInBytes = 0;

            result.Clear();
            while (messageQueue.TryDequeue(out var tuple))
            {
                int msgLength = tuple.msg.RosMessageLength;
                result.Add(new Element(tuple.msg, msgLength, tuple.signal));
                totalQueueSizeInBytes += msgLength;
            }

            return totalQueueSizeInBytes;
        }

        static void DiscardOldMessages(List<Element> queue, int totalQueueSizeInBytes, int maxQueueSizeInBytes,
            out int numDropped, out int bytesDropped)
        {
            int c = queue.Count - 1;

            int remainingBytes = maxQueueSizeInBytes;
            for (int i = 0; i < MaxPacketsWithoutConstraint; i++)
            {
                remainingBytes -= queue[c - i].messageLength;
            }

            int consideredPackets = MaxPacketsWithoutConstraint;
            if (remainingBytes > 0)
            {
                // start discarding old messages
                for (int i = MaxPacketsWithoutConstraint; i < queue.Count; i++)
                {
                    int currentMsgLength = queue[c - i].messageLength;
                    if (currentMsgLength > remainingBytes)
                    {
                        break;
                    }

                    remainingBytes -= currentMsgLength;
                    consideredPackets++;
                }
            }

            numDropped = queue.Count - consideredPackets;
            bytesDropped = totalQueueSizeInBytes - maxQueueSizeInBytes + remainingBytes;
        }

        public override string ToString() => $"[TcpSender '{Topic}' :{Endpoint.Port.ToString()} >>'{RemoteCallerId}']";

        readonly struct Element
        {
            readonly T message;
            public readonly int messageLength;
            public readonly TaskCompletionSource? signal;

            public Element(T message, int messageLength, TaskCompletionSource? signal) =>
                (this.message, this.messageLength, this.signal) = (message, messageLength, signal);

            public void Deconstruct(out T outMessage, out int outMessageLength, out TaskCompletionSource? outSignal) =>
                (outMessage, outMessageLength, outSignal) = (message, messageLength, signal);
        }
    }
}
