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

        string? errorDescription;
        int connectionTimeoutInMs;

        readonly CancellationTokenSource runningTs = new CancellationTokenSource();
        bool KeepRunning => !runningTs.IsCancellationRequested;
        bool IsConnected => tcpClient?.Connected ?? false;

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
            IsAlive, IsConnected, requestNoDelay, endpoint,
            RemoteUri, remoteEndpoint,
            numReceived, bytesReceived,
            errorDescription
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

            stream?.Dispose();
            tcpClient?.Dispose();

            try
            {
                sessionTask.WaitAndUnwrapException();
            }
            catch (Exception e)
            {
                Logger.LogErrorFormat("{0}: Error in task wait: {1}", this, e);
            }
        }

        async Task<TcpClient?> TryToConnect()
        {
            TcpClient client = new TcpClient();
            try
            {
                Task connectionTask = client.ConnectAsync(remoteEndpoint.Hostname, remoteEndpoint.Port);
                if (await connectionTask.WaitFor(connectionTimeoutInMs, runningTs.Token).Caf() &&
                    connectionTask.RanToCompletion())
                {
                    return client;
                }
            }
            catch (Exception e) when (e is IOException || e is SocketException || e is OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Logger.LogFormat("{0}: {1}", this, e);
            }

            client.Dispose();
            return null;
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

                try
                {
                    await Task.Delay(WaitBetweenRetriesInMs, runningTs.Token).Caf();
                }
                catch (OperationCanceledException)
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

        async Task SendHeader()
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

            await Utils.WriteHeaderAsync(stream!, contents).Caf();
        }


#if !NETSTANDARD2_0
        async ValueTask<int> ReceivePacket()
#else
        async Task<int> ReceivePacket()
#endif
        {
            if (!await stream!.ReadChunkAsync(readBuffer, 4, runningTs.Token))
            {
                return -1;
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

            if (!await stream!.ReadChunkAsync(readBuffer, length, runningTs.Token))
            {
                return -1;
            }

            return length;
        }

        async Task ProcessHandshake()
        {
            await SendHeader().Caf();

            int receivedLength = await ReceivePacket();
            if (receivedLength == -1)
            {
                throw new IOException("Connection closed during handshake");
            }

            List<string> responses = Utils.ParseHeader(readBuffer, receivedLength);
            if (responses.Count == 0 || !responses[0].HasPrefix("error"))
            {
                return;
            }

            int index = responses[0].IndexOf('=');
            string errorMsg = index != -1 ? responses[0].Substring(index + 1) : responses[0];
            errorDescription = errorMsg;
            throw new RosRpcException($"Failed handshake: {errorMsg}");
        }

        async Task StartSession()
        {
            while (KeepRunning)
            {
                tcpClient = null;
                Logger.LogDebugFormat("{0}: Trying to connect!", this);

                TcpClient? newTcpClient = await KeepReconnecting().Caf();
                IPEndPoint? newEndPoint;
                if (newTcpClient == null
                    || (newEndPoint = (IPEndPoint?) newTcpClient.Client.RemoteEndPoint) == null)
                {
                    Logger.LogDebugFormat(
                        KeepRunning ? "{0}: Ran out of retries. Leaving!" : "{0}: Disposed! Getting out.", this);
                    break;
                }

                endpoint = new Endpoint(newEndPoint);
                Logger.LogDebugFormat("{0}: Connected!", this);
                errorDescription = null;

                try
                {
                    using (tcpClient = newTcpClient)
                    using (stream = tcpClient.GetStream())
                    {
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

            errorDescription = null;
            tcpClient = null;
            stream = null;
            Logger.LogFormat("{0}: Stopped!", this);
        }

        async Task ProcessLoop()
        {
            await ProcessHandshake().Caf();

            while (KeepRunning)
            {
                int rcvLength = await ReceivePacket();
                if (rcvLength == -1)
                {
                    Logger.LogDebugFormat("{0}: Partner closed connection", this);
                    return;
                }

                T message = Buffer.Deserialize(topicInfo.Generator!, readBuffer, rcvLength);
                manager.MessageCallback(message);

                numReceived++;
                bytesReceived += rcvLength + 4;
            }
        }

        public override string ToString()
        {
            return $"[TcpReceiver Uri={RemoteUri} {remoteEndpoint.Hostname}:{remoteEndpoint.Port} '{Topic}']";
        }
    }
}