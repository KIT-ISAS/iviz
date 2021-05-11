using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib.Utils;
using Iviz.XmlRpc;
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

    public interface IRosTcpSender
    {
        string Topic { get; }
        string? RemoteCallerId { get; }
        Endpoint RemoteEndpoint { get; }
        Endpoint Endpoint { get; }
        IReadOnlyList<string>? TcpHeader { get; }
        PublisherSenderState State { get; }
        bool IsAlive { get; }
        SenderStatus Status { get; }
    }

    internal sealed class TcpSenderAsync<T> : IRosTcpSender where T : IMessage
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
            Endpoint = new Endpoint((IPEndPoint) tcpClient.Client.LocalEndPoint!);
            RemoteEndpoint = new Endpoint((IPEndPoint) tcpClient.Client.RemoteEndPoint!);
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

            await task.AwaitNoThrow(5000, this, token);

            runningTs.Dispose();
        }

        async ValueTask<Rent<byte>> ReceivePacket(NetworkStream stream)
        {
            if (!await stream.ReadChunkAsync(lengthBuffer, 4, runningTs.Token))
            {
                throw new IOException("Connection closed during handshake.");
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
                    throw new IOException("Connection closed during handshake.");
                }
            }
            catch (Exception)
            {
                readBuffer.Dispose();
                throw;
            }

            return readBuffer;
        }

        async Task SendHeader(NetworkStream stream, bool latching, string? errorMessage)
        {
            string[] contents;
            if (errorMessage != null)
            {
                contents = new[]
                {
                    errorMessage,
                    $"md5sum={topicInfo.Md5Sum}",
                    $"type={topicInfo.Type}",
                    $"callerid={topicInfo.CallerId}"
                };
            }
            else
            {
                contents = new[]
                {
                    $"md5sum={topicInfo.Md5Sum}",
                    $"type={topicInfo.Type}",
                    $"callerid={topicInfo.CallerId}",
                    latching ? "latching=1" : "latching=0",
                    $"message_definition={topicInfo.MessageDependencies}",
                };

                TcpHeader = contents.AsReadOnly();
            }

            await stream.WriteHeaderAsync(contents, runningTs.Token);
        }

        string? ProcessRemoteHeader(List<string> fields)
        {
            if (fields.Count < 5)
            {
                return "error=Expected at least 5 fields, closing connection";
            }

            Dictionary<string, string> values = new();
            foreach (string field in fields)
            {
                int index = field.IndexOf('=');
                if (index < 0)
                {
                    return $"error=Invalid field '{field}'";
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
                return "error=Missing entry 'callerid'";
            }

            if (!values.TryGetValue("topic", out string? receivedTopic) || receivedTopic != topicInfo.Topic)
            {
                return
                    $"error=Expected topic '{topicInfo.Topic}' but received '{receivedTopic}', closing connection";
            }

            if (!values.TryGetValue("type", out string? receivedType) || receivedType != topicInfo.Type)
            {
                if (receivedType == "*")
                {
                    // OK
                }
                else
                {
                    return
                        $"error=Expected type '{topicInfo.Type}' but received '{receivedType}', closing connection";
                }
            }

            if (!values.TryGetValue("md5sum", out string? receivedMd5Sum) || receivedMd5Sum != topicInfo.Md5Sum)
            {
                if (receivedMd5Sum != "*")
                {
                    return
                        $"error=Expected md5 '{topicInfo.Md5Sum}' but received '{receivedMd5Sum}', closing connection";
                }
            }

            if (TcpNoDelay || values.TryGetValue("tcp_nodelay", out string? receivedNoDelay) && receivedNoDelay == "1")
            {
                TcpNoDelay = true;
            }

            return null;
        }

        async Task ProcessHandshake(NetworkStream stream, bool latching)
        {
            List<string> fields;
            using (Rent<byte> readBuffer = await ReceivePacket(stream))
            {
                fields = BaseUtils.ParseHeader(readBuffer);
            }

            string? errorMessage = ProcessRemoteHeader(fields);
            
            await SendHeader(stream, latching, errorMessage);

            if (errorMessage != null)
            {
                throw new RosHandshakeException($"Partner sent error message: [{errorMessage}]");
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
                    $"Connection for '{RemoteCallerId}' is shutting down", this));
            }
        }

        RosQueueException CreateQueueException(Exception e) =>
            new($"An unexpected exception was thrown while sending to node '{RemoteCallerId}'", e, this);

        RosQueueOverflowException CreateOverflowException() =>
            new($"Message could not be sent to node '{RemoteCallerId}'", this);

        async Task ProcessLoop(NetworkStream stream, NullableMessage<T> latchedMsg)
        {
            using ResizableRent<byte> writeBuffer = new(4);

            void WriteLengthToBuffer(uint i)
            {
                byte[] array = writeBuffer.Array;
                array[3] = (byte) (i >> 0x18);
                array[0] = (byte) i;
                array[1] = (byte) (i >> 8);
                array[2] = (byte) (i >> 0x10);
            }

            await ProcessHandshake(stream, latchedMsg.HasValue);

            if (latchedMsg.HasValue)
            {
                Publish(latchedMsg.Value);
            }

            List<(T msg, int msgLength, TaskCompletionSource? signal)> tmpQueue = new();

            while (KeepRunning)
            {
                await signal.WaitAsync(runningTs.Token);

                int totalQueueSizeInBytes = ReadFromQueue(tmpQueue);

                int startIndex, newBytesDropped;
                if (tmpQueue.Count <= MaxSizeInPacketsWithoutConstraint ||
                    totalQueueSizeInBytes < MaxQueueSizeInBytes)
                {
                    startIndex = 0;
                    newBytesDropped = 0;
                }
                else
                {
                    DiscardOldMessages(tmpQueue, totalQueueSizeInBytes, MaxQueueSizeInBytes,
                        out startIndex, out newBytesDropped);
                }

                numDropped += startIndex;
                bytesDropped += newBytesDropped;

                for (int i = 0; i < startIndex; i++)
                {
                    tmpQueue[i].signal?.TrySetException(CreateOverflowException());
                }

                for (int i = startIndex; i < tmpQueue.Count; i++)
                {
                    try
                    {
                        (T msg, int msgLength, var msgSignal) = tmpQueue[i];
                        writeBuffer.EnsureCapability(msgLength + 4);

                        uint sendLength = msg.SerializeToArray(writeBuffer.Array, 4);

                        WriteLengthToBuffer(sendLength);
                        await stream.WriteChunkAsync(writeBuffer.Array, (int) sendLength + 4, runningTs.Token);

                        numSent++;
                        bytesSent += (int) sendLength + 4;
                        msgSignal?.TrySetResult(null);
                    }
                    catch (Exception e)
                    {
                        for (int j = i; j < tmpQueue.Count; j++)
                        {
                            tmpQueue[j].signal?.TrySetException(CreateQueueException(e));
                        }

                        throw;
                    }
                }
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

        int ReadFromQueue(List<(T, int, TaskCompletionSource?)> result)
        {
            int totalQueueSizeInBytes = 0;

            result.Clear();
            while (messageQueue.TryDequeue(out var tuple))
            {
                int msgLength = tuple.msg.RosMessageLength;
                result.Add((tuple.msg, msgLength, tuple.signal));
                totalQueueSizeInBytes += msgLength;
            }

            return totalQueueSizeInBytes;
        }

        static void DiscardOldMessages(List<(T, int msgLength, TaskCompletionSource?)> queue,
            int totalQueueSizeInBytes, int maxQueueSizeInBytes, out int numDropped, out int bytesDropped)
        {
            int c = queue.Count - 1;

            int remainingBytes = maxQueueSizeInBytes;
            for (int i = 0; i < MaxSizeInPacketsWithoutConstraint; i++)
            {
                remainingBytes -= queue[c - i].msgLength;
            }

            int consideredPackets = MaxSizeInPacketsWithoutConstraint;
            if (remainingBytes > 0)
            {
                // start discarding old messages
                for (int i = MaxSizeInPacketsWithoutConstraint; i < queue.Count; i++)
                {
                    int currentMsgLength = queue[c - i].msgLength;
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
    }
}