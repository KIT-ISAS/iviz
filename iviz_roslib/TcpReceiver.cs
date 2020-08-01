using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    internal sealed class TcpReceiver : IDisposable
    {
        readonly TopicInfo topicInfo;
        readonly Action<IMessage> callback;

        TcpClient tcpClient;
        NetworkStream stream;
        BinaryWriter writer;

        const int BufferSizeIncrease = 1024;
        byte[] readBuffer = new byte[BufferSizeIncrease];

        bool keepRunning;
        Task task;

        public Uri RemoteUri { get; }
        public string RemoteHostname { get; }
        public int RemotePort { get; }
        public string Hostname { get; private set; }
        public int Port { get; private set; }

        public string Topic => topicInfo.Topic;
        public bool IsAlive => task != null && !task.IsCompleted && !task.IsFaulted;
        public int NumReceived { get; private set; }
        public int BytesReceived { get; private set; }

        public bool RequestNoDelay { get; }

        public TcpReceiver(
            Uri remoteUri,
            string remoteHostname,
            int remotePort,
            TopicInfo topicInfo,
            Action<IMessage> callback,
            bool requestNoDelay)
        {
            RemoteUri = remoteUri;
            RemoteHostname = remoteHostname;
            RemotePort = remotePort;
            this.topicInfo = topicInfo;
            this.callback = callback;
            RequestNoDelay = requestNoDelay;
        }

        public void Start(int timeoutInMs)
        {
            keepRunning = true;
            task = Task.Run(() => Run(timeoutInMs));
        }

        public void Stop()
        {
            keepRunning = false;
            tcpClient?.Close();
            task?.Wait();
            tcpClient = null;
            task = null;
        }


        int SerializeHeader()
        {
            string[] contents = {
                $"message_definition={topicInfo.MessageDefinition}",
                $"callerid={topicInfo.CallerId}",
                $"topic={topicInfo.Topic}",
                $"md5sum={topicInfo.Md5Sum}",
                $"type={topicInfo.Type}",
                $"tcp_nodelay={(RequestNoDelay ? "1" : "0")}",
            };
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
            }
            return totalLength;
        }

        List<string> ParseHeader(int totalLength)
        {
            int numRead = 0;

            List<string> contents = new List<string>();
            while (numRead < totalLength)
            {
                int length = BitConverter.ToInt32(readBuffer, numRead);
                numRead += 4;
                string entry = BuiltIns.UTF8.GetString(readBuffer, numRead, length);
                numRead += length;
                contents.Add(entry);
            }
            return contents;
        }

        int ReceivePacket()
        {
            int numRead = 0;
            while (numRead < 4)
            {
                int readNow = stream.Read(readBuffer, numRead, 4 - numRead);
                if (readNow == 0)
                {
                    return 0;
                }
                numRead += readNow;
            }

            int length = BitConverter.ToInt32(readBuffer, 0);
            if (readBuffer.Length < length)
            {
                readBuffer = new byte[length + BufferSizeIncrease];
            }
            numRead = 0;
            while (numRead < length)
            {
                int readNow = stream.Read(readBuffer, numRead, length - numRead);
                if (readNow == 0)
                {
                    return 0;
                }
                numRead += readNow;
            }
            return length;
        }


        List<string> DoHandshake()
        {
            SerializeHeader();

            int totalLength = ReceivePacket();
            if (totalLength == 0)
            {
                throw new TimeoutException("Connection closed before handshake finished.");
            }
            return ParseHeader(totalLength);
        }

        void Run(int timeoutInMs)
        {
            const int numTries = 5;

            for (int round = 0; round < numTries && keepRunning; round++)
            {
                using (tcpClient = new TcpClient())
                {
                    try
                    {
                        Task connectionTask = tcpClient.ConnectAsync(RemoteHostname, RemotePort);
                        if (!connectionTask.Wait(timeoutInMs) || connectionTask.IsCanceled)
                        {
                            throw new TimeoutException();
                        }

                        round = 0; // reset if successful

                        IPEndPoint endPoint = (IPEndPoint) tcpClient.Client.LocalEndPoint;
                        Hostname = endPoint.Address.ToString();
                        Port = endPoint.Port;

                        stream = tcpClient.GetStream();
                        writer = new BinaryWriter(stream);
                        
                        ProcessLoop();
                    }
                    catch (Exception e) when
                    (e is ConnectionException || e is IOException || e is AggregateException ||
                     e is SocketException || e is TimeoutException)
                    {
                        Logger.LogDebug($"{this}: " + e);
                    }
                    catch (Exception e)
                    {
                        Logger.Log($"{this}: " + e);
                    }
                }

                if (keepRunning)
                {
                    Thread.Sleep(1000);
                }

                Logger.LogDebug($"{this}: Connection closed. Retrying... ({round + 1}/{numTries})");
            }

            tcpClient = null;
            stream = null;
            Logger.Log($"{this}: Stopped!");
        }

        void ProcessLoop()
        {
            List<string> responses = DoHandshake();
            if (responses.Count != 0 && responses[0].HasPrefix("error"))
            {
                int index = responses[0].IndexOf('=');
                string errorMsg = (index != -1) ? responses[0].Substring(index + 1) : responses[0];
                throw new ConnectionException("Partner sent error code: " + errorMsg);
            }

            while (keepRunning)
            {
                int rcvLength = ReceivePacket();
                if (rcvLength == 0)
                {
                    throw new ConnectionException("Partner closed connection.");
                }

                try
                {
                    IMessage result = Msgs.Buffer.Deserialize(topicInfo.Generator, readBuffer, rcvLength);
                    callback(result);
                    NumReceived++;
                    BytesReceived += rcvLength + 4;
                }
                catch (Exception e) when (e is ArgumentException || e is IndexOutOfRangeException)
                {
                    Logger.Log($"{this}: {e}"); // shouldn't happen
                }
            }
        }

        public SubscriberReceiverState State => new SubscriberReceiverState(
                IsAlive, RequestNoDelay, Hostname, Port,
                RemoteUri, RemoteHostname, RemotePort,
                NumReceived, BytesReceived
            );

        public override string ToString()
        {
            return $"[TcpSender {RemoteHostname}:{RemotePort} '{Topic}']";
        }

        public void Dispose()
        {
            tcpClient.Dispose();
            stream.Dispose();

            keepRunning = false;
            task?.Wait();
            task?.Dispose();
        }
    }
}
