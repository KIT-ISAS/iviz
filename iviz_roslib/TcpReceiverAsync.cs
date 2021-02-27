using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;
using Iviz.XmlRpc;
using Buffer = Iviz.Msgs.Buffer;

namespace Iviz.Roslib
{
    public interface IRosTcpReceiver
    {
        string Topic { get; }
        Uri RemoteUri { get; }
        Endpoint? RemoteEndpoint { get; }
        Endpoint? Endpoint { get; }
        IReadOnlyList<string>? TcpHeader { get; }
        SubscriberReceiverState State { get; }
    }

    internal sealed class TcpReceiverAsync<T> : IRosTcpReceiver where T : IMessage
    {
        const int MaxConnectionRetries = 120;
        const int DisposeTimeoutInMs = 2000;

        readonly TcpReceiverManager<T> manager;

        TopicInfo<T> topicInfo;

        readonly bool requestNoDelay;

        public Endpoint? RemoteEndpoint { get; private set; }
        public Endpoint? Endpoint { get; private set; }
        public IReadOnlyList<string>? TcpHeader { get; private set; }

        long numReceived;
        long bytesReceived;

        string? errorDescription;
        int connectionTimeoutInMs;

        readonly CancellationTokenSource runningTs = new();

        NetworkStream? stream;
        Task? task;
        TcpClient? tcpClient;
        bool disposed;

        bool KeepRunning => !runningTs.IsCancellationRequested;
        public bool IsConnected => tcpClient != null && tcpClient.Connected;

        public TcpReceiverAsync(TcpReceiverManager<T> manager,
            Uri remoteUri, Endpoint? remoteEndpoint, TopicInfo<T> topicInfo,
            bool requestNoDelay)
        {
            RemoteUri = remoteUri;
            RemoteEndpoint = remoteEndpoint;
            this.topicInfo = topicInfo;
            this.manager = manager;
            this.requestNoDelay = requestNoDelay;
        }

        public Uri RemoteUri { get; }
        public string Topic => topicInfo.Topic;
        public bool IsAlive => task != null && !task.IsCompleted;

        public SubscriberReceiverState State => new(RemoteUri)
        {
            IsAlive = IsAlive,
            IsConnected = IsConnected,
            RequestNoDelay = requestNoDelay,
            EndPoint = Endpoint,
            RemoteEndpoint = RemoteEndpoint,
            NumReceived = numReceived,
            BytesReceived = bytesReceived,
            ErrorDescription = errorDescription
        };

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

            task = null;
            runningTs.Dispose();
        }


        public void Start(int timeoutInMs)
        {
            connectionTimeoutInMs = timeoutInMs;
            task = Task.Run(StartSession, runningTs.Token);
        }

        async ValueTask<TcpClient?> TryToConnect(Endpoint testEndpoint)
        {
            TcpClient client = new(AddressFamily.InterNetworkV6) {Client = {DualMode = true}};
            var (hostname, port) = testEndpoint;

            try
            {
                await client.TryConnectAsync(hostname, port, runningTs.Token, connectionTimeoutInMs);
                return client;
            }
            catch (Exception e)
            {
                if (!(e is IOException || e is SocketException || e is OperationCanceledException))
                {
                    Logger.LogFormat(Utils.GenericExceptionFormat, this, e);
                }

                client.Dispose();
                return null;
            }
        }

        async ValueTask<TcpClient?> KeepReconnecting()
        {
            for (int i = 0; i < MaxConnectionRetries && KeepRunning; i++)
            {
                if (RemoteEndpoint != null)
                {
                    TcpClient? client = await TryToConnect(RemoteEndpoint.Value).Caf();
                    if (client != null)
                    {
                        return client;
                    }
                }

                try
                {
                    await Task.Delay(WaitTimeInMsFromTry(i), runningTs.Token).Caf();
                }
                catch (OperationCanceledException)
                {
                    return null;
                }

                Endpoint? newEndpoint =
                    await manager.RequestConnectionFromPublisherAsync(RemoteUri, runningTs.Token).Caf();
                if (newEndpoint == null || newEndpoint.Equals(RemoteEndpoint))
                {
                    continue;
                }

                Logger.LogFormat("{0}: Changed endpoint from {1} to {2}", this, RemoteEndpoint, newEndpoint);
                RemoteEndpoint = newEndpoint;
            }

            return null;
        }

        static int WaitTimeInMsFromTry(int index)
        {
            return index switch
            {
                < 10 => 2000,
                < 50 => 5000,
                _ => 10000
            };
        }

        Task SendHeader()
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

