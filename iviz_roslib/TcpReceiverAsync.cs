using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.XmlRpc;
using Nito.AsyncEx.Synchronous;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.Roslib
{
    internal sealed class TcpReceiverAsync<T> : IDisposable where T : IMessage
    {
        const int BufferSizeIncrease = 1024;
        const int MaxConnectionRetries = 120;
        const int WaitBetweenRetriesInMs = 2000;

        readonly TcpReceiverManager<T> manager;
        readonly TopicInfo<T> topicInfo;
        readonly bool requestNoDelay;

        Endpoint remoteEndpoint;
        Endpoint? endpoint;
        int numReceived;
        int bytesReceived;

        int connectionTimeoutInMs;

        readonly CancellationTokenSource runningTs = new CancellationTokenSource();
        bool KeepRunning => !runningTs.IsCancellationRequested;

        byte[] readBuffer = new byte[BufferSizeIncrease];
        NetworkStream? stream;
        Task? task;
        TcpClient? tcpClient;
        bool disposed;

        public TcpReceiverAsync(TcpReceiverManager<T> manager,
            Uri remoteUri, Endpoint remoteEndpoint, TopicInfo<T> topicInfo,
            bool requestNoDelay)
        {
            RemoteUri = remoteUri;
            this.remoteEndpoint = remoteEndpoint;
            this.topicInfo = topicInfo;
            this.manager = manager;
            this.requestNoDelay = requestNoDelay;
        }

        public Uri RemoteUri { get; }
        string Topic => topicInfo.Topic;
        public bool IsAlive => task != null && !task.IsCompleted;

        public SubscriberReceiverState State => new SubscriberReceiverState(
            IsAlive, requestNoDelay, endpoint,
            RemoteUri, remoteEndpoint,
            numReceived, bytesReceived
        );

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            runningTs.Cancel();

            try
            {
                task?.WaitAndUnwrapException();
            }
            catch (Exception e)
            {
                Logger.LogFormat("{0}: {1}", this, e);
            }

            runningTs.Dispose();
            task = null;
        }

        public void Start(int timeoutInMs)
        {
            connectionTimeoutInMs = timeoutInMs;
            task = Task.Run(Run);
        }

        async Task Run()
        {
            Task sessionTask = StartSession();

            // HACK! sometimes sessionTask gets stuck, and the only way to get it
            // out is by disposing the tcpClient
            // so we wait for the cancel, dispose everything, then wait for the task
            try
            {
                await Task.Delay(-1, runningTs.Token);
            }
            catch (OperationCanceledException) { }

#if !NETSTANDARD2_0
            if (stream != null)
            {
                await stream.DisposeAsync();
            }
#else
            stream?.Dispose();
#endif
            tcpClient?.Dispose();

            await sessionTask;
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
                requestNoDelay ? "tcp_nodelay=1" : "tcp_nodelay=0"
            };

            int totalLength = 4 * contents.Length + contents.Sum(entry => entry.Length);

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

            await stream!.WriteAsync(array, 0, array.Length).Caf();
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

#if !NETSTANDARD2_0
        async ValueTask<int> ReceivePacket()
#else
        async Task<int> ReceivePacket()
#endif
        {
            int numRead = 0;
            while (numRead < 4)
            {
#if !NETSTANDARD2_0
                int readNow =
                    await stream!.ReadAsync(
                        new Memory<byte>(readBuffer, numRead, 4 - numRead), 
                        runningTs.Token);
#else
                int readNow = await stream!.ReadAsync(readBuffer, numRead, 4 - numRead, runningTs.Token).Caf();
#endif
                if (readNow == 0)
                {
                    return -1;
                }

                numRead += readNow;
            }

            int length = BitConverter.ToInt32(readBuffer, 0);
            if (length == 0)
            {
                return 0;
            }

            if (readBuffer.Length < length)
            {
                readBuffer = new byte[length + BufferSizeIncrease];
            }

            numRead = 0;
            while (numRead < length)
            {
#if !NETSTANDARD2_0
                int readNow = await stream!.ReadAsync(
                    new Memory<byte>(readBuffer, numRead, length - numRead),
                    runningTs.Token);
#else
                int readNow = await stream!.ReadAsync(readBuffer, numRead, length - numRead, runningTs.Token).Caf();
#endif

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
            await SerializeHeader().Caf();

            int receivedLength = await ReceivePacket();
            if (receivedLength == 0)
            {
                throw new TimeoutException("Connection closed before handshake finished.");
            }

            return ParseHeader(receivedLength);
        }

        async Task<TcpClient?> TryToConnect()
        {
            TcpClient client = new TcpClient();
            try
            {
                Task connectionTask = client.ConnectAsync(remoteEndpoint.Hostname, remoteEndpoint.Port);
                if (!await connectionTask.WaitFor(connectionTimeoutInMs, runningTs.Token).Caf() ||
                    !connectionTask.RanToCompletion() ||
                    client.Client?.LocalEndPoint == null)
                {
                    client.Dispose();
                    return null;
                }
            }
            catch (Exception e) when (e is IOException || e is SocketException || e is OperationCanceledException)
            {
                client.Dispose();
                return null;
            }
            catch (Exception e)
            {
                Logger.LogFormat("{0}: {1}", this, e);
                client.Dispose();
                return null;
            }

            return client;
        }

        async Task<TcpClient?> KeepReconnecting()
        {
            for (int i = 0; i < MaxConnectionRetries && KeepRunning; i++)
            {
                var client = await TryToConnect().Caf();
                if (client != null)
                {
                    return client;
                }

                await Task.Delay(WaitBetweenRetriesInMs, runningTs.Token).Caf();
                if (!KeepRunning)
                {
                    return null;
                }

                Endpoint? newEndpoint = await manager.RequestConnectionFromPublisherAsync(RemoteUri).Caf();
                if (newEndpoint == null || newEndpoint.Equals(remoteEndpoint))
                {
                    continue;
                }

                Logger.LogFormat("{0}: Changed endpoint from {1} to {2}", this, remoteEndpoint, newEndpoint);
                remoteEndpoint = newEndpoint;
            }

            return null;
        }

        async Task StartSession()
        {
            while (KeepRunning)
            {
                tcpClient = null;

                Logger.LogDebugFormat("{0}: Trying to connect!", this);
                
                try
                {
                    tcpClient = await KeepReconnecting().Caf();
                }
                catch (OperationCanceledException) { }

                if (tcpClient == null)
                {
                    Logger.LogDebugFormat(
                        KeepRunning ? "{0}: Ran out of retries. Leaving!" : "{0}: Disposed! Getting out.", this);
                    break;
                }

                Logger.LogDebugFormat("{0}: Connected!", this);

                try
                {
                    using (tcpClient)
                    using (stream = tcpClient.GetStream())
                    {
                        endpoint = new Endpoint((IPEndPoint) tcpClient.Client.LocalEndPoint);
                        await ProcessLoop().Caf();
                    }
                }
                catch (Exception e) when (e is ObjectDisposedException || e is OperationCanceledException)
                {
                }
                catch (Exception e) when (e is IOException || e is SocketException || e is TimeoutException)
                {
                    Logger.LogDebugFormat("{0}: {1}", this, e);
                }
                catch (Exception e)
                {
                    Logger.LogFormat("{0}: {1}", this, e);
                }
            }

            tcpClient = null;
            stream = null;
            Logger.LogFormat("{0}: Stopped!", this);
        }

        async Task ProcessLoop()
        {
            if (!await ProcessHandshake().Caf())
            {
                return;
            }

            while (KeepRunning)
            {
                int rcvLength = await ReceivePacket();
                if (rcvLength == -1)
                {
                    Logger.LogDebugFormat("{0}: Partner closed connection.", this);
                    return;
                }

                T message = Buffer.Deserialize(topicInfo.Generator!, readBuffer, rcvLength);
                manager.MessageCallback(message);

                numReceived++;
                bytesReceived += rcvLength + 4;
            }
        }

        async Task<bool> ProcessHandshake()
        {
            List<string> responses = await DoHandshake().Caf();
            if (responses.Count == 0 || !responses[0].HasPrefix("error"))
            {
                return true;
            }

            int index = responses[0].IndexOf('=');
            string errorMsg = index != -1 ? responses[0].Substring(index + 1) : responses[0];
            Logger.LogDebugFormat("{0}: Partner sent error code: {1}", this, errorMsg);
            return false;
        }

        public override string ToString()
        {
            return $"[TcpReceiver Uri={RemoteUri} {remoteEndpoint.Hostname}:{remoteEndpoint.Port} '{Topic}']";
        }
    }
}