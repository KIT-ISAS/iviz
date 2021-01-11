using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.MsgsGen;
using Iviz.MsgsGen.Dynamic;
using Iviz.XmlRpc;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.Roslib
{
    internal sealed class TcpReceiverAsync<T> where T : IMessage
    {
        const int BufferSizeIncrease = 1024;
        const int MaxConnectionRetries = 120;
        const int WaitBetweenRetriesInMs = 2000;
        const int DisposeTimeoutInMs = 2000;

        readonly TcpReceiverManager<T> manager;

        TopicInfo<T> topicInfo;
        readonly bool requestNoDelay;

        Endpoint remoteEndpoint;
        Endpoint? endpoint;
        long numReceived;
        long bytesReceived;

        string? errorDescription;
        int connectionTimeoutInMs;

        readonly CancellationTokenSource runningTs = new CancellationTokenSource();
        bool KeepRunning => !runningTs.IsCancellationRequested;
        bool IsConnected => tcpClient?.Connected ?? false;

        byte[] readBuffer = new byte[4];
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

        public async Task DisposeAsync()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            runningTs.Cancel();
            tcpClient?.Dispose();

            if (task != null)
            {
                await task
                    .WaitForWithTimeout(DisposeTimeoutInMs, "Receiver task dispose timed out.")
                    .AwaitNoThrow(this);
            }

            readBuffer = Array.Empty<byte>();
            task = null;
        }


        public void Start(int timeoutInMs)
        {
            connectionTimeoutInMs = timeoutInMs;
            task = Task.Run(StartSession, runningTs.Token);
        }

        async Task<TcpClient?> TryToConnect()
        {
            TcpClient client = new TcpClient(AddressFamily.InterNetworkV6) {Client = {DualMode = true}};

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
                Logger.LogFormat(Utils.GenericExceptionFormat, this, e);
            }

            client.Dispose();
            return null;
        }

        async Task<TcpClient?> KeepReconnecting()
        {
            for (int i = 0; i < MaxConnectionRetries && KeepRunning; i++)
            {
                TcpClient? client = await TryToConnect().Caf();
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

                Endpoint? newEndpoint =
                    await manager.RequestConnectionFromPublisherAsync(RemoteUri, runningTs.Token).Caf();
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
                $"message_definition={topicInfo.MessageDependencies}",
                $"callerid={topicInfo.CallerId}",
                $"topic={topicInfo.Topic}",
                $"md5sum={topicInfo.Md5Sum}",
                $"type={topicInfo.Type}",
                requestNoDelay ? "tcp_nodelay=1" : "tcp_nodelay=0"
            };

            await Utils.WriteHeaderAsync(stream!, contents, runningTs.Token).Caf();
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

            const int maxMessageLength = 64 * 1024 * 1024;
            if (length < 0 || length > maxMessageLength)
            {
                throw new RosInvalidPackageSizeException($"Invalid packet size '{length}', disconnecting.");
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
            if (responses.Count != 0 && responses[0].HasPrefix("error"))
            {
                int index = responses[0].IndexOf('=');
                string errorMsg = index != -1 ? responses[0].Substring(index + 1) : responses[0];
                errorDescription = errorMsg;
                throw new RosHandshakeException($"Failed handshake: {errorMsg}");
            }

            if (DynamicMessage.IsDynamic<T>())
            {
                GenerateNewTopicInfo(responses);
            }
        }

        void GenerateNewTopicInfo(List<string> responses)
        {
            const string typePrefix = "type=";
            const string definitionPrefix = "message_definition=";

            string callerId = topicInfo.CallerId;
            string topicName = topicInfo.Topic;
            string? dynamicType = responses.FirstOrDefault(
                entry => entry.HasPrefix(typePrefix))?.Substring(typePrefix.Length);
            string? dynamicDependency = responses.FirstOrDefault(
                entry => entry.HasPrefix(definitionPrefix))?.Substring(definitionPrefix.Length);
            if (dynamicType == null || dynamicDependency == null)
            {
                throw new RosHandshakeException(
                    "Partner did not send type and definition, required to instantiate dynamic messages.");
            }

            DynamicMessage generator = DynamicMessage.CreateFromDependencyString(dynamicType, dynamicDependency);
            topicInfo = new TopicInfo<T>(callerId, topicName, generator);
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
                    Logger.LogDebugFormat(KeepRunning
                            ? "{0}: Ran out of retries. Leaving!"
                            : "{0}: Disposed! Getting out.",
                        this);
                    break;
                }

                endpoint = new Endpoint(newEndPoint);
                Logger.LogDebugFormat("{0}: Connected!", this);
                errorDescription = null;

                try
                {
                    using (tcpClient = newTcpClient)
                    {
                        stream = tcpClient.GetStream();
                        await ProcessLoop().Caf();
                    }
                }
                catch (Exception e) when (e is ObjectDisposedException || e is OperationCanceledException)
                {
                }
                catch (Exception e) when (e is IOException || e is SocketException || e is TimeoutException)
                {
                    Logger.LogDebugFormat(Utils.GenericExceptionFormat, this, e);
                }
                catch (Exception e) when (e is RoslibException)
                {
                    Logger.LogErrorFormat(Utils.GenericExceptionFormat, this, e);
                }
                catch (Exception e)
                {
                    Logger.LogFormat(Utils.GenericExceptionFormat, this, e);
                }
            }

            errorDescription = null;
            tcpClient = null;
            stream = null;
            Logger.LogDebugFormat("{0}: Stopped!", this);
        }

        async Task ProcessLoop()
        {
            await ProcessHandshake().Caf();

            bool hasFixedSize = BuiltIns.TryGetFixedSize(typeof(T), out int fixedSize);

            if (hasFixedSize)
            {
                await ProcessLoopFixed(fixedSize);
            }
            else
            {
                await ProcessLoopVariable();
            }
        }

        async Task ProcessLoopFixed(int fixedSize)
        {
            int fixedSizeWithHeader = 4 + fixedSize;
            if (readBuffer.Length < fixedSizeWithHeader)
            {
                readBuffer = new byte[fixedSizeWithHeader];
            }

            while (KeepRunning)
            {
                bool success = await stream!.ReadChunkAsync(readBuffer, fixedSizeWithHeader, runningTs.Token);
                if (!success)
                {
                    Logger.LogDebugFormat("{0}: Partner closed connection", this);
                    return;
                }

                int receivedSize = BitConverter.ToInt32(readBuffer, 0);
                if (receivedSize != fixedSize)
                {
                    throw new RosInvalidPackageSizeException(
                        $"Receiver expected packet with fixed size of {fixedSize} bytes, but got a packet of size {receivedSize}!");
                }

                T message = Buffer.Deserialize(topicInfo.Generator, readBuffer, fixedSize, 4);
                manager.MessageCallback(message);

                numReceived++;
                bytesReceived += fixedSizeWithHeader;
            }
        }

        async Task ProcessLoopVariable()
        {
            while (KeepRunning)
            {
                int rcvLength = await ReceivePacket();
                if (rcvLength == -1)
                {
                    Logger.LogDebugFormat("{0}: Partner closed connection", this);
                    return;
                }

                T message = Buffer.Deserialize(topicInfo.Generator, readBuffer, rcvLength);
                manager.MessageCallback(message);

                numReceived++;
                bytesReceived += rcvLength + 4;
            }
        }

        public override string ToString()
        {
            return $"[TcpReceiver for '{Topic}' PartnerUri={RemoteUri} " +
                   $"PartnerSocket={remoteEndpoint.Hostname}:{remoteEndpoint.Port}]";
        }
    }
}