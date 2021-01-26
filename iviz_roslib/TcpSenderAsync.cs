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
        const int BufferSizeIncrease = 1024;
        const int MaxSizeInPacketsWithoutConstraint = 2;
        const int MaxConnectionRetries = 3;

        readonly SemaphoreSlim signal = new SemaphoreSlim(0);
        readonly ConcurrentQueue<(T, SemaphoreSlim?)> messageQueue = new ConcurrentQueue<(T, SemaphoreSlim?)>();
        readonly CancellationTokenSource runningTs = new CancellationTokenSource();
        readonly TopicInfo<T> topicInfo;
        readonly bool latching;

        long bytesDropped;
        long bytesSent;
        bool disposed;

        Endpoint? endpoint;
        Endpoint? remoteEndpoint;

        long numDropped;
        long numSent;
        SenderStatus status;
        NetworkStream? stream;
        Task? task;
        TcpClient? tcpClient;
        TcpListener? listener;

        byte[] writeBuffer = Array.Empty<byte>();

        bool KeepRunning => !runningTs.IsCancellationRequested;

        bool tcpNoDelay;

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


        public TcpSenderAsync(string remoteCallerId, TopicInfo<T> topicInfo, bool latching)
        {
            RemoteCallerId = remoteCallerId;
            this.topicInfo = topicInfo;
            status = SenderStatus.Inactive;
            this.latching = latching;
        }

        public string RemoteCallerId { get; }
        string Topic => topicInfo.Topic;
        public bool IsAlive => task != null && !task.IsCompleted;
        public int MaxQueueSizeInBytes { get; set; } = 50000;

        public PublisherSenderState State =>
            new PublisherSenderState(
                IsAlive, latching, status,
                endpoint, RemoteCallerId, remoteEndpoint,
                0, MaxQueueSizeInBytes,
                numSent, bytesSent, numDropped, bytesDropped
            );

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
            writeBuffer = Array.Empty<byte>();
        }

        public Endpoint Start(int timeoutInMs, SemaphoreSlim managerSignal)
        {
            listener = new TcpListener(IPAddress.IPv6Any, 0) {Server = {DualMode = true}};
            listener.Start();

            IPEndPoint localEndpoint = (IPEndPoint) listener.LocalEndpoint;
            endpoint = new Endpoint(localEndpoint);

            task = Task.Run(async () => await StartSession(timeoutInMs, managerSignal).Caf());

            return endpoint;
        }

        async Task<byte[]?> ReceivePacket()
        {
            byte[] lengthBuffer = new byte[4];
            if (!await stream!.ReadChunkAsync(lengthBuffer, 4, runningTs.Token).Caf())
            {
                return null;
            }

            int length = BitConverter.ToInt32(lengthBuffer, 0);
            if (length == 0)
            {
                return Array.Empty<byte>();
            }

            byte[] readBuffer = new byte[length];
            if (!await stream!.ReadChunkAsync(readBuffer, length, runningTs.Token).Caf())
            {
                return null;
            }

            return readBuffer;
        }

        async Task SendHeader(string? errorMessage)
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
            }

            await Utils.WriteHeaderAsync(stream!, contents).Caf();
        }

        string? ProcessRemoteHeader(List<string> fields)
        {
            if (fields.Count < 5)
            {
                return "error=Expected at least 5 fields, closing connection";
            }

            Dictionary<string, string> values = new Dictionary<string, string>();
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
                Logger.LogDebugFormat("{0}: requested tcp_nodelay", this);
            }

            return null;
        }

        async Task ProcessHandshake()
        {
            byte[]? readBuffer = await ReceivePacket().Caf();
            if (readBuffer == null)
            {
                throw new IOException("Connection closed during handshake.");
            }

            List<string> fields = Utils.ParseHeader(readBuffer);
            string? errorMessage = ProcessRemoteHeader(fields);

            await SendHeader(errorMessage).Caf();

            if (errorMessage != null)
            {
                throw new RosRpcException("Failed handshake: " + errorMessage);
            }
        }

        async Task StartSession(int timeoutInMs, SemaphoreSlim? managerSignal)
        {
            status = SenderStatus.Waiting;

            for (int round = 0; round < MaxConnectionRetries && KeepRunning; round++)
            {
                try
                {
                    Task<TcpClient> connectionTask = listener!.AcceptTcpClientAsync();

                    managerSignal?.Release();
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
                            this, round + 1, MaxConnectionRetries, connectionTask.Exception);
                        continue;
                    }

                    round = 0;

                    using (tcpClient = newTcpClient)
                    using (stream = tcpClient.GetStream())
                    {
                        status = SenderStatus.Active;
                        remoteEndpoint = new Endpoint(newRemoteEndPoint);
                        Logger.LogDebugFormat("{0}: Started!", this);
                        await ProcessLoop().Caf();
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

            status = SenderStatus.Dead;
            listener?.Stop();
            tcpClient = null;
            stream = null;

            try
            {
                managerSignal?.Release();
            }
            catch (ObjectDisposedException)
            {
            }
        }

        void WriteLengthToBuffer(uint i)
        {
            writeBuffer[0] = (byte) i;
            writeBuffer[1] = (byte) (i >> 8);
            writeBuffer[2] = (byte) (i >> 0x10);
            writeBuffer[3] = (byte) (i >> 0x18);
        }

        async Task ProcessLoop()
        {
            await ProcessHandshake().Caf();

            List<(T msg, int msgLength, SemaphoreSlim? signal)> tmpQueue = new List<(T, int, SemaphoreSlim?)>();

            if (BuiltIns.TryGetFixedSize(typeof(T), out int fixedSize))
            {
                writeBuffer = new byte[4 + fixedSize];
            }

            while (KeepRunning)
            {
                await signal.WaitAsync(runningTs.Token);

                int totalQueueSizeInBytes = ReadFromQueueWithoutBlocking(tmpQueue);

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
                    tmpQueue[i].signal?.Dispose();
                }

                for (int i = startIndex; i < tmpQueue.Count; i++)
                {
                    T message = tmpQueue[i].msg;
                    int msgLength = tmpQueue[i].msgLength;
                    if (writeBuffer.Length < msgLength + 4)
                    {
                        writeBuffer = new byte[msgLength + 4 + BufferSizeIncrease];
                    }

                    uint sendLength = Buffer.Serialize(message, writeBuffer, 4);
                    WriteLengthToBuffer(sendLength);
                    await stream!.WriteAsync(writeBuffer, 0, (int) sendLength + 4, runningTs.Token).Caf();

                    numSent++;
                    bytesSent += (int) sendLength + 4;
                    tmpQueue[i].signal?.Release();
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
            }

            SemaphoreSlim msgSignal = new SemaphoreSlim(0);
            messageQueue.Enqueue((message, msgSignal));
            signal.Release();

            try
            {
                await msgSignal.WaitAsync(token);
            }
            catch (ObjectDisposedException)
            {
                throw new RosQueueOverflowException($"Message could not be sent to node '{RemoteCallerId}'");
            }
        }

        int ReadFromQueueWithoutBlocking(ICollection<(T msg, int msgLength, SemaphoreSlim? signal)> result)
        {
            int totalQueueSizeInBytes = 0;

            result.Clear();
            while (messageQueue.TryDequeue(out (T msg, SemaphoreSlim? signal) tuple))
            {
                int msgLength = tuple.msg.RosMessageLength;
                result.Add((tuple.msg, msgLength, tuple.signal));
                totalQueueSizeInBytes += msgLength;
            }

            return totalQueueSizeInBytes;
        }

        static void DiscardOldMessages(List<(T msg, int msgLength, SemaphoreSlim?)> queue,
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
            return $"[TcpSender '{Topic}' :{endpoint?.Port ?? -1} >>'{RemoteCallerId}']";
        }
    }
}