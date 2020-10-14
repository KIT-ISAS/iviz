//#define DEBUG__

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    internal sealed class TcpSenderAsync : IDisposable
    {
        const int BufferSizeIncrease = 1024;
        readonly List<IMessage> messageQueue = new List<IMessage>();
        readonly SemaphoreSlim signal = new SemaphoreSlim(0, 1);

        readonly TopicInfo topicInfo;

        bool disposed;

        bool keepRunning;
        NetworkStream stream;
        Task task;
        TcpClient tcpClient;

        TcpListener tcpListener;
        byte[] writeBuffer = new byte[BufferSizeIncrease];

        public TcpSenderAsync(
            Uri callerUri,
            string remoteCallerId,
            TopicInfo topicInfo,
            bool latching)
        {
            RemoteCallerId = remoteCallerId;
            this.topicInfo = topicInfo;
            CallerUri = callerUri;
            Status = SenderStatus.Inactive;
            Latching = latching;
        }

        public string RemoteCallerId { get; }
        Uri CallerUri { get; }
        bool Latching { get; }

        string Topic => topicInfo.Topic;
        public bool IsAlive => task != null && !task.IsCompleted && !task.IsFaulted;
        Endpoint Endpoint { get; set; }
        Endpoint RemoteEndpoint { get; set; }
        public int MaxQueueSizeInBytes { get; set; } = 50000;

        int CurrentQueueSize
        {
            get
            {
                lock (messageQueue)
                {
                    return messageQueue.Count;
                }
            }
        }

        int NumSent { get; set; }
        int BytesSent { get; set; }
        int NumDropped { get; set; }
        int BytesDropped { get; set; }
        public SenderStatus Status { get; private set; }

        public PublisherSenderState State =>
            new PublisherSenderState(
                IsAlive, Latching, Status,
                Endpoint, RemoteCallerId, RemoteEndpoint,
                CurrentQueueSize, MaxQueueSizeInBytes, NumSent, BytesSent, NumDropped
            );

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            keepRunning = false;

            try { signal.Release(); }
            catch (SemaphoreFullException) { }

            task?.Wait();
        }

        public IPEndPoint Start(int timeoutInMs, SemaphoreSlim managerSignal)
        {
            tcpListener = new TcpListener(IPAddress.Any, 0);
            tcpListener.Start();

            IPEndPoint localEndpoint = (IPEndPoint) tcpListener.LocalEndpoint;
            Endpoint = new Endpoint(localEndpoint);

            keepRunning = true;
            task = Task.Run(async () => await Run(timeoutInMs, managerSignal));

            return localEndpoint;
        }

        static List<string> ParseHeader(byte[] readBuffer)
        {
            int numRead = 0;

            List<string> contents = new List<string>();
            while (numRead < readBuffer.Length)
            {
                int length = BitConverter.ToInt32(readBuffer, numRead);
                numRead += 4;
                string entry = BuiltIns.UTF8.GetString(readBuffer, numRead, length);
                numRead += length;
#if DEBUG__
                Logger.Log("<<< " + entry);
#endif
                contents.Add(entry);
            }

            return contents;
        }

        async Task<byte[]> ReceiveHeader()
        {
            byte[] lengthBuffer = new byte[4];
            int numRead = 0;
            while (numRead < 4)
            {
                int readNow = await stream.ReadAsync(lengthBuffer, numRead, 4 - numRead);
                if (readNow == 0)
                {
                    return null;
                }

                numRead += readNow;
            }

            int length = BitConverter.ToInt32(lengthBuffer, 0);
            byte[] readBuffer = new byte[length];
            numRead = 0;
            while (numRead < length)
            {
                int readNow = await stream.ReadAsync(readBuffer, numRead, length - numRead);
                if (readNow == 0)
                {
                    return null;
                }

                numRead += readNow;
            }

            return readBuffer;
        }

        async Task SendResponseHeader(string errorMessage)
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
                    $"latching={(Latching ? "1" : "0")}"
                };
            }

            int totalLength = 4 * contents.Length;
            foreach (string entry in contents)
            {
                totalLength += entry.Length;
            }

            byte[] array = new byte[4 + totalLength];
            using (BinaryWriter writer = new BinaryWriter(new MemoryStream(array)))
            {
                writer.Write(totalLength);
                foreach (string entry in contents)
                {
                    writer.Write(entry.Length);
                    writer.Write(BuiltIns.UTF8.GetBytes(entry));
#if DEBUG__
                Logger.Log(">>> " + contents[i]);
#endif
                }
            }

            await stream.WriteAsync(array, 0, array.Length);
        }

        string ProcessRemoteHeader(ICollection<string> fields)
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

            if (!values.TryGetValue("callerid", out string receivedId) || receivedId != RemoteCallerId)
            {
                return
                    $"error=Expected callerid '{RemoteCallerId}' but received instead '{receivedId}', closing connection";
            }

            if (!values.TryGetValue("topic", out string receivedTopic) || receivedTopic != topicInfo.Topic)
            {
                return
                    $"error=Expected topic '{topicInfo.Topic}' but received instead '{receivedTopic}', closing connection";
            }

            if (!values.TryGetValue("type", out string receivedType) || receivedType != topicInfo.Type)
            {
                if (receivedType == "*")
                {
                    Logger.LogDebug(
                        $"{this}: Expected type '{topicInfo.Type}' but received instead '{receivedType}'. Continuing...");
                }
                else
                {
                    return
                        $"error=Expected type '{topicInfo.Type}' but received instead '{receivedType}', closing connection";
                }
            }

            if (!values.TryGetValue("md5sum", out string receivedMd5Sum) || receivedMd5Sum != topicInfo.Md5Sum)
            {
                if (receivedMd5Sum == "*")
                {
                    Logger.LogDebug(
                        $"{this}: Expected md5 '{topicInfo.Md5Sum}' but received instead '{receivedMd5Sum}'. Continuing...");
                }
                else
                {
                    return
                        $"error=Expected md5 '{topicInfo.Md5Sum}' but received instead '{receivedMd5Sum}', closing connection";
                }
            }

            if (values.TryGetValue("tcp_nodelay", out string receivedNoDelay) && receivedNoDelay == "1")
            {
                tcpClient.NoDelay = true;
                Logger.LogDebug($"{this}: requested tcp_nodelay");
            }

            return null;
        }

        async Task<bool> DoHandshake()
        {
            byte[] readBuffer = await ReceiveHeader();
            if (readBuffer == null)
            {
                throw new TimeoutException("Connection closed during handshake.");
            }

            List<string> fields = ParseHeader(readBuffer);
            string errorMessage = ProcessRemoteHeader(fields);

            if (errorMessage != null)
            {
                Logger.Log($"{this}: Failed handshake\n{errorMessage}");
            }

            await SendResponseHeader(errorMessage);

            return errorMessage == null;
        }

        async Task Run(int timeoutInMs, SemaphoreSlim managerSignal)
        {
            try
            {
                Logger.LogDebug($"{this}: initialized!");
                Status = SenderStatus.Waiting;

                Task<TcpClient> connectionTask = tcpListener.AcceptTcpClientAsync();

                managerSignal.Release();

                if (!await connectionTask.WaitFor(timeoutInMs) || !connectionTask.IsCompleted)
                {
                    throw new TimeoutException("Connection timed out!", connectionTask.Exception);
                }

                using (tcpClient = await connectionTask)
                {
                    IPEndPoint remoteEndPoint = (IPEndPoint) tcpClient.Client.RemoteEndPoint;
                    RemoteEndpoint = new Endpoint(remoteEndPoint);

                    Status = SenderStatus.Active;

                    Logger.LogDebug($"{this}: started!");
                    stream = tcpClient.GetStream();

                    if (!await DoHandshake())
                    {
                        keepRunning = false;
                    }

                    List<IMessage> tmpQueue = new List<IMessage>();

                    byte[] lengthArray = new byte[4];

                    byte[] ToLengthArray(uint i)
                    {
                        lengthArray[0] = (byte) i;
                        lengthArray[1] = (byte) (i >> 8);
                        lengthArray[2] = (byte) (i >> 0x10);
                        lengthArray[3] = (byte) (i >> 0x18);
                        return lengthArray;
                    }

                    while (keepRunning)
                    {
                        await signal.WaitAsync(1000);
                        if (!keepRunning)
                        {
                            break;
                        }

                        tmpQueue.Clear();
                        lock (messageQueue)
                        {
                            tmpQueue.AddRange(messageQueue);
                            messageQueue.Clear();
                        }

                        foreach (IMessage message in tmpQueue)
                        {
                            int msgLength = message.RosMessageLength;
                            if (writeBuffer.Length < msgLength)
                            {
                                writeBuffer = new byte[msgLength + BufferSizeIncrease];
                            }

                            try
                            {
                                uint sendLength = Buffer.Serialize(message, writeBuffer);
                                await stream.WriteAsync(ToLengthArray(sendLength), 0, 4);
                                await stream.WriteAsync(writeBuffer, 0, (int) sendLength);

                                NumSent++;
                                BytesSent += (int) sendLength + 4;
                            }
                            catch (Exception e) when
                                (e is ArgumentException || e is IndexOutOfRangeException)
                            {
                                Logger.LogDebug($"{this}: {e}");
                            }
                        }
                    }
                }
            }
            catch (Exception e) when
                (e is IOException || e is TimeoutException || e is AggregateException || e is SocketException)
            {
                Logger.LogDebug($"{this}: {e}");
            }
            catch (Exception e)
            {
                Logger.Log($"{this}: {e}");
            }

            Status = SenderStatus.Dead;
            tcpClient = null;
            stream = null;
        }

        public void Publish(IMessage message)
        {
            if (!IsAlive)
            {
                NumDropped++;
                return;
            }

            const int minQueueSize = 2;
            lock (messageQueue)
            {
                messageQueue.Add(message);
                if (messageQueue.Count > minQueueSize)
                {
                    ApplyQueueSizeConstraint(minQueueSize);
                }
            }

            try { signal.Release(); }
            catch (SemaphoreFullException) { }
        }

        void ApplyQueueSizeConstraint(int minQueueSize)
        {
            // start discarding old messages
            int totalQueueSizeInBytes = messageQueue.Sum(message => message.RosMessageLength);
            if (totalQueueSizeInBytes <= MaxQueueSizeInBytes)
            {
                return;
            }

            int overflowInBytes = totalQueueSizeInBytes - MaxQueueSizeInBytes;
            int toDrop;
            for (toDrop = 0; toDrop < messageQueue.Count - minQueueSize && overflowInBytes > 0; toDrop++)
            {
                overflowInBytes -= messageQueue[toDrop].RosMessageLength;
            }

            NumDropped += toDrop;
            BytesDropped = totalQueueSizeInBytes - MaxQueueSizeInBytes - overflowInBytes;
            messageQueue.RemoveRange(0, toDrop);
        }

        public override string ToString()
        {
            return $"[TcpSender :{Endpoint.Port} '{Topic}' >>'{RemoteCallerId}']";
        }
    }
}