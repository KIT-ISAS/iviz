//#define DEBUG__

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Iviz.RoslibSharp
{
    public class BufferOverflowException : Exception
    {
        public readonly uint missing;

        public BufferOverflowException(uint missing)
        {
            this.missing = missing;
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SenderStatus
    {
        Inactive,
        Waiting,
        Active,
        Dead
    }


    class TcpSender
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

        public readonly string RemoteCallerId;
        public readonly Uri CallerUri;
        public readonly bool Latching;
        readonly TopicInfo TopicInfo;

        public string Topic => TopicInfo.Topic;
        public bool IsAlive => task != null && !task.IsCompleted && !task.IsFaulted;
        public string Hostname { get; private set; }
        public int Port { get; private set; }
        public string RemoteHostname { get; private set; }
        public int RemotePort { get; private set; }
        public int MaxQueueSize { get; set; } = 3;
        public int CurrentQueueSize => messageQueue.Count;
        public int NumSent { get; private set; }
        public int BytesSent { get; private set; }
        public int NumDropped { get; private set; }

        public SenderStatus Status { get; private set; }

        public TcpSender(
            Uri callerUri,
            string remoteCallerId,
            TopicInfo topicInfo,
            bool latching)
        {
            RemoteCallerId = remoteCallerId;
            TopicInfo = topicInfo;
            CallerUri = callerUri;
            Status = SenderStatus.Inactive;
            Latching = latching;
        }

        public IPEndPoint Start()
        {
            string localHostname = CallerUri.Host;
            IPAddress localAddress = Dns.GetHostAddresses(localHostname)[0];
            tcpListener = new TcpListener(localAddress, 0);
            tcpListener.Start();

            //Logger.Log("TcpSender: Listening at " + tcpListener.LocalEndpoint);

            keepRunning = true;
            task = Task.Run(Run);

            IPEndPoint localEndpoint = (IPEndPoint)tcpListener.LocalEndpoint;
            Hostname = localEndpoint.Address.ToString();
            Port = localEndpoint.Port;

            return localEndpoint;
        }

        public void Stop()
        {
            keepRunning = false;
            tcpClient?.Close();
            tcpListener?.Stop();
            if (task != null && !task.IsCompleted)
            {
                task.Wait();
            }
            tcpClient = null;
            tcpListener = null;
            task = null;
        }

        List<string> ParseHeader(byte[] readBuffer)
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
                    $"md5sum={TopicInfo.Md5Sum}",
                    $"type={TopicInfo.Type}",
                    $"callerid={TopicInfo.CallerId}",
                };
            }
            else
            {
                contents = new[] {
                    $"md5sum={TopicInfo.Md5Sum}",
                    $"type={TopicInfo.Type}",
                    $"callerid={TopicInfo.CallerId}",
                    $"latching={(Latching ? "1" : "0")}",
                };
            }
            int totalLength = 4 * contents.Length;
            for (int i = 0; i < contents.Length; i++)
            {
                totalLength += contents[i].Length;
            }
            writer.Write(totalLength);
            for (int i = 0; i < contents.Length; i++)
            {
                writer.Write(contents[i].Length);
                writer.Write(BuiltIns.UTF8.GetBytes(contents[i]));
#if DEBUG__
                Logger.Log(">>> " + contents[i]);
#endif
            }
            return totalLength;
        }

        string ProcessRemoteHeader(List<string> fields)
        {
            if (fields.Count < 5)
            {
                return "error=Expected at least 5 fields, closing connection";
            }

            Dictionary<string, string> values = new Dictionary<string, string>();
            for (int i = 0; i < fields.Count; i++)
            {
                int index = fields[i].IndexOf('=');
                if (index < 0)
                {
                    return $"error=Invalid field '{fields[i]}'";
                }
                string key = fields[i].Substring(0, index);
                values[key] = fields[i].Substring(index + 1);
            }

            if (!values.TryGetValue("callerid", out string receivedId) || receivedId != RemoteCallerId)
            {
                return $"error=Expected callerid '{RemoteCallerId}' but received instead '{receivedId}', closing connection";
            }
            if (!values.TryGetValue("topic", out string receivedTopic) || receivedTopic != TopicInfo.Topic)
            {
                return $"error=Expected topic '{TopicInfo.Topic}' but received instead '{receivedTopic}', closing connection";
            }
            if (!values.TryGetValue("md5sum", out string receivedMd5sum) || receivedMd5sum != TopicInfo.Md5Sum)
            {
                if (receivedMd5sum == "*")
                {
                    Logger.LogDebug($"{this}: Expected md5 '{TopicInfo.Md5Sum}' but received instead '{receivedMd5sum}'. Continuing...");
                }
                else
                {
                    return $"error=Expected md5 '{TopicInfo.Md5Sum}' but received instead '{receivedMd5sum}', closing connection";
                }
            }
            if (!values.TryGetValue("type", out string receivedType) || receivedType != TopicInfo.Type)
            {
                if (receivedType == "*")
                {
                    Logger.LogDebug($"{this}: Expected type '{TopicInfo.Type}' but received instead '{receivedType}'. Continuing...");
                }
                else
                {
                    return $"error=Expected type '{TopicInfo.Type}' but received instead '{receivedType}', closing connection";
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
            List<string> fields = ParseHeader(readBuffer);
            string errorMessage = ProcessRemoteHeader(fields);

            if (errorMessage != null)
            {
                Logger.Log($"{this}: Failed handshake\n{errorMessage}");
            }
            SendResponseHeader(errorMessage);

            return errorMessage == null;
        }

        void Run()
        {
            try
            {
                //Logger.Log($"{this}: initialized! " + tcpListener.LocalEndpoint);
                Status = SenderStatus.Waiting;
                Task<TcpClient> task = tcpListener.AcceptTcpClientAsync();
                if (!task.Wait(5000))
                {
                    throw new TimeoutException();
                }
                using (tcpClient = task.Result)
                {
                    IPEndPoint remoteEndPoint = (IPEndPoint)tcpClient.Client.RemoteEndPoint;
                    RemoteHostname = remoteEndPoint.Address.ToString();
                    RemotePort = remoteEndPoint.Port;

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
                                uint sendLength = BuiltIns.Serialize(message, writeBuffer);

                                //Debug.Log($"{this}: sending {sendLength}");
                                writer.Write(sendLength);
                                writer.Write(writeBuffer, 0, (int)sendLength);
                                NumSent++;
                                BytesSent += (int)sendLength + 4;
                            }
                            catch (Exception e) when
                            (e is ArgumentException || e is IndexOutOfRangeException || e is BufferOverflowException)
                            {
                                Logger.LogDebug($"{this}: {e.Message}");
                                Logger.LogDebug(e.StackTrace);
                            }
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Logger.LogDebug($"{this}: {e.Message}");
                Logger.LogDebug(e.StackTrace);
            }
            catch (Exception e)
            {
                Logger.LogError($"{this}: {e.Message}");
                Logger.LogError(e.StackTrace);
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
                messageQueue.Add(message);
                if (MaxQueueSize != 0 && messageQueue.Count > MaxQueueSize)
                {
                    messageQueue.RemoveAt(0);
                    NumDropped++;
                }
                Monitor.Pulse(condVar);
            }
        }

        public PublisherSenderState GetState()
        {
            return new PublisherSenderState(
                IsAlive, Latching, Status,
                Hostname, Port, RemoteCallerId, RemoteHostname, RemotePort,
                CurrentQueueSize, MaxQueueSize, NumSent, BytesSent, NumDropped
            );
        }

        public override string ToString()
        {
            return $"[TcpSender {Hostname}:{Port} '{Topic}' >>'{RemoteCallerId}']";
        }
    }
}
