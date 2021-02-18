using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Buffer = Iviz.Msgs.Buffer;
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
        string RemoteCallerId { get; }
        Endpoint? RemoteEndpoint { get; }
        Endpoint? Endpoint { get; }
        IReadOnlyList<string>? TcpHeader { get; }
        PublisherSenderState State { get; }
        bool IsAlive { get; }
        SenderStatus Status { get; }
    }

    internal sealed class TcpSenderAsync<T> : IRosTcpSender where T : IMessage
    {
        const int MaxSizeInPacketsWithoutConstraint = 2;
        const int MaxConnectionRetries = 3;

        readonly SemaphoreSlim signal = new(0);
        readonly ConcurrentQueue<(SharedMessage<T> msg, TaskCompletionSource? signal)> messageQueue = new();
        readonly CancellationTokenSource runningTs = new();
        readonly TopicInfo<T> topicInfo;
        readonly bool latching;
        readonly Task? task;
        readonly TcpListener? listener;
        readonly int timeoutInMs;
        readonly byte[] lengthBuffer = new byte[4];


        long bytesDropped;
        long bytesSent;
        bool disposed;

        long numDropped;
        long numSent;
        TcpClient? tcpClient;
        bool tcpNoDelay;

        bool KeepRunning => !runningTs.IsCancellationRequested;
        public Endpoint Endpoint { get; }
        public Endpoint? RemoteEndpoint { get; private set; }
        public IReadOnlyList<string>? TcpHeader { get; private set; }
        public SenderStatus Status { get; private set; }

        public bool TcpNoDelay
        {
            get => tcpNoDelay;
            set
            {
                tcpNoDelay = value;
                if (tcpClient != null)
                {
                    tcpClient.NoDelay = true;
                }
            }
        }


        public TcpSenderAsync(string remoteCallerId, TopicInfo<T> topicInfo, bool latching, int timeoutInMs,
            SemaphoreSlim managerSignal)
        {
            RemoteCallerId = remoteCallerId;
            Status = SenderStatus.Inactive;
            this.timeoutInMs = timeoutInMs;
            this.topicInfo = topicInfo;
            this.latching = latching;

            listener = new TcpListener(IPAddress.IPv6Any, 0) {Server = {DualMode = true}};
            listener.Start();
            Endpoint = new Endpoint((IPEndPoint) listener.LocalEndpoint);
            task = Task.Run(async () => await StartSession(managerSignal).Caf());
        }

        public string RemoteCallerId { get; }
        public string Topic => topicInfo.Topic;
        public bool IsAlive => task != null && !task.IsCompleted;
        public int MaxQueueSizeInBytes { get; set; } = 50000;

        public PublisherSenderState State =>
            new()
            {
                IsAlive = IsAlive,
                Latching = latching,
                Status = Status,
                Endpoint = Endpoint,
                RemoteId = RemoteCallerId,
                RemoteEndpoint = RemoteEndpoint,
                CurrentQueueSize = messageQueue.Count,
                MaxQueueSize = MaxQueueSizeInBytes,
                NumSent = numSent,
                BytesSent = bytesSent,
                NumDropped = numDropped,
                BytesDropped = bytesDropped
            };

        public async Task DisposeAsync()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;

            runningTs.Cancel();

            if (task != null)
            {
                await task.WaitForWithTimeout(5000, "Sender task dispose timed out.").AwaitNoThrow(this);
            }

            runningTs.Dispose();
        }

        async Task<Rent<byte>> ReceivePacket(NetworkStream stream)
        {
            if (!await stream.ReadChunkAsync(lengthBuffer, 4, runningTs.Token).Caf())
            {
                throw new IOException("Connection closed during handshake.");
            }

            int length = BitConverter.ToInt32(lengthBuffer, 0);

            if (length == 0)
            {
                return new Rent<byte>(0);
            }

            var readBuffer = new Rent<byte>(length);
            try
            {
                if (!await stream.ReadChunkAsync(readBuffer.Array, length, runningTs.Token).Caf())
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

        async Task SendHeader(NetworkStream stream, string? errorMessage)
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
                    $"message_definition={topicInfo.MessageDependencies}",
                    latching ? "latching=1" : "latching=0"
                };

                TcpHeader = contents.AsReadOnly();
            }

            await Utils.WriteHeaderAsync(stream, contents, runningTs.Token).Caf();
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

            if (!values.TryGetValue("callerid", out string? receivedId) || receivedId != RemoteCallerId)
            {
                return
                    $"error=Expected callerid '{RemoteCallerId}' but partner provided '{receivedId}', closing connection";
            }

            if (!values.TryGetValue("topic", out string? receivedTopic) || receivedTopic != topicInfo.Topic)
            {
                return
                    $"error=Expected topic '{topicInfo.Topic}' but partner provided '{receivedTopic}', closing connection";
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
                        $"error=Expected type '{topicInfo.Type}' but partner provided '{receivedType}', closing connection";
                }
            }

            if (!values.TryGetValue("md5sum", out string? receivedMd5Sum) || receivedMd5Sum != topicInfo.Md5Sum)
            {
                if (receivedMd5Sum == "*")
                {
                    // OK
                }
                else
                {
                    return
                        $"error=Expected md5 '{topicInfo.Md5Sum}' but partner provided '{receivedMd5Sum}', closing connection";
                }
            }

            if (TcpNoDelay || values.TryGetValue("tcp_nodelay", out string? receivedNoDelay) && receivedNoDelay == "1")
            {
                TcpNoDelay = true;
            }

            return null;
        }

        async Task ProcessHandshake(NetworkStream stream)
        {
            List<string> fields;
            using (Rent<byte> readBuffer = await ReceivePacket(stream).Caf())
            {
                fields = Utils.ParseHeader(readBuffer);
            }

            string? errorMessage = ProcessRemoteHeader(fields);

            await SendHeader(stream, errorMessage).Caf();

            if (errorMessage != null)
            {
                throw new RosRpcException("Failed handshake: " + errorMessage);
            }
        }

        async Task StartSession(SemaphoreSlim? managerSignal)
        {
            Status = SenderStatus.Waiting;

            for (int round = 0; round < MaxConnectionRetries && KeepRunning; round++)
            {
                try
                {
                    Task<TcpClient> connectionTask = listener!.AcceptTcpClientAsync();

                    TryRelease(managerSignal);
                    managerSignal = null;

                    if (!KeepRunning)
                    {
                        break;
                    }

                    TcpClient newTcpClient;
                    IPEndPoint? newRemoteEndPoint;
                    if (!await connectionTask.WaitFor(timeoutInMs, runningTs.Token).Caf()
                        || !connectionTask.RanToCompletion()
                        || (newTcpClient = await connectionTask.Caf()) == null
                        || (newRemoteEndPoint = (IPEndPoint?) newTcpClient.Client.RemoteEndPoint) == null)
                    {
                        Logger.LogFormat("{0}: Connection timed out (round {1}/{2}): {3}",
                            this, (round + 1).ToString(), MaxConnectionRetries.ToString(), connectionTask.Exception);
                        continue;
                    }

                    round = 0;

                    using (tcpClient = newTcpClient)
                    using (NetworkStream stream = tcpClient.GetStream())
                    {
                        Status = SenderStatus.Active;
                        RemoteEndpoint = new Endpoint(newRemoteEndPoint);
                        Logger.LogDebugFormat("{0}: Started!", this);
                        await ProcessLoop(stream).Caf();
                    }
                }
                catch (Exception e)
                {
                    switch (e)
                    {
                        case OperationCanceledException _:
                        case ObjectDisposedException _:
                            break;
                        case IOException _:
                        case TimeoutException _:
                        case SocketException _:
                            Logger.LogDebugFormat(Utils.GenericExceptionFormat, this, e);
                            break;
                        default:
                            Logger.LogFormat(Utils.GenericExceptionFormat, this, e);
                            break;
                    }
                }
            }

            try
            {
                runningTs.Cancel();
            }
            catch (ObjectDisposedException) { }

            Status = SenderStatus.Dead;
            listener?.Stop();
            tcpClient = null;
            TryRelease(managerSignal);

            foreach (var (msg, msgSignal) in messageQueue)
            {
                msg.Dispose();
                msgSignal?.TrySetException(new RosQueueException(
                    $"Connection for '{RemoteCallerId}' is shutting down", this));
            }
        }

        static void TryRelease(SemaphoreSlim? s)
        {
            try
            {
                s?.Release();
            }
            catch (ObjectDisposedException)
            {
            }
        }

        async Task ProcessLoop(NetworkStream stream)
        {
            using ResizableRent<byte> writeBuffer = new(4);

            void WriteLengthToBuffer(uint i)
            {
                var array = writeBuffer.Array;
                array[3] = (byte) (i >> 0x18);
                array[0] = (byte) i;
                array[1] = (byte) (i >> 8);
                array[2] = (byte) (i >> 0x10);
            }

            await ProcessHandshake(stream).Caf();

            List<(SharedMessage<T> msg, int msgLength, TaskCompletionSource? signal)> tmpQueue = new();

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
                    var (msg, _, msgSignal) = tmpQueue[i];
                    msg.Dispose();
                    msgSignal?.TrySetException(
                        new RosQueueOverflowException($"Message could not be sent to node '{RemoteCallerId}'", this));
                }

                for (int i = startIndex; i < tmpQueue.Count; i++)
                {
                    try
                    {
                        var (msg, msgLength, msgSignal) = tmpQueue[i];
                        writeBuffer.EnsureCapability(msgLength + 4);

                        uint sendLength;
                        using (msg)
                        {
                            sendLength = Buffer.Serialize(msg.Message, writeBuffer.Array, 4);
                        }

                        WriteLengthToBuffer(sendLength);
                        await stream.WriteChunkAsync(writeBuffer.Array, (int) sendLength + 4, runningTs.Token).Caf();

                        numSent++;
                        bytesSent += (int) sendLength + 4;
                        msgSignal?.TrySetResult(null);
                    }
                    catch (Exception e)
                    {
                        Exception queueException = new RosQueueException(
                            $"An unexpected exception was thrown while sending to node '{RemoteCallerId}'",
                            e, this);
                        for (int j = i; j < tmpQueue.Count; j++)
                        {
                            var (msg, _, msgSignal) = tmpQueue[j];
                            msg.Dispose();
                            msgSignal?.TrySetException(queueException);
                        }

                        throw;
                    }
                }
            }
        }

        public void Publish(in SharedMessage<T> message)
        {
            if (!IsAlive)
            {
                numDropped++;
                return;
            }

            messageQueue.Enqueue((message.Share(), null));
            signal.Release();
        }

        public async Task PublishAndWaitAsync(SharedMessage<T> message, CancellationToken token)
        {
            if (!IsAlive)
            {
                numDropped++;
                throw new InvalidOperationException("Sender has been disposed.");
            }

            TaskCompletionSource msgSignal = new();
            messageQueue.Enqueue((message.Share(), msgSignal));
            signal.Release();

            using var registration = token.Register(() => msgSignal.TrySetCanceled(token));
            await msgSignal.Task;
        }

        int ReadFromQueue(List<(SharedMessage<T>, int, TaskCompletionSource?)> result)
        {
            int totalQueueSizeInBytes = 0;

            result.Clear();
            while (messageQueue.TryDequeue(out var tuple))
            {
                int msgLength = tuple.msg.Message.RosMessageLength;
                result.Add((tuple.msg, msgLength, tuple.signal));
                totalQueueSizeInBytes += msgLength;
            }

            return totalQueueSizeInBytes;
        }

        static void DiscardOldMessages(List<(SharedMessage<T>, int msgLength, TaskCompletionSource?)> queue,
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
            return $"[TcpSender '{Topic}' :{Endpoint?.Port ?? -1} >>'{RemoteCallerId}']";
        }
    }
}