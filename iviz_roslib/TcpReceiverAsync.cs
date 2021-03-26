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
using Iviz.Roslib.Utils;
using Iviz.XmlRpc;

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
        readonly bool requestNoDelay;
        readonly CancellationTokenSource runningTs = new();
        readonly Task task;
        readonly int connectionTimeoutInMs;

        TopicInfo<T> topicInfo;
        TcpClient? tcpClient;
        bool disposed;

        long numReceived;
        long bytesReceived;

        string? errorDescription;

        public Endpoint? RemoteEndpoint { get; private set; }
        public Endpoint? Endpoint { get; private set; }
        public IReadOnlyList<string>? TcpHeader { get; private set; }
        bool KeepRunning => !runningTs.IsCancellationRequested;
        public bool IsConnected => tcpClient != null && tcpClient.Connected;
        public Uri RemoteUri { get; }
        public string Topic => topicInfo.Topic;
        public bool IsAlive => !task.IsCompleted;
        public bool IsPaused { get; set; }

        public TcpReceiverAsync(TcpReceiverManager<T> manager,
            Uri remoteUri, Endpoint? remoteEndpoint, TopicInfo<T> topicInfo,
            bool requestNoDelay, int timeoutInMs)
        {
            RemoteUri = remoteUri;
            RemoteEndpoint = remoteEndpoint;
            this.topicInfo = topicInfo;
            this.manager = manager;
            this.requestNoDelay = requestNoDelay;
            connectionTimeoutInMs = timeoutInMs;
            task = TaskUtils.StartLongTask(StartSession, runningTs.Token);
        }

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

        public async Task DisposeAsync(CancellationToken token)
        {
            if (disposed)
            {
                return;
            }

            //Logger.LogDebugFormat("{0}: Disposing!", this);

            disposed = true;
            runningTs.Cancel();
            tcpClient?.Dispose();

            await task.AwaitNoThrow(DisposeTimeoutInMs, this, token);
            runningTs.Dispose();
        }

        async ValueTask<TcpClient?> TryToConnect(Endpoint newEndpoint)
        {
            TcpClient client = new(AddressFamily.InterNetworkV6) {Client = {DualMode = true}};
            (string hostname, int port) = newEndpoint;

            try
            {
                await client.TryConnectAsync(hostname, port, runningTs.Token, connectionTimeoutInMs);
                return client;
            }
            catch (Exception e)
            {
                if (!(e is IOException || e is SocketException || e is OperationCanceledException))
                {
                    Logger.LogFormat(BaseUtils.GenericExceptionFormat, this, e);
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
                    TcpClient? client = await TryToConnect(RemoteEndpoint.Value);
                    if (client != null)
                    {
                        return client;
                    }
                }

                if (!KeepRunning)
                {
                    return null;
                }

                try
                {
                    await Task.Delay(WaitTimeInMsFromTry(i), runningTs.Token);
                }
                catch (OperationCanceledException)
                {
                    return null;
                }

                Endpoint? newEndpoint =
                    await manager.RequestConnectionFromPublisherAsync(RemoteUri, runningTs.Token);
                if (newEndpoint == null || newEndpoint.Value.Equals(RemoteEndpoint))
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
            return BaseUtils.Random.Next(0, 1000) +
                   index switch
                   {
                       < 10 => 2000,
                       < 50 => 5000,
                       _ => 10000
                   };
        }

        Task SendHeader(NetworkStream stream)
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

            return stream.WriteHeaderAsync(contents, runningTs.Token);
        }

        async ValueTask<int> ReceivePacket(NetworkStream stream, ResizableRent<byte> readBuffer)
        {
            if (!await stream.ReadChunkAsync(readBuffer.Array, 4, runningTs.Token))
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
            if (!await stream.ReadChunkAsync(readBuffer.Array, length, runningTs.Token))
            {
                return -1;
            }

            return length;
        }

        async ValueTask<int> ReceiveAndIgnore(NetworkStream stream, byte[] bufferForSize)
        {
            if (!await stream.ReadChunkAsync(bufferForSize, 4, runningTs.Token))
            {
                return -1;
            }

            int length = BitConverter.ToInt32(bufferForSize, 0);
            if (length == 0)
            {
                return 0;
            }

            if (!await stream.ReadAndIgnoreAsync(length, runningTs.Token))
            {
                return -1;
            }

            return length;
        }

        async Task ProcessHandshake(NetworkStream stream)
        {
            await SendHeader(stream);

            using ResizableRent<byte> readBuffer = new(4);

            int receivedLength = await ReceivePacket(stream, readBuffer);
            if (receivedLength == -1)
            {
                throw new IOException("Connection closed during handshake");
            }


            List<string> responses = BaseUtils.ParseHeader(readBuffer.Array, receivedLength);
            if (responses.Count != 0 && responses[0].HasPrefix("error"))
            {
                int index = responses[0].IndexOf('=');
                string errorMsg = index != -1 ? responses[0].Substring(index + 1) : responses[0];
                throw new RosHandshakeException($"Failed handshake: {errorMsg}");
            }

            TcpHeader = responses.AsReadOnly();

            if (DynamicMessage.IsDynamic<T>() || DynamicMessage.IsGenericMessage<T>())
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

            Type? lookupMsgName;
            object? lookupGenerator;
            if (DynamicMessage.IsGenericMessage<T>()
                && (lookupMsgName = BuiltIns.TryGetTypeFromMessageName(dynamicMsgName)) != null
                && (lookupGenerator = Activator.CreateInstance(lookupMsgName)) != null)
            {
                topicInfo = new TopicInfo<T>(callerId, topicName, (IDeserializable<T>) lookupGenerator);
                return;
            }

            DynamicMessage generator =
                DynamicMessage.CreateFromDependencyString(dynamicMsgName, dynamicDependencies);
            topicInfo = new TopicInfo<T>(callerId, topicName, generator);
        }

        async Task StartSession()
        {
            while (KeepRunning)
            {
                tcpClient = null;
                Logger.LogDebugFormat("{0}: Trying to connect!", this);

                TcpClient? newTcpClient = await KeepReconnecting();
                if (newTcpClient == null)
                {
                    Logger.LogDebugFormat(KeepRunning
                            ? "{0}: Ran out of retries. Leaving!"
                            : "{0}: Disposed! Getting out.",
                        this);
                    break;
                }

                IPEndPoint? newEndPoint = (IPEndPoint?) newTcpClient.Client.RemoteEndPoint;
                if (newEndPoint == null)
                {
                    Logger.LogDebugFormat("{0}: Connection interrupted! Getting out.", this);
                    break;
                }

                Endpoint = new Endpoint(newEndPoint);
                Logger.LogDebugFormat("{0}: Connected!", this);
                errorDescription = null;

                try
                {
                    using (tcpClient = newTcpClient)
                    {
                        await ProcessLoop(tcpClient.GetStream());
                    }
                }
                catch (Exception e)
                {
                    switch (e)
                    {
                        case ObjectDisposedException:
                        case OperationCanceledException:
                            break;
                        case IOException:
                        case SocketException:
                        case TimeoutException:
                            Logger.LogDebugFormat(BaseUtils.GenericExceptionFormat, this, e);
                            break;
                        case RoslibException:
                            errorDescription = e.Message;
                            Logger.LogErrorFormat(BaseUtils.GenericExceptionFormat, this, e);
                            break;
                        default:
                            Logger.LogFormat(BaseUtils.GenericExceptionFormat, this, e);
                            break;
                    }
                }
            }

            tcpClient = null;
            try
            {
                runningTs.Cancel();
            }
            catch (ObjectDisposedException)
            {
            }

            Logger.LogDebugFormat("{0}: Stopped!", this);
        }

        async Task ProcessLoop(NetworkStream stream)
        {
            await ProcessHandshake(stream);

            bool hasFixedSize = BuiltIns.TryGetFixedSize(typeof(T), out int fixedSize);

            await (hasFixedSize
                ? ProcessLoopFixed(stream, fixedSize)
                : ProcessLoopVariable(stream));
        }

        async Task ProcessLoopFixed(NetworkStream stream, int fixedSize)
        {
            int fixedSizeWithHeader = 4 + fixedSize;
            using var readBuffer = new Rent<byte>(fixedSizeWithHeader);

            while (KeepRunning)
            {
                bool success = await stream.ReadChunkAsync(readBuffer.Array, fixedSizeWithHeader, runningTs.Token);
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

                if (!IsPaused)
                {
                    T message = topicInfo.Generator.DeserializeFromArray(readBuffer.Array, fixedSizeWithHeader, 4);
                    manager.MessageCallback(message, this);
                }

                await Task.Yield();
            }
        }

        async Task ProcessLoopVariable(NetworkStream stream)
        {
            using ResizableRent<byte> readBuffer = new(4);
            while (KeepRunning)
            {
                if (IsPaused)
                {
                    int rcvLength = await ReceiveAndIgnore(stream, readBuffer.Array);
                    if (rcvLength == -1)
                    {
                        Logger.LogDebugFormat("{0}: Partner closed connection", this);
                        return;
                    }

                    numReceived++;
                    bytesReceived += rcvLength + 4;
                }
                else
                {
                    int rcvLength = await ReceivePacket(stream, readBuffer);
                    if (rcvLength == -1)
                    {
                        Logger.LogDebugFormat("{0}: Partner closed connection", this);
                        return;
                    }

                    numReceived++;
                    bytesReceived += rcvLength + 4;

                    T message = topicInfo.Generator.DeserializeFromArray(readBuffer.Array, rcvLength);
                    manager.MessageCallback(message, this);
                }

                await Task.Yield();
            }
        }

        public override string ToString()
        {
            return $"[TcpReceiver for '{Topic}' PartnerUri={RemoteUri} " +
                   $"PartnerSocket={RemoteEndpoint?.Hostname ?? "(none)"}:{RemoteEndpoint?.Port ?? -1}]";
        }
    }
}