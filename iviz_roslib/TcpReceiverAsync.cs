using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;

namespace Iviz.Roslib
{
    internal sealed class TcpReceiverAsync : IDisposable
    {
        readonly TopicInfo topicInfo;
        readonly Action<IMessage> callback;

        TcpClient tcpClient;
        NetworkStream stream;

        const int BufferSizeIncrease = 1024;
        byte[] readBuffer = new byte[BufferSizeIncrease];

        bool keepRunning;
        Task task;

        public Uri RemoteUri { get; }
        Endpoint RemoteEndpoint { get; }
        Endpoint Endpoint { get; set; }

        string Topic => topicInfo.Topic;
        public bool IsAlive => task != null && !task.IsCompleted && !task.IsFaulted;
        int NumReceived { get; set; }
        int BytesReceived { get; set; }

        bool RequestNoDelay { get; }

        public TcpReceiverAsync(Uri remoteUri, Endpoint remoteEndpoint, TopicInfo topicInfo, Action<IMessage> callback,
            bool requestNoDelay)
        {
            RemoteUri = remoteUri;
            RemoteEndpoint = remoteEndpoint;
            this.topicInfo = topicInfo;
            this.callback = callback;
            RequestNoDelay = requestNoDelay;
        }

        public void Start(int timeoutInMs)
        {
            keepRunning = true;
            task = Task.Run(async () => await Run(timeoutInMs));
        }

        async Task SerializeHeader()
        {
            string[] contents =
            {
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

            byte[] array = new byte[totalLength + 4];
            using (BinaryWriter writer = new BinaryWriter(new MemoryStream(array)))
            {
                writer.Write(totalLength);
                foreach (string entry in contents)
                {
                    writer.Write(entry.Length);
                    writer.Write(BuiltIns.UTF8.GetBytes(entry));
                }
            }

            await stream.WriteAsync(array, 0, array.Length);
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

        async Task<int> ReceivePacket()
        {
            int numRead = 0;
            while (numRead < 4)
            {
                int readNow = await stream.ReadAsync(readBuffer, numRead, 4 - numRead);
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
                int readNow = await stream.ReadAsync(readBuffer, numRead, length - numRead);
                if (readNow == 0)
                {
                    return 0;
                }

                numRead += readNow;
            }

            return length;
        }


        async Task<List<string>> DoHandshake()
        {
            await SerializeHeader();

            int receivedLength = await ReceivePacket();
            if (receivedLength == 0)
            {
                throw new TimeoutException("Connection closed before handshake finished.");
            }

            return ParseHeader(receivedLength);
        }


        readonly SemaphoreSlim signal = new SemaphoreSlim(0, 1);

        async Task Run(int timeoutInMs)
        {
            Task runLoopTask = RunLoop(timeoutInMs);

            while (keepRunning)
            {
                await signal.WaitAsync(1000);
            }

            tcpClient?.Dispose();
            stream?.Dispose();

            await runLoopTask;
        }

        async Task RunLoop(int timeoutInMs)
        {
            const int numTries = 5;

            for (int round = 0; round < numTries && keepRunning; round++)
            {
                using (tcpClient = new TcpClient())
                {
                    try
                    {
                        Task connectionTask = tcpClient.ConnectAsync(RemoteEndpoint.Hostname, RemoteEndpoint.Port);
                        if (!await connectionTask.WaitFor(timeoutInMs) || !connectionTask.IsCompleted)
                        {
                            Logger.LogDebug($"{this}: Connection timed out! Retrying... ({round + 1}/{numTries})");
                            continue;
                        }

                        if (tcpClient.Client?.LocalEndPoint == null)
                        {
                            Logger.LogDebug($"{this}: Connection failed! Retrying... ({round + 1}/{numTries})");
                            continue;
                        }
                        
                        IPEndPoint endPoint = (IPEndPoint) tcpClient.Client.LocalEndPoint;
                        
                        Endpoint = new Endpoint(endPoint);

                        stream = tcpClient.GetStream();

                        round = 0; // reset if successful

                        await ProcessLoop();
                    }
                    catch (ObjectDisposedException)
                    {
                        Logger.LogDebug($"{this}: Leaving thread"); // expected
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

            keepRunning = false;
            try { signal.Release(); }
            catch(SemaphoreFullException) {}
            
            Logger.Log($"{this}: Stopped!");
        }

        async Task ProcessLoop()
        {
            List<string> responses = await DoHandshake();
            if (responses.Count != 0 && responses[0].HasPrefix("error"))
            {
                int index = responses[0].IndexOf('=');
                string errorMsg = (index != -1) ? responses[0].Substring(index + 1) : responses[0];
                throw new ConnectionException("Partner sent error code: " + errorMsg);
            }

            while (keepRunning)
            {
                int rcvLength = await ReceivePacket();
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
            IsAlive, RequestNoDelay, Endpoint,
            RemoteUri, RemoteEndpoint,
            NumReceived, BytesReceived
        );

        public override string ToString()
        {
            return $"[TcpReceiver {RemoteEndpoint.Hostname}:{RemoteEndpoint.Port} '{Topic}']";
        }

        bool disposed;

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            keepRunning = false;

            try { signal.Release(); }
            catch(SemaphoreFullException) {}


            task?.Wait();
        }
    }
}