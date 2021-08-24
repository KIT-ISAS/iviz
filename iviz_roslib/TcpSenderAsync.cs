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
using Iviz.Roslib.Utils;
using Iviz.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TaskCompletionSource = System.Threading.Tasks.TaskCompletionSource<object?>;

namespace Iviz.Roslib
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SenderStatus
    {
        Inactive,
        Waiting,
        Active,
        Dead
    }

    internal sealed class TcpSenderAsync<T> where T : IMessage
    {
        const int MaxSizeInPacketsWithoutConstraint = 2;

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

        bool KeepRunning => !runningTs.IsCancellationRequested;
        public Endpoint Endpoint { get; }
        public Endpoint RemoteEndpoint { get; }
        public IReadOnlyList<string>? TcpHeader { get; private set; }
        public SenderStatus Status { get; private set; }
        public ILoopbackReceiver<T>? LoopbackReceiver { private get; set; }

        public bool TcpNoDelay
        {
            get => tcpClient.NoDelay;
            set => tcpClient.NoDelay = value;
        }

        public string? RemoteCallerId { get; private set; }
        public string Topic => topicInfo.Topic;
        public bool IsAlive => !task.IsCompleted && tcpClient.Client.CheckIfAlive();
        public int MaxQueueSizeInBytes { get; set; } = 50000;

        public PublisherSenderState State =>
            new()
            {
                IsAlive = IsAlive,
                Status = Status,
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

        public TcpSenderAsync(TcpClient client, TopicInfo<T> topicInfo, NullableMessage<T> latchedMsg)
        {
            Status = SenderStatus.Inactive;
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

        async ValueTask<Rent<byte>> ReceivePacket(NetworkStream stream)
        {
            if (!await stream.ReadChunkAsync(lengthBuffer, 4, runningTs.Token))
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
                if (!await stream.ReadChunkAsync(readBuffer.Array, length, runningTs.Token))
                {
                    throw new IOException("Connection closed during handshake");
                }
            }
            catch (Exception)
            {
                readBuffer.Dispose();
                throw;
            }

            return readBuffer;
        }

        async Task SendHeader(NetworkStream stream, bool latching)
        {
            string[] contents =
            {
                $"md5sum={topicInfo.Md5Sum}",
                $"type={topicInfo.Type}",
                $"callerid={topicInfo.CallerId}",
                latching ? "latching=1" : "latching=0",
                $"message_definition={topicInfo.MessageDependencies}",
            };

            TcpHeader = contents.AsReadOnly();
            await stream.WriteHeaderAsync(contents, runningTs.Token);
        }

        async Task SendErrorHeader(NetworkStream stream, string errorMessage)
        {
            string[] contents =
            {
                errorMessage,
                $"md5sum={topicInfo.Md5Sum}",
                $"type={topicInfo.Type}",
                $"callerid={topicInfo.CallerId}"
            };
            await stream.WriteHeaderAsync(contents, runningTs.Token);
        }

        bool ProcessRemoteHeader(List<string> fields, out string errorMessage)
        {
            if (fields.Count < 5)
            {
                errorMessage = "error=Expected at least 5 fields. Closing connection";
                return false;
            }

            Dictionary<string, string> values = new();
            foreach (string field in fields)
            {
                int index = field.IndexOf('=');
                if (index < 0)
                {
                    errorMessage = $"error=Invalid entry. Expected '=' separator.";
                    return false;
                }

                string key = field.Substring(0, index);
                values[key] = field.Substring(index + 1);
            }

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
                if (receivedType == "*")
                {
                    // OK
                }
                else
                {
                    errorMessage =
                        $"error=Expected message type [{topicInfo.Type}] but received [{receivedType}]";
                    return false;
                }
            }

            if (!values.TryGetValue("md5sum", out string? receivedMd5Sum) || receivedMd5Sum != topicInfo.Md5Sum)
            {
                if (receivedMd5Sum != "*")
                {
                    errorMessage =
                        $"error=Expected md5 '{topicInfo.Md5Sum}' but received '{receivedMd5Sum}'";
                    return false;
                }
            }

            if (TcpNoDelay || values.TryGetValue("tcp_nodelay", out string? receivedNoDelay) && receivedNoDelay == "1")
            {
                TcpNoDelay = true;
            }

            errorMessage = "";
            return true;
        }

        async Task ProcessHandshake(NetworkStream stream, bool latching)
        {
            List<string> fields;
            using (Rent<byte> readBuffer = await ReceivePacket(stream))
            {
                fields = BaseUtils.ParseHeader(readBuffer);
            }

            bool success = ProcessRemoteHeader(fields, out string errorMessage);
            if (success)
            {
                await SendHeader(stream, latching);
            }
            else
            {
                await SendErrorHeader(stream, errorMessage);
                throw new RosHandshakeException(
                    $"Failed to parse header sent by partner. Error message: [{errorMessage}]");
            }
        }

        async Task StartSession(NullableMessage<T> latchedMsg)
        {
            Status = SenderStatus.Waiting;

            try
            {
                using (tcpClient)
                {
                    Status = SenderStatus.Active;
                    Logger.LogDebugFormat("{0}: Started!", this);
                    await ProcessLoop(tcpClient.GetStream(), latchedMsg);
                }
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

            try
            {
                runningTs.Cancel();
            }
            catch (ObjectDisposedException)
            {
            }

            Status = SenderStatus.Dead;
            foreach (var (_, msgSignal) in messageQueue)
            {
                msgSignal?.TrySetException(new RosQueueException(
                    $"Connection for '{RemoteCallerId}' is shutting down", CreateConnectionInfo()));
            }
        }

        RosQueueException CreateQueueException(Exception e) =>
            new($"An unexpected exception was thrown while sending to node '{RemoteCallerId}'", e,
                CreateConnectionInfo());

        RosQueueOverflowException CreateOverflowException() =>
            new($"Message could not be sent to node '{RemoteCallerId}'", CreateConnectionInfo());

        IRosTcpSender CreateConnectionInfo() => new RosTcpSender(
            Topic,
            topicInfo.Type,
            RemoteCallerId,
            RemoteEndpoint,
            Endpoint,
            TcpHeader ?? Array.Empty<string>(),
            State,
            IsAlive,
            Status
        );

        async Task ProcessLoop(NetworkStream stream, NullableMessage<T> latchedMsg)
        {
            using ResizableRent<byte> writeBuffer = new(4);

            await ProcessHandshake(stream, latchedMsg.HasValue);

            if (latchedMsg.HasValue)
            {
                Publish(latchedMsg.Value);
            }

            List<Element> queue = new();

            while (KeepRunning)
            {
                await signal.WaitAsync(runningTs.Token);

                int totalQueueSizeInBytes = ReadFromQueue(queue);

                int startIndex, newBytesDropped;
                if (queue.Count <= MaxSizeInPacketsWithoutConstraint ||
                    totalQueueSizeInBytes < MaxQueueSizeInBytes)
                {
                    startIndex = 0;
                    newBytesDropped = 0;
                }
                else
                {
                    DiscardOldMessages(queue, totalQueueSizeInBytes, MaxQueueSizeInBytes,
                        out startIndex, out newBytesDropped);
                }

                numDropped += startIndex;
                bytesDropped += newBytesDropped;

                foreach (var (_, _, msgSignal) in queue.Take(startIndex))
                {
                    msgSignal?.TrySetException(CreateOverflowException());
                }

                if (LoopbackReceiver != null)
                {
                    SendWithLoopback(queue, startIndex);
                }
                else
                {
                    await SendWithSocketAsync(stream, queue, startIndex, writeBuffer);
                }
            }
        }

        void SendWithLoopback(IReadOnlyList<Element> queue, int startIndex)
        {
            var loopbackReceiver = LoopbackReceiver ??
                                   throw new NullReferenceException("Can only use this when loopback receiver is set");

            try
            {
                foreach (var (msg, msgLength, msgSignal) in queue.Skip(startIndex))
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

        async Task SendWithSocketAsync(NetworkStream stream, IReadOnlyList<Element> queue, int startIndex,
            ResizableRent<byte> writeBuffer)
        {
            void WriteLengthToBuffer(uint i)
            {
                byte[] array = writeBuffer.Array;
                array[3] = (byte)(i >> 0x18);
                array[0] = (byte)i;
                array[1] = (byte)(i >> 8);
                array[2] = (byte)(i >> 0x10);
            }

            try
            {
                foreach (var (msg, msgLength, msgSignal) in queue.Skip(startIndex))
                {
                    writeBuffer.EnsureCapability(msgLength + 4);

                    uint sendLength = msg.SerializeToArray(writeBuffer.Array, 4);

                    WriteLengthToBuffer(sendLength);
                    await stream.WriteChunkAsync(writeBuffer.Array, (int)sendLength + 4, runningTs.Token);

                    numSent++;
                    bytesSent += (int)sendLength + 4;
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
                .Select(element => element.Signal)
                .Where(msgSignal => msgSignal != null && !msgSignal.Task.IsCompleted);
            foreach (var msgSignal in entries)
            {
                msgSignal?.TrySetException(CreateQueueException(e));
            }
        }


        public void Publish(in T message)
        {
            if (!IsAlive)
            {
                numDropped++;
                return;
            }

            messageQueue.Enqueue((message, null));
            signal.Release();
        }

        public async Task PublishAndWaitAsync(T message, CancellationToken token)
        {
            if (!IsAlive)
            {
                numDropped++;
                throw new InvalidOperationException("Sender has been disposed.");
            }

            TaskCompletionSource msgSignal = new();
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
            for (int i = 0; i < MaxSizeInPacketsWithoutConstraint; i++)
            {
                remainingBytes -= queue[c - i].MessageLength;
            }

            int consideredPackets = MaxSizeInPacketsWithoutConstraint;
            if (remainingBytes > 0)
            {
                // start discarding old messages
                for (int i = MaxSizeInPacketsWithoutConstraint; i < queue.Count; i++)
                {
                    int currentMsgLength = queue[c - i].MessageLength;
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

        public override string ToString()
        {
            return $"[TcpSender '{Topic}' :{Endpoint.Port.ToString()} >>'{RemoteCallerId}']";
        }

        readonly struct Element
        {
            T Message { get; }
            public int MessageLength { get; }
            public TaskCompletionSource? Signal { get; }

            public Element(T message, int msgLength, TaskCompletionSource? signal) =>
                (Message, MessageLength, Signal) = (message, msgLength, signal);

            public void Deconstruct(out T message, out int msgLength, out TaskCompletionSource? signal) =>
                (message, msgLength, signal) = (Message, MessageLength, Signal);
        }
    }
}