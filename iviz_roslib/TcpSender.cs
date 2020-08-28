//#define DEBUG__

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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

    internal sealed class TcpSender : IDisposable
    {
        readonly object condVar = new object();
        readonly List<IMessage> messageQueue = new List<IMessage>();

        TcpListener tcpListener;
        TcpClient tcpClient;
        NetworkStream stream;
        BinaryWriter writer;

        const int BufferSizeIncrease = 1024;
        byte[] writeBuffer = new byte[BufferSizeIncrease];

        bool keepRunning;
        Task task;

        string RemoteCallerId { get; }
        Uri CallerUri { get; }
        bool Latching { get; }
        
        readonly TopicInfo topicInfo;

        string Topic => topicInfo.Topic;
        public bool IsAlive => task != null && !task.IsCompleted && !task.IsFaulted;
        Endpoint Endpoint { get; set; }
        Endpoint RemoteEndpoint { get; set; }
        public int MaxQueueSizeBytes { get; set; } = 50000;
        int CurrentQueueSize => messageQueue.Count;
        int NumSent { get; set; }
        int BytesSent { get; set; }
        int NumDropped { get; set; }
        int BytesDropped { get; set; }
        public SenderStatus Status { get; private set; }

        public TcpSender(
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

        public IPEndPoint Start(int timeoutInMs)
        {
            tcpListener = new TcpListener(IPAddress.Any, 0);
            tcpListener.Start();

            IPEndPoint localEndpoint = (IPEndPoint)tcpListener.LocalEndpoint;
            Endpoint = new Endpoint(localEndpoint);

            keepRunning = true;
            task = Task.Run(() => Run(timeoutInMs));

            return localEndpoint;
        }

        public void Stop()
        {
            keepRunning = false;
            tcpClient?.Close();
            tcpListener?.Stop();
            task?.Wait();
            tcpClient = null;
            tcpListener = null;
            task = null;
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

        byte[] ReceiveHeader()
        {
            byte[] lengthBuffer = new byte[4];
            int numRead = 0;
            while (numRead < 4)
            {
                int readNow = stream.Read(lengthBuffer, numRead, 4 - numRead);
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
                int readNow = stream.Read(readBuffer, numRead, length - numRead);
                if (readNow == 0)
                {
                    return null;
                }
                numRead += readNow;
            }
            return readBuffer;
        }

        int SendResponseHeader(string errorMessage)
        {
            string[] contents;
            if (errorMessage != null)
            {
                contents = new[] {
                    errorMessage,
                    $"md5sum={topicInfo.Md5Sum}",
                    $"type={topicInfo.Type}",
                    $"callerid={topicInfo.CallerId}",
                };
            }
            else
            {
                contents = new[] {
                    $"md5sum={topicInfo.Md5Sum}",
                    $"type={topicInfo.Type}",
                    $"callerid={topicInfo.CallerId}",
                    $"latching={(Latching ? "1" : "0")}",
                };
            }
            int totalLength = 4 * contents.Length;
            foreach (string entry in contents)
            {
                totalLength += entry.Length;
            }
            writer.Write(totalLength);
            foreach (string entry in contents)
            {
                writer.Write(entry.Length);
                writer.Write(BuiltIns.UTF8.GetBytes(entry));
#if DEBUG__
                Logger.Log(">>> " + contents[i]);
#endif
            }
            return totalLength;
        }

        string ProcessRemoteHeader(IReadOnlyCollection<string> fields)
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
                return $"error=Expected callerid '{RemoteCallerId}' but received instead '{receivedId}', closing connection";
            }
            if (!values.TryGetValue("topic", out string receivedTopic) || receivedTopic != topicInfo.Topic)
            {
                return $"error=Expected topic '{topicInfo.Topic}' but received instead '{receivedTopic}', closing connection";
            }
            if (!values.TryGetValue("type", out string receivedType) || receivedType != topicInfo.Type)
            {
                if (receivedType == "*")
                {
                    Logger.LogDebug($"{this}: Expected type '{topicInfo.Type}' but received instead '{receivedType}'. Continuing...");
                }
                else
                {
                    return $"error=Expected type '{topicInfo.Type}' but received instead '{receivedType}', closing connection";
                }
            }
            if (!values.TryGetValue("md5sum", out string receivedMd5Sum) || receivedMd5Sum != topicInfo.Md5Sum)
            {
                if (receivedMd5Sum == "*")
                {
                    Logger.LogDebug($"{this}: Expected md5 '{topicInfo.Md5Sum}' but received instead '{receivedMd5Sum}'. Continuing...");
                }
                else
                {
                    return $"error=Expected md5 '{topicInfo.Md5Sum}' but received instead '{receivedMd5Sum}', closing connection";
                }
            }
            if (values.TryGetValue("tcp_nodelay", out string receivedNoDelay) && receivedNoDelay == "1")
            {
                tcpClient.NoDelay = true;
                Logger.LogDebug($"{this}: requested tcp_nodelay");

            }
            return null;
        }

        bool DoHandshake()
        {
            byte[] readBuffer = ReceiveHeader();
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
            SendResponseHeader(errorMessage);

            return errorMessage == null;
        }

        void Run(int timeoutInMs)
        {
            try
            {
                Logger.LogDebug($"{this}: initialized! " + tcpListener.LocalEndpoint);
                Status = SenderStatus.Waiting;

                Task<TcpClient> task = tcpListener.AcceptTcpClientAsync();
                if (!task.Wait(timeoutInMs) || task.IsCanceled || task.IsFaulted || !keepRunning)
                {
                    throw new TimeoutException();
                }
                using (tcpClient = task.Result)
                {
                    IPEndPoint remoteEndPoint = (IPEndPoint)tcpClient.Client.RemoteEndPoint;
                    RemoteEndpoint = new Endpoint(remoteEndPoint);
                    
                    Status = SenderStatus.Active;

                    Logger.LogDebug($"{this}: started!");
                    stream = tcpClient.GetStream();
                    writer = new BinaryWriter(stream);

                    if (!DoHandshake())
                    {
                        keepRunning = false;
                    }

                    List<IMessage> tmpQueue = new List<IMessage>();
                    while (keepRunning)
                    {
                        lock (condVar)
                        {
                            Monitor.Wait(condVar, 1000);
                            if (!keepRunning)
                            {
                                break;
                            }
                            tmpQueue.Clear();
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
                                uint sendLength = Msgs.Buffer.Serialize(message, writeBuffer);

                                //Debug.Log($"{this}: sending {sendLength}");
                                writer.Write(sendLength);
                                writer.Write(writeBuffer, 0, (int)sendLength);
                                NumSent++;
                                BytesSent += (int)sendLength + 4;
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
            lock (condVar)
            {
                const int minQueueSize = 2;
                messageQueue.Add(message);

                if (messageQueue.Count > minQueueSize)
                {
                    int totalQueueSize = messageQueue.Sum(x => x.RosMessageLength);
                    if (totalQueueSize > MaxQueueSizeBytes)
                    {
                        int overflow = totalQueueSize - MaxQueueSizeBytes;
                        int i;
                        for (i = 0; i < messageQueue.Count - minQueueSize && overflow > 0; i++)
                        {
                            overflow -= messageQueue[i].RosMessageLength;
                        }
                        NumDropped += i;
                        BytesDropped = totalQueueSize - MaxQueueSizeBytes - overflow;
                        messageQueue.RemoveRange(0, i);
                    }
                }
                
                Monitor.Pulse(condVar);
            }
        }

        public void Dispose()
        {
            tcpClient.Dispose();
            stream.Dispose();

            keepRunning = false;
            task?.Wait();
            task?.Dispose();
        }

        public PublisherSenderState State =>
            new PublisherSenderState(
                IsAlive, Latching, Status,
                Endpoint, RemoteCallerId, RemoteEndpoint,
                CurrentQueueSize, MaxQueueSizeBytes, NumSent, BytesSent, NumDropped
            );

        public override string ToString()
        {
            return $"[TcpSender :{Endpoint.Port} '{Topic}' >>'{RemoteCallerId}']";
        }
    }
}