            return Utils.WriteHeaderAsync(stream!, contents, runningTs.Token);
        }

        async ValueTask<int> ReceivePacket(ResizableRent<byte> readBuffer)
        {
            if (!await stream!.ReadChunkAsync(readBuffer.Array, 4, runningTs.Token))
            {
                return -1;
            }

            int length = BitConverter.ToInt32(readBuffer.Array, 0);
            if (length == 0)
            {
                return 0;
            }

            const int maxMessageLength = 64 * 1024 * 1024;
            if (length < 0 || length > maxMessageLength)
            {
                throw new RosInvalidPackageSizeException($"Invalid packet size '{length}', disconnecting.");
            }

            readBuffer.EnsureCapability(length);
            if (!await stream!.ReadChunkAsync(readBuffer.Array, length, runningTs.Token).Caf())
            {
                return -1;
            }

            return length;
        }

        async Task ProcessHandshake()
        {
            await SendHeader().Caf();

            using ResizableRent<byte> readBuffer = new(4);

            int receivedLength = await ReceivePacket(readBuffer).Caf();
            if (receivedLength == -1)
            {
                throw new IOException("Connection closed during handshake");
            }


            List<string> responses = Utils.ParseHeader(readBuffer.Array, receivedLength);
            if (responses.Count != 0 && responses[0].HasPrefix("error"))
            {
                int index = responses[0].IndexOf('=');
                string errorMsg = index != -1 ? responses[0].Substring(index + 1) : responses[0];
                throw new RosHandshakeException($"Failed handshake: {errorMsg}");
            }

            TcpHeader = responses.AsReadOnly();

            if (DynamicMessage.IsDynamic<T>())
            {
                GenerateDynamicTopicInfo(responses);
            }
        }

        void GenerateDynamicTopicInfo(List<string> responses)
        {
            const string typePrefix = "type=";
            const string definitionPrefix = "message_definition=";

            string callerId = topicInfo.CallerId;
            string topicName = topicInfo.Topic;
            string? dynamicMsgName = responses.FirstOrDefault(
                entry => entry.HasPrefix(typePrefix))?.Substring(typePrefix.Length);
            string? dynamicDependencies = responses.FirstOrDefault(
                entry => entry.HasPrefix(definitionPrefix))?.Substring(definitionPrefix.Length);
            if (dynamicMsgName == null || dynamicDependencies == null)
            {
                throw new RosHandshakeException(
                    "Partner did not send type and definition, required to instantiate dynamic messages.");
            }

            DynamicMessage generator = DynamicMessage.CreateFromDependencyString(dynamicMsgName, dynamicDependencies);
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

                Endpoint = new Endpoint(newEndPoint);
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
                catch (Exception e)
                {
                    switch (e)
                    {
                        case ObjectDisposedException _:
                        case OperationCanceledException _:
                            break;
                        case IOException _:
                        case SocketException _:
                        case TimeoutException _:
                            Logger.LogDebugFormat(Utils.GenericExceptionFormat, this, e);
                            break;
                        case RoslibException _:
                            errorDescription = e.Message;
                            Logger.LogErrorFormat(Utils.GenericExceptionFormat, this, e);
                            break;
                        default:
                            Logger.LogFormat(Utils.GenericExceptionFormat, this, e);
                            break;
                    }
                }
            }

            tcpClient = null;
            stream = null;
            try
            {
                runningTs.Cancel();
            }
            catch (ObjectDisposedException) { }

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
            using var readBuffer = new Rent<byte>(fixedSizeWithHeader);

            while (KeepRunning)
            {
                bool success = await stream!.ReadChunkAsync(readBuffer.Array, fixedSizeWithHeader, runningTs.Token);
                if (!success)
                {
                    Logger.LogDebugFormat("{0}: Partner closed connection", this);
                    return;
                }

                int receivedSize = BitConverter.ToInt32(readBuffer.Array, 0);
                if (receivedSize != fixedSize)
                {
                    throw new RosInvalidPackageSizeException(
                        $"Receiver expected packet with fixed size of {fixedSize} bytes, but got a packet of size {receivedSize}!");
                }

                numReceived++;
                bytesReceived += fixedSizeWithHeader;

                T message = Buffer.Deserialize(topicInfo.Generator, readBuffer.Array, fixedSizeWithHeader, 4);
                manager.MessageCallback(message, this);
            }
        }

        async Task ProcessLoopVariable()
        {
            using ResizableRent<byte> readBuffer = new(4);
            while (KeepRunning)
            {
                int rcvLength = await ReceivePacket(readBuffer);
                if (rcvLength == -1)
                {
                    Logger.LogDebugFormat("{0}: Partner closed connection", this);
                    return;
                }

                numReceived++;
                bytesReceived += rcvLength + 4;

                T message = Buffer.Deserialize(topicInfo.Generator, readBuffer.Array, rcvLength);
                manager.MessageCallback(message, this);
            }
        }

        public override string ToString()
        {
            return $"[TcpReceiver for '{Topic}' PartnerUri={RemoteUri} " +
                   $"PartnerSocket={RemoteEndpoint?.Hostname ?? "(none)"}:{RemoteEndpoint?.Port ?? -1}]";
        }
    }
}